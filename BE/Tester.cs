using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Xml.Serialization;


namespace BE
{
    [Serializable]

    public class Tester //: DependencyObject
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender TestersGender { get; set; }
        public int PhoneNumber { get; set; }
        public Address TesterAddress { get; set; }
        public int YearsOfExperience { get; set; }
        public int MaxTestsPerWeek { get; set; }
        public TypeOfVehicle Vehicle { get; set; }
        public TypeOfGearControl Gear { get; set; }

        //public bool[,] WorkingHours
        //{
        //    get { return (bool[,])GetValue(MyPropertyProperty); }
        //    set { SetValue(MyPropertyProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MyPropertyProperty =
        //    DependencyProperty.Register("MyProperty", typeof(bool[,]), typeof(Tester), new PropertyMetadata(0));
        [XmlIgnore]
        public bool[,] WorkingHours = new bool[5, 6];
        public string Help_Matrix
        {
            get//return matrix as string
            {
                string result = "";
                if (WorkingHours != null)//succeded to intiliaze
                {
                    result += WorkingHours[0, 0];//while adding know to convert to string
                    for (int i = 0; i < 5; i++)//rows
                        for (int j = 0; j < 6; j++)//coulmns
                            if (i != 0 || j != 0)//we already have the first place so dont want to add again
                                result += "," + WorkingHours[i, j];

                }
                return result;
            }
            set//set values in matrix from string
            {
                if (value != null && value.Length > 0)//there is string to take values from
                {
                    int index = 0;
                    string[] values = value.Split(',');//divide all string toeach value of matrix 
                    for (int i = 0; i < 5; i++)
                        for (int j = 0; j < 6; j++)
                            WorkingHours[i, j] = bool.Parse(values[index++]);//insert
                }
            }
        }

        public int MaxDistance { get; set; }
        public int Password { get; set; }//password==id
        public int Age { get; set; }
        public bool Isrealicitzian { get; set; }
        RegionInfo RegionCountry { get; set; }//if Isrealicitzian ==false than we ask whats is country
        public Tester()
        {

        }
        public Tester(int id, string lastName, string firstName, string email, DateTime birthday, Gender gender, int phonenumber, Address address, int yearsofexperiance, int maxtestsperweek, TypeOfVehicle vehicle, TypeOfGearControl gear, int maxdistance, bool isrealicitzian, RegionInfo regioncountry, int password = 0)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            BirthDate = birthday;
            TestersGender = gender;
            PhoneNumber = phonenumber;
            TesterAddress = address;
            YearsOfExperience = yearsofexperiance;
            MaxTestsPerWeek = maxtestsperweek;
            Vehicle = vehicle;
            Gear = gear;
            WorkingHours = new bool[5, 6];
            MaxDistance = maxdistance;
            if (password != 0)
                Password = password;
            else
                Password = Id;
            Isrealicitzian = isrealicitzian;
            RegionCountry = regioncountry;
            Age = setAge(BirthDate);
        }

        public Tester(Tester testerToCopy)
        {
            Id = testerToCopy.Id;
            LastName = testerToCopy.LastName;
            FirstName = testerToCopy.FirstName;
            Email = testerToCopy.Email;
            BirthDate = testerToCopy.BirthDate;
            TestersGender = testerToCopy.TestersGender;
            PhoneNumber = testerToCopy.PhoneNumber;
            TesterAddress = testerToCopy.TesterAddress;
            YearsOfExperience = testerToCopy.YearsOfExperience;
            MaxTestsPerWeek = testerToCopy.MaxTestsPerWeek;
            Vehicle = testerToCopy.Vehicle;
            Gear = testerToCopy.Gear;
            WorkingHours = testerToCopy.WorkingHours;
            MaxDistance = testerToCopy.MaxDistance;
            if (testerToCopy.Password != 0)
                Password = testerToCopy.Password;
            else
                Password = testerToCopy.Id;
            Isrealicitzian = testerToCopy.Isrealicitzian;
            RegionCountry = testerToCopy.RegionCountry;
            Age = testerToCopy.Age;
        }

        public override string ToString()//print all the testers details
        {
            return (":פרטי הטסטר\n" + FirstName + " " + LastName +
            "\n:תז\n" + Id +
            "\n:מין\n" + TestersGender.ToString() +
             "\n:תאריך לידה\n" + BirthDate.Year + "/" + BirthDate.Month + "/" + BirthDate.Day +
            "\n:גיל\n" + Age +
            "\n:מספר פלאפון\n" + PhoneNumber +
            "\n:כתובת\n" + TesterAddress.ToString() +
            "\n:תושב ישראל\n" + Isrealicitzian +
            "\n:מספר שנות ניסיון\n" + YearsOfExperience +
            "\n:מספר מקסימלי של טסטים בשבוע\n" + MaxTestsPerWeek +
            "\n:התמחות דרגת הרשיון\n" + Vehicle.ToString() +
            "\n:התמחות בתיבת הילוכים\n" + Gear.ToString() +
            "\n:מרחק מביתך לנקודת ההתחלה של הטסט\n" + MaxDistance +
            "\n:מערכת השעות שלך\n" + printWorkinkhours());
        }

        public string printWorkinkhours()
        {
            string str = "";
            int rowLength = WorkingHours.GetLength(0);
            int colLength = WorkingHours.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    str += string.Format("{0} ", WorkingHours[i, j]);
                }
                str += Environment.NewLine + Environment.NewLine;
            }
            return str;
        }

        public int setAge(DateTime birthDate)
        {
            int age;
            DateTime help = DateTime.Today;
            System.TimeSpan diff1 = help.Subtract(birthDate);
            age = (diff1.Days / 365);
            return age;
        }

        public void setTesterPassword(Tester tester, int newPass, int oldPass = 0)//seting a new password
        {
            if (oldPass == 0)
            {
                Password = Id;
            }
            if (tester.Password == oldPass)
                tester.Password = newPass;
            else
                throw new Exception("סיסמא שגויה\n");
        }

    }
}