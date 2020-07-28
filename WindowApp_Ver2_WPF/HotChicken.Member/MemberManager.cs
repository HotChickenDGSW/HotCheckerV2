using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HotChicken;
using HotChicken.Member.Service;

namespace HotChicken.Member
{
    public class MemberManager
    {

        public static List<Model.Member> members
        {
            get;
            set;
        } = null;

        public async Task LoadMembers()
        {
            MemberService memberService = new MemberService();
            var resp = await memberService.GetMembers();
            if(resp.respStatus == System.Net.HttpStatusCode.OK)
            {
                members = resp.respData;
            }
        }

        public static Model.Member FindMemberByCardId(string cardId)
        {
            if(members == null)
            {
                return null;
            }

            if (members.FindIndex(x => x.CardId == cardId) == -1)
            {
                return null;
            }
            if (cardId.Length == 3)
            {
                cardId = members.Where(x => x.Name == cardId) == null ? null : members.Where(x => x.Name == cardId).ToList()[0].CardId;
            }
            if (cardId.Length > 10)
            {
                if (cardId.Contains("S"))
                {
                    cardId = cardId.Substring(0, 10);
                }
                else if (cardId.Contains("T"))
                {
                    cardId = cardId.Substring(0, 8);
                }
            }

            var data = (members.Where(x => x.CardId == cardId) == null ? null : members.Where(x => x.CardId == cardId).ToList()[0]);
            return data;

        }
    }
}
