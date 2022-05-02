using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;

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
            /* obj.Department = _context.Department.First(); // pega o id do department para usar como chave estrangeira. Não é mais necessário, pois agora já está instanciado com o department */
            _context.Add(obj);
            _context.SaveChanges();
        }
        public Seller FindByID(int id)
        {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
        public void Update(Seller obj)
        {
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException err) // interceptando uma possível exceção do banco e personalizando-a
            {
                throw new DbConcurrencyException(err.Message);
            }

        }
    }
}
