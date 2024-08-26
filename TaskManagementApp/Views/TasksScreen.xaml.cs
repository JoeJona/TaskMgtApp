using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TaskManagementApp.DbContexts;
using TaskManagementApp.Models;
using Task = TaskManagementApp.Models.Task;

namespace TaskManagementApp.Views
{
    /// <summary>
    /// Interaction logic for TasksScreen.xaml
    /// </summary>
    public partial class TasksScreen : Window
    {

        int uid = 0;
        int taskUpdateId =0;
        
        public TasksScreen(int userId)
        {
            InitializeComponent();

            // Use this for built-in local db
            TaskContext taskContext = new TaskContext("Server = (localdb)\\MSSQLLocalDB; Database = TaskMgtDB; Integrated Security = True;");

            // Use this for Users Installed SQL SERVER
            // UserContext userContext = new UserContext("Server = .\\sqlexpress; Database = TaskMgtDB; Integrated Security = True;");

            uid = userId;

            var tasks = taskContext.Tasks.Where(t => t.UserId == userId).ToList();
            if(tasks.Count != 0)
            {
                foreach(var task in tasks)
                {
                    TasksList.Items.Add(task);
                }
            }


        }

        private void txtAdd_Click(object sender, RoutedEventArgs e)
        {
            // Use this for built-in local db
            TaskContext taskContext = new TaskContext("Server = (localdb)\\MSSQLLocalDB; Database = TaskMgtDB; Integrated Security = True;");


            // Use this for Users Installed SQL SERVER
            // UserContext userContext = new UserContext("Server = .\\sqlexpress; Database = TaskMgtDB; Integrated Security = True;");


            // Add Task
            var title = txtTitle.Text.ToString();
            var description = txtDescription.Text.ToString();
            var dueDate = txtDueDate.Text.ToString();
            var status = txtStatus.Text.ToString();

                Task addTask = new Task()
                {
                    UserId = uid,
                    Title = title,
                    Description = description,
                    Due_Date = dueDate,
                    Status = status,
                };

            if (txtTitle.Text == "" || txtDescription.Text == "" || txtDueDate.Text == "" || txtStatus.Text == "")
            {
                MessageBox.Show("All Fields Are Required!");
            }
            else
            {
                taskContext.Tasks.Add(addTask);
                taskContext.SaveChanges();

                TasksList.Items.Add(addTask);

                txtTitle.Text = "";
                txtDescription.Text = "";
                txtDueDate.Text = "";
                txtStatus.Text = "";
            }

            
        }

        public class ButtonShow
        {
            public bool isUpdated { get; set; } = true;
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            // Use this for built-in local db
            TaskContext taskContext = new TaskContext("Server = (localdb)\\MSSQLLocalDB; Database = TaskMgtDB; Integrated Security = True;");


            // Use this for Users Installed SQL SERVER
            // UserContext userContext = new UserContext("Server = .\\sqlexpress; Database = TaskMgtDB; Integrated Security = True;");


            // Load data to update Task
            var title = txtTitle.Text.ToString();
            var description = txtDescription.Text.ToString();
            var dueDate = txtDueDate.Text.ToString();
            var status = txtStatus.Text.ToString();

            ButtonShow bs = new ButtonShow();
            bs.isUpdated = false;

            int taskId = (TasksList.SelectedItem as Task).Id;
            Task task = taskContext.Tasks.Where(t => t.Id == taskId).FirstOrDefault();

            taskUpdateId = task.Id;

            txtTitle.Text = task.Title;
            txtDescription.Text = task.Description;
            txtDueDate.Text = task.Due_Date;
            txtStatus.Text = task.Status;

        }

        private void txtUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Use this for built-in local db
            TaskContext taskContext = new TaskContext("Server = (localdb)\\MSSQLLocalDB; Database = TaskMgtDB; Integrated Security = True;");


            // Use this for Users Installed SQL SERVER
            // UserContext userContext = new UserContext("Server = .\\sqlexpress; Database = TaskMgtDB; Integrated Security = True;");


            // Add Task
            var title = txtTitle.Text.ToString();
            var description = txtDescription.Text.ToString();
            var dueDate = txtDueDate.Text.ToString();
            var status = txtStatus.Text.ToString();


