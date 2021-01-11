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
    /// Interaction logic for DataGridTester.xaml
    /// </summary>
    public partial class DataGridTester : Window
    {
        BL.IBL bl;
        public DataGridTester()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            var help = bl.GetsTestersExperticeBL(true);
            help = help.ToList();
            List<BE.Tester> helpList = new List<BE.Tester>();
            foreach (var element in help)
            {
                foreach (var element1 in element)
                {
                    helpList.Add(element1);
                }
            }
            this.TesterDataGrid.ItemsSource =helpList ;
        }

    }
}