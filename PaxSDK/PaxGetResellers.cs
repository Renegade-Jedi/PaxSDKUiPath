using System;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using Newtonsoft.Json;

namespace PaxSDK
{
    public class PaxGetResellers : CodeActivity
    {
        //[Category("Input")]
        //[RequiredArgument]
        //[Description("Enter URL")]
        //public InArgument<string> BaseURL { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Api Key")]
        public InArgument<string> APIKey { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Api Secret")]
        public InArgument<string> Secret { get; set; }

        [Category("Output")]
        public OutArgument<string> ResellerList { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            //string BASEURL = BaseURL.Get(context);
            string KEY = APIKey.Get(context);
            string SECRET = Secret.Get(context);

            string BASEURL = "https://api.paxstore.us/p-market-api";
            //string KEY = "S83Y6CUH0THROWNAVZLS";
            //string SECRET = "AXQTM1W2JJ1Q95P00ZZUQNGS9S8BFZ45V0Z53KCR";

            try
            {
                Result<PagedReseller> GetResellers()
                {
                    ResellerApi resellerApi = new ResellerApi(BASEURL, KEY, SECRET);
                    Result<PagedReseller> resellerList = resellerApi.SearchReseller(1, 10, ResellerSearchOrderBy.Name, "reseller", ResellerStatus.Active);
                    return resellerList;
                }
                string jsonResellerList = JsonConvert.SerializeObject(GetResellers());
                ResellerList.Set(context, jsonResellerList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }

    }

    public class PaxGetTerminal : CodeActivity
    {
        //[Category("Input")]
        //[RequiredArgument]
        //[Description("Enter URL")]
        //public InArgument<string> BaseURL { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Api Key")]
        public InArgument<string> APIKey { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Api Secret")]
        public InArgument<string> Secret { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter TID")]
        public InArgument<string> TId { get; set; }

        [Category("Output")]
        public OutArgument<string> TerminalList { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            //string BASEURL = BaseURL.Get(context);
            string KEY = APIKey.Get(context);
            string SECRET = Secret.Get(context);
            string TID = TId.Get(context);
            string BASEURL = "https://api.paxstore.us/p-market-api";
            //string KEY = "S83Y6CUH0THROWNAVZLS";
            //string SECRET = "AXQTM1W2JJ1Q95P00ZZUQNGS9S8BFZ45V0Z53KCR";

            try
            {
                Result<Terminal> GetTerminal()
                {
                    TerminalApi terminalrApi = new TerminalApi(BASEURL, KEY, SECRET);
                    Result<Terminal> terminalList = terminalrApi.SearchTerminal(1, 10, TerminalSearchOrderBy.SerialNo, TerminalStatus.All,TID);
                    return terminalList;
                }
                string jsonTerminalList = JsonConvert.SerializeObject(GetTerminal());
                TerminalList.Set(context, jsonTerminalList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }

    }
}
