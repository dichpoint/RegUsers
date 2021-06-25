using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace RegUsers
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>

    // Авторизация
    public partial class AuthWindow : Window
    {
        // конструктор
        internal AuthWindow()
        {
            InitializeComponent();
        }

        // обработчик события ("Войти")
        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            // получение полей
            string login = textBoxLogin.Text.Trim().ToLower();
            string pass = passBox.Password.Trim();

            int check = 0;
            // CHECK ALL
            check = CheckLogin(login) ? ++check : check;
            check = CheckPass(pass) ? ++check : check;
            if (check == 2)
            {
                User authUser = null;
                using (AppContext db = new AppContext())
                {
                    authUser = db.Users.Where(b => (b.Login == login) && (b.Pass == pass)).FirstOrDefault(); // проверка логина и пароля в БД
                }
                if (authUser != null)
                {
                    MessageBox.Show("Авторизация прошла успешно");

                    // регистрация прошла успешно, => переходим в авторизацию
                    UserPageWindow userPageWindow = new UserPageWindow();
                    userPageWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует!");
                }
            }
            else
            {
                check = 0;
            }
        }

        // ф-ции для проверки логина и пароля
        private bool CheckLogin(string login)
        {
            // checking LOGIN
            if (login.Length < 5)
            {
                textBoxLogin.ToolTip = "Логин должен содержать не меньше 5 символов!";
                textBoxLogin.Background = Brushes.DarkRed;
                return false;
            }
            else if (Regex.IsMatch(login, "[а-я]"))
            {
                textBoxLogin.ToolTip = "Логин должен состоять из букв латинского алфавита!";
                textBoxLogin.Background = Brushes.DarkRed;
                return false;
            }
            else if (!(Regex.IsMatch(login, "[A-Za-z]")))
            {
                textBoxLogin.ToolTip = "Логин должен содержать хотя бы одну букву!";
                textBoxLogin.Background = Brushes.DarkRed;
                return false;
            }
            else
            {
                textBoxLogin.ToolTip = "";
                textBoxLogin.Background = Brushes.Transparent;
                return true;
            }
        }
        private bool CheckPass(string pass)
        {
            // checking PASS
            if (pass.Length < 7)
            {
                passBox.ToolTip = "Пароль должен содержать не меньше 7 символов!";
                passBox.Background = Brushes.DarkRed;
                return false;
            }
            else if (Regex.IsMatch(pass, "[а-я]"))
            {
                passBox.ToolTip = "Пароль должен состоять из букв латинского алфавита!";
                passBox.Background = Brushes.DarkRed;
                return false;
            }
            else if (!(Regex.IsMatch(pass, "[A-Za-z]")))
            {
                passBox.ToolTip = "Пароль должен содержать хотя бы одну букву!";
                passBox.Background = Brushes.DarkRed;
                return false;
            }
            else if (!(Regex.IsMatch(pass, "[0-9]")))
            {
                passBox.ToolTip = "Пароль должен содержать хотя бы одну цифру!";
                passBox.Background = Brushes.DarkRed;
                return false;
            }
            else
            {
                passBox.ToolTip = "";
                passBox.Background = Brushes.Transparent;
                return true;
            }
        }

        // обработчик события ("Регистрация")
        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}