using Xunit;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Domain.Constants;
using FluentAssertions;

namespace Restaurants.Application.Users.Tests
{
    public class CurrentUserTests
    {
        // TestMethod_Scenario_ExpectResult -- Format

        //To run multiple test with parameters..
        [Theory()]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.User)]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            //arrange
            //Create an instance of a current user
            var currentUser = new CurrentUser("1", "test@gmail.com", [UserRoles.Admin,UserRoles.User],null,null);

            //act
            //Now check if the currentUser isInRole
            var isInRole = currentUser.IsInRole(roleName);


            //assert
            isInRole.Should().BeTrue(); 
        }


        // TestMethod_Scenario_ExpectResult
        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            //arrange
            //Create an instance of a current user
            var currentUser = new CurrentUser("1", "test@gmail.com", [UserRoles.Admin, UserRoles.User], null, null);

            //act
            //Now check if the currentUser isInRole
            var isInRole = currentUser.IsInRole(UserRoles.Owner);


            //assert
            isInRole.Should().BeFalse();
        }

        // TestMethod_Scenario_ExpectResult
        [Fact()]
        public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
        {
            //arrange
            //Create an instance of a current user
            var currentUser = new CurrentUser("1", "test@gmail.com", [UserRoles.Admin, UserRoles.User], null, null);

            //act
            //Now check if the currentUser isInRole
            var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());


            //assert
            isInRole.Should().BeFalse();
        }
    }
}