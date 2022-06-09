using Intercom.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOTechExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            List<RootObject> DeviceList = new List<RootObject>();
            List<Items> ItemList = new List<Items>();

            var JsonFile = File.ReadAllText("../../devices.json");

            RootObject obj = JsonConvert.DeserializeObject<RootObject>(JsonFile);
            DeviceList.Add(obj);

            for (int i = 0; i < 5; i++)
            {
                ItemList.Add(new Items { Name = DeviceList[0].Devices[i].name });
                ItemList[i].Type = DeviceList[0].Devices[i].type;
                ItemList[i].Info = DeviceList[0].Devices[i].info;
                
                var oldUUID = DeviceList[0].Devices[i].info;
                var start = oldUUID.IndexOf(':');
                var UUID = oldUUID.Substring(start + 1);
                UUID = UUID.Split(',')[0];
                ItemList[i].Uuid = UUID;

                int payload1 = DeviceList[0].Devices[i].Sensors[0].payload;
                int payload2 = DeviceList[0].Devices[i].Sensors[1].payload;
                ItemList[i].PayloadTotal = payload1 + payload2;
            }

            var sortedList = ItemList.OrderBy(x => x.Name);
            string newjson = JsonConvert.SerializeObject(sortedList, Formatting.Indented);

            File.WriteAllText("../../devicesReformatted.json", newjson);


        }
    }

    public class RootObject
    {
        public List<Devices> Devices { get; set; }
    }

    public class Devices
    {
        public string name { get; set; }
        public string type { get; set; }
        public string info { get; set; }
        public List<Sensors> Sensors { get; set; }
    }

    public class Sensors
    {
        public string name { get; set; }
        public int payload { get; set; }
    }

    public class Items
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Info { get; set; }
        public string Uuid { get; set; }
        public int PayloadTotal { get; set; }
    }
}
