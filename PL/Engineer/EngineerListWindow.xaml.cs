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
            EngineersList = temp == null ? new() : new(temp);
        }
        public ObservableCollection<BO.Engineer> EngineersList
        {
            get { return (ObservableCollection<BO.Engineer>)GetValue(EngineersListProperty); }
            set { SetValue(EngineersListProperty, value); }
        }
        public static readonly DependencyProperty EngineersListProperty =
        DependencyProperty.Register("EngineersList", typeof(ObservableCollection<BO.Engineer>),
        typeof(EngineerListWindow), new PropertyMetadata(null));
    }
}
