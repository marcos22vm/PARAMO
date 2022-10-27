using Sat.Recruitment.Api.Core.Model;
using Sat.Recruitment.Api.Core.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Core.Data
{
    public class DataProvider : IDataProvider
    {
        private readonly ICollection<User> _users = new List<User>();

        public DataProvider() {
            LoadData();
        }

        public Task<User> AddUser(User user)
        {
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task<ICollection<User>> GetUsers()
        {
            return Task.FromResult(_users);
        }

        private void LoadData() {

            var reader = FileReader.ReadFile("/Files/Users.txt");

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(user);
            }
            reader.Close();
        }

    }
}
