namespace ZooCorp
{
    public abstract class Thing : IInventory
    {
        public int Number { get; internal set; }
        public string Name { get; set; }

        public Thing(string name)
        {
            Name = name;
        }
    }

    public class Table : Thing
    {
        public Table() : base("Стол") { }
    }

    public class Computer : Thing
    {
        public Computer() : base("Компьютер") { }
    }
}