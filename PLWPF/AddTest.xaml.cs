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
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddTest.xaml
    /// </summary>
    public partial class AddTest : Window
    {
        BE.Test test;
        BL.IBL bl;

        public AddTest()
        {
            InitializeComponent();

            test = new BE.Test();
            this.testDetails.DataContext = test;

            bl = BL.FactoryBL.GetBL();

            DateTime a = DateTime.Now;
            DateTime b = a;
            b = b.AddMonths(2);
            string time = ":00";
            testTime.Items.Add("09:00");
            for (int i = 10; i < 15; i++)
            {
                testTime.Items.Add(i + time);
            }
            testDate.DisplayDateStart = a;
            testDate.DisplayDateEnd = b;
            this.TypeOfVehicle.ItemsSource = Enum.GetValues(typeof(BE.TypeOfVehicle));
            this.TypeOfGearControl.ItemsSource = Enum.GetValues(typeof(BE.TypeOfGearControl));



        }

        private void endProsses_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime theDate;
                string help = testDate.ToString();
                theDate = DateTime.Parse(help);
                if (theDate.DayOfWeek == DayOfWeek.Friday || theDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    string mess = ":שעות פעילות\n" + "יום א'-ה' בין 9:00-15:00";
                    MessageBox.Show(mess);
                }
                else
                {
                    
                    string id = this.studentIdTextBox.Text;
                    int newid = int.Parse(id);
                    BE.Trainee helpTrainee = new BE.Trainee(bl.getTraineeBL(newid));
                    helpTrainee.Id = newid;
                    if (helpTrainee == null)
                        throw new Exception("הת.ז שהוכנס לא קיים במערכת");
                    test.StartingPoint = this.startingPoint.Text;
                    bl.AddTestBL(helpTrainee, test, test.TestTime);
                    BE.Test helptest = bl.getTestByIdAndDate(helpTrainee.Id, test.TestTime);
                    string testNumber = helptest.TestNumber;
                    string num = testNumber.ToString();
                    string mass = "\n!נרשמת למבחן בהצלחה\n" + ":מספר המבחן שלך הוא\n"
                        + num
                        + "\n!שים לב מספר זה הינו אישי ואינו ניתן להעברה"
                        + "\n*שמור על מספר זה במקום שמור ומוגן,כי רק על ידו תוכלו לקבל את תוצאות המבחן שלכם*\n";
                    mass += "\nתאריך ושעת הטסט\n"
                           + helptest.TestTime.ToString() +
                           "\nנקודת איסוף לטסט\n"
                           + helptest.StartingPoint;
                    test = new BE.Test();
                    this.testDetails.DataContext = test;
                    if (helpTrainee.PhoneNumber == 508472027 || helpTrainee.PhoneNumber == 548141393)
                    {
                        //sending an sms:

                       
                        string toNumber = "+972";
                        toNumber += helpTrainee.PhoneNumber.ToString();

                        const string accountSid = "AC8c78d6b254d5b81d4941b21260d15574";
                        const string authToken = "381f1ce3ceac9c8e3026af2df3ce9c46";

                        TwilioClient.Init(accountSid, authToken);

                        var message = MessageResource.Create(
                            body: mass,
                            from: new Twilio.Types.PhoneNumber("+14158799353"),
                            to: new Twilio.Types.PhoneNumber(toNumber)
                        );
                    }
                    MessageBox.Show(mass);
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