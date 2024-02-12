using BO;
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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
 
        public EngineerWindow(int Id = 0)
        {
            InitializeComponent();
            if (Id == 0)
                CurrentEngineer = new BO.Engineer { Id = 0, Name = "", Email = "", Level = 0, Cost = 0, Task = null };
            else
                CurrentEngineer = s_bl?.Engineer.Read(Id)!;
        }
        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }
        public static readonly DependencyProperty EngineerProperty =
        DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer),
        typeof(EngineerWindow), new PropertyMetadata(null));
        public BO.EngineerExperience level { get; set; } = BO.EngineerExperience.All;

        public event EventHandler ProductUpdatedAdd;
        private void BtnAddOrUpdateEngineer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CurrentEngineer.Task = null;
                string content = (sender as Button)!.Content.ToString()!;//add/update
                if (content == "Add")
                {
                    s_bl.Engineer.Create(CurrentEngineer);//create Bo.Engineer
                    MessageBox.Show($"Engineer with id={CurrentEngineer.Id} added");
                    ProductUpdatedAdd?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    s_bl.Engineer.Update(CurrentEngineer);//update Bo.Engineer
                    MessageBox.Show($"Engineer with id={CurrentEngineer.Id} updated!");
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
}
