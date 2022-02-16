using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sabio.Models;
using Sabio.Models.AppSettings;
using Sabio.Models.Domain;
using Sabio.Models.Requests;
using Sabio.Services;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/billing")]
    [ApiController]
    public class BillingApiController : BaseApiController
    {
        private IBillingService _service;
        private IConfiguration _config;
        private SiteSettings _siteSettings;
        private IWebHostEnvironment _env;
        private IAuthenticationService<int> _authService;

        public BillingApiController(IBillingService service, IConfiguration config, IOptions<SiteSettings> siteSettings, IWebHostEnvironment env, IAuthenticationService<int> authService, ILogger<BillingApiController> logger) : base(logger)
        {
            _service = service;
            _config = config;
            _siteSettings = siteSettings.Value;
            _env = env;
            _authService = authService;
        }


        [HttpGet("subscriptions/grouped")]
        public ActionResult<ItemsResponse<GroupedSubscription>> GetAllGrouped()
        {
            ActionResult result = null;
            try
            {
                List<GroupedSubscription> subscriptions = _service.GetAllGrouped();
                if (subscriptions == null)
                {
                    result = NotFound404(new ErrorResponse("Subscriptions records not found."));
                }
                else
                {
                    ItemsResponse<GroupedSubscription> response = new ItemsResponse<GroupedSubscription>();
                    response.Items = subscriptions;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }

            return result;
        }

        [HttpGet("subscriptions/count")]
        public ActionResult<ItemsResponse<AnalyticsCount>> GetSubsCount()
        {
            ActionResult result = null;
            try
            {
                List<AnalyticsCount> subsCounts = _service.GetSubsCount();
                if (subsCounts == null)
                {
                    result = NotFound404(new ErrorResponse("Subscriptions records not found."));
                }
                else
                {
                    ItemsResponse<AnalyticsCount> response = new ItemsResponse<AnalyticsCount>();
                    response.Items = subsCounts;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }

            return result;
        }
    }
}

