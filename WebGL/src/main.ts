import { inherits } from "util";

require('./favicon.ico');
require('./style.css');
require('./leaves.jpg');

class WebGL {
  constructor(canvasId: string) {
    this.canvasId = canvasId;
    this.canvas = document.getElementById(canvasId);
    this.canvas.width = this.canvasWidth;
    this.canvas.height = this.canvasHeight;
    this.gl = this.canvas.getContext('webgl', { // this.canvas.getContext('webgl2');
      premultipliedAlpha: false, // https://webglfundamentals.org/webgl/lessons/webgl-and-alpha.html
      alpha: true, 
    });
    this.gl.enable(this.gl.DEPTH_TEST);
    this.program = this.gl.createProgram() as any;

    // console.log(this.gl.getSupportedExtensions());

    this.initShader();
    this.initVertex();
    this.initTexture();
    requestAnimationFrame(this.mainLoop);
  }

  canvasId: string;
  canvas: any;
  canvasWidth: number = 640;
  canvasHeight: number = 480;
  gl: WebGLRenderingContext;
  program: WebGLProgram;

  vertexShaderSource: string = `
    attribute vec3 vertPosition; // IN
    attribute vec3 vertColor; // IN
    varying vec3 fragColor; // OUT
    void main() {
      fragColor = vertColor;
      gl_Position = vec4(vertPosition, 1.0); // (X, Y, Z, W) W is used for clipping.
    }`;

  fragmentShaderSource: string = `
  precision mediump float;
  #define resolution vec2(640.0, 480.0)
  #define thickness 0.003

  float drawLine(vec2 p1, vec2 p2) { // https://stackoverflow.com/questions/15276454/is-it-possible-to-draw-line-thickness-in-a-fragment-shader
    vec2 uv = gl_FragCoord.xy / resolution.xy;

    float a = abs(distance(p1, uv));
    float b = abs(distance(p2, uv));
    float c = abs(distance(p1, p2));

    if ( a >= c || b >=  c ) return 0.0;

    float p = (a + b + c) * 0.5;

    // median to (p1, p2) vector
    float h = 2.0 / c * sqrt( p * ( p - a) * ( p - b) * ( p - c));

    return mix(0.5, 0.0, smoothstep(1.0 * thickness, 1.0 * thickness, h));
  }

  precision mediump  float;
  varying vec3 fragColor; // IN
  uniform vec4 uniformColor; // IN Global
  uniform sampler2D uniformTexture; // IN Global
  void main() {
      vec2 texcoord = vec2(gl_FragCoord.x / resolution.x, gl_FragCoord.y / resolution.y);  // get a value from the middle of the texture
      if (texture2D(uniformTexture, (vec2(0, 0))) == vec4(1, 1, 1, 0)) // Texture not yet loaded. See also: Uint8Array([255, 255, 255, 0])
      {
        gl_FragColor = vec4(fragColor, 1); // Color defined in vertex.
      }
      else
      {
        gl_FragColor = gl_FragColor + texture2D(uniformTexture, texcoord); // Color from texture.
      }
      gl_FragColor = gl_FragColor + vec4(drawLine(vec2(0.6, 0.3), vec2(0.6, 0.5))); // Draw line.
      gl_FragColor = gl_FragColor * uniformColor; // Color defined global.
      // gl_FragColor = vec4(drawLine(vec2(0.6, 0.3), vec2(0.6, 0.5))) + texture2D(uniformTexture, texcoord) * uniformColor;
    }`;

  initShader(): void {
    var vertexShader = this.createShader(this.gl, this.vertexShaderSource, this.gl.VERTEX_SHADER);
    var fragmentShader = this.createShader(this.gl, this.fragmentShaderSource, this.gl.FRAGMENT_SHADER)

    this.gl.attachShader(this.program, vertexShader);
    this.gl.attachShader(this.program, fragmentShader);

    this.gl.linkProgram(this.program);

    if (!this.gl.getProgramParameter(this.program, this.gl.LINK_STATUS)) {
      var info = this.gl.getProgramInfoLog(this.program);
      throw 'WebGL linkProgram failed!\n\n' + info;
    }
  }

  vertex: Float32Array = new Float32Array();
  vertexElementCount: Number = 6; // Number of entries in one vertex.

