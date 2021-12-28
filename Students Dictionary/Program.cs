using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsDictionary
{
    public class Program
    {
        
        /// Главный метод программы. Создаем Форму для Карточки Студентов.
       
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.StudentForm());
        }
    }
}
