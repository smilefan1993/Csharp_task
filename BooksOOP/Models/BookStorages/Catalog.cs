using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;

//Базовый каталог
namespace BooksOOP.Models.BookStorages
{
    public class Catalog : BookStorage
    {
        public Catalog (string name)
            : base(name)
        {
        }
    }
}
