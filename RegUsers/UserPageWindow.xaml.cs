using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RegUsers
{
    /// <summary>
    /// Логика взаимодействия для UserPageWindow.xaml
    /// </summary>
    
    // Кабинет пользователя
    public partial class UserPageWindow : Window
    {
        // конструктор
        internal UserPageWindow()
        {
            InitializeComponent();

            AppContext db = new AppContext();
            List<User> users = db.Users.ToList();
            listOfUsers.ItemsSource = users;
        }
    }
}