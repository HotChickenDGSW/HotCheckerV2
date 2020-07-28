using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HotChicken.Member.Service
{
    public class MemberService
    {
        Rest.RestManager restManager = new Rest.RestManager();
        const string MEMBER_URL = "/allUser";
        public async Task<(List<Model.Member> respData, HttpStatusCode respStatus)> GetMembers()
        {
            return await restManager.GetResponse<List<Model.Member>>("/allUser", 0);
        }
    }
}
