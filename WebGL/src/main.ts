import { inherits } from "util";

require('./favicon.ico');
require('./style.css');
require('./leaves.jpg');

class WebGL {
  constructor(canvasId: string) {
    this.canvasId = canvasId;
    this.canvas = document.getElementById(canvasId);
    this.gl = this.canvas.getContext('webgl', { // this.canvas.getContext('webgl2');
      premultipliedAlpha: false, // https://webglfundamentals.org/webgl/lessons/webgl-and-alpha.html
      alpha: true, 
    });
    this.program = this.gl.createProgram() as any;

    // console.log(this.gl.getSupportedExtensions());

    this.initShader();
    this.initVertex();
    this.initTexture();
    requestAnimationFrame(this.mainLoop);
  }

  canvasId: string;
  canvas: any;
  gl: WebGLRenderingContext;
  program: WebGLProgram;

  vertexShaderSource: string = `
    attribute vec2 vertPosition; // IN
    attribute vec3 vertColor; // IN
    varying vec3 fragColor; // OUT
    void main() {
      fragColor = vertColor;
      gl_Position = vec4(vertPosition, 0, 1);
    }`;

  fragmentShaderSource: string = `
    precision mediump float;
    varying vec3 fragColor; // IN
    uniform vec4 uniformColor; // IN Global
    uniform sampler2D uniformTexture; // IN Global
    void main() {
        vec2 texcoord = vec2(gl_FragCoord.x / 400.0, gl_FragCoord.y / 400.0);  // get a value from the middle of the texture
        gl_FragColor = texture2D(uniformTexture, texcoord) * uniformColor; // ; // vec4(fragColor, 1)
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
  vertexElementCount: Number = 5; // Number of entries in one vertex.

  initVertex(): void {
    this.vertex = new Float32Array([
      // (X, Y, R, G, B)
      -0.5, -0.5, 1, 0, 0,
      0.5, 0.5, 0, 1, 1,
      0.5, -0.5, 0, 1, 0,

      -0.9, 0.2, 1, 1, 1,
      -0.2, 0.9, 0, 1, 0,
      -0.9, 0.9, 0, 0, 1,
    ]);

    var buffer = this.gl.createBuffer();
    this.gl.bindBuffer(this.gl.ARRAY_BUFFER, buffer);
    this.gl.bufferData(this.gl.ARRAY_BUFFER, this.vertex, this.gl.STATIC_DRAW);

    this.gl.useProgram(this.program);

    // Shader attribute "vertPosition"
    var vertPosition = this.gl.getAttribLocation(this.program, 'vertPosition');
    this.gl.enableVertexAttribArray(vertPosition);
    this.gl.vertexAttribPointer(vertPosition,
      2, // Number of elements per attribute
      this.gl.FLOAT, false,
      Float32Array.BYTES_PER_ELEMENT * (this.vertexElementCount as any), // Size of an individual vertex (X, Y)
      Float32Array.BYTES_PER_ELEMENT * 0); // Offset from the beginning of a single vertex to this attribute

    // Shader attribute "vertColor"
    var vertColor = this.gl.getAttribLocation(this.program, 'vertColor');
    this.gl.enableVertexAttribArray(vertColor);
    this.gl.vertexAttribPointer(vertColor,
      3, // Number of elements per attribute
      this.gl.FLOAT, false,
      Float32Array.BYTES_PER_ELEMENT * (this.vertexElementCount as any), // Size of an individual vertex (X, Y)
      Float32Array.BYTES_PER_ELEMENT * 2); // Offset from the beginning of a single vertex to this attribute
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

    var pixel = new Uint8Array([0, 0, 255, 255]); // RGBA. See also: premultipliedAlpha: false
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
    this.gl.clear(this.gl.COLOR_BUFFER_BIT); // | this.gl.COLOR_BUFFER_BIT

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

