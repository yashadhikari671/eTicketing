using eTicketing.Data;
using eTicketing.Data.Services;
using eTicketing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTicketing.Controllers
{
    public class ProducerController : Controller
    {
        private readonly IProducerService _service;

        public ProducerController(IProducerService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allproducer = await _service.GetAllAsync();
            return View(allproducer);
        }
        public async Task<IActionResult> Details(int id)
        {
            var producerDetail = await _service.GetByIdAsync(id);
            if(producerDetail == null) return View("Not Found");
            return View(producerDetail);
        }
        //get/producer/create
        public IActionResult Create()
        {
           return View();
        }
        [HttpPost,ActionName("Create")]
        public async Task<IActionResult> CreateProducer([Bind("ProfilePictureUrl,ProducerName,Bio")]Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);
            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index)); 
        }
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetail = await _service.GetByIdAsync(id);
            if (producerDetail == null) return View("Not found");
            return View(producerDetail);
        }
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditProducer(int id,[Bind("Id,ProfilePictureUrl,ProducerName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);
            if(id == producer.Id)
            {
                await _service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
            
            return View(producer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producerDetail = await _service.GetByIdAsync(id);
            if (producerDetail == null) return View("Not found");
            return View(producerDetail);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProducer(int id)
        {
            var producerdetail = await _service.GetByIdAsync(id);
            if (producerdetail == null) return View("Not Found");
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));

        } 
    }
}
