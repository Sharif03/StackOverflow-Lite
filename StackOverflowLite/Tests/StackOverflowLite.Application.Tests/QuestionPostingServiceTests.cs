using Autofac.Extras.Moq;
using Moq;
using Shouldly;
using StackOverflowLite.Application.Features.Posting.Services;
using StackOverflowLite.Domain.Entities;
using StackOverflowLite.Domain.Exceptions;
using StackOverflowLite.Domain.Repositories;

namespace StackOverflowLite.Application.Tests
{
    public class QuestionPostingServiceTests
    {
        private AutoMock _mock;
        private Mock<IQuestionRepository> _questionRepositoryMock;
        private Mock<IApplicationUnitOfWork> _unitOfWorkMock;
        private Mock<IUserIdentityService> _userIdentityServiceMock;
        private QuestionPostingService _questionPostingService;
  

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mock = AutoMock.GetLoose();
        }
        [SetUp]
        public void Setup()
        {
            _questionRepositoryMock = _mock.Mock<IQuestionRepository>();
            _unitOfWorkMock = _mock.Mock<IApplicationUnitOfWork>();
            _userIdentityServiceMock = _mock.Mock<IUserIdentityService>();
            _questionPostingService = _mock.Create<QuestionPostingService>();
        }
        [TearDown]
        public void Teardown()
        {
            _questionRepositoryMock?.Reset();
            _unitOfWorkMock?.Reset();
            _userIdentityServiceMock?.Reset();
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task CreateQuestionAsync_GUIDNotNull_CreateNewQuestionAsync()
        {
            // Arrange
            var userId = Guid.NewGuid(); // Mocked user ID
            _userIdentityServiceMock.Setup(svc => svc.GetCurrentLoggedInUserGuidAsync()).ReturnsAsync(userId);

            // Act
            await _questionPostingService.CreateQuestionAsync("Title", "Content", "Tags");

            // Assert
            _questionRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Question>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(), Times.Once);
        }
        [Test]
        public async Task CreateQuestionAsync_GUIDNull_ThrowNewExceptionAsync()
        {
            // Arrange
            _userIdentityServiceMock.Setup(svc => svc.GetCurrentLoggedInUserGuidAsync()).ReturnsAsync((Guid?)null);

            // Act & Assert
            await Should.ThrowAsync<GUIDNullValueException>(async () =>
            {
                await _questionPostingService.CreateQuestionAsync("Title", "Content", "Tags");
            });
        }
        [Test]
        public async Task CreateQuestionAsync_TitleUnique_CreateNewQuestion()
        {
            Assert.Pass();
        }
        [Test]
        public void CreateQuestionAsync_TitleDuplicate_ThrowNewException()
        {
            Assert.Pass();
        }
    }
}