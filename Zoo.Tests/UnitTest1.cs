using ZooCorp;

namespace Zoo.Tests
{
    public class MockVetClinic : IVeterinaryClinic
    {
        public bool ResultToReturn { get; set; } = true;
        public bool CheckHealth(Animal animal)
        {
            return ResultToReturn;
        }
    }

    public class ZooServiceTests
    {
        [Fact]
        public void AddAnimal_Healthy_ShouldAssignNumber()
        {
            // Arrange
            var vet = new MockVetClinic { ResultToReturn = true };
            var zoo = new ZooService(vet);
            var monkey = new Monkey("Чичи", 10, 7);

            // Act
            bool result = zoo.AddAnimal(monkey);

            // Assert
            Assert.True(result);
            Assert.Single(zoo.GetInventoryList());
            Assert.Equal(1, monkey.Number); // Проверяем, что номер присвоился
        }

        [Fact]
        public void AddAnimal_Sick_ShouldNotAddToInventory()
        {
            // Arrange
            var vet = new MockVetClinic { ResultToReturn = false };
            var zoo = new ZooService(vet);
            var tiger = new Tiger("Шерхан", 20);

            // Act
            bool result = zoo.AddAnimal(tiger);

            // Assert
            Assert.False(result);
            Assert.Empty(zoo.GetInventoryList());
        }

        [Fact]
        public void CalculateTotalFood_ShouldSumFoodCorrectly()
        {
            var vet = new MockVetClinic { ResultToReturn = true };
            var zoo = new ZooService(vet);

            zoo.AddAnimal(new Monkey("Бобо", 5, 5)); // 5 кг
            zoo.AddAnimal(new Tiger("Ричард", 10));  // 10 кг
            zoo.AddThing(new Table());               // Вещи еду не едят

            int total = zoo.CalculateTotalFood();

            Assert.Equal(15, total);
        }

        [Fact]
        public void GetContactZooAnimals_ShouldReturnOnlyKindHerbos()
        {
            var vet = new MockVetClinic();
            var zoo = new ZooService(vet);

            zoo.AddAnimal(new Monkey("Добрая", 5, 8)); // Доброта 8 (>5) -> Да
            zoo.AddAnimal(new Rabbit("Злой", 2, 3));   // Доброта 3 (<5) -> Нет
            zoo.AddAnimal(new Wolf("Волк", 10));       // Хищник -> Нет

            var contactList = zoo.GetContactZooAnimals();

            Assert.Single(contactList);
            Assert.Equal("Добрая", contactList.First().Name);
        }
    }
}