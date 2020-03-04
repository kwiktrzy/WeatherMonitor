using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektInz
{
    public class ControllersDataAdjustHelper
    {
        public List<string> ListOfXs { get; private set; }
        public List<string> ListOfYs { get; private set; }
        /// <summary>
        /// Helps to work with axis x and y to fill chart 
        /// </summary>
        /// <typeparam name="T">Numeric type: int double, float... </typeparam>
        /// <param name="interval">Intervals in minutes between labels. ex. interval 15 = 0:15,0:30,0:45 etc.</param>
        /// <param name="readDates"></param>
        /// <param name="valuesY">From databases list of numeric reads</param>
        /// <returns></returns>
        public void AxisLabelsAndReadings<T>(int interval, List<DateTime> readDates, List<T> valuesY)
        {
            try
            {
                List<ControllersDataAdjustHelper> result = new List<ControllersDataAdjustHelper> ();
                ListOfXs = new List<string>();
                ListOfYs = new List<string>();
                for (int i = 0; i < 1440; i = i + interval) // 1440 minutes in one day 
                {
                    var generatedDate = DateTime.MinValue.AddMinutes(i);//generate date
                    var hour = generatedDate.Hour.ToString().Length == 1 ? ("0" + generatedDate.Hour.ToString()) : generatedDate.Hour.ToString(); //plain DateTime.Hour/minute returns one digit but needed two diggit accuracy. Thats it 
                    var minute = generatedDate.Minute.ToString().Length == 1 ? ("0" + generatedDate.Minute.ToString()) : generatedDate.Minute.ToString();
                    ListOfXs.Add($"{hour}:{minute}"); //adds it to label list
                    if (readDates.Exists(p => p.Hour == generatedDate.Hour && p.Minute == generatedDate.Minute))//checks if we have that time in db rows 
                    {//if exists were sure we have the same index in "value"
                        ListOfYs.Add((valuesY[readDates.FindIndex(p => p.Hour == generatedDate.Hour && p.Minute == generatedDate.Minute)].ToString()).Replace(",","."));//and adds it to the listofY
                        //In the end changing , on . cuz angulars charts doesnt like commas 
                    }
                    else
                    {
                        ListOfYs.Add("null"); // if doesnt exists just return quasi null...
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Overload method when dont have equal minutes in datebase. Without interval just every 60 minutes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="readDates"></param>
        /// <param name="valuesY"></param>
        public void AxisLabelsAndReadings<T>(List<DateTime> readDates, List<T> valuesY)
        {
            try
            {
                List<ControllersDataAdjustHelper> result = new List<ControllersDataAdjustHelper>();
                ListOfXs = new List<string>();
                ListOfYs = new List<string>();
                for (int i = 0; i < 1440; i = i + 60) // 1440 minutes in one day 
                {
                    var generatedDate = DateTime.MinValue.AddMinutes(i);//generate date
                    var hour = generatedDate.Hour.ToString().Length == 1 ? ("0" + generatedDate.Hour.ToString()) : generatedDate.Hour.ToString(); //plain DateTime.Hour/minute returns one digit but needed two diggit accuracy. Thats it 
                    var minute = generatedDate.Minute.ToString().Length == 1 ? ("0" + generatedDate.Minute.ToString()) : generatedDate.Minute.ToString();
                    ListOfXs.Add($"{hour}:{minute}"); //adds it to label list
                    if (readDates.Exists(p => p.Hour == generatedDate.Hour))//checks if we have that time in db rows 
                    {//if exists were sure we have the same index in "value"
                        ListOfYs.Add((valuesY[readDates.FindIndex(p => p.Hour == generatedDate.Hour)].ToString()).Replace(",", "."));//and adds it to the listofY
                        //In the end changing , on . cuz angulars charts doesnt like commas 
                    }
                    else
                    {
                        ListOfYs.Add("null"); // if doesnt exists just return quasi null...
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
