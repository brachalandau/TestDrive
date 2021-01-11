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
    /// Interaction logic for DisplayResultsForTester.xaml
    /// </summary>
    public partial class DisplayResultsForTester : Window
    {
        public DisplayResultsForTester()
        {
            InitializeComponent();
        }
        public void DisplayResult(Object sender, EventArgs e)
        {

            BL.IBL bl = BL.FactoryBL.GetBL();
            var bc = new BrushConverter();
            string id = this.checkId.Text;
            int newid = int.Parse(id);

            BE.Tester isExistTester = bl.getTesterBL(newid);
            if (isExistTester != null)
            {
                BE.Test testOfnumber = bl.getTestByNumber((checkTestNumber.Text));
                if (testOfnumber != null)
                {
                    if (isExistTester.Id == testOfnumber.TesterId)
                    {
                        details.Foreground = (Brush)bc.ConvertFrom("#019EAA");

                        if (testOfnumber.PassedTheTest == true)
                            details.Text = ".הנבחן עבר את הטסט בהצלחה\n\n\n";
                        else
                            details.Text = ".הנבחן נכשל בטסט\n\n\n";

                        string help = ":ההערה שלך\n";
                        help += testOfnumber.TesterComment;
                        details.Text += help;
                        string help1 = "\n\n:פירוט תוצאות המבחן\n";
                        help1 += testOfnumber.TestReport();
                        details.Text += help1;

                    }
                    else
                    {
                        details.Foreground = (Brush)bc.ConvertFrom("Red");
                        details.Text = "הת.ז. לא מתאים למספר מבחן שהוזן ";
                    }
                }
                else
                {
                    details.Foreground = (Brush)bc.ConvertFrom("Red");
                    details.Text = "המספר מבחן שהוזן לא נמצא במערכת";
                }
            }
            else
            {
                details.Foreground = (Brush)bc.ConvertFrom("Red");
                details.Text = "הת.ז. שהוזן לא נמצא במערכת ";
            }

        }

        private void updateResults(object sender, RoutedEventArgs e)
        {
            Window update = new SetTestResult();
            update.Show();
        }
    }
}