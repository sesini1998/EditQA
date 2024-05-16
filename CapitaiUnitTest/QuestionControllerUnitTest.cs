using CapitalTest.Controllers;
using CapitalTest.DTOs;
using CapitalTest.IServices;
using CapitalTest.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CapitalTest.CapitaiUnitTest
{
    public class QuestionControllerUnitTest
    {
        private readonly Mock<IQuestionService> _questionServiceMock = new Mock<IQuestionService>();
        private readonly Mock<ILogger<QuestionController>> _loggerMock = new Mock<ILogger<QuestionController>>();

        [Fact]
        public async Task AddQuestion_ValidModel_ReturnsCreatedAtAction()
        {
            // Arrange
            var controller = new QuestionController(_questionServiceMock.Object, _loggerMock.Object);
            var questionDto = new CreateQuestionDto { QuestionTitle = "Sample Title" };
            var question = new Questions { Id = Guid.NewGuid(), QuestionTitle = "Sample Title", QuestionType = QuestionType.Paragraph };
            _questionServiceMock.Setup(x => x.AddQuestion(It.IsAny<CreateQuestionDto>())).ReturnsAsync(question);

            // Act
            var result = await controller.AddQuestion(questionDto) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(nameof(QuestionController.GetQuestionById), result.ActionName);
            Assert.NotNull(result.RouteValues["id"]);
            Assert.Equal(question.Id, result.RouteValues["id"]);
        }

        [Fact]
        public async Task AddQuestion_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var controller = new QuestionController(_questionServiceMock.Object, _loggerMock.Object);
            controller.ModelState.AddModelError("QuestionTitle", "Question title is required");

            // Act
            var result = await controller.AddQuestion(new CreateQuestionDto { QuestionTitle = "Sample Title" }) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task DeleteQuestion_ValidId_ReturnsOk()
        {
            // Arrange
            var controller = new QuestionController(_questionServiceMock.Object, _loggerMock.Object);
            var questionId = Guid.NewGuid();
            _questionServiceMock.Setup(x => x.DeleteQuestion(questionId)).ReturnsAsync(true);

            // Act
            var result = await controller.DeleteQuestion(questionId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public async Task GetQuestionsByType_ValidType_ReturnsOk()
        {
            // Arrange
            var controller = new QuestionController(_questionServiceMock.Object, _loggerMock.Object);
            var questionType = QuestionType.Paragraph;
            var questions = new List<Questions> { new Questions {
                Id = Guid.NewGuid(), QuestionTitle = "Sample Title1", QuestionType = QuestionType.Paragraph },
                new Questions{Id = Guid.NewGuid(), QuestionTitle = "Sample Title2", QuestionType = QuestionType.Date } };
            _questionServiceMock.Setup(x => x.GetQuestionsByType(questionType)).ReturnsAsync(questions);

            // Act
            var result = await controller.GetQuestionsByType(questionType) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(questions, result.Value);
        }

        [Fact]
        public async Task GetQuestionById_ValidId_ReturnsOk()
        {
            // Arrange
            var controller = new QuestionController(_questionServiceMock.Object, _loggerMock.Object);
            var questionId = Guid.NewGuid();
            var question = new Questions { Id = questionId, QuestionTitle = "Sample Title", QuestionType = QuestionType.Paragraph };
            _questionServiceMock.Setup(x => x.GetQuestionById(questionId)).ReturnsAsync(question);

            // Act
            var result = await controller.GetQuestionById(questionId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(question, result.Value);
        }

        [Fact]
        public async Task UpdateQuestion_ValidModel_ReturnsOk()
        {
            // Arrange
            var controller = new QuestionController(_questionServiceMock.Object, _loggerMock.Object);
            var questionId = Guid.NewGuid();
            var updateDto = new UpdateQuestionDto { QuestionTitle = "Sample Title" };
            var updatedQuestion = new Questions { Id = questionId, QuestionTitle = "Sample Title", QuestionType = QuestionType.Paragraph };
            _questionServiceMock.Setup(x => x.UpdateQuestion(updateDto, questionId)).ReturnsAsync(updatedQuestion);

            // Act
            var result = await controller.UpdateQuestion(updateDto, questionId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(updatedQuestion, result.Value);
        }
    }
}
