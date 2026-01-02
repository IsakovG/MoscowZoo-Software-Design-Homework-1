namespace ZooCorp
{
    public class VeterinaryClinic : IVeterinaryClinic
    {
        public bool CheckHealth(Animal animal)
        {
            // Здесь логика проверки здоровья. Вернем true для примера.
            return true;
        }
    }
}