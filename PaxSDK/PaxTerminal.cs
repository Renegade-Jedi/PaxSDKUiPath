using System;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using System.Collections.Generic;
using System.Activities;
using System.ComponentModel;
using Newtonsoft.Json;

namespace PaxSDKUiPath
{
    #region PaxGetTerminal
    public class PaxGetTerminal : CodeActivity
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
        [Description("Enter TID")]
        public InArgument<string> TId { get; set; }

        [Category("Output")]
        public OutArgument<string> TerminalList { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string KEY = APIKey.Get(context);
            string SECRET = Secret.Get(context);
            string TID = TId.Get(context);
            string BASEURL = "https://api.paxstore.us/p-market-api";

            try
            {
                Result<Terminal> GetTerminal()
                {
                    TerminalApi terminalApi = new TerminalApi(BASEURL, KEY, SECRET);
                    Result<Terminal> terminalList = terminalApi.SearchTerminal(1, 10, TerminalSearchOrderBy.SerialNo, TerminalStatus.All, TID);
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
    #endregion

    #region PaxCreateTerminalAPK
    public class PaxCreateTerminalAPK : CodeActivity
    {
        #region Inputs
        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Api Key")]
        public InArgument<string> APIKey { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Enter Api Secret")]
        public InArgument<string> Secret { get; set; }

        [Category("Input")]
        [Description("The TID of terminal")]
        public InArgument<string> TId { get; set; }

        [Category("Input")]
        [Description("The serial number of terminal")]
        public InArgument<string> SerialNo { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("The package name which indicate the application you want to push to the terminal")]
        public InArgument<string> PackageName { get; set; }

        [Category("Input")]
        [Description("The version name of application which you want to push, if it is blank API will use the latest version")]
        public InArgument<string> Version { get; set; }

        [Category("Input")]
        [Description("The template file name of paramter application. The template file name can be found in the detail of the parameter application. If user want to push more than one template the please use | to concact the different template file names like tempate1.xml|template2.xml|template3.xml, the max size of template file names is 10.")]
        public InArgument<string> TemplateName { get; set; }

        [Category("Input")]
        [Description("The parameter key and value, the key the the PID in template")]
        public InArgument<Dictionary<string, string>> Parameters { get; set; }
        #endregion

        [Category("Output")]
        public OutArgument<string> RequestOutput { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string KEY = APIKey.Get(context);
            string SECRET = Secret.Get(context);
            string BASEURL = "https://api.paxstore.us/p-market-api";
            string _TID = TId.Get(context);
            string serialNo = SerialNo.Get(context);
            string packageName = PackageName.Get(context);
            string version = Version.Get(context);
            string templateName = TemplateName.Get(context);
            Dictionary<string, string> _Parameters = Parameters.Get(context);
            
            try
            {
                Result<string> GetTerminal()
                {
                    TerminalApkApi api = new TerminalApkApi(BASEURL, KEY, SECRET);
                    CreateTerminalApkRequest createTerminalApkRequest = new CreateTerminalApkRequest
                    {
                        TID = _TID,
                        SerialNo = serialNo,
                        PackageName = packageName,
                        Version = version,
                        TemplateName = templateName,
                        Parameters = _Parameters
                    };
                    Result<string> result = api.CreateTerminalApk(createTerminalApkRequest);
                    return result;
                }
                string jsonOutput = JsonConvert.SerializeObject(GetTerminal());
                RequestOutput.Set(context, jsonOutput);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }
    }
    #endregion

    #region PaxCreateTerminal
    public class PaxCreateTerminal : CodeActivity
    {
        #region Inputs
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
        [Description("The name of terminal, max length is 64.")]
        public InArgument<string> Name { get; set; }

        [Category("Input")]
        [Description("he tid of terminal. If it is empty system will generate a tid when creating. And the length range is from 8 to 16")]
        public InArgument<string> TID { get; set; }

        [Category("Input")]
        [Description("The serial number of terminal. If the status is active the serial number is mandatory.")]
        public InArgument<string> SerialNo { get; set; }

        [Category("Input")]
        [Description("The merchant of terminal belongs to. If the initial is active then merchantName is mandatory. The max length is 64. Make sure the merchant belongs to the given reseller")]
        public InArgument<string> MerchantName { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("The reseller of terminal belongs to. Max length is 64.")]
        public InArgument<string> ResellerName { get; set; }

        [Category("Input")]
        [Description("The model name of terminal. Max length is 64.")]
        public InArgument<string> ModelName { get; set; }

        [Category("Input")]
        [Description("The location of terminal, max length is 64.")]
        public InArgument<string> Location { get; set; }

        [Category("Input")]
        [Description("The remark of terminal, max length is 64.")]
        public InArgument<string> Remark { get; set; }

        [Category("Input")]
        [Description("Status of terminal, valus can be one of A(Active) and P(Pendding). If status is null the initial status is P(Pendding) when creating.")]
        public InArgument<string> Status { get; set; }

        #endregion

        [Category("Output")]
        public OutArgument<string> RequestOutput { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string KEY = APIKey.Get(context);
            string SECRET = Secret.Get(context);
            string BASEURL = "https://api.paxstore.us/p-market-api";
            
            string name = Name.Get(context);
            string tid = TID.Get(context);
            string serialNo = SerialNo.Get(context);
            string merchantName = MerchantName.Get(context);
            string resellerName = ResellerName.Get(context);
            string modelName = ModelName.Get(context);
            string location = Location.Get(context);
            string remark = Remark.Get(context);
            string status = Status.Get(context);

            try
            {
                Result<Terminal> GetTerminal()
                {
                    TerminalApi api = new TerminalApi(BASEURL, KEY, SECRET);
                    TerminalCreateRequest createRequest = new TerminalCreateRequest
                    {
                        Name = name,
                        TID = tid,
                        SerialNo = serialNo,
                        MerchantName = merchantName,
                        ResellerName = resellerName,
                        ModelName = modelName,
                        Location = location,
                        Remark = remark,
                        Status = status
                    };
                    Result<Terminal> result = api.CreateTerminal(createRequest);
                    return result;
                }
                string jsonOutput = JsonConvert.SerializeObject(GetTerminal());
                RequestOutput.Set(context, jsonOutput);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }
    }
    #endregion

    #region PaxUpdateTerminal
    public class PaxUpdateTerminal : CodeActivity
    {
        #region Inputs
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
        [Description("Terminal's id(Get from the result of create terminal, not the TID of terminal)")]
        public InArgument<long> TerminalId { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("The name of terminal, max length is 64.")]
        public InArgument<string> Name { get; set; }

        [Category("Input")]
        [Description("he tid of terminal. If it is empty system will generate a tid when creating. And the length range is from 8 to 16")]
        public InArgument<string> TID { get; set; }

        [Category("Input")]
        [Description("The serial number of terminal. If the status is active the serial number is mandatory.")]
        public InArgument<string> SerialNo { get; set; }

        [Category("Input")]
        [Description("The merchant of terminal belongs to. If the initial is active then merchantName is mandatory. The max length is 64. Make sure the merchant belongs to the given reseller")]
        public InArgument<string> MerchantName { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("The reseller of terminal belongs to. Max length is 64.")]
        public InArgument<string> ResellerName { get; set; }

        [Category("Input")]
        [Description("The model name of terminal. Max length is 64.")]
        public InArgument<string> ModelName { get; set; }

        [Category("Input")]
        [Description("The location of terminal, max length is 64.")]
        public InArgument<string> Location { get; set; }

        [Category("Input")]
        [Description("The remark of terminal, max length is 64.")]
        public InArgument<string> Remark { get; set; }


        #endregion

        [Category("Output")]
        public OutArgument<string> RequestOutput { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string KEY = APIKey.Get(context);
            string SECRET = Secret.Get(context);
            string BASEURL = "https://api.paxstore.us/p-market-api";

            long terminalId = TerminalId.Get(context);
            string name = Name.Get(context);
            string tid = TID.Get(context);
            string serialNo = SerialNo.Get(context);
            string merchantName = MerchantName.Get(context);
            string resellerName = ResellerName.Get(context);
            string modelName = ModelName.Get(context);
            string location = Location.Get(context);
            string remark = Remark.Get(context);           

            try
            {
                Result<Terminal> UpdateTerminal()
                {
                    TerminalApi api = new TerminalApi(BASEURL, KEY, SECRET);
                    TerminalUpdateRequest updateRequest = new TerminalUpdateRequest
                    {
                        Name = name,
                        TID = tid,
                        SerialNo = serialNo,
                        MerchantName = merchantName,
                        ResellerName = resellerName,
                        ModelName = modelName,
                        Location = location,
                        Remark = remark
                    };
                    Result<Terminal> updateResult = api.UpdateTerminal(terminalId, updateRequest);
                    return updateResult;
                }
                string jsonOutput = JsonConvert.SerializeObject(UpdateTerminal());
                RequestOutput.Set(context, jsonOutput);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }
    }
    #endregion

    #region PaxDeleteTerminal
    public class PaxDeleteTerminal : CodeActivity
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
        [Description("The terminal id(Get from the result of create terminal, not the TID of terminal)")]
        public InArgument<long> TerminalId { get; set; }

        [Category("Output")]
        public OutArgument<string> RequestOutput { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string KEY = APIKey.Get(context);
            string SECRET = Secret.Get(context);
            long terminalId = TerminalId.Get(context);
            string BASEURL = "https://api.paxstore.us/p-market-api";

            try
            {
                TerminalApi terminalApi = new TerminalApi(BASEURL, KEY, SECRET);
                Result<string> result = terminalApi.DeleteTerminal(terminalId);
                string jsonTerminalList = JsonConvert.SerializeObject(result);
                RequestOutput.Set(context, jsonTerminalList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }
    }
    #endregion

    #region PaxDisableTerminal
    public class PaxDisableTerminal : CodeActivity
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
        [Description("The terminal id(Get from the result of create terminal, not the TID of terminal)")]
        public InArgument<long> TerminalId { get; set; }

        [Category("Output")]
        public OutArgument<string> RequestOutput { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string KEY = APIKey.Get(context);
            string SECRET = Secret.Get(context);
            long terminalId = TerminalId.Get(context);
            string BASEURL = "https://api.paxstore.us/p-market-api";

            try
            {
                TerminalApi terminalApi = new TerminalApi(BASEURL, KEY, SECRET);
                Result<string> result = terminalApi.DisableTerminal(terminalId);
                string jsonTerminalList = JsonConvert.SerializeObject(result);
                RequestOutput.Set(context, jsonTerminalList);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: {0}", ex.Message);
            }
        }
    }
    #endregion
}
