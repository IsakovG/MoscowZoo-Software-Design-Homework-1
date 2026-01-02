namespace ZooCorp
{
    // Базовый класс
    public abstract class Animal : IAlive, IInventory
    {
        public int Food { get; protected set; }
        public int Number { get; internal set; }
        public string Name { get; protected set; }

        protected Animal(string name, int food)
        {
            Name = name;
            Food = food;
        }
    }

    // Травоядные
    public abstract class Herbo : Animal
    {
        public int Kindness { get; set; }

        protected Herbo(string name, int food, int kindness)
            : base(name, food)
        {
            Kindness = kindness;
        }
    }

    // Хищники
    public abstract class Predator : Animal
    {
        protected Predator(string name, int food)
            : base(name, food) { }
    }

    // Конкретные виды
    public class Monkey : Herbo
    {
        public Monkey(string name, int food, int kindness)
            : base(name, food, kindness) { }
    }

    public class Rabbit : Herbo
    {
        public Rabbit(string name, int food, int kindness)
            : base(name, food, kindness) { }
    }

    public class Tiger : Predator
    {
        public Tiger(string name, int food)
            : base(name, food) { }
    }

    public class Wolf : Predator
    {
        public Wolf(string name, int food)
            : base(name, food) { }
    }
}