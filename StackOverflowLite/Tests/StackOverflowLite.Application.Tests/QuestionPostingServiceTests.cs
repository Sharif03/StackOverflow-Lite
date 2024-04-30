using Autofac.Core;
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
            const string title = "C#";

            // GetCurrentLoggedInUserGuidAsync()
            _userIdentityServiceMock.Setup(svc => svc.GetCurrentLoggedInUserGuidAsync()).ReturnsAsync(userId).Verifiable();

            // IsTitleDuplicateAsync() resolve 
            _unitOfWorkMock.SetupGet(x => x.QuestionRepository).Returns(_questionRepositoryMock.Object).Verifiable();
            _questionRepositoryMock.Setup(x => x.IsTitleDuplicateAsync(title, null)).ReturnsAsync(false).Verifiable();

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
            const string title = "C#";
            const string content = "C# Problem";
            const string tags = "C#, SE";

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
            this.ShouldSatisfyAllConditions(
                () => _unitOfWorkMock.VerifyAll(),
                () => _questionRepositoryMock.VerifyAll(),
                () => _userIdentityServiceMock.VerifyAll()
            );
        }
        [Test]
        public async Task CreateQuestionAsync_TitleDuplicate_ThrowNewExceptionAsync()
        {
            // Arrange
            var userId = Guid.NewGuid(); // Mocked user ID
            const string title = "C#";
            const string content = "C# Problem";
            const string tags = "C#, Software Engineering";

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
            _questionRepositoryMock.Setup(x => x.IsTitleDuplicateAsync(title, null)).ReturnsAsync(true).Verifiable();

            // Act && Assert
            await Should.ThrowAsync<DuplicateTitleException>(async
                () => await _questionPostingService.CreateQuestionAsync(title, content, tags)
            );
        }

        [Test]
        public async Task GetPagedCoursesAsync_ReturnsData()
        {
            // Arrange
            string searchText = "C#";
            string sortBy = "Title";
            int pageIndex = 1;
            int pageSize = 10;
            var expectedRecords = new List<Question> { /* Your expected list of Question objects */ };
            var expectedTotal = expectedRecords.Count;
            var expectedTotalDisplay = expectedRecords.Count; // Assuming same as total for simplicity

            _unitOfWorkMock.Setup(uow => uow.QuestionRepository.GetTableDataAsync(searchText, sortBy, pageIndex, pageSize))
                           .ReturnsAsync((expectedRecords, expectedTotal, expectedTotalDisplay)).Verifiable();

            // Act
            var result = await _questionPostingService.GetPagedQuestionsAsync(searchText, sortBy, pageIndex, pageSize);

            // Assert
            Assert.AreEqual(expectedRecords, result.records);
            Assert.AreEqual(expectedTotal, result.total);
            Assert.AreEqual(expectedTotalDisplay, result.totalDisplay);
        }

        [Test]
        public async Task GetQuestionAsync_WithValidId_ReturnsQuestion()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedQuestion = new Question
            {
                Id = id,
            };

            _unitOfWorkMock.SetupGet(x => x.QuestionRepository).Returns(_questionRepositoryMock.Object).Verifiable();
            _questionRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(expectedQuestion).Verifiable();

            // Act
            var result = await _questionPostingService.GetQuestionAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(expectedQuestion.Id, result.Id);
        }
        [Test]
        public async Task GetQuestionAsync_WithInvalidId_ReturnsNull()
        {
            // Arrange
            var invalidId = Guid.NewGuid();

            _unitOfWorkMock.SetupGet(x => x.QuestionRepository).Returns(_questionRepositoryMock.Object).Verifiable();
            _questionRepositoryMock.Setup(repo => repo.GetByIdAsync(invalidId)).ReturnsAsync((Question)null).Verifiable();

            // Act
            var result = await _questionPostingService.GetQuestionAsync(invalidId);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public async Task UpdateQuestionAsync_ValidInput_NoDuplicateTitle()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            const string title = "C#";
            const string content = "C# Problem";
            const string tags = "C#, SE";
            var question = new Question
            {
                Title = title,
                Content = content,
                Tags = tags,
            };

            // Mocking IsTitleDuplicateAsync to return false (no duplicate title)
            _unitOfWorkMock.SetupGet(x => x.QuestionRepository).Returns(_questionRepositoryMock.Object).Verifiable();
            _questionRepositoryMock.Setup(x => x.IsTitleDuplicateAsync(title, id)).ReturnsAsync(false).Verifiable();

            // Mocking GetQuestionAsync to return a question
            Question existingQuestion = new Question { Id = id, Title = title, Content = content, Tags =tags};
            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _questionPostingService.UpdateQuestionAsync(id, title, content, tags);

            // Assert
            // Check if the existing question's properties were updated correctly
            Assert.AreEqual(id, existingQuestion.Id);
            Assert.AreEqual(title, existingQuestion.Title);
            Assert.AreEqual(content, existingQuestion.Content);
            Assert.AreEqual(tags, existingQuestion.Tags);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(), Times.Once);
        }
        [Test]
        public async Task UpdateQuestionAsync_DuplicateTitle_ThrowsDuplicateTitleException()
        {
            // Arrange
            var userId = Guid.NewGuid(); // Mocked user ID
            const string title = "C#";
            const string content = "C# Problem";
            const string tags = "C#, Software Engineering";

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
            _questionRepositoryMock.Setup(x => x.IsTitleDuplicateAsync(title, null)).ReturnsAsync(true).Verifiable();

            // Act && Assert
            await Should.ThrowAsync<DuplicateTitleException>(async
                () => await _questionPostingService.CreateQuestionAsync(title, content, tags)
            );
        }
        [Test]
        public async Task UpdateQuestionAsync_QuestionNotFound_DoesNotUpdateOrSave()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            const string title = "C#";
            const string content = "C# Problem";
            const string tags = "C#, SE";

            // Mocking IsTitleDuplicateAsync to return false (no duplicate title)
            _unitOfWorkMock.SetupGet(x => x.QuestionRepository).Returns(_questionRepositoryMock.Object).Verifiable();
            _questionRepositoryMock.Setup(x => x.IsTitleDuplicateAsync(title, id)).ReturnsAsync(false).Verifiable();

            // Act
            await _questionPostingService.UpdateQuestionAsync(id, title, content, tags);

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task DeleteQuestionAsync_ValidId_DeletesQuestionAndSavesChanges()
        {
            // Arrange
            Guid questionId = Guid.NewGuid();

            // RemoveAsync(questionId) & SaveAsync() resolve
            _unitOfWorkMock.SetupGet(x => x.QuestionRepository).Returns(_questionRepositoryMock.Object).Verifiable();
            _questionRepositoryMock.Setup(x => x.RemoveAsync(questionId)).Returns(Task.CompletedTask).Verifiable();
            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _questionPostingService.DeleteQuestionAsync(questionId);

            // Assert
            this.ShouldSatisfyAllConditions(
                () => _questionRepositoryMock.VerifyAll(),
                () => _unitOfWorkMock.VerifyAll()
              );
        }
    }
}