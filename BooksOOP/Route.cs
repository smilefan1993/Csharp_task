using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Базовый класс для роутинга контроллеров
//В используется как контейнеры для отдельных роутов
namespace BooksOOP
{
    public class Route
    {
        public string Id { get; }
        public string Controller { get; }
        public string Action { get; }

        public Route(string id, string controller, string action)
        {
            Id = id;
            Controller = controller;
            Action = action;
        }
    }
}
