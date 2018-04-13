using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Класс "макулатуры", при добавлении книги, мы проверяем, достигнут ли лимит по весу, который необходим для нашего хранилища
namespace BooksOOP.Models.BookStorages
{
    public class Wastepaper : BookStorage
    {
        public int RequiredWeight { set; get; }

        private int _currentWeight;

        public Wastepaper(string name, int weight)
            :base(name)
        {
            RequiredWeight = weight;
            _currentWeight = 0;
        }

        public int GetWeight()
        {
            return _currentWeight;
        }

        public override string AddBook(Book book)
        {
            if (RequiredWeight > _currentWeight + book.Weight)
            {
                base.AddBook(book);
                return "Book was added";
            }
            else
            {
                return "Cannot contain more books";
            }
        }
    }
}
