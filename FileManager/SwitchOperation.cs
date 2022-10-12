using System;
using System.IO;
using System.Text;


namespace FileManager
{
    internal partial class Program
    {
        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        /// <param name="errorInfo">Информацию, которую нужно отметить как ошибку</param>
        private static void Error(string errorInfo)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(errorInfo);
            Console.ResetColor();
        }
        /// <summary>
        /// Операция, запускает метод в зависимости 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="dir"></param>
        /// <param name="firstDir"></param>
        public static void SwitchOfOperations(string[] command, string dir, string firstDir)
        {
            try
            {
                switch (command[0])
                {
                    case "ls":
                        ListOfFiles(command, dir);
                        break;
                    case "cd":
                        ChangeDirectory(command);
                        break;
                    case "dr":
                        Drives();
                        break;
                    case "mv":
                        Move(command);
                        break;
                    case "rm":
                        Remove(command);
                        break;
                    case "pls":
                        Plus(command, dir);
                        break;
                    case "reg":
                        RegSearch(command, dir);
                        break;
                    case "dev":
                        DeveloperMode(command, dir);
                        break;
                    case "read":
                        Read(command, dir);
                        break;
                    case "diff":
                        Different(command);
                        break;
                    case "create":
                        Create(command, dir);
                        break;
                    case "copy":
                        Copy(command, dir);
                        break;
                    case "first":
                        Directory.SetCurrentDirectory(firstDir);
                        break;
                    case "clear":
                        Console.Clear();
                        break;
                    case "regall":
                        RegSearch(command, dir, false);
                        break;
                    case "help":
                        Directory.SetCurrentDirectory(firstDir);
                        ReadSwitch(@"..\..\..\..\command.txt");
                        break;
                    default:
                        if (command[0] != "exit")
                        {
                            Error("Такой команды не существует.");
                        }
                        else
                        {
                            Console.WriteLine("Спасибо за использование программы.");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Error("Что-то пошло не так...");
                Error(ex.Message);
            }
        }
    }
}