using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace RegUsers
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    // Регистрация
    public partial class MainWindow : Window
    {
        AppContext db; // создаем объект контекста

        // конструктор
        public MainWindow()
        {
            InitializeComponent();

            db = new AppContext(); // выделяем память
        }

        // обработчик события ("Зарегистрироваться")
        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim().ToLower();
            string pass = passBox.Password.Trim();
            string pass_2 = passBox_2.Password.Trim();
            string email = textBoxEmail.Text.Trim().ToLower();
            
            int check = 0;
            // CHECK ALL
            check = CheckLogin(login) ? ++check : check;
            check = CheckPass(pass) ? ++check : check;
            check = CheckPass_2(pass, pass_2) ? ++check : check;
            check = CheckEmail(email) ? ++check : check;
            if (check == 4)
            {
                User user = new User(login, pass, email); // создаем ОБЪЕКТ

                // проверка  на существование данного логина в БД
                if (user == db.Users.Where(b => (b.Login == login)).FirstOrDefault())
                {
                    textBoxLogin.ToolTip = "Логин {login} уже занят в системе!";
                    textBoxLogin.Background = Brushes.DarkRed;
                }
                else
                {
                    db.Users.Add(user); // если такого ОБЪЕКТА нет в БД, то мы его добавляем
                    db.SaveChanges(); // сохраняем изменения в БД
                    MessageBox.Show("Регистрация прошла успешно!");
                    AuthWindow authWindow = new AuthWindow();
                    authWindow.Show();
                    Close();

                }
            }
            else
            {
                check = 0;
                //MessageBox.Show("Регистрация прошла НЕ успешно!");
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
        private bool CheckPass_2(string pass, string pass_2)
        {
            // checking PASS_2
            string alph = "ABCDEFGHIJKLMNOPRSTUVWXYZ123456789!\"#$%&'()*+,-./::<=>?@[\\]:_{|}";
            if (pass.IndexOfAny(alph.ToCharArray()) == -1)
            {
                passBox_2.ToolTip = "Пароль должен содержать хотя бы одну цифру и букву (лат. алф.)!";
                passBox_2.Background = Brushes.DarkRed;
                return false;
            }
            else if (pass != pass_2)
            {
                passBox_2.ToolTip = "Пароли должны быть одинаковыми!";
                passBox_2.Background = Brushes.DarkRed;
                return false;
            }
            else
            {
                passBox_2.ToolTip = "";
                passBox_2.Background = Brushes.Transparent;
                return true;
            }
        }
        private bool CheckEmail(string email)
        {
            //checking EMAIL
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            if (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            {
                textBoxEmail.ToolTip = "";
                textBoxEmail.Background = Brushes.Transparent;
                return true;
            }
            else
            {
                textBoxEmail.ToolTip = "Некорректный Email!";
                textBoxEmail.Background = Brushes.DarkRed;
                return false;
            }
        }

        // обработчик события ("Войти")
        private void Button_WinAuth_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }
    }
}