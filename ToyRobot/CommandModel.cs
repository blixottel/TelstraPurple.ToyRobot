namespace ToyRobot
{
    public class CommandModel
    {
        public string Command;
        public int X;
        public int Y;
        public DirectionEnum.Direction Direction = DirectionEnum.Direction.Undefined;

        public string ValidationMessage = string.Empty;
    }
}
