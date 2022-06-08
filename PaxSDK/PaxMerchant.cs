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

namespace PaxSDKUiPath
{
    #region PaxGetMerchant
    public class PaxGetMerchant : CodeActivity
    {

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
        [Description("Search filter by merchant name")]
        public InArgument<string> Name { get; set; }

        [Category("Output")]
        public OutArgument<string> MerchantlList { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string KEY = APIKey.Get(context);
            string SECRET = Secret.Get(context);
            string NAME = Name.Get(context);
            string BASEURL = "https://api.paxstore.us/p-market-api";

            try
            {
                Result<PagedMerchant> GetMerchant()
                {
                    MerchantApi merchantApi = new MerchantApi(BASEURL, KEY, SECRET);
                    Result<PagedMerchant> merchantlList = merchantApi.SearchMerchant(1, 10, MerchantSearchOrderBy.Name, NAME, MerchantStatus.All);
                    return merchantlList;
                }
                string jsonTerminalList = JsonConvert.SerializeObject(GetMerchant());
                MerchantlList.Set(context, jsonTerminalList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }

    }
    #endregion

    #region PaxDisableMerchant
    public class PaxDisableMerchant : CodeActivity
    {

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
        [Description("The merchant id.")]
        public InArgument<long> MerchantId { get; set; }

        [Category("Output")]
        public OutArgument<string> RequestOutput { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string KEY = APIKey.Get(context);
            string SECRET = Secret.Get(context);
            long merchantId = MerchantId.Get(context);
            string BASEURL = "https://api.paxstore.us/p-market-api";

            try
            {
                MerchantApi api = new MerchantApi(BASEURL, KEY, SECRET);
                Result<string> result = api.DisableMerchant(merchantId);
                string jsonResult = JsonConvert.SerializeObject(result);
                RequestOutput.Set(context, jsonResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }
    }
    #endregion

    #region PaxDeleteMerchant
    public class PaxDeleteMerchant : CodeActivity
    {

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
        [Description("The merchant id.")]
        public InArgument<long> MerchantId { get; set; }

        [Category("Output")]
        public OutArgument<string> RequestOutput { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string KEY = APIKey.Get(context);
            string SECRET = Secret.Get(context);
            long merchantId = MerchantId.Get(context);
            string BASEURL = "https://api.paxstore.us/p-market-api";

            try
            {
                MerchantApi api = new MerchantApi(BASEURL, KEY, SECRET);
                Result<string> result = api.DeleteMerchant(merchantId);
                string jsonResult = JsonConvert.SerializeObject(result);
                RequestOutput.Set(context, jsonResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }
    }
    #endregion
}
