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

        [HttpPost("search")]
        public List<Item> Get(Item item)
        {
            return _itemService.FindItem(item);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Item item)
        {
            return _itemService.AddItem(item) != null ? new ObjectResult(item) { StatusCode = StatusCodes.Status201Created } : new ObjectResult(item) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] Item item)
        {
            return _itemService.DeleteItem(item) != null ? new ObjectResult(item) { StatusCode = StatusCodes.Status200OK} : new ObjectResult(item) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpPut]
        public IActionResult Update([FromBody] Item item)
        {
            return _itemService.UpdateItem(item) != null ? new ObjectResult(item) { StatusCode = StatusCodes.Status200OK } : new ObjectResult(item) { StatusCode = StatusCodes.Status400BadRequest };

        }

    }
}
