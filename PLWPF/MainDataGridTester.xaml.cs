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
    /// Interaction logic for MainDataGridTester.xaml
    /// </summary>
    public partial class MainDataGridTester : Window
    {
        public MainDataGridTester()
        {
            InitializeComponent();
        }

        private void show1(object sender, RoutedEventArgs e)
        {
            Window show = new DataGridTeacher();
            show.Show();
        }

        private void show2(object sender, RoutedEventArgs e)
        {
            Window show = new DataGridSchool();
            show.Show();
        }

        private void show3(object sender, RoutedEventArgs e)
        {
            Window show = new DataGridNumTests();
            show.Show();
        }
    }
}
