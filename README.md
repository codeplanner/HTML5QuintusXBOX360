## HTML5 Quintus with XBOX360 controller
It would be awesome if I knew how to write games, but I do not....
However, I do know how to setup real-time communications between pretty much anything that has TCP/IP.

[HTML5Quintus](http://www.html5quintus.com/) is a awesome peace of work, as mentioned above I do not know anything about creating  a game. So I downloaded the samples from https://github.com/cykod/Quintus and took a look.

My goal was to be able to control a Quintus game from my Xbox360 controller.
As it turns out Quintus has a very easy way to create custom modules. I wanted to enable remote control as well as sending feedback to the XBox360 controller on collisions (to make it vibrate).

### Pre Req
If you would like to run the sample you will need the following.

 - Xbox360 controller
 - Xbox360 PC Wireless Gaming Receiver
 - Visual Studio

### Code
All code is under the "src" folder, but the Quintur module that I had to add wsa so simple that I will paste in in below.

    Quintus.Xbox = function (Q) {
        Q.XSockets = new XSockets.WebSocket('ws://127.0.0.1:4502', ['generic']);
        Q.XSockets.controller('generic').on('cmd', function (d) { Q.inputs = d; });
   
        Q.xboxVibrate = function () {        
            Q.XSockets.controller('generic').invoke('vibrate',300);
        }
    };

The only magic in this is the SharpDX.XInput library that I installed from nuget.

If you have any questions, feel free to contact me. My email is uffe [at] xsockets.net