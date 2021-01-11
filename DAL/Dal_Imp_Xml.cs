using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DAL
{
    class Dal_Imp_Xml : Idal
    {

        XElement TraineeRoot;
        XElement ConfigRoot;
        string pathTrainee = @"TraineeXml.xml";
        string pathTester = @"TesterXml.xml";
        string pathTest = @"TestXml.xml";
        string pathConfig = @"ConfigXml.xml";

        public Dal_Imp_Xml()
        {

            if (!File.Exists(pathTest))
            {
                List<Test> TestList = new List<Test>();//empty list for start
                SaveToXML<List<Test>>(TestList, pathTest);
            }

            if (!File.Exists(pathTester))
            {
                List<Tester> TesterList = new List<Tester>();//empty list for start
                SaveToXML<List<Tester>>(TesterList, pathTester);
            }

            if (!File.Exists(pathConfig))
                CreateFileConfig();
            else//ensure all data is according to current information
                LoadDataTrainee();

            if (!File.Exists(pathTrainee))//we have to add new file
                CreateFileTrainee();
            else//ensure all data is according to current information
                LoadDataTrainee();
        }

        private void LoadDataTrainee()//load from file to program
        {
            try
            {
                TraineeRoot = XElement.Load(pathTrainee);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        private void LoadDataConfig()//load from file to program
        {
            try
            {
                ConfigRoot = XElement.Load(pathConfig);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }


        private void CreateFileConfig()//for new file
        {            
            ConfigRoot = new XElement("NumberOfTest","00000000");
            ConfigRoot.Save(pathConfig);//add new main element
        }

        private void CreateFileTrainee()//for new file
        {
            TraineeRoot = new XElement("Trainee");
            TraineeRoot.Save(pathTrainee);//add new main element
        }

        public static void SaveToXML<T>(T source, string path)//save objects like elements from program to  file
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source);
            file.Close();
        }

        public static T LoadFromXML<T>(string path)//save elements like objects from file to program 
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(file);
            file.Close();
            return result;
        }




        #region Tester
        public void addTesterDAL(Tester tes)
        {
            Tester tester = getTesterDAL(tes.Id);
            if (tester == null)
            {
                List<Tester> testerList = LoadFromXML<List<Tester>>(pathTester);
                testerList.Add(tes);
                SaveToXML<List<Tester>>(testerList, pathTester);
            }
            else
                throw new Exception("הטסטר הזה כבר קיים במערכת\n");
        }

        public void deleteTesterDAL(int id)
        {
            List<Tester> testersListHelp = getTestersDAL().ToList();
            if (testersListHelp.Count() == 0)
                throw new Exception("לא קיים טסטר כזה במערכת\n");
            Tester tester = getTesterDAL(id);
            if (tester != null)
            {
                List<Tester> testersList = LoadFromXML<List<Tester>>(pathTester);
                testersList.RemoveAll(t => t.Id == id);
                SaveToXML<List<Tester>>(testersList, pathTester);
                throw new Exception("נמחקת בהצלחה מהמערכת");
            }
            else
                throw new Exception("לא נמצא טסטר כזה במערכת\n");
        }

        public void setTesterDetailsDAL(Tester tes)
        {
            if (File.Exists(pathTester))
            {
                List<Tester> testersList = LoadFromXML<List<Tester>>(pathTester);
                int index = testersList.FindIndex(s => s.Id == tes.Id);
                if (index != -1)
                {
                    testersList[index] = tes;
                    SaveToXML<List<Tester>>(testersList, pathTester);
                    throw new Exception("פרטיך עודכנו בהצלחה במערכת");

                }
            }
            else
                throw new Exception("לא נמצא טסטר כזה במערכת\n");
        }

        public Tester getTesterDAL(int id)
        {
            if (File.Exists(pathTester))
            {

                List<Tester> testersList = LoadFromXML<List<Tester>>(pathTester);
                if (testersList.Count() == 0)
                {
                    return null;
                }
                return testersList.FirstOrDefault(s => s.Id == id);
            }
            return null;
        }

        public IEnumerable<Tester> getTestersDAL(Func<Tester, bool> predicat = null)
        {
            if (predicat == null)
                return LoadFromXML<List<Tester>>(pathTester).AsEnumerable().ToList();

            return LoadFromXML<List<Tester>>(pathTester).Where(predicat).ToList();
        }

        #endregion

        #region Test

        public void addTestDAL(Test test)
        {
            if (File.Exists(pathTrainee) && File.Exists(pathTest))
            {

                getNumberOfTest();
                test.TestNumber = Configuration.NumberOfTest;
                int c = int.Parse(Configuration.NumberOfTest);
                c++;
                Configuration.NumberOfTest = c.ToString();
                char pad = '0';
                Configuration.NumberOfTest = Configuration.NumberOfTest.PadLeft(8, pad);
                SaveNumberOfTest();


                List<Test> testhelp = LoadFromXML<List<Test>>(pathTest);
                testhelp.Add(test);
                TraineeRoot.Save(pathTrainee);
                SaveToXML<List<Test>>(testhelp, pathTest);
            }
        }

        public void setTestDAL(Test test)
        {
            if (File.Exists(pathTest))
            {
                List<Test> testhelp = LoadFromXML<List<Test>>(pathTest);
                int index = testhelp.FindIndex(s => s.TestNumber == test.TestNumber);
                if (index != -1)
                {
                    testhelp[index] = test;
                    SaveToXML<List<Test>>(testhelp, pathTest);
                }
            }
            else
                throw new Exception("לא נמצא מבחן כזה במערכת\n");
        }

        public Test getTestDAL(string num)
        {
            if (File.Exists(pathTest))
            {
                List<Test> testhelp = LoadFromXML<List<Test>>(pathTest);
                if (testhelp.Count == 0)
                    return null;
                return new Test(testhelp.FirstOrDefault(s => s.TestNumber == num));
            }
            return null;
        }

        public IEnumerable<Test> getTestsDAL(Func<Test, bool> predicat = null)
        {
            if (predicat == null)
                return LoadFromXML<List<Test>>(pathTest).AsEnumerable().ToList();

            return LoadFromXML<List<Test>>(pathTest).Where(predicat).ToList();
        }

        #endregion

        #region Trainee
        public void addTraineeDAL(Trainee trainee)
        {
            LoadDataTrainee();
            Trainee train = getTraineeDAL(trainee.Id);
            if (train != null)
                throw new Exception("כבר קיים תלמיד עם אותה ת.ז");
            XElement Id = new XElement("Id", trainee.Id);
            XElement FirstName = new XElement("FirstName", trainee.FirstName);
            XElement LastName = new XElement("LastName", trainee.LastName);
            XElement Email = new XElement("Email", trainee.Email);
            XElement TraineeGender = new XElement("TraineeGender", trainee.TraineeGender);
            XElement PhoneNumber = new XElement("PhoneNumber", trainee.PhoneNumber);
            XElement Address = new XElement("StudentAddress", new XElement("street", trainee.StudentAddress.street), new XElement("buildingNumber", trainee.StudentAddress.buildingNumber), new XElement("town", trainee.StudentAddress.town));
            XElement BirthDate = new XElement("BirthDate", trainee.BirthDate);
            XElement Vehicle = new XElement("Vehicle", trainee.Vehicle);
            XElement Gear = new XElement("Gear", trainee.Gear);
            XElement DrivingSchoolName = new XElement("DrivingSchoolName", trainee.DrivingSchoolName);
            XElement TeacherName = new XElement("TeacherName", trainee.TeacherName);
            XElement NumberOfClasses = new XElement("NumberOfClasses", trainee.NumberOfClasses);
            XElement Age = new XElement("Age", trainee.Age);
            XElement Isrealicitzian = new XElement("Isrealicitzian", trainee.Isrealicitzian);
            XElement NumOfTest = new XElement("NumOfTest", trainee.NumOfTest);

            XElement complete = new XElement("Trainee", Id, FirstName, LastName, Email, TraineeGender, PhoneNumber, Address, BirthDate, Vehicle, Gear, DrivingSchoolName, TeacherName, NumberOfClasses, Age, Isrealicitzian,NumOfTest);

            TraineeRoot.Add(complete);//add new trainee to all trainee in file
            TraineeRoot.Save(pathTrainee);
        }

        public void deleteTraineeDAL(int id)
        {
            LoadDataTrainee();
            XElement deleteTraineeElement;
            deleteTraineeElement = (from trElement in TraineeRoot.Elements()
                                    where int.Parse(trElement.Element("Id").Value) == id
                                    select trElement).FirstOrDefault();
            if (deleteTraineeElement == null)
                throw new Exception("לא קיים תלמיד כזה במערכת");
            else
            {
                deleteTraineeElement.Remove();
                TraineeRoot.Save(pathTrainee);
            }

        }

        public void setTraineeDAL(Trainee trainee)
        {
            LoadDataTrainee();
            //find element of this trainee
            XElement setTraineeElement;
            setTraineeElement = (from trElement in TraineeRoot.Elements()
                                 where int.Parse(trElement.Element("Id").Value) == trainee.Id
                                 select trElement).FirstOrDefault();
            if (setTraineeElement == null)
                throw new Exception("לא קיים תלמיד עם אותה ת.ז");
            setTraineeElement.Element("Id").Value = trainee.Id.ToString();
            setTraineeElement.Element("FirstName").Value = trainee.FirstName;
            setTraineeElement.Element("LastName").Value = trainee.LastName;
            setTraineeElement.Element("Email").Value = trainee.Email;
            setTraineeElement.Element("TraineeGender").Value = trainee.TraineeGender.ToString();
            setTraineeElement.Element("PhoneNumber").Value = trainee.PhoneNumber.ToString();
            setTraineeElement.Element("StudentAddress").Element("street").Value = trainee.StudentAddress.street;
            setTraineeElement.Element("StudentAddress").Element("buildingNumber").Value = trainee.StudentAddress.buildingNumber.ToString();
            setTraineeElement.Element("StudentAddress").Element("town").Value = trainee.StudentAddress.town;
            setTraineeElement.Element("BirthDate").Value = trainee.BirthDate.ToString();
            setTraineeElement.Element("Vehicle").Value = trainee.Vehicle.ToString();
            setTraineeElement.Element("Gear").Value = trainee.Gear.ToString();
            setTraineeElement.Element("DrivingSchoolName").Value = trainee.DrivingSchoolName;
            setTraineeElement.Element("TeacherName").Value = trainee.TeacherName;
            setTraineeElement.Element("NumberOfClasses").Value = trainee.NumberOfClasses.ToString();
            setTraineeElement.Element("Age").Value = trainee.Age.ToString();
            setTraineeElement.Element("Isrealicitzian").Value = trainee.Isrealicitzian.ToString();
            setTraineeElement.Element("NumOfTest").Value = trainee.NumOfTest.ToString();
            //save new to file            
            TraineeRoot.Save(pathTrainee);

        }
        public Trainee getTraineeDAL(int id)
        {
            LoadDataTrainee();
            //find element of this trainee
            Trainee trainee=null;
            foreach (XElement trElement in TraineeRoot.Elements())
            {
                
                if (int.Parse(trElement.Element("Id").Value) == id)
                {
                    trainee = new Trainee();
                    trainee.Id = int.Parse(trElement.Element("Id").Value);
                    trainee.FirstName = trElement.Element("FirstName").Value;
                    trainee.LastName = trElement.Element("LastName").Value;
                    trainee.Email = trElement.Element("Email").Value;
                    trainee.TraineeGender = (Gender)Enum.Parse(typeof(Gender), trElement.Element("TraineeGender").Value);
                    trainee.PhoneNumber = int.Parse(trElement.Element("PhoneNumber").Value);
                    trainee.StudentAddress = ToAddress(trElement.Element("StudentAddress"));
                    trainee.BirthDate = DateTime.Parse(trElement.Element("BirthDate").Value);
                    trainee.Vehicle = (TypeOfVehicle)Enum.Parse(typeof(TypeOfVehicle), trElement.Element("Vehicle").Value);
                    trainee.Gear = (TypeOfGearControl)Enum.Parse(typeof(TypeOfGearControl), trElement.Element("Gear").Value);
                    trainee.DrivingSchoolName = trElement.Element("DrivingSchoolName").Value;
                    trainee.TeacherName = trElement.Element("TeacherName").Value;
                    trainee.NumberOfClasses = int.Parse(trElement.Element("NumberOfClasses").Value);
                    trainee.Age = int.Parse(trElement.Element("Age").Value);
                    trainee.Isrealicitzian = bool.Parse(trElement.Element("Isrealicitzian").Value);
                    trainee.NumOfTest = int.Parse(trElement.Element("NumOfTest").Value);
                }
            }
            
            return trainee;
            
        }


        public static Address ToAddress(XElement a)
        {
            Address help = new Address();
            help.street = a.Element("street").Value;
            help.buildingNumber = Int32.Parse(a.Element("buildingNumber").Value);
            help.town = a.Element("town").Value;
            return help;
        }

        public IEnumerable<Trainee> getTraineesDAL(Func<Trainee, bool> predicate = null)
        {
            LoadDataTrainee();
            IEnumerable<Trainee> allTrainee = (from trElement in TraineeRoot.Elements()
                                               select new Trainee()//convert from element in file to object of trainee
                                               {
                                                   Id = int.Parse(trElement.Element("Id").Value),
                                                   FirstName = trElement.Element("FirstName").Value,
                                                   LastName = trElement.Element("LastName").Value,
                                                   Email = trElement.Element("Email").Value,
                                                   TraineeGender = (Gender)Enum.Parse(typeof(Gender), trElement.Element("TraineeGender").Value),
                                                   PhoneNumber = int.Parse(trElement.Element("PhoneNumber").Value),
                                                   StudentAddress = ToAddress(trElement.Element("StudentAddress")),
                                                   BirthDate = DateTime.Parse(trElement.Element("BirthDate").Value),
                                                   Vehicle = (TypeOfVehicle)Enum.Parse(typeof(TypeOfVehicle), trElement.Element("Vehicle").Value),
                                                   Gear = (TypeOfGearControl)Enum.Parse(typeof(TypeOfGearControl), trElement.Element("Gear").Value),
                                                   DrivingSchoolName = trElement.Element("DrivingSchoolName").Value,
                                                   TeacherName = trElement.Element("TeacherName").Value,
                                                   NumberOfClasses = int.Parse(trElement.Element("NumberOfClasses").Value),
                                                   Age = int.Parse(trElement.Element("Age").Value),
                                                   Isrealicitzian = bool.Parse(trElement.Element("Isrealicitzian").Value),
                                                   NumOfTest = int.Parse(trElement.Element("NumOfTest").Value)
                                               });

            if (predicate == null)//no condition
            {
                return allTrainee;
            }
            else
            {
                return allTrainee.Where(predicate);
            }
        }
        #endregion

        #region Config
        public void SaveNumberOfTest()
        {
            ConfigRoot = new XElement("NumberOfTest", Configuration.NumberOfTest);
            ConfigRoot.Save(pathConfig);

        }


        public void getNumberOfTest()
        {
            ConfigRoot = XElement.Load(pathConfig);           
            Configuration.NumberOfTest = ConfigRoot.Value;
        }
        #endregion

    }

}