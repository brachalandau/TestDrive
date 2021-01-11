using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.ComponentModel;//בשביל הדפנסי פרופרטי

namespace BE
{
    [Serializable]
    public class Trainee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender TraineeGender { get; set; }
        public int PhoneNumber { get; set; }
        public Address StudentAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public TypeOfVehicle Vehicle { get; set; }
        public TypeOfGearControl Gear { get; set; }
        public string DrivingSchoolName { get; set; }
        public string TeacherName { get; set; }
        public int NumberOfClasses { get; set; }
        public int Password { get; set; }//password==id
        public int Age { get; set; }
        public bool Isrealicitzian { get; set; }
        public int NumOfTest { get; set; }

        public Trainee()
        {
            Id = 0;
            FirstName = "";
            LastName = "";
            Email = "";
            TraineeGender = Gender.זכר;
            PhoneNumber = 0;
            StudentAddress = new Address("", 0, "");
            BirthDate = DateTime.Now;
            Vehicle = TypeOfVehicle.רכב_פרטי;
            Gear = TypeOfGearControl.אוטומט;
            DrivingSchoolName = "";
            TeacherName = "";
            NumberOfClasses = 0;
            Age = 0;
            Isrealicitzian = false;
        }
        public Trainee(Trainee toCopy)//copy con.
        {
            Id = toCopy.Id;
            FirstName = toCopy.FirstName;
            LastName = toCopy.LastName;
            Email = toCopy.Email;
            TraineeGender = toCopy.TraineeGender;
            PhoneNumber = toCopy.PhoneNumber;
            StudentAddress = toCopy.StudentAddress;
            BirthDate = toCopy.BirthDate;
            Vehicle = toCopy.Vehicle;
            Gear = toCopy.Gear;
            DrivingSchoolName = toCopy.DrivingSchoolName;
            TeacherName = toCopy.TeacherName;
            NumberOfClasses = toCopy.NumberOfClasses;
            Age = toCopy.Age;
            Isrealicitzian = toCopy.Isrealicitzian;
            //RegionCountry = toCopy.RegionCountry;
            Password = toCopy.Password;
        }
        public Trainee(int id, string firstname, string lastname, string email, Gender traineegender, int phonenumber, Address studentaddress, DateTime birthdate, TypeOfVehicle vehicle, TypeOfGearControl gear, string drivingschoolname, string teachername, int numberofclasses, bool isrealicitzian, RegionInfo regionCountry, int password = 0)//constractor
        {
            if (id < 1000000000 && id > 99999999)
            {
                Id = id;
            }
            else
                throw new Exception("ת.ז שגויה של הנבחן\n");
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            TraineeGender = traineegender;
            PhoneNumber = phonenumber;
            StudentAddress = studentaddress;
            BirthDate = birthdate;
            Vehicle = vehicle;
            Gear = gear;
            DrivingSchoolName = drivingschoolname;
            TeacherName = teachername;
            NumberOfClasses = numberofclasses;
            Isrealicitzian = isrealicitzian;
            // RegionCountry = regionCountry;
            if (password != 0)
                Password = password;
            else
                Password = Id;
            Age = setAge(BirthDate);
        }

        public int setAge(DateTime birthDate)
        {
            int age;
            DateTime help = DateTime.Today;
            System.TimeSpan diff1 = help.Subtract(birthDate);
            age = (diff1.Days / 365);
            return age;
        }
        public override string ToString()
        {
            return ":פרטי הנבחן\n" + FirstName + " " + LastName +
                "\n:ת.ז \n" + Id + "\n:מין\n" + TraineeGender.ToString() +
            "\n:מספר פלאפון\n" + PhoneNumber +
            "\n:כתובת\n" + StudentAddress.ToString() +
            "\n:תושב ישראל\n" + Isrealicitzian +
            //"\nCountry of origin: " + PrintRagion() +
            "\n:כתובת\n" + StudentAddress +
            "\n:תאריך לידה\n" + BirthDate.ToString("dd/mm/yyyy") +
            "\n:גיל\n" + Age +
            "\n:בית ספר בו למד הנבחן\n" + DrivingSchoolName +
            "\n:שם הטסטר\n" + TeacherName +
            "\n:סוג רישיון\n" + Vehicle.ToString() +
            "\n:סוג הילוך\n" + Gear.ToString() +
            "\n:מספר שיעורי נהיגה שנלמדו\n" + NumberOfClasses;
        }

        public void setTesterPassword(Trainee trainee, int oldPass, int newPass)//seting a new password
        {
            if (trainee.Password == oldPass)
                trainee.Password = newPass;
            else
                throw new Exception("סיסמא שגויה\n");
        }
    }
}