using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;



namespace BE
{
    [Serializable]

    public class Configuration
    {
        public static string NumberOfTest = "00000000";
        public static int TesterMinimumAge = 40;
        public static int TraineeMinimumAge = 17;//we chack the law so we change it from 18 to 17
        public static int WaitingPeriodBetweenTests = 7;
        public static int MinimumClassesToTake = 20;

    }

    [Serializable]
    public struct Address
    {
        public string street { get; set; }
        public int buildingNumber { get; set; }
        public string town { get; set; }
        
        public Address(string value1, int v, string value2)
        {
            street = value1;
            buildingNumber = v;
            town = value2;
        }

        public override string ToString()
        {
            return (street + " " + buildingNumber + " " + town);
        }
    };

    

}