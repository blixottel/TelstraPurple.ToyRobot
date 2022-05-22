using NUnit.Framework;
using System;
using ToyRobot;

namespace Tests
{
    public class MovementTests
    {
        private static LocationModel ParseAndProcessCommandString(string commandString, LocationModel locationModel)
        {
            Command command = new() { CommandString = commandString };
            CommandModel commandModel = command.ParseString();
            locationModel = Program.ProcessCommand(locationModel, commandModel);
            return locationModel;
        }

        [Test]
        public static void Place3ArgumentsValid()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 0, 0, NORTH", locationModel);
            Assert.Multiple(() =>
            {
                Assert.That(locationModel.ValidationMessage, Is.EqualTo(""));
                Assert.That(locationModel.X, Is.EqualTo(0));
                Assert.That(locationModel.Y, Is.EqualTo(0));
                Assert.That(locationModel.Direction, Is.EqualTo(DirectionEnum.Direction.North));
            });
        }

        [Test]
        public static void Place2ArgumentsAlreadyPlaced()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 0, 0, NORTH", locationModel);
            locationModel = ParseAndProcessCommandString("PLACE 1, 1", locationModel);
            Assert.Multiple(() =>
            {
                Assert.That(locationModel.ValidationMessage, Is.EqualTo(""));
                Assert.That(locationModel.X, Is.EqualTo(1));
                Assert.That(locationModel.Y, Is.EqualTo(1));
                Assert.That(locationModel.Direction, Is.EqualTo(DirectionEnum.Direction.North));
            });
        }

        [Test]
        public static void Place2ArgumentsNotPlaced()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 0, 0", locationModel);
            Assert.That(locationModel.ValidationMessage, Is.EqualTo("Unable to place the robot without a direction."));
        }

        [Test]
        public static void TurnLeft()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 0, 0, EAST", locationModel);
            locationModel = ParseAndProcessCommandString("LEFT", locationModel);
            Assert.Multiple(() =>
            {
                Assert.That(locationModel.ValidationMessage, Is.EqualTo(""));
                Assert.That(locationModel.X, Is.EqualTo(0));
                Assert.That(locationModel.Y, Is.EqualTo(0));
                Assert.That(locationModel.Direction, Is.EqualTo(DirectionEnum.Direction.North));
            });
        }

        [Test]
        public static void TurnLeftFromNorth()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 3, 4, NORTH", locationModel);
            locationModel = ParseAndProcessCommandString("LEFT", locationModel);
            Assert.Multiple(() =>
            {
                Assert.That(locationModel.ValidationMessage, Is.EqualTo(""));
                Assert.That(locationModel.X, Is.EqualTo(3));
                Assert.That(locationModel.Y, Is.EqualTo(4));
                Assert.That(locationModel.Direction, Is.EqualTo(DirectionEnum.Direction.West));
            });
        }

        [Test]
        public static void TurnRight()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 0, 0, NORTH", locationModel);
            locationModel = ParseAndProcessCommandString("RIGHT", locationModel);
            Assert.Multiple(() =>
            {
                Assert.That(locationModel.ValidationMessage, Is.EqualTo(""));
                Assert.That(locationModel.X, Is.EqualTo(0));
                Assert.That(locationModel.Y, Is.EqualTo(0));
                Assert.That(locationModel.Direction, Is.EqualTo(DirectionEnum.Direction.East));
            });
        }

        [Test]
        public static void TurnRightFromWest()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 3, 4, WEST", locationModel);
            locationModel = ParseAndProcessCommandString("RIGHT", locationModel);
            Assert.Multiple(() =>
            {
                Assert.That(locationModel.ValidationMessage, Is.EqualTo(""));
                Assert.That(locationModel.X, Is.EqualTo(3));
                Assert.That(locationModel.Y, Is.EqualTo(4));
                Assert.That(locationModel.Direction, Is.EqualTo(DirectionEnum.Direction.North));
            });
        }

        [Test]
        public static void MoveNorth()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 2, 2, NORTH", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            Assert.Multiple(() =>
            {
                Assert.That(locationModel.ValidationMessage, Is.EqualTo(""));
                Assert.That(locationModel.X, Is.EqualTo(2));
                Assert.That(locationModel.Y, Is.EqualTo(3));
                Assert.That(locationModel.Direction, Is.EqualTo(DirectionEnum.Direction.North));
            });
        }

        [Test]
        public static void MoveEast()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 2, 2, EAST", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            Assert.Multiple(() =>
            {
                Assert.That(locationModel.ValidationMessage, Is.EqualTo(""));
                Assert.That(locationModel.X, Is.EqualTo(3));
                Assert.That(locationModel.Y, Is.EqualTo(2));
                Assert.That(locationModel.Direction, Is.EqualTo(DirectionEnum.Direction.East));
            });
        }

        [Test]
        public static void MoveSouth()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 2, 2, SOUTH", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            Assert.Multiple(() =>
            {
                Assert.That(locationModel.ValidationMessage, Is.EqualTo(""));
                Assert.That(locationModel.X, Is.EqualTo(2));
                Assert.That(locationModel.Y, Is.EqualTo(1));
                Assert.That(locationModel.Direction, Is.EqualTo(DirectionEnum.Direction.South));
            });
        }

        [Test]
        public static void MoveWest()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 2, 2, WEST", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            Assert.Multiple(() =>
            {
                Assert.That(locationModel.ValidationMessage, Is.EqualTo(""));
                Assert.That(locationModel.X, Is.EqualTo(1));
                Assert.That(locationModel.Y, Is.EqualTo(2));
                Assert.That(locationModel.Direction, Is.EqualTo(DirectionEnum.Direction.West));
            });
        }

        [Test]
        public static void MoveNorthDontCrash()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 2, 5, NORTH", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            Assert.That(locationModel.ValidationMessage, Is.EqualTo("Destruction alert! Toy Robot will crash off the North edge."));
        }

        [Test]
        public static void MoveEastDontCrash()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 5, 2, EAST", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            Assert.That(locationModel.ValidationMessage, Is.EqualTo("Destruction alert! Toy Robot will crash off the East edge."));
        }

        [Test]
        public static void MoveSouthDontCrash()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 2, 0, SOUTH", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            Assert.That(locationModel.ValidationMessage, Is.EqualTo("Destruction alert! Toy Robot will crash off the South edge."));
        }

        [Test]
        public static void MoveWestDontCrash()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 0, 2, WEST", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            Assert.That(locationModel.ValidationMessage, Is.EqualTo("Destruction alert! Toy Robot will crash off the West edge."));
        }

        [Test]
        public static void Report()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 0, 2, WEST", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            Location location = new() { LocationModel = locationModel };
            Assert.That(location.GetReport(), Is.EqualTo("Output: 0, 2, WEST"));
        }

        [Test]
        public static void ProvidedExample1()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 0, 0, NORTH", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            Assert.Multiple(() =>
            {
                Assert.That(locationModel.ValidationMessage, Is.EqualTo(""));
                Assert.That(locationModel.X, Is.EqualTo(0));
                Assert.That(locationModel.Y, Is.EqualTo(1));
                Assert.That(locationModel.Direction, Is.EqualTo(DirectionEnum.Direction.North));
            });
        }

        [Test]
        public static void ProvidedExample2()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 0, 0, NORTH", locationModel);
            locationModel = ParseAndProcessCommandString("LEFT", locationModel);
            Assert.Multiple(() =>
            {
                Assert.That(locationModel.ValidationMessage, Is.EqualTo(""));
                Assert.That(locationModel.X, Is.EqualTo(0));
                Assert.That(locationModel.Y, Is.EqualTo(0));
                Assert.That(locationModel.Direction, Is.EqualTo(DirectionEnum.Direction.West));
            });
        }

        [Test]
        public static void ProvidedExample3()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 1, 2, EAST", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            locationModel = ParseAndProcessCommandString("LEFT", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            Assert.Multiple(() =>
            {
                Assert.That(locationModel.ValidationMessage, Is.EqualTo(""));
                Assert.That(locationModel.X, Is.EqualTo(3));
                Assert.That(locationModel.Y, Is.EqualTo(3));
                Assert.That(locationModel.Direction, Is.EqualTo(DirectionEnum.Direction.North));
            });
        }

        [Test]
        public static void ProvidedExample4()
        {
            LocationModel locationModel = new();
            locationModel = ParseAndProcessCommandString("PLACE 1, 2, EAST", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            locationModel = ParseAndProcessCommandString("LEFT", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            locationModel = ParseAndProcessCommandString("PLACE 3, 1", locationModel);
            locationModel = ParseAndProcessCommandString("MOVE", locationModel);
            Assert.Multiple(() =>
            {
                Assert.That(locationModel.ValidationMessage, Is.EqualTo(""));
                Assert.That(locationModel.X, Is.EqualTo(3));
                Assert.That(locationModel.Y, Is.EqualTo(2));
                Assert.That(locationModel.Direction, Is.EqualTo(DirectionEnum.Direction.North));
            });
        }

    }

}
