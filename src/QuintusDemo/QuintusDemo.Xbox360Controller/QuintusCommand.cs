namespace QuintusDemo.Xbox360Controller
{
    /// <summary>
    /// The signature of the command that we will pass to quintus.
    /// </summary>
    public class QuintusCommand
    {
        public bool left { get; set; }
        public bool right { get; set; }
        public bool up { get; set; }
        public bool down { get; set; }
        public bool action { get; set; }
    }
}