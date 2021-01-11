using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using System.Xml;
using BE;

//using DAL;

namespace BL
{
    public class Ibl_Imp : IBL
    {
        public DAL.Idal dal = DAL.FactoryDAL.GetDal();


        public void AddTraineeBL(Trainee trainee)//add trainee
        {
            DateTime help = DateTime.Today;

            System.TimeSpan diff1 = help.Subtract(trainee.BirthDate);
            trainee.Age = (diff1.Days / 365);
            if (trainee.Age > Configuration.TraineeMinimumAge)//we check if the trainee age is more than 17
            {
                dal.addTraineeDAL(trainee);//func from Dal_Imp
            }
            else
                throw new Exception("הנבחן שהוכנס צעיר מדי בשביל להרשם לטסט\n");
        }
        public void SetTraineeBL(Trainee trainee)
        {
            dal.setTraineeDAL(trainee);
        }
        public Trainee getTraineeBL(int id)
        {
            return dal.getTraineeDAL(id);
        }
        public void deleteTraineeBL(int id)
        {
            dal.deleteTraineeDAL(id);
        }


        public void AddTesterBL(Tester tester)
        {
            DateTime help = DateTime.Today;

            System.TimeSpan diff1 = help.Subtract(tester.BirthDate);
            tester.Age = (diff1.Days / 365);
            if (tester.Age > Configuration.TesterMinimumAge)
            {
                dal.addTesterDAL(tester);//func from Dal_Imp
            }
            else
                throw new Exception("הטסטר שהוכנס צעיר מדי, לפי נהלי משרד התחבורה \n");
        }

        public void SetTesterBL(Tester tester)
        {
            dal.setTesterDetailsDAL(tester);
        }

        public void deleteTesterBL(int id)
        {
            dal.deleteTesterDAL(id);
        }

        public Tester getTesterBL(int id)
        {
            Tester help = dal.getTesterDAL(id);
            if (help == null)
                return null;
            return help;
        }
        public Test getTestByIdsAndDate(int testerId, int traineeId, DateTime date)
        {
            IEnumerable<Test> testsList = GetAllTestsBL();
            if (testsList.Count() == 0)
                return null;
            Test temp = testsList.FirstOrDefault(s => s.TesterId == testerId && s.TraineeId == traineeId && s.TestTime.Date == date.Date);
            return temp;
        }
        public Test getTestByIdAndDate(int traineeId, DateTime date)
        {
            IEnumerable<Test> testsList = dal.getTestsDAL();
            if (testsList.Count() == 0)
                return null;
            Test temp = testsList.FirstOrDefault(s => s.TraineeId == traineeId && s.TestTime == date);
            return temp;
        }
        public void AddTestBL(Trainee student, Test test, DateTime testdate)
        {
            Tester help;
            IEnumerable<Tester> testersList = dal.getTestersDAL();
            if (CheckBasicBL(student, test))
            {
                help = AvailableTesterBL(testdate, student, test);
                DateTime help1 = testdate;
                if (testersList.Count() == 0)
                    throw new Exception("לא קיים טסטר מתאים במערכת");
                if (help == null)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (help == null)
                        {
                            Random r = new Random();
                            int rDay = r.Next(0, 6);
                            int rHour = r.Next(9, 14);
                            help1 = help1.AddDays(rDay);
                            if ((int)help1.DayOfWeek == 5)
                            {
                                help1 = help1.AddDays(3);
                            }
                            if ((int)help1.DayOfWeek == 6)
                            {
                                help1 = help1.AddDays(2);
                            }
                            help1 = help1.AddHours(rHour);
                            if (!(help1.Hour >= 9 && help1.Hour <= 14))
                            {
                                do
                                {
                                    help1 = help1.AddHours(1);
                                } while (!(help1.Hour >= 9 && help1.Hour <= 14));
                            }
                            help = AvailableTesterBL(testdate, student, test);
                        }
                        else
                            break;
                    }
                    if (help != null)
                    {
                        throw new Exception("תאריך ושעת המבחן לא זמינים\nניתן להכניס את התאריך הפנוי הזה " + help1 + "\nעם הטסטר הזה\n" + help.FirstName + " " + help.LastName + " " + help.Id);
                    }
                    else
                    {
                        throw new Exception("לצערנו אין טסט פנוי בתאריך ובשעה המבוקשת אנו ממליצים לך לנסות לבחור תאריך ו/או שעה אחרת לטסט");
                    }
                }

