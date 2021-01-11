using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public class DataSource
    {
        public static List<Tester> testersList= new List<Tester>();
        public static List<Trainee> traineesList=new List<Trainee>();
        public static List<Test> testsList= new List<Test>();

        public List<Tester> GetTesterList()
        {
            return testersList;
        }
        public List<Trainee> GetTraineeList()
        {
            return traineesList;
        }
        public List<Test> GetTestList()
        {
            return testsList;
        }
    }
}
