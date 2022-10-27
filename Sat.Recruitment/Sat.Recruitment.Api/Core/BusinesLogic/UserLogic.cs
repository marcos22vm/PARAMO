using Sat.Recruitment.Api.Core.Data;
using Sat.Recruitment.Api.Core.Dto;
using Sat.Recruitment.Api.Core.Exceptions;
using Sat.Recruitment.Api.Core.Model;
using Sat.Recruitment.Api.Core.Util;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Core.BusinesLogic
{
    public class UserLogic
    {
        public IDataProvider _dataProvider;
        public UserLogic(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task CreateUser(CreateUserRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email != null ? TextFormater.NomralizeEmail(request.Email) : null,
                Address = request.Address,
                Phone = request.Phone,
                UserType = request.UserType,
                Money = decimal.Parse(request.Money)
            };

            ValidateDataErrors(user);
            SetUserMoneyByType(user);
            await ValidateDuplicateUser(user);
            await _dataProvider.AddUser(user);
            return;
        }
        private void SetUserMoneyByType(User user)
        {
            switch (user?.UserType)
            {
                case "Normal":
                    {
                        //If user is normal and has more than USD100
                        if (user.Money > 100)
                        {
                            SetUserMoney(user, 0.12);
                        }
                        else
                        {   //If user is normal and has more than USD10 and less than USD100
                            /*
                                The logic originally written does not include the case of exactly USD100. 
                                Common sense tells me that this case should be included in this sentence, but since it is a refactor, 
                                I'm not going to change that logic.
                            */
                            if (user.Money > 10 && user.Money < 100)
                            {
                                SetUserMoney(user, 0.8);
                            }
                        }
                        break;
                    }
                case "SuperUser":
                    {   //If user is SuperUser and has more than USD100
                        if (user.Money > 100)
                        {
                            SetUserMoney(user, 0.2);
                        }
                        break;
                    }
                case "Premium":
                    {    //If user is Premium and has more than USD100
                        if (user.Money > 100)
                        {
                            SetUserMoney(user, 2);
                        }
                        break;
                    }
            }
        }
        private void SetUserMoney(User user,double dpercentage) {
            var percentage = Convert.ToDecimal(dpercentage);
            var gif = user.Money * percentage;
            user.Money = user.Money + gif;
        }
        private void ValidateDataErrors(User user)
        {
            var errors = "";
            if (user.Name == null)
                //Validate if Name is null
                errors = "The name is required. ";
            if (user.Email == null)
                //Validate if Email is null
                errors = errors + "The email is required. ";
            if (user.Address == null)
                //Validate if Address is null
                errors = errors + "The address is required. ";
            if (user.Phone == null)
                //Validate if Phone is null
                errors = errors + "The phone is required. ";

            if (errors != "")
                throw new DataUserException(errors);
        }
        private async Task ValidateDuplicateUser(User user)
        {
            var users = (await _dataProvider.GetUsers()).ToList();

            var usersFind =
                from lqUser in users
                where   lqUser.Email == user.Email || 
                        lqUser.Phone == user.Phone ||  
                        (lqUser.Name == user.Name && lqUser.Address == user.Address)
                select new
                {
                    Name = lqUser.Name,
                };

            if (usersFind?.Count() > 0)
                throw new DuplucateUserException("The user is duplicated");
        }
    }
}
