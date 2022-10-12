using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileManager
{
    /// <summary>
    /// Логическая часть программы.
    /// </summary>
    internal partial class Program
    {
        /// <summary>
        /// Вывод всех файлов в текущей директории.
        /// </summary>
        /// <param name="command">Введенная команда</param>
        /// <param name="dir">Текущая директория</param>
        public static void ListOfFiles(string[] command, string dir)
        {
            if (command.Length > 1)
            {
                Error("Неверный формат команды. ls");
            }
            else
            {
                try
                {
                    Console.WriteLine();
                    OperationWithFile.ShowFiles(dir);
                    Console.WriteLine();
                }
                catch (IOException ex)
                {
                    Error(ex.Message);
                }
                catch (Exception ex)
                {
                    Error(ex.Message);
                }
            }
        }
        /// <summary>
        /// Функция cd для перехода в другую директорию.
        /// </summary>
        /// <param name="command">Введенная команда</param>
        public static void ChangeDirectory(string[] command)
        {
            try
            {
                if (command.Length >= 2)
                {
                    // Переменная, которая хранит название директории, разделенной пробелом. Например Program FIles.
                    string antiSpace = "";
                    // Цикл превращает [cd, Program, Files] в Program Files. Иначе переход в такие директории не осуществляется.
                    for (var i = 1; i < command.Length; i++)
                    {
                        antiSpace += command[i];
                        antiSpace += " ";
                    }
                    Directory.SetCurrentDirectory(antiSpace);
                }
                else
                {
                    Error("Неверный формат команды. cd <путь до файла>");
                }
            }
            catch (IOException ex)
            {
                Error(ex.Message);
            }
            catch (Exception ex)
            {
                Error("Не удается заполучить доступ к этому файлу." +
                    "\nЛибо что-то пошло не так.");
                Error(ex.Message);
            }
        }
        /// <summary>
        /// Получение доступных дисков.
        /// </summary>
        public static void Drives()
        {
            try
            {
                OperationWithFile.GetDrive();
            }
            catch (IOException ex)
            {
                Error(ex.Message);
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }
        /// <summary>
        /// Удаление директории, либо файла.
        /// </summary>
        /// <param name="command">Введенная команда</param>
        public static void Remove(string[] command)
        {
            // Удаление файла.
            try
            {
                if (command.Length == 2 && command[1] != "command.txt")
                {
                    File.Delete(command[1]);
                }
                else
                {
                    Error("Нельзя удалять файл с командами!");
                }

            }
            catch
            {
                // Удаление директории.
                try
                {
                    if (command.Length == 2)
                    {
                        Directory.Delete(command[1], true);
                    }
                    else
                    {
                        Error("Некорректный формат команды. rm <путь к файлу>");
                    }
                }
                catch (IOException ex)
                {
                    Error(ex.Message);
                }
                catch (Exception ex)
                {
                    Error(ex.Message);
                }
            }
        }
        /// <summary>
        /// Перемещение директории.
        /// </summary>
        /// <param name="command">Введенная команда.</param>
        public static void Move(string[] command)
        {
            try
            {
                if (command.Length == 3)
                {
                    Directory.Move(command[1], command[2]);
                }
                else
                {
                    Error("Некорректный формат команды. mv <путь до существующей папки> <путь по которому переместить папку>");
                }
            }
            catch (IOException ex)
            {
                Error(ex.Message);
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }
        /// <summary>
        /// Конкатенация двух файлов.
        /// </summary>
        /// <param name="command">Введенная команда</param>
        /// <param name="dir">Текущая директория</param>
        public static void Plus(string[] command, string dir)
        {
            try
            {
                if (command.Length == 3 || command.Length == 4)
                {
                    // В случае, если пользователь решил назвать по-своему новый файл.
                    if (command.Length == 4)
                    {
                        PlusFiles(command[1], command[2], dir, command[3]);
                    }
                    else
                    {
                        PlusFiles(command[1], command[2], dir);
                    }
                }
                else
                {
                    Error("Некорректный формат командны. pls <путь до первого файла> <путь до второго файл> [имя файла]");
                }
            }
            catch (IOException ex)
            {
                Error(ex.Message);
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }
        /// <summary>
        /// Режим разработчика для создания тестового файла.
        /// </summary>
        /// <param name="command">Введенная команда</param>
        /// <param name="dir">Текущая директория</param>
        public static void DeveloperMode(string[] command, string dir)
        {
            try
            {
                if ((command.Length == 1))
                {
                    OperationWithFile.WriteIn(dir);
                }
                else
                {
                    Error("Некорректная команда. Для того, чтобы создать тестовый файл введите dev");
                }

            }
            catch (IOException ex)
            {
                Error(ex.Message);
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }
        /// <summary>
        /// Чтение файлов.
        /// </summary>
        /// <param name="command">Введенная команда</param>
        /// <param name="dir">Текущая директория</param>
        public static void Read(string[] command, string dir)
        {
            try
            {
                // Также вызывается функция для выбора кодировки!
                if (command.Length == 2)
                {
                    ReadSwitch(dir + @"\" + command[1]);
                }
                else
                {
                    Console.WriteLine("Ошибка в чтении файла. read <имя файла>");
                }
            }
            catch (IOException ex)
            {
                Error(ex.Message);
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }
        /// <summary>
        /// Создает файл в выбранной кодировке.
        /// </summary>
        /// <param name="command">Введенная команда</param>
        /// <param name="dir">Текущая директория</param>
        public static void Create(string[] command, string dir)
        {
            try
            {
                string fileIsReal = dir + "\\" + command[1];
                if ((command.Length == 2) || (command.Length == 3))
                {
                    if (!File.Exists(fileIsReal) && command.Length == 3)
                    {
                        OperationWithFile.Create(dir, command[1], command[2]);
                    }
                    else if ((!File.Exists(fileIsReal) && command.Length == 2))
                    {
                        OperationWithFile.Create(dir, command[1]);
                    }
                    else
                    {
                        Console.WriteLine("Файл уже существует.");
                    }
                }
                else
                {
                    Error("Некорректный формат команды. create <имя> <кодировка>");
                }

            }
            catch (IOException ex)
            {
                Error(ex.Message);
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }

        /// <summary>
        /// Копирование файла.
        /// </summary>
        /// <param name="command">Введенная команда</param>
        /// <param name="dir">Текущая директория</param>
        public static void Copy(string[] command, string dir)
        {
            try
            {
                if (command.Length == 2)
                {
                    string[] infoCopy = File.ReadAllLines(dir + "\\" + command[1]);
                    string[] splitFile = command[1].Split('.');
                    // Отсеивается тип файла.
                    OperationWithFile.Create(dir, splitFile[0] + "_copy.txt");
                    File.WriteAllLines(dir + "\\" + splitFile[0] + "_copy.txt", infoCopy);
                }
                else
                {
                    Console.WriteLine("Некорректный формат команды. copy <name>");
                }
            }
            catch (IOException ex)
            {
                Error(ex.Message);
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }
        /// <summary>
        /// Поиск подходящих значений.
        /// </summary>
        /// <param name="command">Введенная команда</param>
        /// <param name="dir">Текущая директория</param>
        public static void RegSearch(string[] command, string curDir, bool check = true)
        {
            List<string> files = new List<string>();
            // Получение всех файлов из директории.
            if (check)
            {
                foreach (string file in Directory.EnumerateFiles(curDir)) // Directory.EmumerateFiles(curDir, ".", SearchOption.AllDirectories);
                {
                    string[] name = file.Split("\\");
                    files.Add(name[name.Length - 1]);
                }
            }
            else
            {
                foreach (string file in Directory.EnumerateFiles(curDir, ".", SearchOption.AllDirectories)) // 
                {
                    string[] name = file.Split("\\");
                    files.Add(name[name.Length - 1]);
                }
            }
            // Поиск совпадений в директории.
            foreach (var el in files)
            {
                Match result = Regex.Match(el, command[1]);
                if (result.Success) 
                    Console.WriteLine(el);
            }
        }
        /// <summary>
        /// Поиск отличий в двух файлов.
        /// </summary>
        /// <param name="command">Введенная команда</param>
        public static void Different(string[] command)
        {
            try
            {
                if (command.Length == 3)
                {
                    List<string> Located = new List<string>();

                    string[] firstFile = File.ReadAllLines(command[1]);
                    string[] secondFile = File.ReadAllLines(command[2]);
                    int lenOne = firstFile.Length, lenTwo = secondFile.Length;
                    // Берем больший по размеру файлу и сравниваем все элементы.
                    if (lenOne >= lenTwo)
                    {
                        foreach (var it in firstFile)
                        {
                            if (secondFile.Contains(it))
                            {
                                // Равные элементы добовляем в лист.
                                Located.Add(it);
                            }
                            else
                            {
                                // Элементов, которые не находим добавим с +
                                Located.Add("+ " + it);
                            }
                        }
                        foreach (var it in secondFile)
                        {
                            if (!Located.Contains(it))
                            {
                                // Тут ищем файлы уже из маленього массива и тех, которых нет добавим с минусом.
                                Located.Add("- " + it);
                            }
                        }
                    }
                    else
                    {
                        // Здесь тоже самое, но только в случае, если второй файл больше первого.
                        foreach (var it in secondFile)
                        {
                            if (firstFile.Contains(it))
                            {
                                // Равные элементы добовляем в лист.
                                Located.Add(it);
                            }
                            else
                            {
                                // Элементов, которые не находим добавим с +
                                Located.Add("+ " + it);
                            }
                        }
                        foreach (var it in firstFile)
                        {
                            if (!Located.Contains(it))
                            {
                                // Тут ищем файлы уже из маленього массива и тех, которых нет добавим с минусом.
                                Located.Add("- " + it);
                            }
                        }
                    }
                    // Вывод результата.
                    DiffShowing(Located);
                }
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }
    }
}
