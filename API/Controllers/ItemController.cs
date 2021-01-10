using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("API/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public IActionResult Get(Item item)
        {
            Item foundItem = _itemService.FindOneItem(item.id);
            return foundItem != null ? new ObjectResult(foundItem) { StatusCode = StatusCodes.Status200OK } : new ObjectResult(foundItem) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpPost("search")]
        public IActionResult Search(Item item)
        {
            List<Item> items = _itemService.FindItem(item);
            return items != null ? new ObjectResult(items) { StatusCode = StatusCodes.Status200OK } : new ObjectResult(items) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpPost]
        public IActionResult Add([FromBody] Item item)
        {
            Item addedItem = _itemService.AddItem(item);
            return addedItem != null ? new ObjectResult(addedItem) { StatusCode = StatusCodes.Status201Created } : new ObjectResult(addedItem) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] Item item)
        {
            Item deletedItem = _itemService.DeleteItem(item);
            return deletedItem != null ? new ObjectResult(deletedItem) { StatusCode = StatusCodes.Status200OK} : new ObjectResult(deletedItem) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpPut]
        public IActionResult Update([FromBody] Item item)
        {
            Item updatedItem = _itemService.UpdateItem(item);
            return updatedItem != null ? new ObjectResult(updatedItem) { StatusCode = StatusCodes.Status200OK } : new ObjectResult(updatedItem) { StatusCode = StatusCodes.Status400BadRequest };
        }

    }
}
