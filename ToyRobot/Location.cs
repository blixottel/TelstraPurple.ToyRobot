using System;
using static ToyRobot.DirectionEnum;

namespace ToyRobot
{
    public class Location
    {
        public LocationModel LocationModel = new();

        public void Place(int x, int y, Direction direction)
        {
            LocationModel.X = x;
            LocationModel.Y = y;
            if (direction != Direction.Undefined) LocationModel.Direction = direction;
            if (LocationModel.Direction == Direction.Undefined) AddValidationMessage($"Unable to place the robot without a direction.");
        }

        public void Left()
        {
            if (LocationModel.Direction == Direction.Undefined) { AddValidationMessage($"Unable to turn, the toy robot has not been placed yet."); }
            else if (LocationModel.Direction == Direction.North) { LocationModel.Direction = Direction.West; }
            else { LocationModel.Direction -= 1; }
        }

        public void Right()
        {
            if (LocationModel.Direction == Direction.Undefined) { AddValidationMessage($"Unable to turn, the toy robot has not been placed yet."); }
            else if (LocationModel.Direction == Direction.West) { LocationModel.Direction = Direction.North; }
            else { LocationModel.Direction += 1; }
        }

        public void Move()
        {
            if (LocationModel.Direction == Direction.Undefined) { AddValidationMessage($"Unable to move, the toy robot has not been placed yet."); }
            else if (LocationModel.Direction == Direction.North) { LocationModel.Y += 1; }
            else if (LocationModel.Direction == Direction.South) { LocationModel.Y -= 1; }
            else if (LocationModel.Direction == Direction.East) { LocationModel.X += 1; }
            else if (LocationModel.Direction == Direction.West) { LocationModel.X -= 1; }

            ValidateBoundaries();
        }

        public string GetReport()
        {
            string report;
            if (LocationModel.Direction == Direction.Undefined) { report = "Toy Robot is not on the board."; }
            else { report = $"Output: {LocationModel.X}, {LocationModel.Y}, {LocationModel.Direction.ToString().ToUpper()}"; }
            return report;
        }

        public void ValidateBoundaries()
        {
            if (LocationModel.X < LocationModel.MinX)
            {
                AddValidationMessage($"Destruction alert! Toy Robot will crash off the West edge.");
                LocationModel.X = LocationModel.MinX;
            }
            if (LocationModel.X > LocationModel.MaxX)
            {
                AddValidationMessage($"Destruction alert! Toy Robot will crash off the East edge.");
                LocationModel.X = LocationModel.MaxX;
            }
            if (LocationModel.Y < LocationModel.MinY)
            {
                AddValidationMessage($"Destruction alert! Toy Robot will crash off the South edge.");
                LocationModel.Y = LocationModel.MinY;
            }
            if (LocationModel.Y > LocationModel.MaxX)
            {
                AddValidationMessage($"Destruction alert! Toy Robot will crash off the North edge.");
                LocationModel.Y = LocationModel.MaxX;
            }
        }

        public void AddValidationMessage(string validationMessage)
        {
            if (LocationModel.ValidationMessage == string.Empty) { LocationModel.ValidationMessage = validationMessage; }
            else { LocationModel.ValidationMessage += Environment.NewLine + validationMessage; }
        }
    }
}
