using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Промежуточный класс, который определяет свойства и Description для последующих наследников сущности книг
namespace BooksOOP.Models
{
    public class Book : IBook
    {
        public string Name { get; set; }

        public int Pages { get; set; }

        public int Weight { get; set; }

        public string Text { get; set; }

        public int Price { get; set; }

        public string Author { get; set; }

        public string Binding { get; set; }

        public virtual string GetDescription()
        {
            return $"{Author}-{Name}";
        }

        public string SearchInfo(string searchedData)
        {
            if (Text.Contains(searchedData))
                return $"Text contain {searchedData}";
            else
                return $"Text doesn't contain {searchedData}";
        }
    }
}
