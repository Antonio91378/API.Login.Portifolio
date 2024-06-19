using API.Login.Domain.Dtos.Request;
using API.Login.Domain.Dtos.Response;
using API.Login.Domain.Entities;
using API.Login.Domain.Interfaces.Email;
using API.Login.Infra.Users;
using API.Login.Service.Users;
using AutoBogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace API.Login.Service.Tests.Users
{
    public class UserServiceUnitTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly UserService _userService;

        public UserServiceUnitTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _emailServiceMock = new Mock<IEmailService>();
            _userService = new UserService(_userRepositoryMock.Object, _emailServiceMock.Object);
        }

        [Fact]
        public async Task RegisterUserAsync_ValidUser_ReturnsSuccess()
        {
            // Arrange
            var newUser = new AutoFaker<UserRegisterDto>().Generate();

            _userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(default(User));
            _userRepositoryMock.Setup(x => x.BeginTransactionAsync()).Returns(Task.FromResult(new Mock<IDbContextTransaction>().Object));
            _userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            _userRepositoryMock.Setup(x => x.CommitAsync(It.IsAny<IDbContextTransaction>())).Returns(Task.CompletedTask);
            _emailServiceMock.Setup(x => x.SendEmailAsync(It.IsAny<EmailRequest>())).ReturnsAsync(new Mock<ControllerMessenger>().Object);

            // Act
            var result = await _userService.RegisterUserAsync(newUser);
            var resultObject = result.ResponseObject as SuccessMessage;
            // Assert
            Assert.False(result.ErrorTriggered);
            Assert.Equal(201, result.StatusCode);
            Assert.NotNull(resultObject);
            Assert.Equal("Objeto criado com sucesso.", resultObject.Message);
        }

        [Fact]
        public async Task RegisterUserAsync_EmailAlreadyTaken_ReturnsBadRequest()
        {
            // Arrange
            var newUser = new AutoFaker<UserRegisterDto>().Generate();
            var existingUser = new AutoFaker<User>()
                .RuleFor(u => u.Email, newUser.Email) 
                .Generate();

            _userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(existingUser);

            // Act
            var result = await _userService.RegisterUserAsync(newUser);
            var resultObject = result.ResponseObject as ProblemDetails;

            // Assert
            Assert.True(result.ErrorTriggered);
            Assert.Equal(400, result.StatusCode);
            Assert.NotNull(resultObject);
            Assert.Equal("The informed email is already taken.", resultObject.Detail);
        }

        [Fact]
        public async Task RegisterUserAsync_EmailSendFails_PerformsRollback()
        {
            // Arrange
            var newUser = new AutoFaker<UserRegisterDto>().Generate();
            var mockTransaction = new Mock<IDbContextTransaction>();

            _userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(default(User));
            _userRepositoryMock.Setup(x => x.BeginTransactionAsync()).ReturnsAsync(mockTransaction.Object);
            _userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            // Simula falha no envio do e-mail
            _emailServiceMock.Setup(x => x.SendEmailAsync(It.IsAny<EmailRequest>()))
                .ReturnsAsync(new ControllerMessenger().ReturnInternalError500("Intern Error"));

            // Act
            var result = await _userService.RegisterUserAsync(newUser);
            var resultObject = result.ResponseObject as ProblemDetails;
            // Assert
            // Verifica se o rollback foi chamado devido à falha no envio do e-mail
            _userRepositoryMock.Verify(t => t.Rollback(mockTransaction.Object), Times.Once);



            // Verifica se o resultado indica falha no envio do e-mail
            Assert.NotNull(resultObject);
            Assert.True(result.ErrorTriggered);
            Assert.Equal(500, resultObject.Status);
        }
    }
}
