using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksOOP.Views;
using BooksOOP.Models;
using System.Reflection;

//Базовый класс контроллера
//Реализация представления вынесена в Action'ы (текст консоли), зато если мы захотим поменять вывод к примеру в файл, нам надо будет только добавить другое View 
namespace BooksOOP.Controllers
{
    public class Controller
    {
        protected IView _view;

        // Т.к мы используем Console Application, мы мапируем роуты для каждого контроллера отдельно, в пределах его класса
        public IList<Route> _routes;

        public Controller(IView view)
        {
            _view = view;
            _routes = new List<Route>();
        }

        //Класс, который производит редирект по контроллерам и Action'm и скрывает создание объектов от самих контроллеров
        public void redirect(string method, string controller, object[] parameters = null)
        {
            try
            {
                var currentNamespace = typeof(Controller);
                Type controllerType = Type.GetType(currentNamespace.Namespace + "." + controller, true, false);
                MethodInfo currentMethod = controllerType.GetMethod(method + "Action");
                if (controllerType != this.GetType())
                {
                    var controllerInstance = Activator.CreateInstance(controllerType);
                    currentMethod.Invoke(controllerInstance, parameters);
                }
                else
                {
                    currentMethod.Invoke(this, parameters);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }    
        }
    }
}
