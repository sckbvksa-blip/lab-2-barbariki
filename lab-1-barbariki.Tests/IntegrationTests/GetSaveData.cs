namespace program_test.IntegrationTests;

using Runner;
using Delivery;
using DayData;
using DeliveryRepository;

[TestFixture]
public class GetSaveData
{
    private AppState appState;
    
    [SetUp]
    public void Setup()
    {
        appState = new AppState();
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
        Assert.That(deliveryRepository.deliveries.count, Is.EqualTo(0));
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
    
    [Test] // - integration test (edge-case test)
    public void Set_NextDayAndRead()
    {
        //Act
        appState.currentDay.AmountOfDepartured = 1;
        appState.NextDay();
        
        // Assert
        Assert.That(appState.GetDayData()[0].AmountOfDepartured, Is.EqualTo(1));
    }
}