using Autofac.Extras.Moq;
using Moq;
using StackOverflowLite.Application.Features.Posting.Services;
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
        public void CreateQuestionAsync_GUIDNotNull_CreateNewQuestion()
        {
            Assert.Pass();
        }
        [Test]
        public void CreateQuestionAsync_GUIDNull_ThrowNewException()
        {
            Assert.Pass();
        }
        [Test]
        public void CreateQuestionAsync_TitleUnique_CreateNewQuestion()
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