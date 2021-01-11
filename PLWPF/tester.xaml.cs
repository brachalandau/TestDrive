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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for tester.xaml
    /// </summary>
    public partial class tester : Window
    {
        public tester()
        {
            InitializeComponent();
        }

        private void addTester(object sender, RoutedEventArgs e)
        {
            Window addTesterWindow = new AddNewTester();
            addTesterWindow.Show();
        }

        private void setTester(object sender, RoutedEventArgs e)
        {
            Window checksettester = new CheckIdToSet();
            checksettester.Show();
        }

        private void print(object sender, RoutedEventArgs e)
        {
            Window printdetails = new PrintDet();
            printdetails.Show();
        }

        private void deleteTester(object sender, RoutedEventArgs e)
        {
            Window del = new DeleteTester();
            del.Show();
        }
        private void tests(object sender, RoutedEventArgs e)
        {
            Window test = new TestTester();
            test.Show();
        }

        private void dataGrid(object sender, RoutedEventArgs e)
        {
            Window show = new MainDataGridTester();
            show.Show();
        }
    }
}