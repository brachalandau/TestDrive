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
    /// Interaction logic for TestTester.xaml
    /// </summary>
    public partial class TestTester : Window
    {
        public TestTester()
        {
            InitializeComponent();
        }

        private void testResult(object sender, RoutedEventArgs e)
        {
            Window result = new DisplayResultsForTester();
            result.Show();
        }

        private void getNumber(object sender, RoutedEventArgs e)
        {
            Window number = new GetTestNumber();
            number.Show();
        }

        private void setResult(object sender, RoutedEventArgs e)
        {
            Window result = new SetTestResult();
            result.Show();
        }
    }
}