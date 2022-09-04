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
            List<CoordinateModel> pointList = JsonSerializer.Deserialize<List<CoordinateModel>>(jsonstring);
            pointList[0].Id = 1;
            for (int i=1; i<pointList.Count; i++)
            {
                pointList[i].Id = i;
                pointList[i].DistanceFromPrevious = HelperSubroutines.Distance(
                                                            pointList[i].longitude,
                                                            pointList[i - 1].longitude,
                                                            pointList[i].latitude,
                                                            pointList[i - 1].latitude
                                                            );
                pointList[i].DistanceFromStart = pointList[i - 1].DistanceFromStart + pointList[i].DistanceFromPrevious; ;
            }
            return pointList;
        }

        public static double Distance(double lon1, double lon2, double lat1, double lat2)       //2a
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

        private static double ToRadians(double angleIn10thofaDegree)        //2b
        {
            // Angle in 10th
            // of a degree
            return (angleIn10thofaDegree * Math.PI) / 180;
        }

        internal static void DisplayResults(List<CoordinateModel> pointList)      //3
        {
            Console.WriteLine($"{"Id",-5}{"Longitute",-15}{"Latitude",-15}{"Distance from previous",-25}{"Distance from start",-25}");
            foreach (var point in pointList)
            {
                //Console.WriteLine($"{point} the longitude is {point.Longitude} and the latitude is {point.Latitude}");
                Console.WriteLine($"" +
                    $"{point.Id,-5}" +
                    $"{Math.Round(point.longitude, 5),-15}" +
                    $"{Math.Round(point.latitude, 5),-15}" +
                    $"{Math.Round(point.DistanceFromPrevious, 3),-25}" +
                    $"{Math.Round(point.DistanceFromStart, 3),-25}");
            }
        }
                
        internal static void WriteToJsonFile(List<CoordinateModel> pointList)     //4
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string json = JsonSerializer.Serialize(pointList, opt);
            File.WriteAllText("Complete.json", json);
        }
    }
}
