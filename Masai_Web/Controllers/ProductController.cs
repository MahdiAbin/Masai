using Core.DTOs.Product;
using Data.Entities.Product;
using Data.Repository;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Masai_Web.Controllers;

public class ProductController : Controller
{
    IWebHostEnvironment environment;
    IProductService _productService;
    IMapper _mapper;    

    public ProductController(IProductService productService, IWebHostEnvironment environment, IMapper mapper)
    {
        _productService = productService;
        this.environment = environment;
        _mapper = mapper;
    }

    // GET
    public async Task<IActionResult> SingleProduct(int id)
    {
        var product =await _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> AddProduct()
    {
        return View();
    }
    
    
[HttpPost]
    public async Task<IActionResult> AddProduct(AddProductDTO dto)
    {
        if (ModelState.IsValid)
        {
            var map = _mapper.Map<Product>(dto);
            if (dto.ImageName != null && dto.ImageName.Length > 0)
            {
                var ms=new MemoryStream();
                dto.ImageName.CopyTo(ms);
                var bytes = ms.ToArray();
                var wwroot=environment.WebRootPath;
                var path = "Images/Products/";
                var filename = Guid.NewGuid().ToString().Substring(0, 10)+"jpg";
                ms.Seek(0, SeekOrigin.Begin);
                var fullpath = Path.Combine(wwroot, path);
                var fullname = Path.Combine(fullpath, filename);
                Directory.CreateDirectory(fullpath);
                System.IO.File.WriteAllBytes(fullname,bytes);
                map.ImageName = fullname;
            }
           var id =await _productService.AddProduct(map);
            return RedirectToAction("SingleProduct", "Product",new {id=id});
        }
        return View(dto);
    }

    public async Task<IActionResult> ShowImage(int ID)
    {
        var product = await _productService.GetProductById(ID);
        var wwroot = environment.WebRootPath;
        var path = "Images/Products/";
        var fullpath=Path.Combine(wwroot,path,product.ImageName);
        var bytes=System.IO.File.ReadAllBytes(fullpath);
        
        return File(bytes,"image/jpeg");
    }
}