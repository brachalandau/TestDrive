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
using System.Web;
using System.Net.Mail;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for SetTestResult.xaml
    /// </summary>
    public partial class SetTestResult : Window
    {
        BE.Test test;
        BE.Trainee trainee;
        BL.IBL bl;
        public SetTestResult()
        {
            InitializeComponent();
            test = new BE.Test();
            trainee = new BE.Trainee();

            this.result.DataContext = test;
            bl = BL.FactoryBL.GetBL();

        }

        private void setTestResult(object sender, RoutedEventArgs e)
        {
            try
            {
                string number = this.setNumber.Text;
                string newNumber = (number);

                BE.Test isExistTest = bl.getTestByNumber(newNumber);
                if (isExistTest != null)
                {
                    if (this.true1.IsChecked == true)
                        isExistTest.CarControl = true;
                    else
                        isExistTest.CarControl = false;
                    if (this.true2.IsChecked == true)
                        isExistTest.GeneralControl = true;
                    else
                        isExistTest.CarControl = false;
                    if (this.true3.IsChecked == true)
                        isExistTest.Signals = true;
                    else
                        isExistTest.Signals = false;
                    if (this.true4.IsChecked == true)
                        isExistTest.LaneSwitching = true;
                    else
                        isExistTest.LaneSwitching = false;
                    if (this.true5.IsChecked == true)
                        isExistTest.Mirrors = true;
                    else
                        isExistTest.Mirrors = false;
                    if (this.true6.IsChecked == true)
                        isExistTest.TrafficSigns = true;
                    else
                        isExistTest.TrafficSigns = false;
                    if (this.true7.IsChecked == true)
                        isExistTest.CrosswalkStopping = true;
                    else
                        isExistTest.CrosswalkStopping = false;
                    if (this.true8.IsChecked == true)
                        isExistTest.StopLine = true;
                    else
                        isExistTest.StopLine = false;
                    if (this.true9.IsChecked == true)
                        isExistTest.TrafficLight = true;
                    else
                        test.TrafficLight = false;
                    if (isExistTest.Gear == BE.TypeOfGearControl.ידני)
                    {
                        if (this.true10.IsChecked == true)
                            isExistTest.GearControl = true;
                        else
                            isExistTest.GearControl = false;
                        if (this.true11.IsChecked == true)
                            isExistTest.ClutchControl = true;
                        else
                            isExistTest.ClutchControl = false;
                    }
                    else
                    {
                        isExistTest.GearControl = false;
                        isExistTest.ClutchControl = false;
                    }
                    if (this.true12.IsChecked == true)
                        isExistTest.CurrectEntering = true;
                    else
                        isExistTest.CurrectEntering = false;
                    if (this.true13.IsChecked == true)
                        isExistTest.CurrectExiting = true;
                    else
                        isExistTest.CurrectExiting = false;
                    if (this.true14.IsChecked == true)
                        isExistTest.RightOfway = true;
                    else
                        isExistTest.RightOfway = false;
                    if (this.true15.IsChecked == true)
                        isExistTest.DidTesterBreak = true;
                    else
                        isExistTest.DidTesterBreak = false;
                    if (this.true16.IsChecked == true)
                        isExistTest.DidTesterTouchStearingWheel = true;
                    else
                        isExistTest.DidTesterTouchStearingWheel = false;
                    if (this.true17.IsChecked == true)
                        isExistTest.Parking = true;
                    else
                        isExistTest.Parking = false;
                    if (this.true18.IsChecked == true)
                        isExistTest.CheckingSurrounding = true;
                    else
                        isExistTest.CheckingSurrounding = false;
                    isExistTest.TesterComment = this.details.Text;
                    bl.SetTestResultBL(isExistTest);
                    string helpmassege;
                    if (isExistTest.PassedTheTest == true)
                        helpmassege = "עבר";
                    else
                        helpmassege = "לא עבר";
                    MessageBoxButton buttons = MessageBoxButton.YesNo;
                    MessageBoxResult result = MessageBox.Show(" האם אתה בטוח שתלמיד" + " "+isExistTest.TraineeId+ " " + helpmassege+" ", "אישור סופי", buttons);
                    if (result == MessageBoxResult.Yes)
                    {
                        string message = "";

                        if (isExistTest.PassedTheTest == true)
                        {
                            BE.Trainee temp = new BE.Trainee(bl.getTraineeBL(isExistTest.TraineeId));
                            bl.CommentInBirthDay(temp,isExistTest);
                            message = "!מזל טוב עברת את הטסט בהצלחה\n\nתודה שבחרתם בטסט דרייב\n\n";
                            message += "טופס רשיון לחצי שנה בדרך לבית שלך\n";
                        }
                        else//test.PassedTheTest == false
                        {
                            message = "לצערנו לא עברת את המבחן\nלא נורא פעם הבאה תצליח\n\n\n";
                        }
                        string help = "\n:הערת הבוחן\n";
                        help += isExistTest.TesterComment;
                        message += help;
                        trainee = bl.getTraineeBL(isExistTest.TraineeId);
                        if (trainee.Email != null&&trainee.Email!="")
                        {
                            MailMessage mail = new MailMessage("testdrivedrivingtest@gmail.com", trainee.Email, "טסט דרייב-תוצאות הטסט", message);
                            SmtpClient client = new SmtpClient("smtp.gmail.com");
                            client.Port = 587;
                            client.Credentials = new System.Net.NetworkCredential("testdrivedrivingtest", "nhbh pruhey");
                            client.EnableSsl = true;
                            client.Send(mail);
                            MessageBox.Show("", "מייל נשלח ומעדכן את הנבחן ", MessageBoxButton.OK);
                        }
                        else
                        {
                            throw new Exception(" תלמיד" +" "+ isExistTest.TraineeId + " עבר את המבחן אנא דאג שיתעדכן");
                        }
                    }
                    else
                    {
                        this.Close();
                        Window gosetresult = new SetTestResult();
                        gosetresult.Show();
                    }
                }

                else
                {
                    throw new Exception("מספר המבחן שהוזן אינו קיים במערכת");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
            this.Close();
        }
    }

}