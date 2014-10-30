/*global Quintus:false */
/**
Quintus HTML5 Game Engine - XBox controller Module
*/

/**
 * Quintus Xbox Module
 *
 * @class Quintus.Xbox

 * The only thing that is going on here is that we connect to a
 * real-time server (XSockets) and the default controller (generic).
 * This allows us to avoid writing server-side code, but we can now
 * send commands to Quintus from anything talking TCP/IP, so
 * the name Xbox might not be correct here but that is what I used in the demo.

 * When ever a command arrives we just set the inputs to match the command.
 * So we expect the data to look like {right:true} for example, but it might
 * also be more complex if state has changed in the controller... like
 * {right:false, action:true, left:true}
 * The line above would stop the moving right and a jump to the left would be initiated.
 */
Quintus.Xbox = function (Q) {
    Q.XSockets = new XSockets.WebSocket('ws://127.0.0.1:4502', ['generic']);
    Q.XSockets.controller('generic').on('cmd', function (d) { Q.inputs = d; });
   
    Q.xboxVibrate = function () {        
        Q.XSockets.controller('generic').invoke('vibrate',300);
    }
};

