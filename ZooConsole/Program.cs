using Microsoft.Extensions.DependencyInjection;

namespace ZooCorp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IVeterinaryClinic, VeterinaryClinic>()
                .AddSingleton<ZooService>()
                .BuildServiceProvider();

            var zoo = serviceProvider.GetRequiredService<ZooService>();

            bool running = true;
            while (running)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n--- ERP СИСТЕМА МОСКОВСКОГО ЗООПАРКА ---");
                Console.ResetColor();
                Console.WriteLine("1. Добавить животное");
                Console.WriteLine("2. Добавить вещь (Стол/Компьютер)");
                Console.WriteLine("3. Отчет по еде (кг/день)");
                Console.WriteLine("4. Список для контактного зоопарка");
                Console.WriteLine("5. Вывести весь инвентарь");
                Console.WriteLine("6. Выход");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Выберите пункт: ");
                Console.ResetColor();
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddAnimalMenu(zoo);
                        break;
                    case "2":
                        AddThingMenu(zoo);
                        break;
                    case "3":
                        Console.WriteLine($"\nВсего требуется еды: {zoo.CalculateTotalFood()} кг в день.");
                        break;
                    case "4":
                        var contactAnimals = zoo.GetContactZooAnimals();
                        Console.WriteLine("\nЖивотные для контактного зоопарка:");
                        if (!contactAnimals.Any()) Console.WriteLine("Нет подходящих животных.");
                        foreach (var a in contactAnimals)
                        {
                            Console.WriteLine($"- {a.Name} (№{a.Number}) | Доброта: {a.Kindness}");
                        }
                        break;
                    case "5":
                        var items = zoo.GetInventoryList();
                        Console.WriteLine("\nВесь инвентарь:");
                        foreach (var item in items)
                        {
                            string info = "Неизвестно";

                            if (item is Animal animal)
                            {
                                string species = animal switch
                                {
                                    Monkey => "Обезьяна",
                                    Rabbit => "Кролик",
                                    Tiger => "Тигр",
                                    Wolf => "Волк",
                                    _ => "Животное"
                                };


                                info = $"{species} - {animal.Name}";
                            }
                            else if (item is Thing thing)
                            {
                                info = $"Вещь - {thing.Name}";
                            }

                            Console.WriteLine($"№{item.Number}: {info}");
                        }
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }
        }

        static void AddAnimalMenu(ZooService zoo)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nКого добавляем?");
            Console.ResetColor();
            Console.WriteLine("1. Обезьяна");
            Console.WriteLine("2. Кролик");
            Console.WriteLine("3. Тигр");
            Console.WriteLine("4. Волк");
            var type = Console.ReadLine();

            // Спрашиваем имя
            Console.Write("Введите кличку животного: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = "Животное" + DateTime.Now.Ticks;

            Console.Write("Количество еды (кг/сутки): ");
            if (!int.TryParse(Console.ReadLine(), out int food)) return;

            Animal animal;
            if (type == "1" || type == "2")
            {
                Console.Write("Уровень доброты (0-10): ");
                if (!int.TryParse(Console.ReadLine(), out int kindness)) return;

                if (type == "1") animal = new Monkey(name, food, kindness);
                else animal = new Rabbit(name, food, kindness);
            }
            else if (type == "3") animal = new Tiger(name, food);
            else if (type == "4") animal = new Wolf(name, food);

            else
            {
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("[X] Неверный тип, животное не добавлено.");
                Console.ResetColor();
                return;
            }
            Console.ResetColor();

            zoo.AddAnimal(animal);
        }

        static void AddThingMenu(ZooService zoo)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nЧто добавляем?");
            Console.ResetColor();
            Console.WriteLine("1. Стол");
            Console.WriteLine("2. Компьютер");
            var type = Console.ReadLine();

            Thing thing;
            if (type == "1") thing = new Table();
            else if (type == "2") thing = new Computer();
            else { Console.WriteLine("Неверный тип."); return; }

            zoo.AddThing(thing);
        }
    }
}