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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BL.IBL bl;

        public MainWindow()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
        }
      

        private void goToSite(object sender, EventArgs e)//goes to testdrive.com
        {
            System.Diagnostics.Process.Start("https://testdrivedriving.wixsite.com/testdrive");
        }

        private void tester(object sender, RoutedEventArgs e)
        {
            Window gotester = new tester();
            gotester.Show();

        }

        private void trainee(object sender, RoutedEventArgs e)
        {
            Window gotrainee = new trainee();
            gotrainee.Show();
        }
      
    }
}