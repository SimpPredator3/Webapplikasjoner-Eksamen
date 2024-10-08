
using MyPetShop.Models;

namespace MyPetShop.ViewModels
{
    public class ItemsViewModel
    {
        public IEnumerable<Item> Items;
        public string? CurrentViewName;

        public ItemsViewModel(IEnumerable<Item> items, string? currentViewName)
        {
            Items = items;
            CurrentViewName = currentViewName;
        }
    }
}
