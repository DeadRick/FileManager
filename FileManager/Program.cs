using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace FileManager
{
    internal partial class OperationWithFile
    {
        // Получение массива дисков.
        private static DriveInfo[] allDrives = DriveInfo.GetDrives();
        // Создаение массива кодировок.
        private static Encoding[] encodingArr = new Encoding[3] { Encoding.UTF8, Encoding.Unicode, Encoding.ASCII };

        /// <summary>
        /// Вывод доступных дисков.
        /// </summary>
        public static void GetDrive()
        {
            Console.WriteLine("Доступные диски:");
            foreach (var drive in allDrives)
            {
                Console.WriteLine($"\t Имя диска: {drive.Name}");
            }
            Console.WriteLine("\nЕсли вы хотите выбрать диск введите cd <имя диска>");
        }
        /// <summary>
        /// Вывод всех возможных файлов в текущей директории.
        /// </summary>
        /// <param name="curDir">Текущая директория.</param>
        public static void ShowFiles(string curDir)
        {
            foreach (string file in Directory.EnumerateFileSystemEntries(curDir))
            {
                Console.WriteLine(file.Split(new char[] { '\\' })[file.Split(new char[] { '\\' }).Length - 1]);
            }
        }

        /// <summary>
        /// Создание файла в доступных кодировках. Таких как
        /// UTF-8, Unicode, Ascii
        /// </summary>
        /// <param name="dir">Текущая директория</param>
        /// <param name="name">Имя файла</param>
        /// <param name="enc">Кодировка. По умолчанию UTF-8</param>
        public static void Create(string dir, string name, string enc = "utf8")
        {
            switch (enc)
            {
                case "utf8":
                    File.WriteAllText(dir + "\\" + name, string.Empty, encodingArr[0]);
                    Console.WriteLine("Файл успешно создан в кодировке UTF-8");
                    break;
                case "unicode":
                    File.WriteAllText(dir + "\\" + name, string.Empty, encodingArr[1]);
                    Console.WriteLine("Файл успешно создан в кодировке Unicode");
                    break;
                case "ascii":
                    File.WriteAllText(dir + "\\" + name, string.Empty, encodingArr[2]);
                    Console.WriteLine("Файл успешно создан в кодировке ASCII");
                    break;
                default:
                    Console.WriteLine("Не получается создать файл.");
                    break;
            }

        }
        /// <summary>
        /// Режим для разработчика. Точнее создание тестового файла.
        /// </summary>
        /// <param name="dir">Текущая директория.</param>
        public static void WriteIn(string dir)
        {
            if (!File.Exists(dir))
            {
                OperationWithFile.Create(dir, "develop_mode");
            }
            File.WriteAllText(dir + @"\" + "develop_mode.txt", "0\n");
            for (var i = 1; i <= 5; i++)
            {
                File.AppendAllText(dir + @"\" + "develop_mode.txt", $"{i}\n");

            }
            Console.WriteLine($"Папка для разработчика создана.");
        }
    }
    internal partial class Program
    {
        /// <summary>
        /// Основная программа.
        /// </summary>
        static void Main()
        {
            Intro();
            string firstDir = Directory.GetCurrentDirectory();
            string commandFull = "";
            MainProgram(firstDir, commandFull);
        }

        /// <summary>
        /// Зацикленная программа. 
        /// Чтение введенных команд.
        /// </summary>
        /// <param name="firstDir"></param>
        /// <param name="commandFull"></param>
        static void MainProgram(string firstDir, string commandFull)
        {
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                // Получение нынешней начальной директории приложения
                string dir = Directory.GetCurrentDirectory(); 
                Console.Write("\b" + dir + ": ");
                commandFull = Console.ReadLine();
                string[] command = commandFull.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                // Обработка введенной команды.
                SwitchOfOperations(command, dir, firstDir);
                Console.ResetColor();
            } while (!commandFull.Contains("exit"));
        }
        /// <summary>
        /// Метод, демонстрирующий отличия двух текстовых файлов.
        /// </summary>
        /// <param name="Located">Лист изменений.</param>
        static void DiffShowing(List<string> Located)
        {
            foreach (var it in Located)
            {
                if (it[0] == '+')
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(it);
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (it[0] == '-')
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(it);
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("  " + it);
                    Console.ForegroundColor = ConsoleColor.Green;
                }
            }
        }
        /// <summary>
        /// Конкатенация двух файлов.
        /// </summary>
        /// <param name="dirOne">Первый файл.</param>
        /// <param name="dirTwo">Вторйо файл.</param>
        /// <param name="curDir">Текущая директория</param>
        /// <param name="name">Имя нового файла.</param>
        static void PlusFiles(string dirOne, string dirTwo, string curDir, string name = "new.txt")
        {
            string[] firstInfo = File.ReadAllLines(curDir + @"\" + dirOne);
            string[] secondInfo = File.ReadAllLines(curDir + @"\" + dirTwo);

            if (!File.Exists(curDir + @"\" + name))
            {
                File.WriteAllText(curDir + @"\" + name, string.Empty);
            }
            File.AppendAllLines(curDir + @"\" + name, firstInfo);
            File.AppendAllLines(curDir + @"\" + name, secondInfo);
        }
        /// <summary>
        /// Чтение файла в разных кодировках.
        /// </summary>
        /// <param name="dir">Текущая директория</param>
        static void ReadSwitch(string dir) // TODO: Декомпозировать через Generic
        {
            string enc = null;
            if (!File.Exists(dir))
            {
                Console.WriteLine("Такого файла не существует.");
            }
            else
            {
                // Чтение списка команд.
                if (!dir.Contains("command.txt"))
                {
                    Console.WriteLine("В какой кодировке вы хотите прочитать данные из файла?" +
                                        "\n\t1. UTF8" +
                                        "\n\t2. Unicode" +
                                        "\n\t3. ASCII");
                    enc = Console.ReadLine();
                }
                else
                {
                    enc = "1";
                }
                switch (enc)
                {
                    case "1":
                        {
                            string[] infoUTF8 = File.ReadAllLines(dir, System.Text.Encoding.UTF8);
                            foreach (var el in infoUTF8)
                            {
                                Console.WriteLine(el);
                            }
                            break;
                        }
                    case "2":
                        {
                            string[] infoUni = File.ReadAllLines(dir, System.Text.Encoding.Unicode);
                            foreach (var el in infoUni)
                            {
                                Console.WriteLine(el);
                            }
                            break;
                        }
                    case "3":
                        {
                            string[] infoASCII = File.ReadAllLines(dir, System.Text.Encoding.ASCII);
                            foreach (var el in infoASCII)
                            {
                                Console.WriteLine(el);
                            }
                            break;
                        }
                    default:
                        {
                            string[] infoUTF8 = File.ReadAllLines(dir, System.Text.Encoding.Unicode);
                            foreach (var el in infoUTF8)
                            {
                                Console.WriteLine(el);
                            }
                            break;
                        }
                }
            }
        }
    }
}

