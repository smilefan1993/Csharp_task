using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Реализация view для консоли
namespace BooksOOP.Views
{
    public class ConsoleView : IView
    {
        public void Show(string message)
        {
            Console.WriteLine(message);
        }

        public void Show(string[] message)
        {
            foreach(string line in message)
            {
                Console.WriteLine(line);
            }
        }
    }
}
