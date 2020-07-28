using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HotChicken.Rest.Model
{
    public class TResponse<T>
    {
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
    }
    public sealed class Nothing
    {
        public static Nothing AtAll => null;
    }

    public class Default
    {
        [JsonProperty("code")]
        public string Code
        {
            get;
            set;
        }
    }
}
