 
using Microsoft.Extensions.Options;
using Sabio.Data.Providers;
using Sabio.Models.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabio.Models.Requests;
using Sabio.Models.Domain;
using Stripe;
using System.Data.SqlClient;
using System.Data;
using Stripe.Checkout;
using Microsoft.Extensions.Configuration;
using Sabio.Data;
using Sabio.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Sabio.Services
{
    public class BillingService : IBillingService
    {
        private IDataProvider _data;
        private AppKeys _appKeys;
        private IConfiguration _config;
        private SiteSettings _siteSettings;
        private IWebHostEnvironment _env;
        private IAuthenticationService<int> _authService;

        public BillingService(IDataProvider data, IOptions<AppKeys> appKeys, IConfiguration config, IOptions<SiteSettings> siteSettings, IWebHostEnvironment env, IAuthenticationService<int> authService)
        {
            _data = data;
            _appKeys = appKeys.Value;
            _config = config;
            _siteSettings = siteSettings.Value;
            _env = env;
            _authService = authService;
            StripeConfiguration.ApiKey = _appKeys.StripeAppKey;
        }

        // public Paged<Models.Domain.Subscription> Paginate(int pageIndex, int pageSize)
        // {
        //     Paged<Models.Domain.Subscription> pagedList = null;
        //     List <Models.Domain.Subscription> list = null;

        //     int totalCount = 0;
        //     string procName = "[dbo].[Subscriptions_SelectAll_Paginated]";

        //     _data.ExecuteCmd(procName, inputParamMapper: delegate (SqlParameterCollection paramCollection)
        //     {
        //         paramCollection.AddWithValue("@PageIndex", pageIndex);
        //         paramCollection.AddWithValue("@PageSize", pageSize);
        //     }, singleRecordMapper: delegate (IDataReader reader, short set)
        //     {
        //         Models.Domain.Subscription subscription = null;

        //         int index = 0;
        //         subscription = new Models.Domain.Subscription();
        //         subscription.Id = reader.GetSafeInt32(index++);
        //         subscription.SubscriptionId = reader.GetSafeString(index++);
        //         subscription.CustomerId = reader.GetSafeString(index++);
        //         subscription.IsActive = reader.GetSafeBool(index++);
        //         subscription.Email = reader.GetSafeString(index++);
        //         subscription.SubscriptionType = reader.GetSafeString(index++);

        //         if (totalCount == 0)
        //         {
        //             totalCount = reader.GetSafeInt32(index++);
        //         }

        //         if (list == null)
        //         {
        //             list = new List<Models.Domain.Subscription>();
        //         }
        //         list.Add(subscription);
        //     });

        //     if(list !=null)
        //     {
        //         pagedList = new Paged<Models.Domain.Subscription>(list, pageIndex, pageSize, totalCount);
        //     }

        //     return pagedList;
        // }


        public List<GroupedSubscription> GetAllGrouped()
        {
            List<GroupedSubscription> list = null;
            GroupedSubscription aSubscriptionType = null;
        
            string procName = "[dbo].[Subscriptions_SelectAll_Grouped]";

            _data.ExecuteCmd(procName, 
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
                 {
                     int index = 0;

                     aSubscriptionType = new GroupedSubscription();
                     aSubscriptionType.UserCount = reader.GetSafeInt32(index++);
                     aSubscriptionType.Name = reader.GetSafeString(index++);

                     if (list == null)
                     {
                         list = new List<GroupedSubscription>();
                     }
                     list.Add(aSubscriptionType);
                 });

            return list;
        }
 

        public List<AnalyticsCount> GetSubsCount()
        {
            List<AnalyticsCount> list = null;
            AnalyticsCount aSubCount = null;

            string procName = "[dbo].[Subscriptions_GetAll_ByMonth]";

            _data.ExecuteCmd(procName,
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int index = 0;

                    aSubCount = new AnalyticsCount();
                    aSubCount.Month = reader.GetSafeInt32(index++);
                    aSubCount.Year = reader.GetSafeInt32(index++);
                    aSubCount.TotalCount = reader.GetSafeInt32(index++);

                    if (list == null)
                    {
                        list = new List<AnalyticsCount>();
                    }
                    list.Add(aSubCount);
                });

            return list;
        }
    }
}