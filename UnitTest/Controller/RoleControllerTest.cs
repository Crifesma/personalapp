using System;
using Xunit;
using API.Controllers;
using API.Services;
using Moq;
using API.Data.Entities;
using System.Collections.Generic;
using API.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using API.Data.Repositories;

namespace UnitTest
{
    public class RoleControllerTest
    {
        private readonly RoleController roleController;
        private readonly IRoleRepository roleRepository;
        public RoleControllerTest(RoleController _roleController,IRoleRepository _roleRepository)
        {
            roleController = _roleController;
            roleRepository = _roleRepository;
        }

        [Fact]
        public async void Post_Test()
        {
            //Arrange
            Role newRole = new Role() { Id = 1, Name = "Administrador" };

            // Act
            var actionResult = await roleController.Post(newRole);
            var result = actionResult.Result as CreatedAtActionResult;

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(1, newRole.Id);
        }

        [Fact]
        public async void Get_Test()
        {
            //Arrange
            QueryParameters queryParameters = new QueryParameters() { currentPage = 1,pageSize=10 };

            // Act
            var actionResult = await roleController.Get(queryParameters);
            var actual = actionResult.Value;
            var expected = await roleRepository.Get(null, null, "", queryParameters.pageSize, queryParameters.currentPage-1);

            // Assert
            Assert.Equal(expected.Data.Count() , actual.Data.Count());
        }
    }
}
