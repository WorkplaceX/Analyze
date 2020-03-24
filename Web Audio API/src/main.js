import './favicon.ico'
import './style.css'

console.log("Hello");
 
var isInit = false;

var audioCtx = null;

function play(){
    if (isInit == false) {
        isInit = true;
        console.log("Init");
        audioCtx = new AudioContext();
    }
    console.log("Play");
    // create Oscillator node

    for (var i = 0; i < 100; i++) {
        beep(i * 60); // One 3 second beep every binute
    }
}

function beep(start) {
    var oscillator = audioCtx.createOscillator();
    oscillator.type = 'sine';
    oscillator.frequency.setValueAtTime(634, start); // value in hertz
    oscillator.connect(audioCtx.destination);
    oscillator.start(start);
    oscillator.stop(start + 3);
}

window.play = play; // Global scope