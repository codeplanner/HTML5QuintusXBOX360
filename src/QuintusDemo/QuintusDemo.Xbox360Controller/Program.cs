using System;
using System.Threading;
using System.Threading.Tasks;
using SharpDX.XInput;
using XSockets.Client40;
using XSockets.Client40.Common.Event.Arguments;

namespace QuintusDemo.Xbox360Controller
{
    class Program
    {
        /// <summary>
        /// Provides access to the XBox260 controller
        /// </summary>
        private static SharpDX.XInput.Controller _controller;

        /// <summary>
        /// Full duplex communication
        /// </summary>
        private static XSocketClient _client;

        /// <summary>
        /// Currenct command state
        /// </summary>
        private static QuintusCommand command;

        /// <summary>
        /// Flag for game loop
        /// </summary>
        private static bool gameOn;

        static void Main(string[] args)
        {
            command = new QuintusCommand();

            _controller = new SharpDX.XInput.Controller(UserIndex.One);

            _client = new XSocketClient("ws://127.0.0.1:4502", "http://localhost", "generic");

            _client.Controller("generic").OnOpen += OnOpen;
            _client.Controller("generic").OnClose += OnClose;
            
            //When the player hits an enemy the controller will vibrate
            _client.Controller("generic").On<int>("vibrate", (duration) =>
            {
                _controller.SetVibration(new Vibration() { LeftMotorSpeed = 32768, RightMotorSpeed = 32768 });
                Thread.Sleep(duration);
                _controller.SetVibration(new Vibration() {LeftMotorSpeed = 0, RightMotorSpeed = 0});
            });

            _client.Open();

            Console.ReadLine();
        }

        static void OnClose(object sender, EventArgs e)
        {
            gameOn = false;
            Console.WriteLine("Disconnected, game ended!");
        }

        static void OnOpen(object sender, OnClientConnectArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                gameOn = true;
                Console.WriteLine("Connected, control the game!");
                while (gameOn)
                {
                    var state = _controller.GetState();

                    //If there was any change we send a new command
                    if (GetCommand(state))
                    {
                        _client.Controller("generic").Invoke("cmd", command);
                    }

                    //Let the gameloop wait 30 ms
                    Thread.Sleep(30);
                }
            });
        }

        private static bool GetCommand(State state)
        {
            var changed = false;

            //Collect data
            var aBtn = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.A);
            var upBtn = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadUp);
            var downBtn = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadDown);
            var rightBtn = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadRight);
            var leftBtn = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadLeft);

            //Did anything change since last time?
            if (aBtn != command.action || upBtn != command.up || downBtn != command.down || rightBtn != command.right ||
                leftBtn != command.left)
            {
                //Flag that changes occured
                changed = true;

                //Set values since atleast one has changed
                command.action = aBtn;
                command.up = upBtn;
                command.down = downBtn;
                command.right = rightBtn;
                command.left = leftBtn;
            }
            return changed;
        }
    }
}
