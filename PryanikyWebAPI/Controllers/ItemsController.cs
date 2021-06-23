using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PryanikyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        static List<Items> itemsList = new List<Items>();
        // GET: api/Items
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(itemsList);
        }
        // POST: api/Items
        [HttpPost]
        public string Post([FromBody] string item)
        {
            if (item == string.Empty)
            {
                return "Bad request";
            }
            if (itemsList.Where(x => x.name == item).Count() != 0)
            {
                int index = itemsList.FindIndex(x => x.name == item);
                itemsList[index] = new Items(item, itemsList[index].quantity + 1);
            }
            else
            {
                itemsList.Add(new Items(item, 1));
            }
            return JsonConvert.SerializeObject(itemsList);
        }

        // DELETE: api/ApiWithActions/
        [HttpDelete]
        public string Delete([FromBody] string item)
        {
            if (item == string.Empty)
            {
                itemsList.Clear();
            }
            if (itemsList.Where(x => x.name == item).Count() != 0)
            {
                int index = itemsList.FindIndex(x => x.name == item);
                itemsList.RemoveAt(index);
            }
            return JsonConvert.SerializeObject(itemsList);
        }

        public struct Items
        {
            public string name;
            public int quantity;
            public Items(string name, int quantity)
            {
                this.name = name;
                this.quantity = quantity;
            }
        }
    }
}
