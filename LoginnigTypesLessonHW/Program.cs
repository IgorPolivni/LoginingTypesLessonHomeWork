using LoginnigTypesLessonHW.Data;
using LoginnigTypesLessonHW.Enums;
using LoginnigTypesLessonHW.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LoginnigTypesLessonHW
{
    class Program
    {
        static void Main(string[] args)
        {

            using (CloudContext cloudContext = new CloudContext())
            {
                //Инициализация корневого элемента
                CloudFile root;
                if (cloudContext.Files.ToList().Count == 0)
                {
                    root = CloudFIleService.CreateRootFolder();
                    cloudContext.Add(root);
                    cloudContext.SaveChanges();
                }
                else
                    root = cloudContext.Files.Where(file => file.Parent == Guid.Empty && file.FileName == "Root").ToList().First();

                //Инициализация необходимых переменных
                int choose = new int();
                string[] fileActions = Enum.GetNames(typeof(FilesActions));
                string[] typesFile = Enum.GetNames(typeof(FileTypes));
                List<string> userActions = new List<string>() { "Выбрать файл", "Выбрать действие", "Выполнить действие" };
                CloudFile currentFile = root;
                CloudFile selectedFile = null;
                int selectedAction = new int();


                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("------ВИРТУАЛЬНОЕ ОБЛАЧНОЕ ХРАНИЛИЩЕ-----");
                Console.WriteLine("-----------------------------------------\n");

                while (true)
                {
                    Console.WriteLine("Нажмите Enter для продолжения...");
                    Console.ReadLine();
                    Console.Clear();

                    Console.WriteLine("Возможные действия:\n");
                    MenuService.ShowElements(userActions);

                    try { choose = MenuService.SelectIndex(userActions.Count); }
                    catch (ArgumentOutOfRangeException exception) { Console.WriteLine(exception.ParamName); continue; }

                    switch (choose)
                    {
                        case (int)ProgrammActions.SelectFile:

                            var files = cloudContext.Files.Where(file => file.Parent == currentFile.Id).ToList();
                            int iterator = new int();
                            foreach (var file in files)
                                Console.WriteLine($"{++iterator}. {CloudFIleService.GetFileName(file.FileName)}");


                            try { choose = MenuService.SelectIndex(files.Count); }
                            catch (ArgumentOutOfRangeException exception) { Console.WriteLine(exception.ParamName); continue; }
                            selectedFile = files[choose - 1];
                            break;

                        case (int)ProgrammActions.SelectActionOnFile:

                            MenuService.ShowElements(fileActions);
                            try { choose = MenuService.SelectIndex(fileActions.Length); }
                            catch (ArgumentOutOfRangeException exception) { Console.WriteLine(exception.ParamName); continue; }

                            selectedAction = choose;

                            break;

                        case (int)ProgrammActions.ExecuteAction:

                            switch ((FilesActions)selectedAction)
                            {
                                case FilesActions.EnterIntoFolder:
                                    if (!selectedFile.IsFolder)
                                        Console.WriteLine("В Файл войти нельзя!!!");
                                    else
                                        currentFile = selectedFile;
                                    break;

                                case FilesActions.ExitFromFolder:

                                    if (currentFile.Id != Guid.Empty)
                                    {
                                        var papentFile = cloudContext.Files.Where(file => file.Id == currentFile.Parent).ToList()[0];
                                        currentFile = papentFile;
                                    }

                                    break;

                                case FilesActions.AddFileOrFolder:

                                    MenuService.ShowElements(typesFile);

                                    try { choose = MenuService.SelectIndex(typesFile.Length); }
                                    catch (ArgumentOutOfRangeException exception) { Console.WriteLine(exception.ParamName); continue; }

                                    bool IsFolder;
                                    if (choose == (int)FileTypes.File) IsFolder = false;
                                    else IsFolder = true;

                                    CloudFile NewFile = new CloudFile();

                                    if (IsFolder)
                                    {
                                        string fileName;
                                        Console.Write("Введите имя папки: ");                               
                                        fileName = Console.ReadLine();
                                        NewFile = CloudFIleService.CreatFolder(fileName, currentFile);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Введите полный путь файла: ");
                                        string path = Console.ReadLine();

                                        if (File.Exists(@path))
                                            NewFile = CloudFIleService.CreateFile(path, currentFile);

                                        else
                                        {
                                            Console.WriteLine("Данный файл не найден!");
                                            continue;
                                        }

                                    }

                                    int count = cloudContext.Files
                                        .Where(file => file.Parent == NewFile.Parent && file.FileName == NewFile.FileName)
                                        .ToList().Count;

                                    if (count == 0)
                                    {
                                        //Добавление элемента в таблицу и сохранение 
                                        cloudContext.Add(NewFile);
                                        cloudContext.SaveChanges();
                                    }

                                    break;
                                case FilesActions.DeleteFileOrFolder:
                                    cloudContext.Remove(selectedFile);
                                    cloudContext.SaveChanges();
                                    break;
                            }

                            break;
                    }
                }
            }


        }



    }
}
