using NUnit.Framework;
using System;
using ToyRobot;

namespace Tests
{
    public class CommandTests
    {
        private static CommandModel ParseCommandString(string commandString)
        {
            Command command = new() { CommandString = commandString };
            CommandModel commandModel = command.ParseString();
            return commandModel;
        }

        [Test]
        public static void PlaceCommandNoArguments()
        {
            CommandModel commandModel = ParseCommandString("PLACE");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo("Arguments should be specified for the PLACE command."));
        }

        [Test]
        public static void PlaceCommandOneArgument()
        {
            CommandModel commandModel = ParseCommandString("PLACE 1");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo("X and Y coordinates should be specified for the PLACE command."));
        }

        [Test]
        public static void PlaceCommandTwoArgumentsNonNumeric()
        {
            CommandModel commandModel = ParseCommandString("PLACE A,B");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo($"The X coordinate for the PLACE command (A) must be a valid number.{Environment.NewLine}The Y coordinate for the PLACE command (B) must be a valid number."));
        }

        [Test]
        public static void PlaceCommandTwoArgumentsValid()
        {
            CommandModel commandModel = ParseCommandString("PLACE 1,1");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public static void PlaceCommandThreeArgumentsValid()
        {
            CommandModel commandModel = ParseCommandString("PLACE 1,1,NORTH");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public static void PlaceCommandArgument1NonNumeric()
        {
            CommandModel commandModel = ParseCommandString("PLACE A, 1");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo("The X coordinate for the PLACE command (A) must be a valid number."));
        }

        [Test]
        public static void PlaceCommandArgument1LessThan0()
        {
            CommandModel commandModel = ParseCommandString("PLACE -1,1");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo("The X coordinate for the PLACE command must be greater than or equal to 0."));
        }

        [Test]
        public static void PlaceCommandArgument1MoreThan5()
        {
            CommandModel commandModel = ParseCommandString("PLACE 6,1");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo("The X coordinate for the PLACE command must be less than or equal to 5."));
        }

        [Test]
        public static void PlaceCommandArgument2NonNumeric()
        {
            CommandModel commandModel = ParseCommandString("PLACE 1,A");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo("The Y coordinate for the PLACE command (A) must be a valid number."));
        }

        [Test]
        public static void PlaceCommandArgument2LessThan0()
        {
            CommandModel commandModel = ParseCommandString("PLACE 1,-1");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo("The Y coordinate for the PLACE command must be greater than or equal to 0."));
        }

        [Test]
        public static void PlaceCommandArgument2MoreThan5()
        {
            CommandModel commandModel = ParseCommandString("PLACE 1,6");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo("The Y coordinate for the PLACE command must be less than or equal to 5."));
        }

        [Test]
        public static void PlaceCommandArgument3Unknown()
        {
            CommandModel commandModel = ParseCommandString("PLACE 1,1,UNKNOWN");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo("The direction value for the PLACE command must be one of NORTH, EAST, SOUTH or WEST."));
        }

        [Test]
        public static void LeftCommandNoArguments()
        {
            CommandModel commandModel = ParseCommandString("LEFT");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo(""));
        }

        [Test]
        public static void LeftCommandArguments()
        {
            CommandModel commandModel = ParseCommandString("LEFT 1,1");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo("Unexpected arguments supplied for LEFT."));
        }

        [Test]
        public static void RightCommandNoArguments()
        {
            CommandModel commandModel = ParseCommandString("RIGHT");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo(""));
        }

        [Test]
        public static void RightCommandArguments()
        {
            CommandModel commandModel = ParseCommandString("RIGHT 1,1");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo("Unexpected arguments supplied for RIGHT."));
        }

        [Test]
        public static void MoveCommandNoArguments()
        {
            CommandModel commandModel = ParseCommandString("MOVE");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo(""));
        }

        [Test]
        public static void MoveCommandArguments()
        {
            CommandModel commandModel = ParseCommandString("MOVE 1,1");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo("Unexpected arguments supplied for MOVE."));
        }

        [Test]
        public static void ReportCommandNoArguments()
        {
            CommandModel commandModel = ParseCommandString("REPORT");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo(""));
        }

        [Test]
        public static void ReportCommandArguments()
        {
            CommandModel commandModel = ParseCommandString("REPORT 1,1");
            Assert.That(commandModel.ValidationMessage, Is.EqualTo("Unexpected arguments supplied for REPORT."));
        }

    }
}