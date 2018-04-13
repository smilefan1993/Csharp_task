using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksOOP.Controllers;
using BooksOOP.Views;
using BooksOOP.Models;

namespace BooksOOP
{
    static class Application
    {
        static public void run()
        {
            Controller controller = new Controller(new ConsoleView());
            controller.redirect("Index", "MainController"); 
        }
    }
}
