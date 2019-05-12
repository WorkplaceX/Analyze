import './favicon.ico'
import './style.css'

var gl;
var program;
var vertices;

window.onload = function() {
    console.log('"Hello2');
    var canvas = document.getElementById("canvas");
    gl = canvas.getContext('webgl')
    console.log(gl);

    var vertexShaderSource = `
    attribute vec4 position;
    void main() {
      gl_Position = position;
    }
    `;

    var fragmentShaderSource = `
    precision highp float;
    uniform vec4 color;
    void main() {
        gl_FragColor = color;
    }
    `

    var vertexShader = createShader(gl, vertexShaderSource, gl.VERTEX_SHADER);
    var fragmentShader = createShader(gl, fragmentShaderSource, gl.FRAGMENT_SHADER)
    
    program = gl.createProgram();

    gl.attachShader(program, vertexShader);
    gl.attachShader(program, fragmentShader);
    
    gl.linkProgram(program);
    
    if (!gl.getProgramParameter(program, gl.LINK_STATUS) ) {
      var info = gl.getProgramInfoLog(program);
      throw 'WebGL linkProgram failed!\n\n' + info;
    }

    vertices = new Float32Array([
        -0.5,-0.5,
        0.5,-0.5,
        0.0,0.5
      ])
      
      var buffer = gl.createBuffer();
      gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
      gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);
      
      gl.useProgram(program);
      
      program.position = gl.getAttribLocation(program, 'position');
      gl.enableVertexAttribArray(program.position);
      gl.vertexAttribPointer(program.position, 2, gl.FLOAT, false, 0, 0);
      
      requestAnimationFrame(mainLoop);
}

function mainLoop(time) {
    console.log(time);

    gl.clearColor(1, 0, 0, 1);
    gl.clear(gl.COLOR_BUFFER_BIT);

    program.color = gl.getUniformLocation(program, 'color');
    gl.uniform4fv(program.color, [0, 1, (time % 1000) / 1000, 1]);
    gl.drawArrays(gl.TRIANGLES, 0, vertices.length / 2);
  
    requestAnimationFrame(mainLoop);
}

function createShader (gl, sourceCode, type) {
    // type gl.VERTEX_SHADER or gl.FRAGMENT_SHADER
    var shader = gl.createShader(type);
    gl.shaderSource(shader, sourceCode);
    gl.compileShader(shader);
  
    if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS) ) {
      var info = gl.getShaderInfoLog(shader);
      throw 'WebGL compileShader failed!\n\n' + info;
    }
    return shader;
  }