using BO;
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
            var temp = s_bl?.EngineerInList.ReadAll();
            EngineersList = temp == null ? new() : new(temp!);
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

        public BO.EngineerExperience level { get; set; } = BO.EngineerExperience.All;

        private void ComboBoxEngineerLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = level == BO.EngineerExperience.All
                                ? s_bl?.EngineerInList.ReadAll()
                                : s_bl?.EngineerInList.ReadAll(item => item.Level == (DO.EngineerExperience)level);
            EngineersList = temp == null ? new() : new(temp!);
        }

        private void Button_ClickAddEngineer(object sender, RoutedEventArgs e)
        {
            EngineerWindow engineerWindow = new EngineerWindow();
            engineerWindow.ProductUpdatedAdd += EngineerWindow_ProductUpdatedAdd!;
            engineerWindow.ShowDialog();
        }



        private void OnClickUpdateEngineer(object sender, RoutedEventArgs e)
        {
            BO.EngineerInList? engineerInList = (sender as ListView)?.SelectedItem as BO.EngineerInList;
            EngineerWindow engineerWindow = new EngineerWindow(engineerInList!.Id);
            engineerWindow.ProductUpdatedAdd += EngineerWindow_ProductUpdatedAdd!;
            engineerWindow.ShowDialog();

        }
        private void EngineerWindow_ProductUpdatedAdd(object sender, EventArgs e)
        {
            // Refresh the list of engineers
            var temp = level == BO.EngineerExperience.All
                                ? s_bl?.EngineerInList.ReadAll()
                                : s_bl?.EngineerInList.ReadAll(item => item.Level == (DO.EngineerExperience)level);
            EngineersList = temp == null ? new() : new(temp!);
        }
    }
}
