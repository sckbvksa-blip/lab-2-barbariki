namespace program_test.IntegrationTests;

using Runner;
using Delivery;
using Enums;

[TestFixture]
public class DeliveryTest
{
    private AppState appState;
    
    [SetUp]
    public void Setup()
    {
        appState = new AppState();
    }
    
    [Test] // - integration test
    public void Delete_Delivery()
    {
        // Arrange 
        string title = "Test";
        
        // Act
        appState.repository.deliveries.Add(new Delivery(title, 1));
        Delivery delivery = appState.FindDelivery(title);
        appState.DeleteDelivery(delivery);
        
        // Assert
        Assert.That(appState.repository.deliveries.count, Is.EqualTo(0));
    }
    
    [Test] // - integration test
    public void Find_DeliveryInDeliveriesTest_Returns_Delivery()
    {
        // Arrange 
        string DeliveryInDeliveriesTitle = "Test";
        string DeliveryInDeparturedTitle = "Test1";
        string DeliveryInDeliveredTitle = "Test2";
        
        // Act
        appState.repository.deliveries.Add(new Delivery(DeliveryInDeliveriesTitle, 1));
        appState.repository.departured.Add(new Delivery(DeliveryInDeparturedTitle, 1));
        appState.repository.delivered.Add(new Delivery(DeliveryInDeliveredTitle, 1));
        Delivery delivery1 = appState.FindDelivery(DeliveryInDeliveriesTitle);
        Delivery delivery2 = appState.FindDelivery(DeliveryInDeparturedTitle);
        Delivery delivery3 = appState.FindDelivery(DeliveryInDeliveredTitle);
        
        
        // Assert
        Assert.That(delivery1.title, Is.EqualTo(DeliveryInDeliveriesTitle));
        Assert.That(delivery2.title, Is.EqualTo(DeliveryInDeparturedTitle));
        Assert.That(delivery3.title, Is.EqualTo(DeliveryInDeliveredTitle));
    }
    
    [Test] // - integration test (edge-case test)
    public void Find_DeliveryTest_Returns_Null()
    {
        // Arrange 
        string title = "Test";
        
        // Act
        appState.repository.deliveries.Add(new Delivery(title, 1));
        Delivery delivery = appState.FindDelivery("421");
        
        // Assert
        Assert.That(delivery, Is.EqualTo(null));
    }
}