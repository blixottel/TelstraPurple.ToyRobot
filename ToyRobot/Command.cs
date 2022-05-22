using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot
{
    public class Command
    {
        public CommandModel CommandModel = new();
        public string CommandString = string.Empty;

        private static readonly List<string> validCommands = new() { "PLACE", "MOVE", "LEFT", "RIGHT", "REPORT" };

        public CommandModel ParseString()
        {
            CommandString = CommandString.ToUpper();
            int charLocation = CommandString.IndexOf(" ", StringComparison.Ordinal);
            if (charLocation >= 0)
            {
                CommandModel.Command = CommandString[..charLocation];
                if (CommandModel.Command == "PLACE") CommandModel = ParseArguments(CommandString[charLocation..]);
                else if (CommandModel.Command != "PLACE" && CommandString[charLocation..].Length > 0) AddValidationMessage($"Unexpected arguments supplied for {CommandModel.Command}."); ;
            }
            else
            {
                CommandModel.Command = CommandString;
                if (CommandModel.Command == "PLACE") { AddValidationMessage($"Arguments should be specified for the PLACE command."); }
            }

            Validate();

            return CommandModel;
        }

        public CommandModel ParseArguments(string argumentString)
        {
            string[] splitArguments = argumentString.Split(",");
            if (splitArguments.Length == 0) { AddValidationMessage($"Arguments should be specified for the PLACE command."); }
            else if (splitArguments.Length == 1) { AddValidationMessage($"X and Y coordinates should be specified for the PLACE command."); }
            else if (splitArguments.Length >= 2)
            {
                ParseX(splitArguments[0].Trim());
                ParseY(splitArguments[1].Trim());
            }
            if (splitArguments.Length >= 3) { ParseDirection(splitArguments[2].Trim()); }
            if (splitArguments.Length >= 4) { AddValidationMessage($"Unexpected arguments supplied for {CommandModel.Command}."); }
            return CommandModel;
        }

        public void ParseX(string stringX)
        {
            bool isValid = int.TryParse(stringX, out int i);
            CommandModel.X = i;
            if (!isValid) { AddValidationMessage($"The X coordinate for the PLACE command ({stringX}) must be a valid number."); }
        }

        public void ParseY(string stringY)
        {
            bool isValid = int.TryParse(stringY, out int i);
            CommandModel.Y = i;
            if (!isValid) { AddValidationMessage($"The Y coordinate for the PLACE command ({stringY}) must be a valid number."); }
        }

        public void ParseDirection(string direction)
        {
            //Numeric values are not allowed.
            bool isValid = !int.TryParse(direction, out _);
            if (isValid)
            {
                isValid = Enum.TryParse(typeof(DirectionEnum.Direction), direction, true, out var directionEnum);
                if (isValid) { CommandModel.Direction = (DirectionEnum.Direction)directionEnum; }
            }
            if (!isValid) { AddValidationMessage($"The direction value for the PLACE command must be one of NORTH, EAST, SOUTH or WEST."); }
        }

        public void Validate()
        {
            if (CommandModel.Command == "PLACE") { ValidatePlace(); }
            else if (!validCommands.Contains(CommandModel.Command)) { AddValidationMessage($"{CommandModel.Command} is not a valid command."); }
        }

        public void ValidatePlace()
        {
            if (CommandModel.X < LocationModel.MinX) { AddValidationMessage($"The X coordinate for the PLACE command must be greater than or equal to {LocationModel.MinX}."); }
            if (CommandModel.X > LocationModel.MaxX) { AddValidationMessage($"The X coordinate for the PLACE command must be less than or equal to {LocationModel.MaxX}."); }
            if (CommandModel.Y < LocationModel.MinY) { AddValidationMessage($"The Y coordinate for the PLACE command must be greater than or equal to {LocationModel.MinY}."); }
            if (CommandModel.Y > LocationModel.MaxY) { AddValidationMessage($"The Y coordinate for the PLACE command must be less than or equal to {LocationModel.MaxY}."); }
        }

        public void AddValidationMessage(string validationMessage)
        {
            if (CommandModel.ValidationMessage == string.Empty) { CommandModel.ValidationMessage = validationMessage; }
            else { CommandModel.ValidationMessage += Environment.NewLine + validationMessage; }
        }

    }

}
