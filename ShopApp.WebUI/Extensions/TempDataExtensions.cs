using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Extensions
{
    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempdata, string key, T value) where T:class
        {
            tempdata[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempdata, string key) where T: class
        {
            object value;
            tempdata.TryGetValue(key, out value);
            return value == null ? null : JsonConvert.DeserializeObject<T>((string)value);
        }
    }
}
