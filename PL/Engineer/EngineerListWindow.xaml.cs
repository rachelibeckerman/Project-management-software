using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public EngineerListWindow()
        {
            InitializeComponent();
            //read all engineer in list and initialize EngineersList with them
            var temp = s_bl?.EngineerInList.ReadAll().OrderBy(engineer => engineer!.Id );
            EngineersList = temp == null ? new() : new(temp!);

            //create an event to invoke when EngineersList's refreshing is needed
            EngineerWindow engineerWindow = new EngineerWindow();
            engineerWindow.ProductUpdatedAdd += EngineerWindow_ProductUpdatedAdd!;
        }
        public ObservableCollection<BO.EngineerInList> EngineersList
        {
            get { return (ObservableCollection<BO.EngineerInList>)GetValue(EngineersListProperty); }
            set { SetValue(EngineersListProperty, value); }
        }
        public static readonly DependencyProperty EngineersListProperty =
        DependencyProperty.Register("EngineersList", typeof(ObservableCollection<BO.EngineerInList>),
        typeof(EngineerListWindow), new PropertyMetadata(null));

        //enum engineer experience
        public BO.EngineerExperience level { get; set; } = BO.EngineerExperience.All;


        /// <summary>
        ///  selection changed to engineer's level combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxEngineerLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = level == BO.EngineerExperience.All
                                ? s_bl?.EngineerInList.ReadAll().OrderBy(engineer => engineer!.Id)//if engineer experience is all,read all
                                : s_bl?.EngineerInList.ReadAll(item => item.Level == (DO.EngineerExperience)level).OrderBy(engineer => engineer!.Id);//returns all engineers with wanted engineer experience
            EngineersList = temp == null ? new() : new(temp!);
        }

        /// <summary>
        /// button click to add engineer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickAddEngineer(object sender, RoutedEventArgs e)
        {
            EngineerWindow engineerWindow = new EngineerWindow();
            engineerWindow.ProductUpdatedAdd += EngineerWindow_ProductUpdatedAdd!;//register the function to recieve the actin of refreshing the EngineersList 
            engineerWindow.ShowDialog();//open a new add engineer window
        }


        /// <summary>
        /// on click to update engineer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickUpdateEngineer(object sender, RoutedEventArgs e)
        {
            BO.EngineerInList? engineerInList = (sender as ListView)?.SelectedItem as BO.EngineerInList;
            EngineerWindow engineerWindow = new EngineerWindow(engineerInList!.Id);
            engineerWindow.ProductUpdatedAdd += EngineerWindow_ProductUpdatedAdd!;//register the function to recieve the actin of refreshing the EngineersList 
            engineerWindow.ShowDialog();//open a new update window

        }

        /// <summary>
        /// function to refresh EngineersList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EngineerWindow_ProductUpdatedAdd(object sender, EventArgs e)
        {
            // Refresh the list of engineers
            var temp = level == BO.EngineerExperience.All
                                ? s_bl?.EngineerInList.ReadAll().OrderBy(engineer => engineer!.Id)
                                : s_bl?.EngineerInList.ReadAll(item => item.Level == (DO.EngineerExperience)level).OrderBy(engineer => engineer!.Id);
            EngineersList = temp == null ? new() : new(temp!);
        }
    }
}
