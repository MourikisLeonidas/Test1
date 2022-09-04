using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Test1;

namespace Helper
{
    internal class HelperSubroutines
    {
        internal static string GetJsonstring()  //1
        {
            return File.ReadAllText("Rome_Naples_Route.json");
        }

        internal static List<CoordinateModel> GetListOfItems(string jsonstring) //2
        {
            return JsonSerializer.Deserialize<List<CoordinateModel>>(jsonstring);
        }

        internal static void WriteToJsonFile(List<CoordinateModel> crdList)     //8
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string json = JsonSerializer.Serialize(crdList, opt);
            File.WriteAllText("Complete.json", json);
        }

        internal static List<CoordinateModel> CreateObjectFromArray(double[,] distanceTable)        //7
        {
            //CoordinateModel crd ;
            List<CoordinateModel> crdList = new List<CoordinateModel>();


            for (int i = 1; i < distanceTable.GetLength(0); i++)
            {
                CoordinateModel crd = new CoordinateModel();
                crd.id = (int)distanceTable[i, 1];
                crd.longitude = distanceTable[i, 2];
                crd.latitude = distanceTable[i, 3];
                crd.distanceFromPrevious = distanceTable[i, 4];
                crd.distranceFromStart = distanceTable[i, 5];
                crdList.Add(crd);
            }

            return crdList;
        }

        internal static void DisplayResults(List<CoordinateModel> crd)      //3
        {
            int n = 1;
            foreach (var item in crd)
            {
                Console.WriteLine($"For point {n} the longitude is {item.longitude} and the latitude is {item.latitude}");
                n++;
            }
            Console.WriteLine($"\r\nTotally {crd.Count} items read");
            Console.ReadLine();
        }

        private static double ToRadians(double angleIn10thofaDegree)        //From Distance
        {
            // Angle in 10th
            // of a degree
            return (angleIn10thofaDegree * Math.PI) / 180;
        }

        public static double Distance(double lon1, double lon2, double lat1, double lat2)
        {

            // The math module contains
            // a function named toRadians
            // which converts from degrees
            // to radians.
            lon1 = ToRadians(lon1);
            lon2 = ToRadians(lon2);
            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            // Haversine formula
            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;
            double a = (Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(dlon / 2), 2));

            double c = (2 * Math.Asin(Math.Sqrt(a)));

            // Radius of earth in
            // kilometers. Use 3956
            // for miles
            double r = 6371;

            // calculate the result
            return (c * r);
        }

        internal static double[,] WriteIdLonLatToTable(List<CoordinateModel> crd)       //4
        {
            double[,] distanceTable = new double[crd.Count + 1, 6];

            int n = 1;
            foreach (var item in crd)
            {
                distanceTable[n, 1] = n;
                distanceTable[n, 2] = item.longitude;
                distanceTable[n, 3] = item.latitude;
                n++;
            }

            return distanceTable;
        }

        internal static double[,] CalcWriteDistancesToTable(double[,] distanceTable)        //5
        {
            for (int i = 2; i < distanceTable.GetLength(0); i++)
            {
                distanceTable[i, 4] = HelperSubroutines.Distance(
                                                            distanceTable[i, 2],
                                                            distanceTable[i - 1, 2],
                                                            distanceTable[i, 3],
                                                            distanceTable[i - 1, 3]
                                                            );
                distanceTable[i, 5] = distanceTable[i - 1, 5] + distanceTable[i, 4];
            }

            return distanceTable;
        }

        internal static void DisplayDistanceTable(double[,] distanceTable)      //6
        {
            for (int i = 1; i < distanceTable.GetLength(0); i++)
            {
                Console.WriteLine($"{distanceTable[i, 1]} \t{distanceTable[i, 2]} \t{distanceTable[i, 3]} \t{distanceTable[i, 4]} \t{distanceTable[i, 5]}");
            }
            Console.ReadLine();
        }
    }
}
