using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Serializable]

    public class Test : Configuration
    {
        public string TestNumber;  
        public int TesterId { get; set; }
        public int TraineeId { get; set; }
        public DateTime TestDateAndTime { get; set; }
        public string StartingPoint { get; set; }
        public bool PassedTheTest { get; set; }
        public String TesterComment { get; set; }
        public TypeOfGearControl Gear { get; set; }
        public TypeOfVehicle Vehicle { get; set; }
        //Criterias
        public bool CarControl { get; set; }//does student have control over the car
        public bool GeneralControl { get; set; }//does student have general control over the street
        public bool Signals { get; set; }//did he signal
        public bool LaneSwitching { get; set; }//does student switch lanes corecttly
        public bool Mirrors { get; set; }//checking all mirrors
        public bool TrafficSigns { get; set; }//traffic signs obedience
        public bool CrosswalkStopping { get; set; }//did he stop at crosswalks for passengers
        public bool StopLine { get; set; }//did he stop at stop line
        public bool TrafficLight { get; set; }//stop slowdown and go at trafficlights
        public bool ClutchControl { get; set; }//does student have control with the clutch
        public bool GearControl { get; set; }//does student have control with gears
        public bool CurrectEntering { get; set; }//did student enter the car and start the car corectly
        public bool CurrectExiting { get; set; }//did student exit the car and shut the car corectly
        public bool RightOfway { get; set; }//did he know the roles of thre right of way
        public bool DidTesterBreak { get; set; }//did tester have to break the car
        public bool DidTesterTouchStearingWheel { get; set; }//did tester have to touch steering wheel
        public bool Parking { get; set; }//how was his parking
        public bool CheckingSurrounding { get; set; }//was he aware of his surrounding 

        public Test()
        {

        }
        public Test(int traineeid, DateTime testdateandtime, string startingpoint, TypeOfGearControl gear, TypeOfVehicle vehicle)
        {
            if (traineeid > 1000000)
            {
                TraineeId = traineeid;
            }
            else
                throw new Exception("Wrong trainee id\n");

            TestTime = testdateandtime;
            StartingPoint = startingpoint;
            PassedTheTest = false;
            Gear = gear;
            Vehicle = vehicle;
        }

        public Test(Test testToCopy)
        {
            TesterId = testToCopy.TesterId;
            TraineeId = testToCopy.TraineeId;
            TestTime = testToCopy.TestTime;
            TestDateAndTime = testToCopy.TestDateAndTime;
            StartingPoint = testToCopy.StartingPoint;
            PassedTheTest = testToCopy.PassedTheTest;
            Gear = testToCopy.Gear;
            Vehicle = testToCopy.Vehicle;
        }
        public DateTime TestTime;
        public DateTime DateOfTheTest
        {
            get { return TestTime; }
            set
            {
                TestTime = new DateTime(value.Year, value.Month, value.Day, TestTime.Hour, TestTime.Minute, TestTime.Second);
            }
        }
        public DateTime TimeOfTheTest
        {
            get { return TestTime; }
            set
            {
                TestTime = new DateTime(TestTime.Year, TestTime.Month, TestTime.Day, value.Hour, value.Minute, value.Second);
            }
        }
        public override string ToString()
        {
            string str = "";
            str += "\nמספר מבחן\n" + TestNumber + "\nתז של הטסטר\n" + TesterId + "\n:תז של הנבחן\n" + TraineeId +
                "\n:תאריך הטסט\n" + TestTime.ToString("dd/mm/yyyy") + "\n:תאריך ושעת מבחן\n" + TestTime +
                "\n:עבר את המבחן\n" + PassedTheTest + "\n:הערת הבוחן\n" + TesterComment.ToString() + "\n:תיבת הילוכים\n" + Gear.ToString();
            return str;
        }
        public string TestReport()
        {
            string str = "";
            str += "\n:שליטה ברכב\n" + CarControl + "\n:שליטה כללית בכביש\n" + GeneralControl + "\n:איתות\n" + Signals + "\n:מעבר בין נתיבים\n" + LaneSwitching + "\n:שימוש במראות\n" + Mirrors + "\n:ציות לתמרורים\n " + TrafficSigns + "\n:מעבר חציה\n" + CrosswalkStopping +
                "\n:עצירה לפני קו עצירה\n" + StopLine + "\n:רמזורים\n" + TrafficLight + "\n:(שליטה בקלאץ (בידני\n" + ClutchControl + "\n:(שליטה בהילוכים (בידני\n" + GearControl + "\n:כניסה נכונה לרכב\n" + CurrectEntering + "\n:יציאה נכונה מהרכב\n" + CurrectExiting + "\n:זכות קדימה\n" + RightOfway + "\n:נגיעה בברקס\n" + DidTesterBreak +
                "\n:נגיעה בהגה\n" + DidTesterTouchStearingWheel + "\n:חניה\n" + Parking + "\n:בדיקת הסביבה\n" + CheckingSurrounding;
            return str;
        }
    }
}