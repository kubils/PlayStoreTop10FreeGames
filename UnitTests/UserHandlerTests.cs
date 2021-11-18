using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application;
using Application.Commands;
using Application.DTOs;
using Application.Handler;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Moq;
using Xunit;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace UnitTests
{
    public class UserHandlerTests : IClassFixture<CommonClassFixture>
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IConfiguration> _configuration;
        private readonly IMapper _mapper;
        private readonly Mock<IGetJwtToken> _tokenGenarator;

        public UserHandlerTests(CommonClassFixture classFixture)
        {
            _userRepository = new Mock<IUserRepository>();
            _mapper = classFixture.Mapper;
            _configuration = new Mock<IConfiguration>();
            _tokenGenarator = new Mock<IGetJwtToken>();
        }

        [Fact]
        public void UserData_Create_WithRequirements_Result_AssertTrue()
        {
            var createUserCommand = new CreateUserCommand() {
                Name = "aA",Surname = "sS",Email = "ks@gmail.com", Password = "ASDAasd1"
            };

            _userRepository.Setup(u => u.Add(It.IsAny<User>())).ReturnsAsync(
                new User()
                {
                    Name = createUserCommand.Name, Surname = createUserCommand.Surname, 
                    CreatedDate = DateTime.Now, Email = createUserCommand.Email, Password = createUserCommand.Password,
                    Id = ObjectId.GenerateNewId()
                });

            UserCommandHandler handler = new UserCommandHandler(_userRepository.Object, _mapper);

            var result = handler.Handle(createUserCommand, new CancellationToken()).Result;

            Assert.True(result);
        }

        [Fact]
        public async Task GetAllUser_UserData_Query()
        {
            var users = GetAllUsers();

            _userRepository.Setup(u => u.GetAll()).ReturnsAsync(users);

            var request = new GetUsersQuery();

            var handler =  new UserQueryHandler(_userRepository.Object, _mapper);

            var result = await handler.Handle(request, CancellationToken.None);
            
            Assert.Equal(users.Count, result.Count);
        }

        [Fact]
        public async Task User_LoginQuery()
        {
            var user = GetUser();
            var token = "token";
 
            _userRepository.Setup(u => u.Authenticate(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);
            _tokenGenarator.Setup(t => t.GenerateToken(user)).ReturnsAsync(token);

            var request = new LoginQuery();
            
            var handler =  new UserQueryHandler(_userRepository.Object, _mapper, _tokenGenarator.Object);

            var result =await handler.Handle(request, CancellationToken.None);
            
            Assert.Equal("token", result);
        }

        [Theory]
        [InlineData("","","ks", "")]
        [InlineData("A1","","ks", "")]
        [InlineData("A1","S1","ks", "")]
        [InlineData("A1","S1","ks@g.com", "")]
        [InlineData("A1","S1","ks", "A1bscsgh")]
        public void UserCreate_User_Is_Not_ValidModel(string name, string surname, string email, string password)
        {
            var createUserCommand = new CreateUserCommand() {
                Name = name,Surname = surname,Email = email, Password = password
            };

            var errors = ValidateModel(createUserCommand);
            
            Assert.NotEmpty(errors);
        }
        
        [Theory]
        [InlineData("A1","S1","ks@g.com", "scsgh")]
        [InlineData("A1","S1","ks@g.com", "A1")]
        [InlineData("A1","S1","ks@g.com", "asadasAAA")]
        [InlineData("A1","S1","ks@g.com", "asadas1111")]
        [InlineData("A1","S1","ks@g.com", "PassworD1ds must be at least 5 max 255 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)PassworD1ds must be at least 5 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public void UserCreate_Password_Is_Not_ValidModel(string name, string surname, string email, string password)
        {
            var createUserCommand = new CreateUserCommand() {
                Name = name,Surname = surname,Email = email, Password = password
            };

            var errors = ValidateModel(createUserCommand);

            
            Assert.True(errors.Count > 0);
            
            Assert.NotNull(errors[0].ErrorMessage);
            Assert.True(errors.Count(x => 
                    x.ErrorMessage
                        .Contains("Passwords must be at least 5 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)"))
                        > 0);
        }
        
        private User GetUser()
        {
            User user = new User()
            {
                Id = ObjectId.GenerateNewId(),
                Name = $"1.Name",
                Surname = $"1.SurName",
                Email = $"1@ma1l.com",
                Password = $"ab123aA",
                CreatedDate = DateTime.Now
            };
            return user;
        }
        private List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            for (int i = 0; i < 5; i++)
            {
                User user = new User();
                user.Id = ObjectId.GenerateNewId();
                user.Name = $"{i}.Name";
                user.Surname = $"{i}.SurName";
                user.Email = $"{i}@mail.com";
                user.Password = $"{i}{i}{i}{i}{i}{i}{i}";
                user.CreatedDate = DateTime.Now;
                users.Add(user);
            }

            return users;
        }
        
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}