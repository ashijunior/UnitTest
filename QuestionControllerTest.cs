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
    public class QuestionControllerTests
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
            var actualQuestions = Assert.IsAssignableFrom<List<Question>>(okResult.Value);
            Assert.Equal(expectedQuestions.Count, actualQuestions.Count);
        }

        [Fact]
        public async Task CreateQuestion_ValidInput_ReturnsOkObjectResult()
        {
            // Arrange
            var questionDTO = new QuestionDTO { QuestTypeID = 1, QuestionText = "New Question" };
            var expectedQuestion = new Question { Id = 1, QuestTypeID = 1, QuestionText = "New Question" };

            var mockRepo = new Mock<IQuestionRepo>();
            mockRepo.Setup(repo => repo.CreateQuestionAsync(questionDTO))
                .ReturnsAsync(expectedQuestion);

            var controller = new QuestionController(mockRepo.Object);

            // Act
            var result = await controller.CreateQuestion(questionDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualQuestion = Assert.IsType<Question>(okResult.Value);
            Assert.Equal(expectedQuestion.Id, actualQuestion.Id);
            Assert.Equal(expectedQuestion.QuestTypeID, actualQuestion.QuestTypeID);
            Assert.Equal(expectedQuestion.QuestionText, actualQuestion.QuestionText);
        }

        [Fact]
        public async Task UpdateQuestion_ValidInput_ReturnsOkObjectResult()
        {
            // Arrange
            var questionId = 1;
            var questionDTO = new QuestionDTO { QuestTypeID = 1, QuestionText = "Updated Question" };
            var existingQuestion = new Question { Id = questionId, QuestTypeID = 1, QuestionText = "Original Question" };
            var updatedQuestion = new Question { Id = questionId, QuestTypeID = 1, QuestionText = "Updated Question" };

            var mockRepo = new Mock<IQuestionRepo>();
            mockRepo.Setup(repo => repo.GetQuestionByIdAsync(questionId))
                .ReturnsAsync(existingQuestion);
            mockRepo.Setup(repo => repo.UpdateQuestionAsync(existingQuestion))
                .ReturnsAsync(updatedQuestion);

            var controller = new QuestionController(mockRepo.Object);

            // Act
            var result = await controller.UpdateQuestion(questionId, questionDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualQuestion = Assert.IsType<Question>(okResult.Value);
            Assert.Equal(updatedQuestion.Id, actualQuestion.Id);
            Assert.Equal(updatedQuestion.QuestTypeID, actualQuestion.QuestTypeID);
            Assert.Equal(updatedQuestion.QuestionText, actualQuestion.QuestionText);
        }

        [Fact]
        public async Task DeleteQuestion_ValidId_ReturnsOkObjectResult()
        {
            // Arrange
            var questionId = 1;

            var mockRepo = new Mock<IQuestionRepo>();
            mockRepo.Setup(repo => repo.DeleteQuestionAsync(questionId))
                .Returns(Task.CompletedTask);

            var controller = new QuestionController(mockRepo.Object);

            // Act
            var result = await controller.DeleteQuestion(questionId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var message = Assert.IsType<string>(okResult.Value);
            Assert.Equal("Question deleted successfully.", message);
        }
    }
}