            if (txtTitle.Text == "" || txtDescription.Text == "" || txtDueDate.Text == "" || txtStatus.Text == "")
            {
                MessageBox.Show("All Fields Are Required!");
            }
            else
            {
                Task task = taskContext.Tasks.Where(t => t.Id == taskUpdateId).FirstOrDefault();

                if (task == null)
                {
                    MessageBox.Show("Update Not Allowed, Task Not Found!");
                }
                else
                {
                    task.Title = title;
                    task.Description = description;
                    task.Due_Date = dueDate;
                    task.Status = status;

                    taskContext.Tasks.AddOrUpdate(task);
                    taskContext.SaveChanges();


                    txtTitle.Text = "";
                    txtDescription.Text = "";
                    txtDueDate.Text = "";
                    txtStatus.Text = "";
                    taskUpdateId = 0;


                    var tasks = taskContext.Tasks.Where(t => t.UserId == uid).ToList();
                    TasksList.Items.Clear();
                    if (tasks.Count != 0)
                    {
                        foreach (var t in tasks)
                        {
                            TasksList.Items.Add(t);
                        }
                    }
                }
            }

        }


        private void txtDelete_Click(object sender, RoutedEventArgs e)
        {
            // Use this for built-in local db
            TaskContext taskContext = new TaskContext("Server = (localdb)\\MSSQLLocalDB; Database = TaskMgtDB; Integrated Security = True;");


            // Use this for Users Installed SQL SERVER
            // UserContext userContext = new UserContext("Server = .\\sqlexpress; Database = TaskMgtDB; Integrated Security = True;");


            // Delete A Task
            int taskId = (TasksList.SelectedItem as Task).Id;
            Task task = taskContext.Tasks.Where(t => t.Id == taskId).FirstOrDefault();
            taskContext.Tasks.Remove(task);
            taskContext.SaveChanges();

            var tasks = taskContext.Tasks.Where(t => t.UserId == uid).ToList();
            if (tasks.Count != 0)
            {
                TasksList.Items.Clear();
                foreach (var t in tasks)
                {
                    TasksList.Items.Add(t);
                }
            }
            else
            {
                TasksList.Items.Clear();
            }


        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            uid = 0;
            new LoginScreen().Show();
            Close();
        }

        private void txtSearch_Click(object sender, RoutedEventArgs e)
        {
            // Use this for built-in local db
            TaskContext taskContext = new TaskContext("Server = (localdb)\\MSSQLLocalDB; Database = TaskMgtDB; Integrated Security = True;");


            // Use this for Users Installed SQL SERVER
            // UserContext userContext = new UserContext("Server = .\\sqlexpress; Database = TaskMgtDB; Integrated Security = True;");


            // Search Task with Title
            var keyword = txtSearchText.Text.ToString();


            if (txtSearchText.Text == "")
            {
                TasksList.Items.Clear();
                var tasks = taskContext.Tasks.Where(t => t.UserId == uid).ToList();
                if (tasks.Count != 0)
                {
                    foreach (var t in tasks)
                    {
                        TasksList.Items.Add(t);
                    }
                }
            }
            else
            {
                TasksList.Items.Clear();
                var tasks = taskContext.Tasks.Where(t => t.UserId == uid && t.Title.Contains(keyword)).ToList();
                if (tasks != null)
                {
                    foreach (var task in tasks)
                    {
                        TasksList.Items.Add(task);
                    }
                }
                txtSearchText.Text = "";
            }
        }


        private void txtFilter_Click(object sender, RoutedEventArgs e)
        {
            // Use this for built-in local db
            TaskContext taskContext = new TaskContext("Server = (localdb)\\MSSQLLocalDB; Database = TaskMgtDB; Integrated Security = True;");


            // Use this for Users Installed SQL SERVER
            // UserContext userContext = new UserContext("Server = .\\sqlexpress; Database = TaskMgtDB; Integrated Security = True;");


            // Filter Tasks with Status
            var keyword = txtFilterText.Text.ToString();


            if (txtFilterText.Text == "")
            {
                TasksList.Items.Clear();
                var tasks = taskContext.Tasks.Where(t => t.UserId == uid).ToList();
                if (tasks.Count != 0)
                {
                    foreach (var t in tasks)
                    {
                        TasksList.Items.Add(t);
                    }
                }
            }
            else
            {
                TasksList.Items.Clear();
                var tasks = taskContext.Tasks.Where(t => t.UserId == uid && t.Status.Contains(keyword)).ToList();
                if (tasks != null)
                {
                    foreach (var task in tasks)
                    {
                        TasksList.Items.Add(task);
                    }
                }
                txtFilterText.Text = "";
            }
        }


    }
}
