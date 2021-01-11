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
    /// Interaction logic for GetTestNumber.xaml
    /// </summary>
    public partial class GetTestNumber : Window
    {
        BL.IBL bl;
        BE.Test test;
        public GetTestNumber()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
        }

        private void getNumber(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();
            string id = this.checkTraineeId.Text;
            int newId = int.Parse(id);
            string id2 = this.checkId.Text;
            int newId2 = int.Parse(id2);
            string helpDate = testDate.Text;

            DateTime helptestdate = Convert.ToDateTime(helpDate);

            BE.Trainee isExistTrainee = bl.getTraineeBL(newId);
            if (isExistTrainee != null)
            {
                BE.Tester isExistTester = bl.getTesterBL(newId2);
                if (isExistTester != null)
                {
                    test = bl.getTestByIdsAndDate(newId2, newId, helptestdate);
                    if (test != null)
                    {
                        details.Foreground = (Brush)bc.ConvertFrom("#019EAA");
                        string help = test.TestNumber.ToString();
                        details.Text = "מספר המבחן של התלמיד " + checkTraineeId.Text + "\n:הוא\n" + help;

                    }
                    else
                    {
                        details.Foreground = (Brush)bc.ConvertFrom("Red");
                        details.Text = " לא קיים מבחן עם הפרטים שהוזנו";
                    }
                }
                else
                {
                    details.Foreground = (Brush)bc.ConvertFrom("Red");
                    details.Text = "הת.ז. שהוזן לא נמצא במערכת ";
                }
            }
            else
            {
                details.Foreground = (Brush)bc.ConvertFrom("Red");
                details.Text = "הת.ז. שהוזן לא נמצא במערכת ";
            }

        }
    }
}