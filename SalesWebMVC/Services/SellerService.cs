using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _context; // readonly para não alterar arquivo

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList(); // função sincrona por enquanto
        }
        public void Insert(Seller obj) // insert do seller
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
