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
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for PrintDet.xaml
    /// </summary>
    public partial class PrintDet : Window
    {
        BL.IBL bl;
        Tester tester;
        public PrintDet()
        {
            InitializeComponent();

            tester = new Tester();
            this.checkId.DataContext = tester.Id;

            bl = BL.FactoryBL.GetBL();
        }

        public void printDet(Object sender, EventArgs e)
        {
            var bc = new BrushConverter();
            string id = this.checkId.Text;
            int newid = int.Parse(id);
            Trainee isExistTrainee = bl.getTraineeBL(newid);

            if (isExistTrainee != null)
            {
                this.details.Foreground = (Brush)bc.ConvertFrom("#019EAA");
                string helpTrainee = isExistTrainee.ToString();
                details.Text = helpTrainee;
            }
            else
            {
                Tester isExistTester = bl.getTesterBL(newid);
                if (isExistTester != null)
                {
                    details.Foreground = (Brush)bc.ConvertFrom("#019EAA");
                    string helpTester = isExistTester.ToString();
                    details.Text = helpTester;
                }

                else
                {
                    details.Text = "This Id dosen't exist in the system\n";
                    details.Foreground = Brushes.Red;

                }
            }
        }
    }
}