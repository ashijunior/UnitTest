using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UnitPractical.Controllers;
using UnitPractical.DTO;
using UnitPractical.Model;
using UnitPractical.Repository.Interface;
using Xunit;

namespace UnitPractical.Tests
{
    public class UserInfoControllerTests
    {
        [Fact]
        public async Task GetUserInfo_ValidId_ReturnsOkObjectResult()
        {
            // Arrange
            var userId = 1;
            var userInfo = new UserInfo
            {
                ID = 1,
                firstName = "John",
                lastName = "Doe",
                email = "john.doe@example.com",
                phoneNumber = "1234567890",
                nationality = "USA",
                currentResidence = "New York",
                idNumber = 123,
                dateOfBirth = "1990-01-01",
                gender = "Male",
                personalInfo = "Some personal info",
                gradYear = "2010",
                multipleChoices = "true",
                rejection = false,
                yearExperience = 5,
                dateOfRelocation = 2022 - 01 - 01
            };

            var mockRepo = new Mock<IUserInfoRepo>();
            mockRepo.Setup(repo => repo.GetUserInfoByIdAsync(userId))
                .ReturnsAsync(userInfo);

            var controller = new UserInfoController(mockRepo.Object);

            // Act
            var result = await controller.GetUserInfo(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUserInfo = Assert.IsType<UserInfoDTO>(okResult.Value);
            Assert.Equal(userInfo.ID, actualUserInfo.ID);
            Assert.Equal(userInfo.firstName, actualUserInfo.firstName);
            // Add more assertions for other properties
        }

        [Fact]
        public async Task CreateUserInfo_ValidInput_ReturnsOkObjectResult()
        {
            // Arrange
            var userInfoDTO = new UserInfoDTO
            {
                ID = 1,
                firstName = "John",
                lastName = "Doe",
                email = "john.doe@example.com",
                phoneNumber = "1234567890",
                nationality = "USA",
                currentResidence = "New York",
                idNumber = 123,
                dateOfBirth = "1990-01-01",
                gender = "Male",
                personalInfo = "Some personal info",
                gradYear = "2010",
                multipleChoices = "true",
                rejection = false,
                yearExperience = 5,
                dateOfRelocation = 2022-01-01
            };
            var addedUserInfo = new UserInfo
            {
                ID = 1,
                firstName = "John",
                lastName = "Doe",
                email = "john.doe@example.com",
                phoneNumber = "1234567890",
                nationality = "USA",
                currentResidence = "New York",
                idNumber = 123,
                dateOfBirth = "1990-01-01",
                gender = "Male",
                personalInfo = "Some personal info",
                gradYear = "2010",
                multipleChoices = "true",
                rejection = false,
                yearExperience = 5,
                dateOfRelocation = 2022 - 01 - 01
            };

            var mockRepo = new Mock<IUserInfoRepo>();
            mockRepo.Setup(repo => repo.AddUserInfoAsync(It.IsAny<UserInfo>()))
                .ReturnsAsync(addedUserInfo);

            var controller = new UserInfoController(mockRepo.Object);

            // Act
            var result = await controller.CreateUserInfo(userInfoDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUserInfo = Assert.IsType<UserInfo>(okResult.Value);
            Assert.Equal(addedUserInfo.ID, actualUserInfo.ID);
            Assert.Equal(addedUserInfo.firstName, actualUserInfo.firstName);
            // Add more assertions for other properties
        }

        [Fact]
        public async Task UpdateUserInfo_ValidInput_ReturnsOkObjectResult()
        {
            // Arrange
            var userInfoDTO = new UserInfoDTO
            {
                ID = 1,
                firstName = "Updated John",
                // Update other properties as needed
            };
            var existingUser = new UserInfo
            {
                ID = 1,
                firstName = "John",
                // Set other properties accordingly
            };
            var updatedUser = new UserInfo
            {
                ID = 1,
                firstName = "Updated John",
                // Set other properties accordingly
            };

            var mockRepo = new Mock<IUserInfoRepo>();
            mockRepo.Setup(repo => repo.GetUserInfoByIdAsync(userInfoDTO.ID))
                .ReturnsAsync(existingUser);
            mockRepo.Setup(repo => repo.UpdateUserAsync(It.IsAny<UserInfo>()))
                .ReturnsAsync(updatedUser);

            var controller = new UserInfoController(mockRepo.Object);

            // Act
            var result = await controller.UpdateUserInfo(userInfoDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUserInfo = Assert.IsType<UserInfo>(okResult.Value);
            Assert.Equal(updatedUser.ID, actualUserInfo.ID);
            Assert.Equal(updatedUser.firstName, actualUserInfo.firstName);
            // Add more assertions for other properties
        }

        [Fact]
        public async Task DeleteUserInfo_ValidId_ReturnsOkObjectResult()
        {
            // Arrange
            var userId = 1;
            var existingUser = new UserInfo
            {
                ID = 1,
                firstName = "John",
                lastName = "Doe",
                email = "john.doe@example.com",
                phoneNumber = "1234567890",
                nationality = "USA",
                currentResidence = "New York",
                idNumber = 123,
                dateOfBirth = "1990-01-01",
                gender = "Male",
                personalInfo = "Some personal info",
                gradYear = "2010",
                multipleChoices = "true",
                rejection = false,
                yearExperience = 5,
                dateOfRelocation = 2022 - 01 - 01
            };

            var mockRepo = new Mock<IUserInfoRepo>();
            mockRepo.Setup(repo => repo.GetUserInfoByIdAsync(userId))
                .ReturnsAsync(existingUser);
            mockRepo.Setup(repo => repo.DeleteUserAsync(userId))
                .Returns(Task.CompletedTask);

            var controller = new UserInfoController(mockRepo.Object);

            // Act
            var result = await controller.DeleteUserInfo(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var message = Assert.IsType<string>(okResult.Value);
            Assert.Equal("User deleted successfully.", message);
        }
    }
}
