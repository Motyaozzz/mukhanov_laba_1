using System;
using System.IO;
using System.Xml.Linq;
using System.Text.Json;
using System.IO.Compression;

namespace mukhanov_laba_1
{
        class Program
    {
        /// <summary>
        /// Выводит информацию о дисках в консоль 
        /// </summary>
        static void diskInfo()
        {
            Console.WriteLine("[1] Информация о дисках\n\n");
            DriveInfo[] drives = DriveInfo.GetDrives();
            //получить информацию о дисках
            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Название диска: {drive.Name}");
                Console.WriteLine($"Тип диска: {drive.DriveType}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Объем диска: {drive.TotalSize / 1024 / 1024 / 1024}");
                    Console.WriteLine($"Свободное место: {drive.TotalFreeSpace/1024/1024/1024}");
                    Console.WriteLine($"Метка: {drive.VolumeLabel}");
                }
                
            }
        }


        /// <summary>
        /// Создает текстовый файл по path.
        /// </summary>
        /// <param name="path"></param>
        static void fileTxt(string path)
        {
            Console.WriteLine("[2] Работа с файлами\n\n");
            FileInfo fileInf = new FileInfo(path);
            try
            {
                using (FileStream fStream = File.Create(path))
                {
                    Console.WriteLine($"Путь файла: {path}");
                    //если файл создан, получить информацию о файле
                    if (fileInf.Exists)
                    {
                        Console.WriteLine("Имя файла: {0}", fileInf.Name);
                        Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                        Console.WriteLine("Размер: {0}", fileInf.Length);
                        Console.WriteLine();
                    }
                }
                //перезаписывает файл, добавляя строку
                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                    Console.Write("Введите строку для записи в файл: ");
                    sw.WriteLine(Console.ReadLine());
                }
                //Открыть поток и прочитать файл
                using (StreamReader sr = new StreamReader(path))
                {
                    Console.Write("\n\nИнформация из файла: ");
                    Console.WriteLine(sr.ReadToEnd());
                }
                //если файл создан, удалить его
                Console.Write("\n\nУдалить файл [1-да, 0-нет]:");
                int sigh = int.Parse(Console.ReadLine());
                if (sigh == 1)
                {
                    if (fileInf.Exists)
                    {
                        fileInf.Delete();
                        Console.WriteLine($"\n\nФайл по пути {path} удален.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"\n\nФайл по пути {path} удален.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Создает файл JSON по path
        /// </summary>
        /// <param name="path"></param>
        static void fileJson(string path)
        {
            Console.WriteLine("[3] Работа с JSON\n\n");
            FileInfo fileJSON = new FileInfo(path);
            try
            {
                using (FileStream fStream = File.Create(path))
                {
                    Console.WriteLine($"путь файла: {path}");
                    //если файл создан, получить информацию о файле
                    if (fileJSON.Exists)
                    {
                        Console.WriteLine("Имя файла: {0}", fileJSON.Name);
                        Console.WriteLine("Время создания: {0}", fileJSON.CreationTime);
                        Console.WriteLine("Размер: {0}", fileJSON.Length);
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                //запись данных
                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                    Student student = new Student();
                    Console.Write("Введите имя студента: ");
                    student.name = Console.ReadLine();
                    Console.Write("Введите фамилию студента: ");
                    student.surName = Console.ReadLine();
                    Console.Write("Введите группу студента: ");
                    student.group = Console.ReadLine();
                    while (true)
                    {
                        Console.Write("Введите год поступления студента: ");
                        string year = Console.ReadLine();
                        if (int.TryParse(year, out int number))
                        {
                            student.year = number;
                            break;
                        }
                        Console.Write("Вы ввели не число, введите число еще раз: ");
                    }
                    sw.WriteLine(JsonSerializer.Serialize<Student>(student));
                }
                //чтение данных
                using (StreamReader sr = new StreamReader(path))
                {
                    Console.Write("\n\nИнформация из файла:\n");
                    Student restoredStudent = JsonSerializer.Deserialize<Student>(sr.ReadToEnd());
                    Console.WriteLine($"name: {restoredStudent.name}\nsurName: {restoredStudent.surName}");
                    Console.WriteLine($"group: {restoredStudent.group}\nyear: {restoredStudent.year}");
                }
                //если файл создан, удалить его
                Console.Write("\n\nУдалить файл [1-да, 0-нет]:");
                int sigh = int.Parse(Console.ReadLine());
                if (sigh == 1)
                {
                    if (fileJSON.Exists)
                    {
                        fileJSON.Delete();
                        Console.WriteLine($"\n\nФайл по пути {path} удален.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"\n\nФайл по пути {path} не удален.");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        ///  Создает файл XML с названием filename
        /// </summary>
        /// <param name="path"></param>
        static void fileXml(string path)
        {
            Console.WriteLine("[4] Работа с XML\n\n");
            FileInfo fileXML = new FileInfo(path);
            try
            {
                using (FileStream fStream = File.Create(path))
                {
                    Console.WriteLine($"Файл, создан по пути: {path}");
                    //если файл создан, получить информацию о файле
                    if (fileXML.Exists)
                    {
                        Console.WriteLine("Имя файла: {0}", fileXML.Name);
                        Console.WriteLine("Время создания: {0}", fileXML.CreationTime);
                        Console.WriteLine("Размер: {0}", fileXML.Length);
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            XDocument xDoc = new XDocument();
            //создаем элемент - студент
            XElement student = new XElement("student");
            Console.Write("Введите имя студента: ");
            XAttribute nameXAttr = new XAttribute("name", Console.ReadLine());
            Console.Write("Введите фамилию студента: ");
            XAttribute surNameXAttr = new XAttribute("surName", Console.ReadLine());
            Console.Write("Введите группу студента: ");
            XElement groupXElm = new XElement("group", Console.ReadLine());
            Console.Write("Введите год поступления студента: ");
            XElement yearXElm = new XElement("year", Console.ReadLine());
            Console.Write("Введите факультет студента: ");
            XElement facultyXElm = new XElement("faculty", Console.ReadLine());
            //добавим выше введенные данные к student
            student.Add(nameXAttr);
            student.Add(surNameXAttr);
            student.Add(groupXElm);
            student.Add(yearXElm);
            student.Add(facultyXElm);
            //создадим корневой элемент
            XElement students = new XElement("students");
            //добавим в корневой элемент введеннго студента
            students.Add(student);
            // добавляем корневой элемент в документ
            xDoc.Add(students);
            //сохраняем документ
            xDoc.Save(path);

            //загружаем документ
            XDocument xDocLoad = XDocument.Load(path);
            XElement studentXElement = xDocLoad.Element("students").Element("student");
            //получаем информацию из документа
            nameXAttr = studentXElement.Attribute("name");
            surNameXAttr = studentXElement.Attribute("surName");
            groupXElm = studentXElement.Element("group");
            yearXElm = studentXElement.Element("year");
            facultyXElm = studentXElement.Element("faculty");
            //вывод информации на консоль
            Console.WriteLine("\n\nИнформация в файле:\n");
            Console.WriteLine($"Имя и фамилия студента: {nameXAttr.Value} {surNameXAttr.Value}");
            Console.WriteLine($"Группа студента: {groupXElm.Value}");
            Console.WriteLine($"Год поступления студента: {yearXElm.Value}");
            Console.WriteLine($"Факультет студента: {facultyXElm.Value}");
            //если файл создан, удалить его
            Console.Write("\n\nУдалить файл [1-да, 0-нет]:");
            int sigh = int.Parse(Console.ReadLine());
            if (sigh == 1)
            {
                if (fileXML.Exists)
                {
                    fileXML.Delete();
                    Console.WriteLine($"\n\nФайл по пути {path} удален.");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"\n\nФайл по пути {path} не удален.");
            }
        }

        /// <summary>
        /// Создает архив по pathArchive, добавляет файл pathFile в архив и выводит его на консоль 
        /// </summary>
        /// <param name="pathArchive"></param>
        /// <param name="pathFile"></param>
        static void zipArch(string pathArchive, string pathFile)
        {
            Console.WriteLine("[5] Работа с ZIP");
            try
            {
                using (FileStream fStream = File.Create(pathArchive))
                {
                    Console.WriteLine($"Файл, создан по пути: {pathArchive}");
                    FileInfo fileInf = new FileInfo(pathArchive);
                    //если файл создан, получить информацию о файле
                    if (fileInf.Exists)
                    {
                        Console.WriteLine("Имя файла: {0}", fileInf.Name);
                        Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                        Console.WriteLine("Размер: {0}", fileInf.Length);
                        Console.WriteLine();
                    }
                }
                //запись файла в архив
                using (FileStream zipToOpen = new FileStream(pathArchive, FileMode.Open))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                    {
                        ZipArchiveEntry fileText = archive.CreateEntry(pathFile);
                        using (StreamWriter writer = new StreamWriter(fileText.Open()))
                        {
                            Console.Write($"Введите данные в файл для добавления его в архив {pathArchive}: ");
                            writer.WriteLine(Console.ReadLine());
                        }
                    }
                }
                FileInfo fileInfArchive = new FileInfo(pathArchive);
                //если файл создан, получить информацию о файле
                if (fileInfArchive.Exists)
                {
                    Console.WriteLine("Имя файла: {0}", fileInfArchive.Name);
                    Console.WriteLine("Время создания: {0}", fileInfArchive.CreationTime);
                    Console.WriteLine("Размер: {0}", fileInfArchive.Length);
                    Console.WriteLine();
                }
                //разархивация файла в архиве
                using (ZipArchive zip = ZipFile.OpenRead(pathArchive))
                {
                    zip.ExtractToDirectory("./");
                }
                Console.WriteLine("Данные из разархифированного файла: ");
                FileInfo fileInfText = new FileInfo(pathFile);
                //если файл создан, получить информацию о файле
                if (fileInfText.Exists)
                {
                    Console.WriteLine("Имя файла: {0}", fileInfText.Name);
                    Console.WriteLine("Время создания: {0}", fileInfText.CreationTime);
                    Console.WriteLine("Размер: {0}", fileInfText.Length);
                    Console.WriteLine();
                }
                //Открыть поток и прочитать файл
                using (StreamReader sr = new StreamReader(pathFile))
                {
                    Console.Write("\n\nИнформация из файла: ");
                    Console.WriteLine(sr.ReadToEnd());
                }
                //если файл создан, удалить его
                if (fileInfText.Exists)
                {
                    fileInfText.Delete();
                    Console.WriteLine($"Файл {pathFile} удален.");
                    Console.WriteLine();
                }
                //удалить файл из архива
                Console.Write("\n\nУдалить файл [1-да, 0-нет]:");
                int del = int.Parse(Console.ReadLine());
                if (del == 1)
                {
                    using (ZipArchive archive = ZipFile.Open(pathArchive, ZipArchiveMode.Update))
                    {
                        ZipArchiveEntry archiveEntry = archive.Entries[0];
                        archiveEntry.Delete();
                    }
                    Console.WriteLine($"\nФайл {pathFile} в архиве {pathArchive} был удален.");
                }
                else
                {
                    Console.WriteLine($"\nФайл {pathFile} в архиве {pathArchive} не был удален.");
                }

                FileInfo fileInfArchiveEmpty = new FileInfo(pathArchive);
                //если файл создан, получить информацию о файле
                if (fileInfArchiveEmpty.Exists)
                {
                    Console.WriteLine("Имя файла: {0}", fileInfArchiveEmpty.Name);
                    Console.WriteLine("Время создания: {0}", fileInfArchiveEmpty.CreationTime);
                    Console.WriteLine("Размер: {0}", fileInfArchiveEmpty.Length);
                    Console.WriteLine();
                }
                //если файл создан, удалить его
                Console.Write("\n\nУдалить файл [1-да, 0-нет]:");
                del = int.Parse(Console.ReadLine());
                if (del == 1)
                {
                    if (fileInfArchive.Exists)
                    {
                        fileInfArchive.Delete();
                        Console.WriteLine($"\n\nФайл по пути {pathArchive} удален.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"\n\nФайл по пути {pathArchive} не удален.");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Муханов Матвей БББО-05-20 Лаба 1\n\nВыберите необходимый пункт:\n");
                Console.WriteLine("[1] Информация о дисках ");
                Console.WriteLine("[2] Работа с файлами");
                Console.WriteLine("[3] Работа с JSON ");
                Console.WriteLine("[4] Работа с XML");
                Console.WriteLine("[5] Работа с ZIP");
                Console.WriteLine("[6] Выход\n");
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        Console.Clear();
                        diskInfo();
                        Console.WriteLine("\nНажмите Enter для выхода в меню...\n");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        string pathTXT = "text.txt";
                        fileTxt(pathTXT);
                        Console.WriteLine("\nНажмите Enter для выхода в меню...\n");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        string pathJSON = "student.json";
                        fileJson(pathJSON);
                        Console.WriteLine("\nНажмите Enter для выхода в меню...\n");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        string pathXML = "students.xml";
                        fileXml(pathXML);
                        Console.WriteLine("\nНажмите Enter для выхода в меню...\n");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        string pathZIP = "fzip.zip";
                        string pathZIPFile = "fzip.txt";
                        zipArch(pathZIP, pathZIPFile);
                        Console.WriteLine("\nНажмите Enter для выхода в меню...\n");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 6:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Ошибка! Повторите ввод!");
                        Thread.Sleep(1500);
                        Console.Clear();
                        break;
                }
            }
        }
    }
}