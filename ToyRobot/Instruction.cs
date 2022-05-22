using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot
{
    internal class Instruction
    {
        public static string GetInstruction()
        {
            try
            {
                StringBuilder sb = new();
                sb.AppendLine("****************************************************************************************************");
                sb.AppendLine("**                                           TOY  ROBOT                                           **");
                sb.AppendLine("****************************************************************************************************");
                sb.AppendLine("** Available commands (case insensitive):                                                         **");
                sb.AppendLine("** PLACE X, Y, DIRECTION - Place Toy Robot at location (X, Y) and face in the specified direction **");
                sb.AppendLine("** MOVE                  - Move forward 1 position                                                **");
                sb.AppendLine("** LEFT                  - Turn 90 degrees to the left                                            **");
                sb.AppendLine("** RIGHT                 - Turn 90 degrees to the right                                           **");
                sb.AppendLine("** REPORT                - Lose Toy Robot? This will tell you where it is!                        **");
                sb.AppendLine("** EXIT                  - Terminate Toy Robot                                                    **");
                sb.AppendLine("****************************************************************************************************");
                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
