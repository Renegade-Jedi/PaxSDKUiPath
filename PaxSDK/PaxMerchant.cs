using System;
using Paxstore.OpenApi;
using Paxstore.OpenApi.Model;
using System.Collections.Generic;
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

    #region PaxCreateMerchant
    public class PaxCreateMerchant : CodeActivity
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
        [Description("Merchant name, max length is 64")]
        public InArgument<string> Name { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Email of merchant, max length is 255")]
        public InArgument<string> Email { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Reseller name of merchant, max length is 64. Make sure the reseller exist.")]
        public InArgument<string> ResellerName { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Contact of merchant, max length is 64")]
        public InArgument<string> Contact { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("The country code, please refer to Country Codes")]
        public InArgument<string> Country { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Phone number of merchant, max length is 32")]
        public InArgument<string> Phone { get; set; }

        [Category("Input")]
        [Description("Postcode of merchant, max length is 16")]
        public InArgument<string> Postcode { get; set; }

        [Category("Input")]
        [Description("Address of merchant, max length is 255")]
        public InArgument<string> Address { get; set; }

        [Category("Input")]
        [Description("Description of merchant, max length is 3000")]
        public InArgument<string> Description { get; set; }

        [Category("Input")]
        [Description("Merchant categories. Make sure the categories are available")]
        public InArgument<List<string>> MerchantCategoryNames { get; set; }

        [Category("Input")]
        [Description("Indicate whether to create user when activate the merchant, won't create user when activate if this value is empty")]
        public InArgument<bool> CreateUserFlag { get; set; }

        [Category("Input")]
        [Description("Whether to activate the merchant when create, default value is false.")]
        public InArgument<bool> ActivateWhenCreate { get; set; }
        #endregion

        [Category("Output")]
        public OutArgument<string> RequestOutput { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string KEY = APIKey.Get(context);
            string SECRET = Secret.Get(context);
            string BASEURL = "https://api.paxstore.us/p-market-api";
            
            string name = Name.Get(context);
            string email = Email.Get(context);
            string resellerName = ResellerName.Get(context);
            string contact = Contact.Get(context);
            string country = Country.Get(context);
            string phone = Phone.Get(context);
            string postcode = Postcode.Get(context);
            string address = Address.Get(context);
            string description = Description.Get(context);
            List<string> merchantCategoryNames = MerchantCategoryNames.Get(context);
            bool createUserFlag = CreateUserFlag.Get(context);
            bool activateWhenCreate = ActivateWhenCreate.Get(context);

            try
            {
                Result<Merchant> CreateMerchant()
                {
                    MerchantApi merchantApi = new MerchantApi(BASEURL, KEY, SECRET);
                    MerchantCreateRequest merchantCreateRequest = new MerchantCreateRequest
                    {
                        Name = name,
                        Email = email,
                        ResellerName = resellerName,
                        Contact = contact,
                        Country = country,
                        Phone = phone,
                        Postcode = postcode,
                        Address = address,
                        Description = description,
                        MerchantCategoryNames = merchantCategoryNames,
                        CreateUserFlag = createUserFlag,
                    };

                    merchantCreateRequest.setActivateWhenCreate(activateWhenCreate);

                    Result<Merchant> result = merchantApi.CreateMerchant(merchantCreateRequest);
                    return result;
                }
                string jsonResult = JsonConvert.SerializeObject(CreateMerchant());
                RequestOutput.Set(context, jsonResult);
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
