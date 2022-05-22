using System;

namespace ToyRobot
{
    public class Program
    {
        static void Main()
        {
            ShowInstruction();
            ReadAndProcessCommand();
        }

        static void ShowInstruction()
        {
            Console.WriteLine(Instruction.GetInstruction());
        }

        static void ReadAndProcessCommand()
        {
            LocationModel currentLocationModel = new();
            string commandString = string.Empty;

            while (commandString.ToUpper() != "EXIT")
            {
                Console.Write("> ");
                commandString = Console.ReadLine();
                currentLocationModel = ProcessCommandString(currentLocationModel, commandString);
            }
        }

        static LocationModel ProcessCommandString(LocationModel currentLocationModel, string commandString)
        {
            Command command = new() { CommandString = commandString };
            CommandModel commandModel = command.ParseString();
            if (commandModel.ValidationMessage == string.Empty)
            {
                currentLocationModel = ProcessCommand(currentLocationModel, commandModel);
                if (currentLocationModel.ValidationMessage != string.Empty)
                {
                    Console.WriteLine($"{currentLocationModel.ValidationMessage}{Environment.NewLine}Toy Robot will not move.{Environment.NewLine}");
                    currentLocationModel.ValidationMessage = string.Empty;
                }
            }
            else
            {
                Console.WriteLine($"{commandModel.ValidationMessage}{Environment.NewLine}Toy Robot will not move.{Environment.NewLine}");
            }
            return currentLocationModel;
        }

        public static LocationModel ProcessCommand(LocationModel currentLocationModel, CommandModel commandModel)
        {
            {
                Location location = new() { LocationModel = currentLocationModel };
                if (commandModel.Command == "PLACE")
                { location.Place(commandModel.X, commandModel.Y, commandModel.Direction); }
                else if (commandModel.Command == "MOVE")
                { location.Move(); }
                else if (commandModel.Command == "LEFT")
                { location.Left(); }
                else if (commandModel.Command == "RIGHT")
                { location.Right(); }
                else if (commandModel.Command == "REPORT")
                { Console.WriteLine(location.GetReport()); }
                return currentLocationModel;
            }
        }
    }
}
