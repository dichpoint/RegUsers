namespace RegUsers
{
    class User // класс-модель
    {
        internal int id { get; set; }
        private string login, pass, email;

        internal string Login
        {
            get { return login; }
            set { login = value; }
        }
        internal string Pass
        {
            get { return pass; }
            set { pass = value; }
        }
        internal string Email
        {
            get { return email; }
            set { email = value; }
        }

        internal User() { }

        internal User(string login, string pass, string email)
        {
            this.login = login;
            this.pass = pass;
            this.email = email;
        }
    }
}