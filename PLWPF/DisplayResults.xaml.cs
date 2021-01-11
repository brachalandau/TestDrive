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
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for DisplayResults.xaml
    /// </summary>
    public partial class DisplayResults : Window
    {
        public DisplayResults()
        {
            InitializeComponent();
        }
        public void DisplayResult(Object sender, EventArgs e)
        {

            BL.IBL bl = BL.FactoryBL.GetBL();
            var bc = new BrushConverter();
            string id = this.checkId.Text;
            int newid = int.Parse(id);
            
            BE.Trainee isExistTrainee = bl.getTraineeBL(newid);
            if (isExistTrainee != null)
            {
                BE.Test testOfnumber = bl.getTestByNumber((checkTestNumber.Text));
                if (testOfnumber != null)
                {
                    if (isExistTrainee.Id == testOfnumber.TraineeId)
                    {
                        details.Foreground = (Brush)bc.ConvertFrom("#019EAA");

                        if (testOfnumber.PassedTheTest == true)
                            details.Text = "!מזל טוב עברת את הטסט בהצלחה \nהודעה רישמית תשלח אליך בהקדם\n\nתודה שבחרתם בטסט דרייב\n\n\n";
                        else
                            details.Text = "לצערנו לא עברת את המבחן\nלא נורא פעם הבאה תצליח\n\n\n";

                        string help = ":הערת הבוחן";
                        help += testOfnumber.TesterComment;
                        details.Text += help;

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
    }
}