  initVertex(): void {
    this.vertex = new Float32Array([ // See also vertexElementCount.
      // (X, Y, Z, R, G, B)
      -0.5, -0.5, +0.0, 1, 0, 0,
      +0.5, +0.5, +0.0, 0, 1, 1,
      +0.5, -0.5, +0.0, 0, 1, 0,

      -0.9, +0.2, +0.0, 1, 1, 1,
      -0.2, +0.9, +0.0, 0, 1, 0,
      -0.9, +0.9, +0.0, 0, 0, 1,

      -0.9, +0.2, +1.0, 1, 0, 0,
      +0.5, +0.2, -0.2, 0, 1, 0,
      -0.2, +0.9, +1.0, 0, 0, 1,
    ])

    var buffer = this.gl.createBuffer();
    this.gl.bindBuffer(this.gl.ARRAY_BUFFER, buffer);
    this.gl.bufferData(this.gl.ARRAY_BUFFER, this.vertex, this.gl.STATIC_DRAW);

    this.gl.useProgram(this.program);

    // Shader attribute "vertPosition"
    var vertPosition = this.gl.getAttribLocation(this.program, 'vertPosition');
    this.gl.enableVertexAttribArray(vertPosition);
    this.gl.vertexAttribPointer(vertPosition,
      3, // vec3(X, Y, Z)
      this.gl.FLOAT, false,
      Float32Array.BYTES_PER_ELEMENT * (this.vertexElementCount as any), // Size of an individual vertex (X, Y, Z)
      Float32Array.BYTES_PER_ELEMENT * 0); // Offset

    // Shader attribute "vertColor"
    var vertColor = this.gl.getAttribLocation(this.program, 'vertColor');
    this.gl.enableVertexAttribArray(vertColor);
    this.gl.vertexAttribPointer(vertColor,
      3, // vec3(R, G, B)
      this.gl.FLOAT, false,
      Float32Array.BYTES_PER_ELEMENT * (this.vertexElementCount as any), // Size of an individual vertex (X, Y, Z)
      Float32Array.BYTES_PER_ELEMENT * 3); // Offset (R, G, B)
  }

  createShader(gl: WebGLRenderingContext, sourceCode: string, type: GLenum): any {
    // type gl.VERTEX_SHADER or gl.FRAGMENT_SHADER
    var shader: any = this.gl.createShader(type);

    gl.shaderSource(shader, sourceCode);
    gl.compileShader(shader);

    if (!gl.getShaderParameter(shader, this.gl.COMPILE_STATUS)) {
      var info = gl.getShaderInfoLog(shader);
      throw 'WebGL compileShader failed!\n\n' + info;
    }
    return shader;
  }

  initTexture() {
    var texture: WebGLTexture = this.gl.createTexture() as any;
    this.gl.activeTexture(this.gl.TEXTURE7); // Tell WebGL we want to affect texture unit 0
    this.gl.bindTexture(this.gl.TEXTURE_2D, texture);
  
    // Set the parameters so we can render any size image.
    this.gl.texParameteri(this.gl.TEXTURE_2D, this.gl.TEXTURE_WRAP_S, this.gl.CLAMP_TO_EDGE);
    this.gl.texParameteri(this.gl.TEXTURE_2D, this.gl.TEXTURE_WRAP_T, this.gl.CLAMP_TO_EDGE);
    this.gl.texParameteri(this.gl.TEXTURE_2D, this.gl.TEXTURE_MIN_FILTER, this.gl.LINEAR);

    var pixel = new Uint8Array([255, 255, 255, 0]); // RGBA. See also: texture2D(uniformTexture, (vec2(0, 0))) == vec4(1, 1, 1, 0). See also: premultipliedAlpha: false
    this.gl.texImage2D(this.gl.TEXTURE_2D, 0, this.gl.RGBA, 1, 1, 0, this.gl.RGBA, this.gl.UNSIGNED_BYTE, pixel);

    var image = new Image();
    image.src = 'leaves.jpg';
    image.onload = () => {
      setTimeout(() => {
        this.gl.texImage2D(this.gl.TEXTURE_2D, 0, this.gl.RGBA, this.gl.RGBA, this.gl.UNSIGNED_BYTE, image);
      }, 1000);
    };
  }

  mainLoop = (time: Number) => {
    this.gl.clearColor(1, 0, 0, 1); // RGBA Range (0..1)
    this.gl.clear(this.gl.COLOR_BUFFER_BIT | this.gl.COLOR_BUFFER_BIT);

    // GLSL parameter "uniformColor"
    var uniformColor = this.gl.getUniformLocation(this.program, 'uniformColor');
    this.gl.uniform4fv(uniformColor, [1, 1, 1, (time as any % 1000) / 1000]);

    // GLSL parameter "uniformTexture"
    var uniformTexture = this.gl.getUniformLocation(this.program, 'uniformTexture');
    this.gl.uniform1i(uniformTexture, 7); // this.gl.TEXTURE7

    this.gl.drawArrays(this.gl.TRIANGLES, 0,
      this.vertex.length / (this.vertexElementCount as any)); // Number of vertex.

    requestAnimationFrame(this.mainLoop);
  }
}

new WebGL('canvas');

