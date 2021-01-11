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
using System.Globalization;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for trainee.xaml
    /// </summary>
    public partial class trainee : Window
    {
        public trainee()
        {
            InitializeComponent();
        }

        private void addTrainee(object sender, RoutedEventArgs e)
        {
            Window addTraineeWindow = new AddNewTrainee();
            addTraineeWindow.Show();
        }
        
        private void print(object sender, RoutedEventArgs e)
        {
            Window printdetails = new PrintDet();
            printdetails.Show();
        }

        private void deleteTrainee(object sender, RoutedEventArgs e)
        {
            Window del = new DeleteTrainee();
            del.Show();
        }

        private void setTrainee(object sender, RoutedEventArgs e)
        {
            Window checksettrainee = new CheckIdToSet();
            checksettrainee.Show();
        }

        private void tests(object sender, RoutedEventArgs e)
        {
            Window test = new TestTrainee();
            test.Show();
        }

        private void dataGrid(object sender, RoutedEventArgs e)
        {
            Window datagrid = new DataGridTester();
            datagrid.Show();
        }
    }
}