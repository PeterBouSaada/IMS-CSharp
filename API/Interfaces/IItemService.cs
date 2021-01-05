using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;


namespace API.Interfaces
{
    public interface IItemService
    {
        Item AddItem(Item item);
        Item UpdateItem(Item item);
        Item DeleteItem(Item item);
        List<Item> FindItem(Item item);
    }
}
