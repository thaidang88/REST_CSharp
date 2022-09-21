using System;
using Xunit;
using Moq;


namespace Catalog.UnitTests
{
    public class ItemControllerTest
    {
        [Fact]
        public void UnitOfWork_StateUnderTest_ExpectedBehavior()
        {

        }

         [Fact]
        public void GetItemAsync_WithUnexistingItem_ReturnsNotFound()
        {
            //Arrange
            var repository = new Mock<IMemRepositoriesAsync>();
            repository.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>())).ReturnAsync((Item)null);

            var controller= new ItemsasyncMongoController(repository.Object);
            //Act

            var result = await(controller.GetItemAsync(Guid.NewGuid()));
            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }
    }
}
