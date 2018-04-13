using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Для демонстрации полиморфизма, разделенные сущности книг идут по жанрам, в данной реализации, детективы имеют переопределенный метод GetDescription
namespace BooksOOP.Models
{
    public class Detective : Book
    {
        public Detective() { }

        public override string GetDescription()
        {
            return $"Detective:{base.GetDescription()}";
        }
    }
}