                if (help != null)
                {
                    if (help1 == testdate)//its the original date and not the recommended one
                    {
                        if (checkNumberOfTestersTestsBL(help, testdate))
                        {
                            if (help.Gear == student.Gear && IsTesterAndTraineeTheSameTypeOfLicenseBL(help, student))
                            {
                                if (CheckNotMoreThenOneTestForTrainnePerHourBL(student, testdate))
                                {
                                    if (!DidPassedTheTestAlreadyWithThisCarType(test, student))
                                    {
                                        Test TestToAdd = new Test(student.Id, testdate, "", help.Gear, help.Vehicle);
                                        TestToAdd.TesterId = help.Id;
                                        TestToAdd.StartingPoint = test.StartingPoint;
                                        dal.addTestDAL(TestToAdd);
                                    }
                                }
                            }
                            else
                                throw new Exception(" הטסטר שנבחן אינו ראשי לבחון את הנבחן " + "עם סוג ההילוך " + student.Gear + " וסוג הרכב" + student.Vehicle);
                        }
                        else
                            throw new Exception("לא ניתן להוסיף טסט, בגלל שאז הטסטר יעבור את המכסה השבועית שלו של טסטים\n");
                    }
                }
                //else
                //throw new Exception("לצערנו אין טסט פנוי בתאריך ובשעה המבוקשת אנו ממליצים לך לנסות לבחור תאריך ו/או שעה אחרת לטסט");

            }
        }

        public bool CheckLessonsBeforeAddBL(Trainee student)
        {
            if (student.NumberOfClasses <= 20)
                return false;
            else
                return true;
        }

        public Tester AvailableTesterBL(DateTime testDate, Trainee student, Test test)
        {
            bool flag = true;
            IEnumerable<Test> testsList = dal.getTestsDAL();

            List<Tester> testersList = ReturnAListOfAllTestersInLessMaxDistanceRadius(test.StartingPoint);
            IEnumerable<Trainee> traineesList = dal.getTraineesDAL();
            if (testersList.Count() != 0)
            {
                foreach (Tester works in testersList)
                {
                    if (works.Vehicle == student.Vehicle && works.Gear == student.Gear)
                    {
                        if (works.WorkingHours[testDate.Hour - 9, ((int)testDate.DayOfWeek)] == true)
                        {
                            foreach (Test available in testsList)
                            {
                                if (available.TesterId == works.Id && available.DateOfTheTest == testDate)
                                {
                                    flag = false;
                                    break;//cause this tester is unavailable at this hour
                                }
                            }
                            if (flag == true)
                                return works;
                        }
                    }
                }
            }
            return null;
        }

        public bool CheckBasicBL(Trainee student, Test test)
        {
            IEnumerable<Test> testsList = dal.getTestsDAL();
            IEnumerable<Trainee> traineesList = dal.getTraineesDAL();
            if (traineesList.Count() == 0)
                return false;

            Trainee help = traineesList.FirstOrDefault(s => s.Id == student.Id);
            if (help == null)
            {
                throw new Exception("הנבחן הזה לא נמצא במערכת\n");
            }
            else
            {
                Test temp = new Test();
                if (testsList.Count() == 0)
                    temp = null;
                else
                    temp = testsList.FirstOrDefault(s => s.TraineeId == student.Id);
                if (temp == null)
                {
                    if (CheckLessonsBeforeAddBL(student))
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("נלמדו פחות מ-20 שיעורים");
                    }
                }
                else
                {
                    Test help2 = test;//mishtane ezer
                    TimeSpan checkDays = help2.TestTime.Subtract(temp.TestTime);//new date-old date
                    if (checkDays.TotalDays <= 7)
                    {
                        throw new Exception("לא עברו 7 ימים מאז מועד הטסט האחרון ");
                    }
                    else
                    {
                        if (CheckLessonsBeforeAddBL(student))
                        {
                            return true;
                        }
                        else
                        {
                            throw new Exception("נללמדו פחות מ-20 שיעורים\n");
                        }
                    }
                }
            }
        }

        public bool checkNumberOfTestersTestsBL(Tester tester, DateTime testDate)
        {
            IEnumerable<Test> testsList = dal.getTestsDAL();
            int count = 0;
            if (testsList.Count() != 0)
            {
                foreach (Test element in testsList)
                {
                    if (element.TesterId == tester.Id && WeekOfYear(element.TestDateAndTime) == WeekOfYear(testDate))
                    {
                        count++;
                        if (count >= tester.MaxTestsPerWeek)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public bool CheckNotMoreThenOneTestForTrainnePerHourBL(Trainee trainee, DateTime testDate)
        {
            IEnumerable<Test> testsList = dal.getTestsDAL();
            if (testsList.Count() != 0)
            {
                foreach (Test element in testsList)
                {
                    if (element.TraineeId == trainee.Id && element.TestDateAndTime == testDate)
                    {
                        throw new Exception("הנבחן כבר רשום לטסט אחר בשעה הזאת\n");
                    }
                }
            }
            return true;
        }

        public static int WeekOfYear(DateTime dt)//returns the number of the week in the year
        {
            int startDays = 0;
            // first day of the year
            DateTime firstJanuary = new DateTime(dt.Year, 1, 1);

            if (firstJanuary.DayOfWeek == DayOfWeek.Tuesday)
            {
                startDays = 1;
            }
            else if (firstJanuary.DayOfWeek == DayOfWeek.Wednesday)
            {
                startDays = 2;
            }
            else if (firstJanuary.DayOfWeek == DayOfWeek.Thursday)
            {
                startDays = 3;
            }
            else if (firstJanuary.DayOfWeek == DayOfWeek.Friday)
            {
                startDays = 4;
            }
            else if (firstJanuary.DayOfWeek == DayOfWeek.Saturday)
            {
                startDays = 5;
            }
            else if (firstJanuary.DayOfWeek == DayOfWeek.Sunday)
            {
                startDays = 6;
            }

            if (DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek == DayOfWeek.Sunday)
            {
                startDays++;
                startDays = startDays % 7;
            }

            return ((dt.DayOfYear + startDays - 1) / 7) + 1;
        }

        public void setTestDetails(Test testToSet, bool carcontrol, bool generalcontrol, bool signals, bool laneswitching, bool mirrors, bool trafficsigns, bool crosswalkstopping, bool stopline, bool trafficlight, bool clutchcontrol, bool gearcontrol, bool currectentering, bool currectexiting, bool rightofway, bool didtesterbreak, bool didtestertouchstearingwheel, bool parking, bool checkingsurrounding)
        {
            //not sure that this function is what they asked for

            testToSet.CarControl = carcontrol;
            testToSet.GeneralControl = generalcontrol;
            testToSet.Signals = signals;
            testToSet.LaneSwitching = laneswitching;
            testToSet.Mirrors = mirrors;
            testToSet.TrafficSigns = trafficsigns;
            testToSet.CrosswalkStopping = crosswalkstopping;
            testToSet.StopLine = stopline;
            testToSet.TrafficLight = trafficlight;
            testToSet.ClutchControl = clutchcontrol;
            testToSet.GearControl = gearcontrol;
            testToSet.CurrectEntering = currectentering;
            testToSet.CurrectExiting = currectexiting;
            testToSet.RightOfway = rightofway;
            testToSet.DidTesterBreak = didtesterbreak;
            testToSet.DidTesterTouchStearingWheel = didtestertouchstearingwheel;
            testToSet.Parking = parking;
            testToSet.CheckingSurrounding = checkingsurrounding;
            SetTestResultBL(testToSet);
            dal.setTestDAL(testToSet);
        }

        public bool SetTestResultBL(Test test)//this func calculates if the trainne passed the test acourding to the rules that are writen in this func
        {
            int criticl = 0;
            int mediam = 0;
            int notCritical = 0;

            if (test.GeneralControl == false)
                criticl++;

            if (test.Mirrors == false)
                criticl++;

            if (test.CrosswalkStopping == false)
                criticl++;

            if (test.StopLine == false)
                criticl++;

            if (test.TrafficLight == false)
                criticl++;

            if (test.Gear == (TypeOfGearControl)1)//yadani
            {
                if (test.GearControl == false)
                    criticl++;
                if (test.ClutchControl == false)
                    criticl++;
            }

            if (test.RightOfway == false)
                criticl++;

            if (test.Parking == false)
                criticl++;

            if (test.DidTesterBreak == false)
                criticl++;

            if (test.DidTesterTouchStearingWheel == false)
                criticl++;

            if (test.Signals == false)
                mediam++;
            if (test.LaneSwitching == false)
                mediam++;

            if (test.TrafficSigns == false)
                mediam++;

            if (test.CheckingSurrounding == false)
                mediam++;

            if (test.CarControl == false)
                notCritical++;
            if (test.CurrectEntering == false)
                notCritical++;

            if (test.CurrectExiting == false)
                notCritical++;
            if (criticl >= 1 || mediam > 1 || notCritical == 3 || mediam == 1 && notCritical >= 1)
            {
                test.PassedTheTest = false;
                return false;
            }
            else
            {
                test.PassedTheTest = true;
                return true;
            }
        }

        public bool DidPassedTheTestAlreadyWithThisCarType(Test test, Trainee trainee)
        {
            IEnumerable<Test> testsList = dal.getTestsDAL();
            if (testsList.Count() != 0)
            {
                foreach (Test element in testsList)
                {
                    if (element.TraineeId == trainee.Id && element.Gear == test.Gear && element.Vehicle == test.Vehicle && element.PassedTheTest == true)
                    {
                        throw new Exception("הנבחן שהוזן כבר עבר טסט עם סוג הילוך כזה\n");
                    }
                }
            }
            return false;
        }

        public bool IsTesterAndTraineeTheSameTypeOfLicenseBL(Tester tester, Trainee trainee)
        {
            if (tester.Vehicle == trainee.Vehicle && tester.Gear == trainee.Gear)
            {
                return true;
            }
            else
                return false;// new Exception("This tester dosnt have the right expertis to test this trainee\n");
        }

        public List<Tester> ReturnsAlltestersThatAreAvailableAtDateTime(DateTime dateTime)
        {
            IEnumerable<Tester> testersList = dal.getTestersDAL();
            if (testersList.Count() == 0)
                return null;
            List<Tester> AvailableTesters = new List<Tester>();
            foreach (Tester element in testersList)
            {
                if (AvailableTesterForOtherFuncBL(dateTime, element) != null)
                {
                    AvailableTesters.Add(element);
                }
            }
            return AvailableTesters;
        }

        public Tester AvailableTesterForOtherFuncBL(DateTime testDate, Tester works)//this func is similar to "AvailableTesterBL" ,but it has small changes and it was made for "ReturnsAlltestersThatAreAvailableAtDateTime" func
        {
            IEnumerable<Test> testsList = dal.getTestsDAL();
            if (testsList.Count() == 0)
                return null;
            bool flag = true;

            if (works.WorkingHours[testDate.Hour, ((int)testDate.DayOfWeek)] == true)
            {
                foreach (Test available in testsList)
                {
                    if (available.TesterId == works.Id && available.TestDateAndTime == testDate)
                    {
                        flag = false;
                        break;//cause this tester is unavailable at this hour
                    }
                }
                if (flag == true)
                    return works;
            }
            return null;
        }

        public Test getTestByNumber(string testNum)//teturns the test with this number
        {
            IEnumerable<Test> testsList = dal.getTestsDAL();

            if (testsList.Count() == 0)
                return null;
            Test temp = testsList.FirstOrDefault(s => s.TestNumber == testNum);
            return temp;
        }

        public List<Test> ReturnListThatAnswersToTheDelegetQuestion(GetsTestToCheck CheckMethod)
        {
            IEnumerable<Test> testsList = dal.getTestsDAL();
            if (testsList.Count() == 0)
                return null;
            List<Test> testsThatAnswerdToQuestion = new List<Test>();
            foreach (Test element in testsList)
            {
                if (CheckMethod(element))
                {
                    testsThatAnswerdToQuestion.Add(element);
                }
            }
            return testsThatAnswerdToQuestion;
        }

        public void HowManyTestsForEachTraineesBL()
        {
            IEnumerable<Trainee> traineesList = dal.getTraineesDAL();
            foreach (Trainee element in traineesList)
            {
                SetHowManyTestsForThisTraineeBL(element);
            }
        }
        public void SetHowManyTestsForThisTraineeBL(Trainee trainee)
        {
            trainee.NumOfTest = 0;
            IEnumerable<Test> testsList = dal.getTestsDAL();
            if (testsList.Count() == 0)
                return;
            foreach (Test element in testsList)
            {
                if (element.TraineeId == trainee.Id)
                {
                    trainee.NumOfTest++;
                    dal.setTraineeDAL(trainee);
                }
            }
        }

        public bool IsEligibleToGetLicenseBL(Trainee trainee, TypeOfVehicle vehicle, TypeOfGearControl gear)
        {
            IEnumerable<Test> testsList = dal.getTestsDAL();
            if (testsList.Count() == 0)
                return false;
            var t = testsList.Where(x => x.TraineeId == trainee.Id && x.PassedTheTest == true);
            var t1 = t.Where(x => x.Gear == gear);
            var t2 = t1.Where(x => x.Vehicle == vehicle);
            if (t2.Any())
                return true;
            else
                return false;
        }

        public List<Test> ListOfAllTestsThisDayBL(DateTime day)
        {
            IEnumerable<Test> testsList = dal.getTestsDAL();
            if (testsList.Count() == 0)
                return null;
            var d = from test in testsList
                    where test.TestTime.Day == day.Day
                    select new Test(test);
            if (d.Any())
            {
                return (List<Test>)d;
            }
            else
                throw new Exception("אין מבחנים שניתן לבחור ליום הזה עדיין\n");
        }

        public List<Test> ListOfAllTestsThisMonthBL(DateTime month)
        {
            IEnumerable<Test> testsList = dal.getTestsDAL();
            if (testsList.Count() == 0)
                return null;
            var m = testsList.Where(x => x.TestTime.Month == month.Month);
            if (m.Any())
            {
                return (List<Test>)m;
            }
            else
                throw new Exception("אין מבחנים שניתן לבחור לחודש הזה עדיין\n");
        }

        public IEnumerable<object> ViewAllTestsOfTrainee(int traineeId)
        {
            IEnumerable<Test> testsList = dal.getTestsDAL();
            if (testsList.Count() == 0)
                return null;
            //we use here a select new
            return from item in testsList
                   where item.TraineeId == traineeId
                   select new
                   {
                       Id = item.TestNumber,
                       TeacherId = item.TesterId,
                       Year = item.TestTime.Year
                   };
        }

        public void SetTesterCommentBL(string comm, Test test)//its sets testers comment, if ther is a comment already then it adds it to the existing comment
        //we use here a anonomys delegate
        {
            GetsTestToCheck nc = delegate (Test t)
            {
                if (t.TesterComment != null)
                    return true;
                else
                    return false;
            };
            bool help = nc(test);
            if (help)
                test.TesterComment += comm;
            else
                test.TesterComment = comm;
        }

        public void CommentInBirthDay(Trainee trainee, Test test)//if trainee passed the test on his birthday in "testers comment" it will say happy birthday
        {
            if (trainee.BirthDate.Day == test.DateOfTheTest.Day && trainee.BirthDate.Month == test.DateOfTheTest.Month)
            {
                test.TesterComment += "\n!יום הולדת שמח\n";
            }
        }

        public List<Tester> GetAllTestersBL()
        {
            return dal.getTestersDAL().ToList();
        }
        public List<Trainee> GetAllTraineesBL()
        {
            return dal.getTraineesDAL().ToList();
        }
        public List<Test> GetAllTestsBL()
        {
            return dal.getTestsDAL().ToList();
        }


        public IEnumerable<IGrouping<TypeOfVehicle, Tester>> GetsTestersExperticeBL(bool sort = false)
        {

            IEnumerable<Tester> testersList = dal.getTestersDAL();
            if (testersList.Count() == 0)
                return null;
            if (sort)
            {
                var list = from tester in dal.getTestersDAL()
                           orderby tester.LastName
                           group tester by tester.Vehicle into TesterGroup
                           select TesterGroup;
                return list;
            }
            else
            {
                var list =
                    from tester in dal.getTestersDAL()
                    group tester by tester.Vehicle into TesterGroup
                    select TesterGroup;
                return list;
            }
        }

        public IEnumerable<IGrouping<string, Trainee>> GroupsByDrivingSchoolBL(bool sort = false)
        {
            HowManyTestsForEachTraineesBL();
            IEnumerable<Trainee> traineesList = dal.getTraineesDAL();
            if (traineesList.Count() == 0)
                return null;
            if (sort)
            {
                IEnumerable<IGrouping<string, Trainee>> list = from trainee in traineesList
                                                               orderby trainee.LastName
                                                               group trainee by trainee.DrivingSchoolName;
                return list;
            }
            else
            {
                IEnumerable<IGrouping<string, Trainee>> list = from trainee in traineesList
                                                               group trainee by trainee.DrivingSchoolName;
                return list;
            }
        }

        public IEnumerable<IGrouping<string, Trainee>> GroupsByTeacherNameBL(bool sort = false)
        {
            HowManyTestsForEachTraineesBL();
            IEnumerable<Trainee> traineesList = dal.getTraineesDAL();
            if (traineesList.Count() == 0)
                return null;
            if (sort)
            {
                IEnumerable<IGrouping<string, Trainee>> list = from trainee in traineesList
                                                               orderby trainee.LastName
                                                               group trainee by trainee.TeacherName;
                return list;
            }
            else
            {
                IEnumerable<IGrouping<string, Trainee>> list = from trainee in traineesList
                                                               group trainee by trainee.TeacherName;
                return list;
            }
        }

        public IEnumerable<IGrouping<int, Trainee>> GroupsByAmountOfTestTakenBL(bool sort = false)
        {
            HowManyTestsForEachTraineesBL();
            IEnumerable<Trainee> traineesList = dal.getTraineesDAL();
            if (traineesList.Count() == 0)
                return null;
            if (sort)
            {
                IEnumerable<IGrouping<int, Trainee>> list = from trainee in traineesList
                                                                //let numOfTests = HowManyTestsForThisTraineeBL(trainee)
                                                            orderby trainee.LastName
                                                            group trainee by trainee.NumOfTest;
                return list;
            }
            else
            {
                IEnumerable<IGrouping<int, Trainee>> list = from trainee in traineesList
                                                                //let numOfTests = HowManyTestsForThisTraineeBL(trainee)
                                                            group trainee by trainee.NumOfTest;
                return list;
            }
        }

        public static int distance;
        public static string origin;
        public static string destination;

        public void GetMapQuest()
        {
            string KEY = @"DVa48Op6CQcpZ5AzlxBjAbLjFSnlthC1";
            string url = @"https://www.mapquestapi.com/directions/v2/route" +
             @"?key=" + KEY +
             @"&from=" + origin +
             @"&to=" + destination +
             @"&outFormat=xml" +
             @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" +
             @"&enhancedNarrative=false&avoidTimedConditions=false";
            //request from MapQuest service the distance between the 2 addresses
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            //the response is given in an XML format
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);
            if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
            //we have the expected answer
            {
                //display the returned distance
                XmlNodeList distances = xmldoc.GetElementsByTagName("distance");
                double distInMiles = Convert.ToDouble(distances[0].ChildNodes[0].InnerText);
                int distanceInKM = (int)(distInMiles * 1.609344);
                distance = distanceInKM;
            }
            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
            //we have an answer that an error occurred, one of the addresses is not found
            {
                throw new Exception("התרחשה בעיה, אחת מהכתובות לא נמצאו-נסו שוב");
            }
            else //busy network or other error...
            {
                distance = -1;
                //throw new Exception("We have'nt got an answer, maybe the net is busy...");
            }
        }
        public List<Tester> ReturnAListOfAllTestersInLessMaxDistanceRadius(string address)
        {
            IEnumerable<Tester> testersList = dal.getTestersDAL();
            if (testersList.Count() == 0)
                return null;
            Random r = new Random();
            int distanceRandom = r.Next(100, 1000);
            distance = 0;
            List<Tester> testersInDistance = new List<Tester>();
            foreach (Tester element in testersList)
            {

                origin = element.TesterAddress.ToString(); //or "תקווה פתח 100 העם אחד "etc.
                destination = address;//or "גן רמת 10 בוטינסקי'ז "etc.
                Thread thread = new Thread(GetMapQuest);
                thread.Start();
                thread.Join();
                if (distance == -1)//there was an error so..
                    distance = distanceRandom;
                if (distance <= element.MaxDistance)
                {
                    testersInDistance.Add(element);
                }
            }
            return testersInDistance;
        }


    }
}