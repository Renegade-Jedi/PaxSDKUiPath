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
                    CreateTerminalApkRequest createTerminalApkRequest = new CreateTerminalApkRequest();
                    createTerminalApkRequest.TID = _TID;
                    createTerminalApkRequest.SerialNo = serialNo;
                    createTerminalApkRequest.PackageName = packageName;
                    createTerminalApkRequest.Version = version;
                    createTerminalApkRequest.TemplateName = templateName;
                    createTerminalApkRequest.Parameters = _Parameters;
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
