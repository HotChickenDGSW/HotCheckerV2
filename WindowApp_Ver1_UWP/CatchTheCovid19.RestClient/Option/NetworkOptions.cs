using CatchTheCovid19.RestClient.Option;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatchTheCovid19.RestClient.Option
{
    public class NetworkOptions
    {
        public static string serverUrl { get; set; } = "";
        public static TimeEnum nowTime { get; set; }
        public static int timeOut { get; set; } = 30000;
        public static int mode { get; set; } = 1; // 1->no oled, 0->oled
    }
}
