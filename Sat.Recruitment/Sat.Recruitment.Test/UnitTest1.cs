using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Controllers;

using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        private static Api.Core.BusinesLogic.UserLogic  _userLogic=  new Api.Core.BusinesLogic.UserLogic(new Api.Core.Data.DataProvider());

        [Fact]
        public void Test1()
        {
            var userController = new UsersController(_userLogic);

            var result = userController.CreateUser(new Api.Core.Dto.CreateUserRequest {
            Name = "Mike",
                Email = "mike@gmail.com",
                Address= "Av. Juan G",
                Phone = "+349 1122354215",
                UserType= "Normal",
                Money = "124"
            
            }
            ).Result;

            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }
       
        [Fact]
        public void Test2()
        {
            var userController = new UsersController(_userLogic);

            var result = userController.CreateUser(new Api.Core.Dto.CreateUserRequest
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = "124"
            }
            ).Result;

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }


        [Fact]
        public void Test3()
        {
            var userController = new UsersController(_userLogic);

            var result = userController.CreateUser(new Api.Core.Dto.CreateUserRequest
            {
                Name = "Agustina",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = "124"
            }
            ).Result;

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("Incorrect data user:  The email is required. ", result.Errors);
        }

    }
}
