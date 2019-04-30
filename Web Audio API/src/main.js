import './favicon.ico'
import './style.css'

console.log("Hello2");

 var audioCtx = new AudioContext();
// create Oscillator node
var oscillator = audioCtx.createOscillator();

oscillator.type = 'sine';
oscillator.frequency.setValueAtTime(1000, audioCtx.currentTime); // value in hertz
oscillator.connect(audioCtx.destination);
oscillator.start();

var oscillator = audioCtx.createOscillator();
oscillator.type = 'sine';
oscillator.frequency.setValueAtTime(500, audioCtx.currentTime); // value in hertz
oscillator.connect(audioCtx.destination);
oscillator.start();    
