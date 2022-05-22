using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot
{
    public class LocationModel
    {
        public const int MinX = 0;
        public const int MaxX = 5;
        public const int MinY = 0;
        public const int MaxY = 5;

        public int X = 0;
        public int Y = 0;
        public DirectionEnum.Direction Direction = DirectionEnum.Direction.Undefined;

        public string ValidationMessage = string.Empty;
    }
}
