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
            var temp = s_bl?.Engineer.ReadAll();
            EngineersList = temp == null ? new() : new(temp!);
        }
        public ObservableCollection<BO.Engineer> EngineersList
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EngineersListProperty); }
            set { SetValue(EngineersListProperty, value); }
        }
        public static readonly DependencyProperty EngineersListProperty =
        DependencyProperty.Register("EngineersList", typeof(ObservableCollection<BO.Engineer>),
        typeof(EngineerListWindow), new PropertyMetadata(null));

        public BO.EngineerExperience level { get; set; } = BO.EngineerExperience.All;

        private void ComboBoxEngineerLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = level == BO.EngineerExperience.All
                                ? s_bl?.Engineer.ReadAll()
                                : s_bl?.Engineer.ReadAll(item => item.Level == (DO.EngineerExperience)level);
            EngineersList = temp == null ? new() : new(temp!);
        }
    }
}
