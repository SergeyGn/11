using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_InfoAboutDepartment
{
    public class Program
    {
        public static string path = "test.json";

        public static void Save(Department dep)
        {
            var jss = new JsonSerializerSettings();
            jss.TypeNameHandling = TypeNameHandling.All;
            string json = JsonConvert.SerializeObject(dep,jss);
            File.WriteAllText(path, json);

        }
        public static Department  Load()
        {
            Department dep = new Department();
            var jss = new JsonSerializerSettings();
            jss.TypeNameHandling = TypeNameHandling.All;
            string json = File.ReadAllText(path);
            dep=JsonConvert.DeserializeObject<Department>(json,jss);
            return dep;
        }

        public static bool CheckDate(string input)
        {
            DateTime date; 
            bool isDate = false;
            
            if (DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out date))
            {
                return isDate=true;
            }
            else
            {
                return isDate;
            }
        }

    }
}
