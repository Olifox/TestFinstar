using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TestFinstar.Models;

namespace TestFinstar.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBaseService<StoreDTO> _service;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _appEnvironment;

        public HomeController(IStoreService service, IWebHostEnvironment appEnvironment)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StoreModel, StoreDTO>();
                cfg.CreateMap<StoreDTO, StoreModel>();
            });
            _mapper = config.CreateMapper();

            _service = service;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(GetAll(null));
        }

        [HttpGet]
        public IEnumerable<StoreModel> GetAll(StoreFilterOption options)
        {
            var result = _mapper.Map<IEnumerable<StoreDTO>, IEnumerable<StoreModel>>(_service.GetAll());

            if (options != null)
                result = result
                    .Where(x => (options.Ids == null || (options.Ids != null && options.Ids.Contains(x.Id)))
                    && (options.Values == null || (options.Values != null && options.Values.Contains(x.Value)))).ToList();

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRange(IEnumerable<StoreModel> items)
        {
            try
            {

                _service.AddRange(_mapper.Map<IEnumerable<StoreModel>, IEnumerable<StoreDTO>>(items.OrderBy(x => x.Code)));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToPage("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                try
                {
                    using var fileStream = new StreamReader(uploadedFile.OpenReadStream());
                    var json = JsonSerializer.Deserialize<IEnumerable<StoreModel>>(fileStream.ReadToEnd());
                    return AddRange(json);
                }
                catch
                {
                    return RedirectToPage("Error");
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }

    public class StoreFilterOption
    {
        public List<int> Ids;
        public List<string> Values;
    }
}
