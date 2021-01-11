using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public delegate bool GetsTestToCheck(Test test);
    public interface IBL
    {
        void AddTraineeBL(Trainee trainee);
        void SetTraineeBL(Trainee trainee);
        void deleteTraineeBL(int id);
        Trainee getTraineeBL(int id);
        void AddTesterBL(Tester tester);
        void SetTesterBL(Tester tester);
        void deleteTesterBL(int id);
        Tester getTesterBL(int id);
        Test getTestByIdsAndDate(int testerId, int TraineeId, DateTime date);
        Test getTestByIdAndDate(int traineeId, DateTime date);
        void AddTestBL(Trainee student, Test test, DateTime testDate);
        bool CheckLessonsBeforeAddBL(Trainee student);
        Tester AvailableTesterBL(DateTime testDate, Trainee student, Test test);
        bool CheckBasicBL(Trainee student, Test test);
        bool checkNumberOfTestersTestsBL(Tester tester, DateTime testDate);
        bool CheckNotMoreThenOneTestForTrainnePerHourBL(Trainee trainee, DateTime testDate);
        void setTestDetails(Test testToSet, bool carcontrol, bool generalcontrol, bool signals, bool laneswitching, bool mirrors, bool trafficsigns, bool crosswalkstopping, bool stopline, bool trafficlight, bool clutchcontrol, bool gearcontrol, bool currectentering, bool currectexiting, bool rightofway, bool didtesterbreak, bool didtestertouchstearingwheel, bool parking, bool checkingsurrounding);
        bool SetTestResultBL(Test test);
        Test getTestByNumber(string testNum);
        bool DidPassedTheTestAlreadyWithThisCarType(Test test, Trainee trainee);
        bool IsTesterAndTraineeTheSameTypeOfLicenseBL(Tester tester, Trainee trainee);
        List<Tester> ReturnAListOfAllTestersInLessMaxDistanceRadius(string address);
        List<Tester> ReturnsAlltestersThatAreAvailableAtDateTime(DateTime dateTime);
        Tester AvailableTesterForOtherFuncBL(DateTime testDate, Tester works);
        List<Test> ReturnListThatAnswersToTheDelegetQuestion(GetsTestToCheck CheckMethod);
        void HowManyTestsForEachTraineesBL();
        void SetHowManyTestsForThisTraineeBL(Trainee trainee);
        bool IsEligibleToGetLicenseBL(Trainee trainee, TypeOfVehicle vehicle, TypeOfGearControl gear);
        List<Test> ListOfAllTestsThisDayBL(DateTime day);
        List<Test> ListOfAllTestsThisMonthBL(DateTime month);
        List<Tester> GetAllTestersBL();
        List<Trainee> GetAllTraineesBL();
        List<Test> GetAllTestsBL();

        void GetMapQuest();


        IEnumerable<object> ViewAllTestsOfTrainee(int traineeId);
        void SetTesterCommentBL(string comm, Test test);
        void CommentInBirthDay(Trainee trainee,Test test);

        IEnumerable<IGrouping<TypeOfVehicle, Tester>> GetsTestersExperticeBL(bool sort = false);
        IEnumerable<IGrouping<string, Trainee>> GroupsByDrivingSchoolBL(bool sort = false);
        IEnumerable<IGrouping<string, Trainee>> GroupsByTeacherNameBL(bool sort = false);
        IEnumerable<IGrouping<int, Trainee>> GroupsByAmountOfTestTakenBL(bool sort = false);


    }
}