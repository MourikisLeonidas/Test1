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
            // Από τα Nuget πρέπει να γίνει εγκατάσταση το System.Text.Json

            // 1 - Get Json string
            // Στο πιο κάτω βήμα, δημιουργείται ένα string jsonstring που περιέχει το filename του json αρχείου
            string jsonstring = HelperSubroutines.GetJsonstring();

            // 2 - Convert to list
            // Στέλνεται το jsonstring που είναι τύπου string και επιστρέφει μία λίστα pointList που περιέχει objects.
            // Αρχικά περνούν μόνο οι στήλες 2 & 3 από το json file με το longitude & latitude και μετά υπολογίζονται
            // και γράφονται και οι άλλες στήλες (Id, DistanceFromPreviousdPoint, DistanceFronStart)
            List<CoordinateModel> pointList = HelperSubroutines.GetFullListOfPoints(jsonstring);

            // 3 - Write results to console
            HelperSubroutines.DisplayResults(pointList);

            
            // 4 - Write list of point model object to a json file
            HelperSubroutines.WriteToJsonFile(pointList);

            // 5 - Create a new list of point model objects and put in thid list Start point, Endpoint and all 
        }
    }
}
