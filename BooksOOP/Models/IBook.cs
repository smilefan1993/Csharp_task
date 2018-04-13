using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Базовый интерфейс для сущности книг
namespace BooksOOP.Models
{
    public interface IBook
    {
        string Name { get; set; }

        int Pages { get; set; }

        int Weight { get; set; }

        string Text { get; set; }

        int Price { get; set; }

        string Author { get; set; }

        string Binding { get; set; }

        string GetDescription();

        string SearchInfo(string searchData);
    }
}
