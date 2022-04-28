using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService) // injeção de dependência
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken] // anti-malware
        public IActionResult Create(Seller seller) // recebe o objeto que veio na requisição, portanto é só colocar como parâmetro
        {
            _sellerService.Insert(seller);

            return RedirectToAction(nameof(Index));
        }
        // GET
        public IActionResult Delete(int? id) // int opcional
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindByID(id.Value); // pra pegar o valor caso exista
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken] // anti-malware
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindByID(id.Value); // pra pegar o valor caso exista
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
    }
}
