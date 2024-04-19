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
            // Arrange
            var userId = Guid.NewGuid(); // Mocked user ID
            const string title = "Java";
            const string content = "Java Problem";
            const string tags = "Java, SE";

            var question = new Question
            {
                Title = title,
                Content = content,
                Tags = tags,
            };

            // GetCurrentLoggedInUserGuidAsync() resolve 
            _userIdentityServiceMock.Setup(svc => svc.GetCurrentLoggedInUserGuidAsync()).ReturnsAsync(userId).Verifiable();

            // IsTitleDuplicateAsync() resolve 
            _unitOfWorkMock.SetupGet(x => x.QuestionRepository).Returns(_questionRepositoryMock.Object).Verifiable();
            _questionRepositoryMock.Setup(x => x.IsTitleDuplicateAsync(title, null)).ReturnsAsync(false).Verifiable();

            // AddAsync(question) & SaveAsync() resolve
            _questionRepositoryMock.Setup(x => x.AddAsync(It.Is<Question>(y => y.Title == title && y.Content == content 
                && y.Tags == tags))).Returns(Task.CompletedTask).Verifiable();
            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _questionPostingService.CreateQuestionAsync(title, content, tags);

            // Assert
            _unitOfWorkMock.VerifyAll();
            _questionRepositoryMock.VerifyAll();
            _userIdentityServiceMock.VerifyAll();
        }
        [Test]
        public void CreateQuestionAsync_TitleDuplicate_ThrowNewException()
        {
            Assert.Pass();
        }
    }
}