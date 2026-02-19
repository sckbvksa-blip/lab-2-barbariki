namespace program_test.UnitTests;

using Runner;
using Delivery;

[TestFixture]
public class DeliveryTest
{
    private AppState appState;
    
    [SetUp]
    public void Setup()
    {
        appState = new AppState();
    }

    [Test] // - unit test
    public void Create_DeliveryAndAddsToRepository()
    {
        // Arrange
        int initialCount = appState.repository.deliveries.count;

        // Act
        appState.repository.deliveries.Add(new Delivery("Test", 1));

        // Assert
        Assert.That(appState.repository.deliveries.count, Is.EqualTo(initialCount + 1));
        Assert.That(appState.repository.deliveries[initialCount].title, Is.EqualTo("Test"));
    }
}