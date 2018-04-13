using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Промежуточный класс интерфейса IBookStorage, служит для реализации базового функционала для всех хранилищ
namespace BooksOOP.Models
{
    public class BookStorage : IBookStorage
    {
        public string Name { get;}

        protected List<Book> _books;

        public BookStorage(string name)
        {
            _books = new List<Book>();
            Name = name;
        }

        public virtual string AddBook(Book book)
        {
            _books.Add(book);
            return $"Book {book.Name} was added";
        }

        public void DeleteBook(Book book)
        {
            _books.Remove(book);
        }

        public Book GetByName(string name)
        {
            return _books.FirstOrDefault(b => b.Name == name);
        }

        public ICollection<Book> GetBooks()
        {
            return _books;
        }
    }
}
