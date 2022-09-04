using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1 - Get Json string
            string jsonstring = HelperSubroutines.GetJsonstring();

            //2 - Convert to list
            List<CoordinateModel> crd = HelperSubroutines.GetListOfItems(jsonstring);

            //3 - Write results to console
            HelperSubroutines.DisplayResults(crd);

            //4 - Write Id, Longitude, Latitude to columns 1,2,3 of distanceTable
            double[,] distanceTable = HelperSubroutines.WriteIdLonLatToTable(crd);

            //5 - Calculate and write distances to distanceTable
            distanceTable = HelperSubroutines.CalcWriteDistancesToTable(distanceTable);

            //6 - Write distanceTable to console
            HelperSubroutines.DisplayDistanceTable(distanceTable);

            //7 - 
            List<CoordinateModel> crdList = HelperSubroutines.CreateObjectFromArray(distanceTable);

            //8
            HelperSubroutines.WriteToJsonFile(crdList);
        }
    }
}
