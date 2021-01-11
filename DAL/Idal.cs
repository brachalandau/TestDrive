using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface Idal
    {
        void addTesterDAL(Tester tester);
        void deleteTesterDAL(int id);
        void setTesterDetailsDAL(Tester tester);
        Tester getTesterDAL(int id);

        void addTraineeDAL(Trainee trainee);
        void deleteTraineeDAL(int id);
        void setTraineeDAL(Trainee trainee);
        Trainee getTraineeDAL(int id);

        void addTestDAL(Test test);
        void setTestDAL(Test test);
        Test getTestDAL(string num);


        IEnumerable<Tester> getTestersDAL(Func<Tester, bool> predicat = null);
        IEnumerable<Trainee> getTraineesDAL(Func<Trainee, bool> predicat = null);
        IEnumerable<Test> getTestsDAL(Func<Test, bool> predicat = null);
    }
}