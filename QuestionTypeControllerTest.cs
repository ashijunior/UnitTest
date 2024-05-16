using System;
using System.Collections.Generic;
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
    public class QuestionTypeControllerTests
    {
        [Fact]
        public async Task GetQuestionsByTypeId_ValidId_ReturnsOkObjectResult()
        {
            // Arrange
            var questionTypeId = 1;
            var expectedQuestions = new List<Question>
            {
                new Question { Id = 1, QuestionText = "Question 1", QuestTypeID = questionTypeId },
                new Question { Id = 2, QuestionText = "Question 2", QuestTypeID = questionTypeId }
            };

            var mockRepo = new Mock<IQuestionRepo>();
            mockRepo.Setup(repo => repo.GetQuestionsByTypeIdAsync(questionTypeId))
                .ReturnsAsync(expectedQuestions);

            var controller = new QuestionController(mockRepo.Object);

            // Act
            var result = await controller.GetQuestionsByTypeId(questionTypeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualQuestions = Assert.IsAssignableFrom<IEnumerable<Question>>(okResult.Value);
            Assert.Equal(expectedQuestions.Count, actualQuestions.Count());
        }

        [Fact]
        public async Task CreateQuestionType_ValidInput_ReturnsOkObjectResult()
        {
            // Arrange
            var questionTypeDTO = new QuestionTypeDTO { QuestionID = 1, QuestionTypeName = "Type 1" };
            var expectedQuestionType = new QuestionType { QuestionID = 1, QuestionTypeName = "Type 1" };

            var mockRepo = new Mock<IQuestionRepo>();
            mockRepo.Setup(repo => repo.CreateQuestionTypeAsync(It.IsAny<QuestionType>()))
                .ReturnsAsync(expectedQuestionType);

            var controller = new QuestionTypeController(mockRepo.Object);

            // Act
            var result = await controller.CreateQuestionType(questionTypeDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualQuestionType = Assert.IsType<QuestionType>(okResult.Value);
            Assert.Equal(expectedQuestionType.QuestionID, actualQuestionType.QuestionID);
            Assert.Equal(expectedQuestionType.QuestionTypeName, actualQuestionType.QuestionTypeName);
        }

        [Fact]
        public async Task UpdateQuestionType_ValidInput_ReturnsOkObjectResult()
        {
            // Arrange
            var questionTypeDTO = new QuestionTypeDTO { QuestionID = 1, QuestionTypeName = "Updated Type 1" };
            var existingQuestionType = new QuestionType { QuestionID = 1, QuestionTypeName = "Type 1" };
            var updatedQuestionType = new QuestionType { QuestionID = 1, QuestionTypeName = "Updated Type 1" };

            var mockRepo = new Mock<IQuestionRepo>();
            mockRepo.Setup(repo => repo.GetQuestionTypeByIdAsync(questionTypeDTO.QuestionID))
                .ReturnsAsync(existingQuestionType);
            mockRepo.Setup(repo => repo.UpdateQuestionTypeAsync(existingQuestionType))
                .ReturnsAsync(updatedQuestionType);

            var controller = new QuestionTypeController(mockRepo.Object);

            // Act
            var result = await controller.UpdateQuestionType(questionTypeDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualQuestionType = Assert.IsType<QuestionType>(okResult.Value);
            Assert.Equal(updatedQuestionType.QuestionID, actualQuestionType.QuestionID);
            Assert.Equal(updatedQuestionType.QuestionTypeName, actualQuestionType.QuestionTypeName);
        }

        [Fact]
        public async Task DeleteQuestionType_ValidId_ReturnsOkObjectResult()
        {
            // Arrange
            var questionTypeId = 1;
            var existingQuestionType = new QuestionType { QuestionID = questionTypeId, QuestionTypeName = "Type 1" };

            var mockRepo = new Mock<IQuestionRepo>();
            mockRepo.Setup(repo => repo.GetQuestionTypeByIdAsync(questionTypeId))
                .ReturnsAsync(existingQuestionType);
            mockRepo.Setup(repo => repo.DeleteQuestionTypeAsync(questionTypeId))
                .Returns(Task.CompletedTask);

            var controller = new QuestionTypeController(mockRepo.Object);

            // Act
            var result = await controller.DeleteQuestionType(questionTypeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var message = Assert.IsAssignableFrom<string>(okResult.Value);
            Assert.Equal("Question type deleted successfully.", message);
        }
    }
}
