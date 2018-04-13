using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Базовый интерфейс для сущности хранилищ. Реализуется в 3-х сущностях: Каталог, подставка для стола, макулатура и продажи (в последнем случае, мы храним проданные книги
// и обзщую сумму выручки.
namespace BooksOOP.Models
{
    public interface IBookStorage
    {
        string Name { get;  }

        string AddBook(Book book);

        void DeleteBook(Book book);

        Book GetByName(string name);

        ICollection<Book> GetBooks();
    }
}
