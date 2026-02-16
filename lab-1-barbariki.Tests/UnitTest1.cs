namespace program_test;

using Runner;
using Delivery;
using Enums;
using DayData;
using DeliveryRepository;

public class Tests
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
        int initialCount = appState.repository.deliveries.Count;

        // Act
        appState.repository.deliveries.Add(new Delivery("Test", 1));

        // Assert
        Assert.That(appState.repository.deliveries.Count, Is.EqualTo(initialCount + 1));
        Assert.That(appState.repository.deliveries[initialCount].title, Is.EqualTo("Test"));
    }

    [Test] // - integration test
    public void Delete_Delivery()
    {
        // Arrange 
        string title = "Test";
        
        // Act
        appState.repository.deliveries.Add(new Delivery(title, 1));
        Delivery delivery = appState.FindDelivery(title);
        appState.AddOrDeleteDelivery(delivery, DeliveryAction.Delete);
        
        // Assert
        Assert.That(appState.repository.deliveries.Count, Is.EqualTo(0));
    }

    [Test] // - unit test
    public void QuickSortByPrioritySortsCorrectly()
    {
        // Arrange
        List<Delivery> deliveries = new List<Delivery>
        {
            new Delivery("Low", 3),
            new Delivery("High", 1),
            new Delivery("Medium", 2)
        };

        // Act
        appState.QuickSortByPriority(deliveries, 0, deliveries.Count - 1);

        // Assert
        Assert.That(deliveries[0].title, Is.EqualTo("High"));
        Assert.That(deliveries[1].title, Is.EqualTo("Medium"));
        Assert.That(deliveries[2].title, Is.EqualTo("Low"));
    }
    
    [Test] // - unit test (edge-case test)
    public void QuickSortByPrioritySortsCorrectly_When_ListIsEmpty()
    {
        // Arrange
        List<Delivery> deliveries = new List<Delivery>();

        // Act
        appState.QuickSortByPriority(deliveries, 0, deliveries.Count - 1);

        // Assert
        Assert.That(deliveries.Count, Is.EqualTo(0));
    }
    
    [Test] // - unit test (edge-case test)
    public void QuickSortByPrioritySortsCorrectly_When_InListOnlyOneElement()
    {
        // Arrange
        List<Delivery> deliveries = new List<Delivery> { new Delivery("Low", 3) };

        // Act
        appState.QuickSortByPriority(deliveries, 0, deliveries.Count - 1);

        // Assert
        Assert.That(deliveries.Count, Is.EqualTo(1));
    }

    [Test] // - integration test
    public void Find_DeliveryTest_Returns_Delivery()
    {
        // Arrange 
        string title = "Test";
        
        // Act
        appState.repository.deliveries.Add(new Delivery(title, 1));
        Delivery delivery = appState.FindDelivery(title);
        
        // Assert
        Assert.That(delivery.title, Is.EqualTo(title));
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
    
    [Test] // - integration test
    public void Save_RepositoryDataInFile()
    {
        
        // Act
        appState.repository.deliveries.Add(new Delivery("Test", 1));
        appState.SaveRepositoryData();
        
        // Assert
        Assert.That(File.Exists(appState.repositoryFileName), Is.True);
        Assert.That(appState.GetRepositoryData().deliveries[0].title, Is.EqualTo("Test"));
    }
    
    [Test] // - integration test
    public void Save_DayDataInFile()
    {
        
        // Act
        appState.currentDay.AmountOfDepartured = 1;
        appState.SaveDayData();
        
        // Assert
        Assert.That(File.Exists(appState.dayFileName), Is.True);
        Assert.That(appState.GetDayData()[0].AmountOfDepartured, Is.EqualTo(1));
    }

    [Test] // - integration test
    public void Get_RepositoryData_Returns_List()
    {
        
        // Act
        appState.repository.deliveries.Add(new Delivery("Test", 1));
        appState.SaveRepositoryData();
        DeliveryRepository deliveryRepository = appState.GetRepositoryData();
        
        // Assert
        Assert.That(deliveryRepository.deliveries[0].title, Is.EqualTo("Test"));
    }
    
    [Test] // - integration test (edge-case test)
    public void Get_RepositoryData_Returns_EmptyList()
    {

        // Act
        appState.EnsureDataDirectoryExists();
        File.WriteAllText(appState.repositoryFileName, "");
        appState.SaveRepositoryData();
        DeliveryRepository deliveryRepository = appState.GetRepositoryData();
        
        // Assert
        Assert.That(deliveryRepository.deliveries.Count, Is.EqualTo(0));
    }
    
    [Test] // - integration test
    public void Get_DayData_Returns_List()
    {
        
        // Act
        appState.currentDay.AmountOfDepartured = 1;
        appState.SaveDayData();
        List<DayData> daysStorage = appState.GetDayData();
        
        // Assert
        Assert.That(daysStorage[0].AmountOfDepartured, Is.EqualTo(1));
    }
    
    [Test] // - integration test (edge-case test)
    public void Get_DayData_Returns_List_When_DayData_DoesntExist()
    {
        
        // Act
        appState.EnsureDataDirectoryExists();
        File.WriteAllText(appState.dayFileName, "");
        List<DayData> daysStorage = appState.GetDayData();
        
        // Assert
        Assert.That(daysStorage.Count, Is.EqualTo(0));
    }

    [Test] // - unit test
    public void EnsureDataDirCreated()
    {
        // Act
        appState.EnsureDataDirectoryExists();
        
        // Assert
        Assert.That(Directory.Exists(appState.dataDirectory), Is.True);

    }
    

    [Test] // - unit test
    public void Set_NextDay()
    {
        
        //Act
        appState.NextDay();
        
        // Assert
        Assert.That(appState.currentDay.DayCounter, Is.EqualTo(2));
    }
    
    [Test] // - unit test (edge-case test)
    public void Set_NextDayAndRead()
    {
        
        //Act
        appState.currentDay.AmountOfDepartured = 1;
        appState.NextDay();
        
        // Assert
        Assert.That(appState.GetDayData()[0].AmountOfDepartured, Is.EqualTo(1));
    }
    
    [TearDown]
    public void CleanUp()
    {
        var dataDir = Path.Combine(AppContext.BaseDirectory, "Data");

        if (Directory.Exists(dataDir))
        {
            foreach (var file in Directory.GetFiles(dataDir))
            {
                File.Delete(file);
            }

            Directory.Delete(dataDir, true);
        }
    }
}
