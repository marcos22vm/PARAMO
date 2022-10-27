using Sat.Recruitment.Api.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Core.Data
{
    public interface IDataProvider
    {
        /*
         Normally, data access is made to external servers, so I define the interface methods as asynchronous
        */
        public Task<ICollection<User>> GetUsers();
        public Task<User> AddUser(User user);
    }
}
