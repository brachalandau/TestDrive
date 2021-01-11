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
    /// Interaction logic for DataGridNumTests.xaml
    /// </summary>
    public partial class DataGridNumTests : Window
    {
        BL.IBL bl;
        public DataGridNumTests()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            var help = bl.GroupsByAmountOfTestTakenBL(true);
            help = help.ToList();
            List<BE.Trainee> helpList = new List<BE.Trainee>();
            foreach (var element in help)
            {
                foreach (var element1 in element)
                {
                    helpList.Add(element1);
                }
            }           
            this.NumTestDataGrid.ItemsSource =helpList;
        }
    }
}
