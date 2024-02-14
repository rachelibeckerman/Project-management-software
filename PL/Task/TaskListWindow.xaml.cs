using BlApi;
using BO;
using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
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
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public TaskListWindow()
        {
            InitializeComponent();
            //read all taska in list and initialize TasksList with them
            var temp = s_bl?.TaskInList.ReadAll().OrderBy(task => task!.Id);
            TasksList = temp == null ? new() : new(temp!);

            //create an event to invoke when EngineersList's refreshing is needed
            TaskWindow taskWindow = new TaskWindow();
            taskWindow.ProductUpdatedAdd += TaskWindow_ProductUpdatedAdd!;
        }

        public ObservableCollection<BO.TaskInList> TasksList
        {
            get { return (ObservableCollection<BO.TaskInList>)GetValue(TasksListProperty); }
            set { SetValue(TasksListProperty, value); }
        }

        public static readonly DependencyProperty TasksListProperty =
        DependencyProperty.Register("TasksList", typeof(ObservableCollection<BO.TaskInList>),
        typeof(TaskListWindow), new PropertyMetadata(null));

        /// <summary>
        ///enum engineer experience to Complexity Level
        /// </summary>
        public BO.EngineerExperience ComplexityLevel { get; set; } = BO.EngineerExperience.All;

        /// <summary>
        ///  selection changed to task's copmlexity level combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxTaskCopmlexity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = ComplexityLevel == BO.EngineerExperience.All
                                ? s_bl?.TaskInList.ReadAll().OrderBy(task => task!.Id)
                                : s_bl?.TaskInList.ReadAll(item => item.ComplexityLevel == (DO.EngineerExperience)ComplexityLevel).OrderBy(task => task!.Id);
            TasksList = temp == null ? new() : new(temp!);
        }

        /// <summary>
        /// button click to add task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddTask(object sender, RoutedEventArgs e)
        {
            TaskWindow taskWindow = new TaskWindow();
            taskWindow.ProductUpdatedAdd += TaskWindow_ProductUpdatedAdd!;//register the function to recieve the actin of refreshing the TasksList 
            taskWindow.ShowDialog();//open new add task window
        }


        /// <summary>
        /// on click to update a task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickUpdateTask(object sender, RoutedEventArgs e)
        {
            BO.TaskInList? taskInList = (sender as ListView)?.SelectedItem as BO.TaskInList;
            TaskWindow taskWindow = new TaskWindow(taskInList!.Id);
            taskWindow.ProductUpdatedAdd += TaskWindow_ProductUpdatedAdd!;//register the function to recieve the actin of refreshing the TasksList 
            taskWindow.ShowDialog();//open new update task window
        }

        /// <summary>
        /// function to refresh TasksList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskWindow_ProductUpdatedAdd(object sender, EventArgs e)
        {
            // Refresh the list of engineers
            var temp = ComplexityLevel == BO.EngineerExperience.All
                              ? s_bl?.TaskInList.ReadAll().OrderBy(task => task!.Id)
                              : s_bl?.TaskInList.ReadAll(item => item.ComplexityLevel == (DO.EngineerExperience)ComplexityLevel).OrderBy(task => task!.Id);
            TasksList = temp == null ? new() : new(temp!);
        }

   
    }
}
