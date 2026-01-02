using System;
using System.Collections.Generic;
using System.Linq;

namespace ZooCorp
{
    public class ZooService
    {
        private readonly IVeterinaryClinic _vetClinic;
        private readonly List<IInventory> _inventory = [];

        // Счетчик для автоматических ID
        private int _nextId = 1;

        public ZooService(IVeterinaryClinic vetClinic)
        {
            _vetClinic = vetClinic;
        }

        public bool AddAnimal(Animal animal)
        {
            if (_vetClinic.CheckHealth(animal))
            {
                // Автоматически присваиваем номер
                animal.Number = _nextId++;

                _inventory.Add(animal);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[V] {animal.Name} принят(а) в зоопарк. Присвоен номер: {animal.Number}");
                Console.ResetColor();
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[X] {animal.Name} не прошел медосмотр (отказ ветклиники).");
                Console.ResetColor();
                return false;
            }
        }

        public void AddThing(Thing thing)
        {
            // Автоматически присваиваем номер
            thing.Number = _nextId++;

            _inventory.Add(thing);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[V] {thing.Name} добавлен на баланс. Присвоен номер: {thing.Number}");
            Console.ResetColor();
        }

        public int CalculateTotalFood()
        {
            return _inventory.OfType<IAlive>().Sum(x => x.Food);
        }

        public List<Herbo> GetContactZooAnimals()
        {
            return _inventory.OfType<Herbo>().Where(h => h.Kindness > 5).ToList();
        }

        public List<IInventory> GetInventoryList()
        {
            return _inventory;
        }
    }
}