import { inherits } from "util";

require('./favicon.ico');
require('./style.css');

class WebGL {
  constructor(canvasId: string) {
    this.canvasId = canvasId;
    this.canvas = document.getElementById(canvasId);
    this.gl = this.canvas.getContext('webgl'); // this.gl = this.canvas.getContext('webgl2');
    this.program = this.gl.createProgram() as any;

    // console.log(this.gl.getSupportedExtensions());

    this.initShader();
    this.initVertex();

    requestAnimationFrame(this.mainLoop);
  }

  canvasId: string;
  canvas: any;
  gl: WebGLRenderingContext;
  program: WebGLProgram;

  vertexShaderSource: string = `
    attribute vec2 position; // IN
    attribute vec3 vertColor; // IN
    varying vec3 fragColor; // OUT
    void main() {
      fragColor = vertColor;
      gl_Position = vec4(position, 0, 1);
    }`;

  fragmentShaderSource: string = `
    precision mediump float;
    varying vec3 fragColor; // IN
    uniform vec4 color; // IN
    void main() {
        gl_FragColor = vec4(fragColor, 1) + color;
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
      0.5, 0.5, 1, 0, 0,
      0.5, -0.5, 1, 0, 0,

      -0.9, 0.2, 0, 1, 0,
      -0.2, 0.9, 0, 1, 0,
      -0.9, 0.9, 0, 1, 1,
    ]);

    var buffer = this.gl.createBuffer();
    this.gl.bindBuffer(this.gl.ARRAY_BUFFER, buffer);
    this.gl.bufferData(this.gl.ARRAY_BUFFER, this.vertex, this.gl.STATIC_DRAW);

    this.gl.useProgram(this.program);

    // Shader attribute "position"
    var position = this.gl.getAttribLocation(this.program, 'position');
    this.gl.enableVertexAttribArray(position);
    this.gl.vertexAttribPointer(position, 
      2, // Number of elements per attribute
      this.gl.FLOAT, false, 
      Float32Array.BYTES_PER_ELEMENT * (this.vertexElementCount as any), // Size of an individual vertex (X, Y)
      Float32Array.BYTES_PER_ELEMENT * 0); // Offset from the beginning of a single vertex to this attribute

    // Shader attribute "vertColor"
    var position2 = this.gl.getAttribLocation(this.program, 'vertColor');
    this.gl.enableVertexAttribArray(position2);
    this.gl.vertexAttribPointer(position2, 
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

  mainLoop = (time: Number) => {
    this.gl.clearColor(1, 0, 0, 1);
    this.gl.clear(this.gl.COLOR_BUFFER_BIT);

    var color = this.gl.getUniformLocation(this.program, 'color');
    this.gl.uniform4fv(color, [0, 1, (time as any % 1000) / 1000, 0.5]);
    this.gl.drawArrays(this.gl.TRIANGLES, 0, 
      this.vertex.length / (this.vertexElementCount as any)); // Number of vertex.

    requestAnimationFrame(this.mainLoop);
  }
}

new WebGL('canvas');

