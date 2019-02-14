using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarManager
{
    class Screen
    {                 // класс, выводящий все папки и файлы.
        public static string Path { get; set; }

        public FileSystemInfo[] Content { get; set; }

        int curr;
        public int CurrentItem
        {             // индекс выбранной папки или файла.
            get
            {
                return curr;
            }
            set
            {
                if (value >= 0 && value < Content.Length)
                { // ограничения для выбранного файла: индекс >= 0 и < количества элементов
                    curr = value;
                }
                else if (value >= Content.Length)
                {
                    curr = Content.Length - 1;
                }
                else
                {
                    curr = 0;
                }
            }
        }

        public enum FMode
        { // мод для открытия, удаления и переименовывания
            Directory, // мод для папки
            File //мод для файл
        }

        public FMode Mode { get; set; }

        public Screen(string path)
        { //принимает путь для необходимой папки, элементы которой нужно отобразить
            Path = path;
            DirectoryInfo dir = new DirectoryInfo(Path);
            Content = dir.GetFileSystemInfos();
            CurrentItem = 0; // устанавливает выбранный элемент на начальнуй (нулевой) 
            Mode = FMode.Directory; // по умолчанию FMode = Directory
        }

        public void Display()
        { // выводит в консоль всех папок и файлов
            Console.Clear(); // очищает консоли от предыдущих папок и файлов
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Arrow Up - next | Arrow down - previous | Enter - Open | Backspace - Back | R - Rename | Esc - Exit"); // выводит команды в консоль для удобства юзера
            Mode = FMode.Directory;
            for (int i = 0; i < Content.Length; i++)
            {
                if (i == CurrentItem)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed; // выделяет цветом, если индекс соответсвует выбранному
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(i + 1 + ". " + Content[i].Name);
            }
        }
    }

    class Program
    {
        static void OpenItem(FileSystemInfo item, Stack<Screen> screens)
        { // эта функция отвечает за открытие папок и файлов 
            if (item.GetType() == typeof(DirectoryInfo))
            { // при открытии (если) применяет методы для DirectoryInfo
                screens.Push(new Screen(item.FullName)); // добавляем в историю открытую папку
                screens.Peek().Display();
            }
            else
            {
                FileInfo file = new FileInfo(item.FullName);
                screens.Peek().Mode = Screen.FMode.File; // если был открыт файл - изменить Mode на File
                Console.Clear();
                Console.WriteLine("Arrow Up - next | Arrow down - previous | Enter - Open | Backspace - Back | R - Rename | Esc - Exit");
                using (FileStream fs = file.Open(FileMode.Open, FileAccess.ReadWrite))
                { // чтение содержимого открытого файла

                    using (StreamReader sr = new StreamReader(fs)) //используем для вывода
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear();
                        Console.WriteLine(sr.ReadToEnd());
                    }
                    
                }



            }
        }

        static void BackTo(Stack<Screen> history)
        { // функция реализующая назад в историю
            if (history.Peek().Mode == Screen.FMode.Directory)
            { // если открыта папка, то удалить текущую папку из истории и открыть предыдущую
                history.Pop();
                history.Peek().Display();
            }
            else
            {
                history.Peek().Display(); // если файл, то открыть родительскую папку
            }
        }

        static void DeleteItem(FileSystemInfo item, Stack<Screen> screens)
        { // реализция удаления папки
            if (item.GetType() == typeof(DirectoryInfo))
            { // если папка, то использовать методы для DirectoryInfo
                DirectoryInfo dir = new DirectoryInfo(item.FullName);
                dir.Delete(true); // удалить папку
                screens.Pop(); // обновить папку в истории(удалить предыдущую запись из истории и вставить новую)
                screens.Push(new Screen(dir.Parent.FullName) { CurrentItem = screens.Count > 0 ? screens.Peek().CurrentItem-- : 0 });
            }
            else
            {
                FileInfo file = new FileInfo(item.FullName);
                file.Delete(); // удалить файл
                screens.Pop(); // обновить файл в истории(удалить предыдущу запись из истории и вставить новую)
                screens.Push(new Screen(file.DirectoryName));
            }
            screens.Peek().Display();
        }

        static void RenameItem(FileSystemInfo item, Stack<Screen> screens)
        { // реализация переименовывания
            Console.Clear();
            Console.WriteLine("Write new name (Just name, not a path)");
            string newName = Console.ReadLine(); // ввод нового имени
            if (item.GetType() == typeof(DirectoryInfo))
            {
                DirectoryInfo dir = new DirectoryInfo(item.FullName);
                newName = newName.Insert(0, dir.Parent.FullName + "\\"); // добавить абсолютный путь к введенному имени
                dir.MoveTo(newName); // переименовывание реализовано как перемещение папки, изменяя имя
                screens.Pop(); // обновление файла
                screens.Push(new Screen(dir.Parent.FullName)); // обновление файла
            }
            else
            {
                FileInfo file = new FileInfo(item.FullName);
                newName = newName.Insert(0, file.DirectoryName + "\\"); // добавить абсолютный путь к введенному имени
                file.MoveTo(newName);
                screens.Pop(); // обновление файла
                screens.Push(new Screen(file.DirectoryName)); // Обновление файла
            }
            screens.Peek().Display();
        }

        static void Main(string[] args)
        {
            Stack<Screen> screens = new Stack<Screen>();
            screens.Push(new Screen(@"C:\Users\Дина\Desktop\ex")); // создание первого экрана - вывод всех папок и файлов
            screens.Peek().Display();
            ConsoleKeyInfo KeyPress; // переменная хранит имя нажатой клавиши
            do
            { //ждет нажатия клавиши, пока esc не нажата
                KeyPress = Console.ReadKey();
                switch (KeyPress.Key)
                {
                    case ConsoleKey.DownArrow: // отслеживание нажатия "вниз"
                        if (screens.Peek().Mode == Screen.FMode.Directory)
                        {
                            screens.Peek().CurrentItem++;
                            screens.Peek().Display();
                        }
                        break;
                    case ConsoleKey.UpArrow: // "вверх"
                        if (screens.Peek().Mode == Screen.FMode.Directory)
                        {
                            screens.Peek().CurrentItem--;
                            screens.Peek().Display();
                        }
                        break;
                    case ConsoleKey.Enter: // "Enter"
                        OpenItem(screens.Peek().Content[screens.Peek().CurrentItem], screens);
                        break;
                    case ConsoleKey.Backspace: // "BackSpace"
                        BackTo(screens);
                        break;
                    case ConsoleKey.Delete: // "Del"
                        DeleteItem(screens.Peek().Content[screens.Peek().CurrentItem], screens);
                        break;
                    case ConsoleKey.R: // "R"
                        RenameItem(screens.Peek().Content[screens.Peek().CurrentItem], screens);
                        break;
                }
            }
            while (KeyPress.Key != ConsoleKey.Escape); // если нажата esc - выход из консоли
        }
    }
}
