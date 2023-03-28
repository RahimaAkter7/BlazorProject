using MasterDetail.Server.Models;
using MasterDetail.Server.Pages;
using MasterDetail.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace MasterDetail.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
  
    public class MasterDetailsController : ControllerBase
    {
        private readonly FlowerDbContext _context;
        private readonly IWebHostEnvironment _env;

        public MasterDetailsController(FlowerDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this._env = env;
        }

        [HttpGet]
        [Route("GetTypes")]
        public async Task<ActionResult<IEnumerable<BouquetType>>> GetTypes()
        {
            return await _context.BouquetTypes.ToListAsync();
        }

        [HttpGet]
        [Route("GetFlowers")]
        public async Task<ActionResult<IEnumerable<Flower>>> GetClients()
        {
            return await _context.Flowers.Include(c => c.StoreEntries).ThenInclude(b => b.BouquetType).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Flower> GetFlowerById(int id)
        {
            return await _context.Flowers.Where(x => x.FlowerId == id).Include(c => c.StoreEntries).ThenInclude(b => b.BouquetType).FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]FlowerVM flowerVM)
        {

            if (ModelState.IsValid)
            {
                Flower flower = new Flower()
                {
                    FlowerName = flowerVM.FlowerName,
                    StoreDate = flowerVM.StoreDate,
                    Quantity = flowerVM.Quantity,
                    StoreAvaible = flowerVM.StoreAvaible
                };

                //Image
                if (flowerVM.PictureFile is not null)
                {
                    string webroot = _env.WebRootPath;
                    string folder = "Images";
                    string imgFileName = Guid.NewGuid().ToString() + Path.GetExtension(flowerVM.PictureFile.FileName);
                    string fileToWrite = Path.Combine(webroot, folder, imgFileName);

                    using (var stream = new FileStream(fileToWrite, FileMode.Create))
                    {
                        await flowerVM.PictureFile.CopyToAsync(stream);
                        flower.Picture = imgFileName;
                    }
                }
                else
                {
                    flower.Picture = "default.jpg";
                }
                _context.Flowers.Add(flower);

                if (flowerVM.BouquetTypeList.Count() > 0)
                {
                    foreach (BouquetType type in flowerVM.BouquetTypeList)
                    {
                        _context.StoreEntries.Add(new StoreEntry
                        {
                            Flower = flower,
                            FlowerId = flower.FlowerId,
                            BouquetTypeId = type.BouquetTypeId
                        });
                    }
                }


                await _context.SaveChangesAsync();
                return Ok(flower);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] FlowerVM flowerVM)
        {

            if (ModelState.IsValid)
            {
                Flower flower = _context.Flowers.Find(flowerVM.FlowerId);
                flower.FlowerName = flowerVM.FlowerName;
                flower.StoreDate = flowerVM.StoreDate;
                flower.Quantity = flowerVM.Quantity;
                flower.StoreAvaible = flowerVM.StoreAvaible;

                //Image
                if (flowerVM.PictureFile is not null)
                {
                    string webroot = _env.WebRootPath;
                    string folder = "Images";
                    string imgFileName = Guid.NewGuid().ToString() + Path.GetExtension(flowerVM.PictureFile.FileName);
                    string fileToWrite = Path.Combine(webroot, folder, imgFileName);

                    using (var stream = new FileStream(fileToWrite, FileMode.Create))
                    {
                        await flowerVM.PictureFile.CopyToAsync(stream);
                        flower.Picture = imgFileName;
                    }
                }

                var existsBookings = _context.StoreEntries.Where(x => x.FlowerId == flowerVM.FlowerId).ToList();
                if (existsBookings is not null)
                {
                    foreach (var entry in existsBookings)
                    {
                        _context.Remove(entry);
                    }
                }
                

                if (flowerVM.BouquetTypeList.Count() > 0)
                {
                    foreach (BouquetType type in flowerVM.BouquetTypeList)
                    {
                        _context.StoreEntries.Add(new StoreEntry
                        {
                            FlowerId = flower.FlowerId,
                            BouquetTypeId= type.BouquetTypeId
                        });
                    }
                }

                _context.Entry(flower).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(flower);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            Flower flower = _context.Flowers.Find(id);

            var existsBookings = _context.StoreEntries.Where(x => x.FlowerId == flower.FlowerId).ToList();
            if (existsBookings is not null)
            {
                foreach (var entry in existsBookings)
                {
                    _context.Remove(entry);
                }
            }
            _context.Remove(flower);
            try
            {
                await _context.SaveChangesAsync();

                return new OkObjectResult(flower);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }



    }
}
