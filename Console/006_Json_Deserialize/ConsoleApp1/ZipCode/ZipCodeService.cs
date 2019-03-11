using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleApp1.ZipCode
{
    public class ZipCodeService
    {
        public object GetZipCode(string country)
        {
            var json = File.ReadAllText($@".\ZipCode\{country}.json");
            return JsonConvert.DeserializeObject<IEnumerable<IDictionary<string, object>>>(json);
            //return JsonConvert.DeserializeObject<IEnumerable<Dictionary<string, IEnumerable<Dictionary<string, IEnumerable<Dictionary<string,object>>>>>>(json);

        }
    }
}
