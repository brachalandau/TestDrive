using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    public class Dal_Imp : Configuration, Idal
    {
        public DataSource DS = new DataSource();
        //Tester function
        public void addTesterDAL(Tester tes)
        {
            Tester tester = getTesterDAL(tes.Id);
            if (tester == null)
                DataSource.testersList.Add(tes);
            else
                throw new Exception("הטסטר הזה כבר קיים במערכת\n");
        }
        public void deleteTesterDAL(int id)
        {
            List<Tester> testersListHelp = DS.GetTesterList();
            if (testersListHelp.Count() == 0)
                throw new Exception("לא קיים טסטר כזה במערכת\n");
            Tester tester = getTesterDAL(id);
            if (tester != null)
            {
                DataSource.testersList.RemoveAll(sc => sc.Id == id);
                throw new Exception("נמחקת בהצלחה מהמערכת");
            }
            else
                throw new Exception("לא נמצא טסטר כזה במערכת\n");
        }

        public void setTesterDetailsDAL(Tester tes)
        {
            List<Tester> testersListHelp = DS.GetTesterList();
            int index = testersListHelp.FindIndex(s => s.Id == tes.Id);
            if (index != -1)
                DataSource.testersList[index] = tes;
            else
                throw new Exception("לא נמצא טסטר כזה במערכת\n");
        }

        public Tester getTesterDAL(int id)
        {
            List<Tester> testersListHelp = DS.GetTesterList();
            if (testersListHelp.Count() == 0)
            {
                return null;
            }
            var help = testersListHelp.FirstOrDefault(s => s.Id == id);
            if (help == null)
                return null;
            return help;
        }

        //Trainee function
        public void addTraineeDAL(Trainee trainee)
        {
            Trainee train = getTraineeDAL(trainee.Id);
            if (train == null)
            {
                DataSource.traineesList.Add(trainee);
            }
            else
                throw new Exception("הנבחן הזה כבר קיים במערכת\n");
        }
        public void deleteTraineeDAL(int id)
        {
            List<Trainee> traineesListHelp = DS.GetTraineeList();
            if (traineesListHelp.Count() == 0)
                throw new Exception("לא נמצא נבחן כזה במערכת\n");
            Trainee trainee = getTraineeDAL(id);
            if (trainee != null)
                DataSource.traineesList.RemoveAll(c => c.Id == id);
            else
                throw new Exception("לא נמצא נבחן כזה במערכת\n");
        }
        public void setTraineeDAL(Trainee trainee)
        {
            List<Trainee> traineeListHelp = DS.GetTraineeList();
            int index = traineeListHelp.FindIndex(s => s.Id == trainee.Id);
            if (index != -1)
                DataSource.traineesList[index] = trainee;
            else
                throw new Exception("לא נמצא נבחן כזה במערכת\n");
        }
        public Trainee getTraineeDAL(int id)
        {
            List<Trainee> traineesListHelp = DS.GetTraineeList();
            if (traineesListHelp.Count == 0)
                return null;
            return traineesListHelp.FirstOrDefault(s => s.Id == id);
            //return help;
        }

        //Testes function
        public void addTestDAL(Test test)
        {
            List<Trainee> traineehelp = DS.GetTraineeList();
            int index = traineehelp.FindIndex(s => s.Id == test.TraineeId);
            if (index != -1)
                DataSource.traineesList[index].NumOfTest++;
            else
                throw new Exception("לא נמצא מבחן כזה במערכת\n");
            test.TestNumber = Configuration.NumberOfTest;
            int c = int.Parse(Configuration.NumberOfTest);
            c++;
            Configuration.NumberOfTest = Convert.ToString(c);
            char pad = '0';
            Configuration.NumberOfTest = Configuration.NumberOfTest.PadLeft(8, pad);
            DataSource.testsList.Add(test);
        }
        public void setTestDAL(Test test)
        {
            List<Test> testsListHelp = DS.GetTestList();
            int index = testsListHelp.FindIndex(s => s.TestNumber == test.TestNumber);
            if (index != -1)
                DataSource.testsList[index] = test;
            else
                throw new Exception("לא נמצא מבחן כזה במערכת\n");
        }

        public Test getTestDAL(string num)
        {
            List<Test> testsListHelp = DS.GetTestList();
            if (testsListHelp.Count == 0)
                return null;
            Test help = new Test(testsListHelp.FirstOrDefault(s => s.TestNumber == num));
            return help;
        }

        //Get lists function
        public IEnumerable<Tester> getTestersDAL(Func<Tester, bool> predicat = null)
        {
            if (predicat == null)
                return DataSource.testersList.AsEnumerable().ToList();

            return DataSource.testersList.Where(predicat).ToList();
        }

        public IEnumerable<Trainee> getTraineesDAL(Func<Trainee, bool> predicat = null)
        {
            if (predicat == null)
                return DataSource.traineesList.AsEnumerable().ToList();

            return DataSource.traineesList.Where(predicat).ToList();
        }

        public IEnumerable<Test> getTestsDAL(Func<Test, bool> predicat = null)
        {
            if (predicat == null)
                return DataSource.testsList.AsEnumerable().ToList();

            return DataSource.testsList.Where(predicat).ToList();
        }

    }

}