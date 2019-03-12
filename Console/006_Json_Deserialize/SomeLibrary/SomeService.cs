using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SomeLibrary
{
    public class SomeService
    {

        public object GetZipCode(string country)
        {
            var json = File.ReadAllText($@".\SomeCode\{country}.json");
            return JsonConvert.DeserializeObject<IEnumerable<IDictionary<string, object>>>(json);

        }
    }
}
