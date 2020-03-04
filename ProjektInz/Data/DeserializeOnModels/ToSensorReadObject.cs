using Newtonsoft.Json;
using ProjektInz.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektInz.Data.DeserializeOnModels
{
    public static class ToSensorReadObject
    {
        /// <summary>
        /// Contains sensor data readings, would be saved in db 
        /// </summary>
        public static List<SensorRead> SavedSensorReads { get; private set; }
        static ToSensorReadObject()
        {
            SavedSensorReads = new List<SensorRead>();
        }
        /// <summary>
        /// Deserialize value and add it to SavedSensorReads 
        /// </summary>
        /// <param name="jsonInput"></param>
        /// <param name="sensorName"></param>
        public static void Deserialize(string jsonInput, string sensorName)
        {
            var currentSensorRead = JsonConvert.DeserializeObject<SensorRead>(jsonInput);
            currentSensorRead.ReadDate = DateTime.Now; //date + time 
            currentSensorRead.SensorName = sensorName;
            SavedSensorReads.Add(currentSensorRead);
        }
        /// <summary>
        /// Just set List<SensorRead> new instance
        /// </summary>
        public static void ClearList()
        {
            SavedSensorReads = new List<SensorRead>();
        }
    }
}
