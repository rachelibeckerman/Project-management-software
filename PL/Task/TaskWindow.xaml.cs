using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public TaskWindow(int Id = 0)
        {
            InitializeComponent();
            if (Id == 0)
                CurrentTask = new BO.Task
                {
                    Id = 0,
                    Description = "",
                    Alias = "",
                    CreatedAt = DateTime.Now,
                    Status = 0,
                    Start = null,
                    ScheduledDate = DateTime.MinValue,
                    ForecastDate = DateTime.MinValue,
                    Deadline = DateTime.MinValue,
                    Complete = DateTime.MinValue,
                    Deliverables = "",
                    Remarks = "",
                    Engineer = new BO.EngineerInTask() { Id = 0, Name = "" },
                    ComplexityLevel = 0,
                    Dependencies = null,
                    Milestone = null
                };
            else
                CurrentTask = s_bl?.Task.Read(Id)!;
        }

        public BO.Task CurrentTask
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        public static readonly DependencyProperty TaskProperty =
        DependencyProperty.Register("CurrentTask", typeof(BO.Task),
        typeof(TaskWindow), new PropertyMetadata(null));

        public BO.EngineerExperience ComplexityLevel { get; set; } = BO.EngineerExperience.All;

        public event EventHandler ProductUpdatedAdd;

        private void BtnAddOrUpdateTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string content = (sender as Button)!.Content.ToString()!;//add/update
                if (content == "Add")
                {
                    s_bl.Task.Create(CurrentTask);//create Bo.Engineer
                    MessageBox.Show($"Task added");
                    ProductUpdatedAdd?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    s_bl.Task.Update(CurrentTask);//update Bo.Engineer
                    MessageBox.Show($"Task updated!");
                    ProductUpdatedAdd?.Invoke(this, EventArgs.Empty);
                }
                this.Close();//close add/update windows 



            }
            catch (BO.BlInvalidInputException ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (BO.BlAlreadyExistException ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (BO.BlNullPropertyException ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch { MessageBox.Show("Oops! something went wrong"); }
        }
     
    }
};
            
     



