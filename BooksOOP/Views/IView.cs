using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Базовый интерфейс для view
namespace BooksOOP.Views
{
    public interface IView
    {
        void Show(string message);

        void Show(string[] message);
    }
}
