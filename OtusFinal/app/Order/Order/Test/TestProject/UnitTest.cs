
using Application.EventHandler;
using Application.Interfaces;
using Application.Interfaces.Broker;
using Domen.Aggregate;
using Domen.Aggregate.Events;
using Domen.ValueObject;
using Microsoft.Extensions.Logging;
using Moq;

namespace TestProject
{
    public class UnitTest
    {
        [Fact]
        public async Task Handle_ShouldAddEventAndSendMessage()
        {

            // Arrange
            var mockRepository = new Mock<IRepositoryEvent<EventsOrder>>();
            var mockSendMessage = new Mock<ISendMessage>();
            var mockLogger = new Mock<ILogger<WithdrawedNotMoneyEventHandler>>();

            mockRepository
                .Setup(r => r.AddAsync(It.IsAny<EventsOrder>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new EventsOrder());
            var handler = new WithdrawedNotMoneyEventHandler(mockLogger.Object, mockRepository.Object, mockSendMessage.Object);

            var testEvent = new WithdrawedMoneyNotEvent(1,new Price(100), Guid.NewGuid(), 5);

            // Act
            await handler.Handle(testEvent, CancellationToken.None);

            // Assert
            mockSendMessage.Verify(s => s.PublishMessage(
                    testEvent.UserId,
                    testEvent.Price.Value,
                    "mailOrder",
                    It.Is<string>(msg => msg.Contains($"Order оформлен не успешно. Событие WithdrawedMoneyNotEvent")),
                    false), Times.Once);

        }

        [Fact]
        public void Constructor_ValidPrice_SetsValue()
        {
            // Arrange
            float validPrice = 123.45f;

            // Act
            var price = new Price(validPrice);

            // Assert
            Assert.Equal(validPrice, price.Value);
        }

        [Theory]
        [InlineData(-1)]      // меньше нуля
        [InlineData(-0.01f)]  // меньше нуля
        public void Constructor_NegativePrice_ThrowsArgumentException(float invalidPrice)
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Price(invalidPrice));
            Assert.Equal("Price cannot be negative. (Parameter 'price')", ex.Message);
        }

        [Theory]
        [InlineData(0.001f)]
        [InlineData(123.456f)] // больше 2 знаков после запятой
        public void Constructor_PriceWithTooManyDecimalPlaces_ThrowsArgumentException(float invalidPrice)
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Price(invalidPrice));
            Assert.Equal("Price cannot have more than 2 decimal places.", ex.Message);
        }
    }
}
