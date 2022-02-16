using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests;
using Sabio.Models.Requests.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class UserService : IUserService
    {
        private IAuthenticationService<int> _authenticationService;
        private IDataProvider _dataProvider;

        public UserService(IAuthenticationService<int> authSerice, IDataProvider dataProvider)
        {
            _authenticationService = authSerice;
            _dataProvider = dataProvider;
        }


        public List<AnalyticsCount> GetUsersCount()
        {
            List<AnalyticsCount> list = null;
            AnalyticsCount aUserCount = null;

            string procName = "[dbo].[Users_GetAll_ByMonth]";

            _dataProvider.ExecuteCmd(procName,
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;

                    aUserCount = new AnalyticsCount();
                    aUserCount.Month = reader.GetSafeInt32(startingIndex++);
                    aUserCount.Year = reader.GetSafeInt32(startingIndex++);
                    aUserCount.TotalCount = reader.GetSafeInt32(startingIndex++);

                    if (list == null)
                    {
                        list = new List<AnalyticsCount>();
                    }
                    list.Add(aUserCount);
                });

            return list;
        }

    }
}