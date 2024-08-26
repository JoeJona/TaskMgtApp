using System;
using System.Linq;
using System.Text;
using System.Windows;
using TaskManagementApp.Contexts;
using TaskManagementApp.Models;
using TaskManagementApp.Views;

namespace TaskManagementApp
{
    /// <summary>
    /// Interaction logic for SignUpScreen.xaml
    /// </summary>
    public partial class SignUpScreen : Window
    {
        public SignUpScreen()
        {
            InitializeComponent();
        }

        private void txtSignUp_Click(object sender, RoutedEventArgs e)
        {
            // Use this for built-in local db
            UserContext userContext = new UserContext("Server = (localdb)\\MSSQLLocalDB; Database = TaskMgtDB; Integrated Security = True;");


            // Use this for Users Installed SQL SERVER
            // UserContext userContext = new UserContext("Server = .\\sqlexpress; Database = TaskMgtDB; Integrated Security = True;");


            // User Sign UP

            var name = txtUserName.Text.ToString();
            var encodePassword = Encoding.UTF8.GetBytes(txtPassword.Password.ToString());
            var password = Convert.ToBase64String(encodePassword);

            User userExist = userContext.Users.AsEnumerable().Where(user => user.Name.Contains(name) && user.Password.Equals(password)).FirstOrDefault();

            if (txtUserName.Text == "" || txtPassword.Password == "")
            {
                txtUserName.Text = "";
                txtPassword.Password = "";
                MessageBox.Show("Both Fields Are Required!");
            }else
            {
                if (userExist != null)
                {
                    User user = new User()
                    {
                        Name = name,
                        Password = password,
                    };

                    userContext.Users.Add(user);
                    userContext.SaveChanges();

                    new TasksScreen(userExist.Id).Show();
                    Close();

                }
                else
                {
                    txtUserName.Text = "";
                    txtPassword.Password = "";
                    MessageBox.Show("User Name Already Used, Try Another One!");
                }
            }
            

        }

        private void txtLogin_Click(object sender, RoutedEventArgs e)
        {
            new LoginScreen().Show();
            Close();
        }

        
    }
}
