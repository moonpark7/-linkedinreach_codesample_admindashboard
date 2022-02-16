using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Enums;
using Sabio.Models.Requests;
using Sabio.Models.Requests.Users;
using Sabio.Services;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Threading.Tasks;
using Sabio.Models.AppSettings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserApiController : BaseApiController
    {
        private IUserService _service = null;
        private IEmailService _email = null;
        private IAuthenticationService<int> _authService = null;
        private SiteSettings _siteSettings;

        public UserApiController(IUserService service
            , IEmailService email
            , IAuthenticationService<int> authService
            , IOptions<SiteSettings> siteSettings
            , ILogger<UserApiController> logger) : base(logger)
        {
            _service = service;
            _email = email;
            _authService = authService;
            _siteSettings = siteSettings.Value;
        }

        [HttpGet("count")]
        public ActionResult<ItemsResponse<AnalyticsCount>> GetUsersCount()
        {
            ActionResult result = null;
            try
            {
                List<AnalyticsCount> usersCounts = _service.GetUsersCount();
                if (usersCounts == null)
                {
                    result = NotFound404(new ErrorResponse("Subscriptions records not found."));
                }
                else
                {
                    ItemsResponse<AnalyticsCount> response = new ItemsResponse<AnalyticsCount>();
                    response.Items = usersCounts;
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
