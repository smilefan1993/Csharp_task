using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Для демонстрации полиморфизма, разделенные сущности книг идут по жанрам, в данной реализации, хорроры имеют переопределенный метод GetDescription
namespace BooksOOP.Models
{
    public class Horror : Book
    {
        public Horror() { }

        public override string GetDescription()
        {
            return $"Horror:{base.GetDescription()}";
        }
    }
}
