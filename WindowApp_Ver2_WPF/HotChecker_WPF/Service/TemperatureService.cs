using HotChicken.Member.Model;
using HotChicken.Rest;
using HotChicken.Rest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HotChecker_WPF.Service
{
    public class TemperatureService
    {
        RestManager restManager = new RestManager();
        const string INSERT_RECORD = "/insertRecord";

        public async Task<(Default resp, HttpStatusCode respStatus)> PostTemperatureData(Member member, double temperatureData)
        {
            QueryParam[] queryParam = new QueryParam[3];
            queryParam[0] = new QueryParam("Idx", member.Idx);
            //queryParam[1] = new QueryParam("code", (int)NetworkOptions.nowTime);
            queryParam[2] = new QueryParam("temp", temperatureData);
            return await restManager.GetResponse<Default>(INSERT_RECORD, 1, null, queryParam);
        }
    }
}
