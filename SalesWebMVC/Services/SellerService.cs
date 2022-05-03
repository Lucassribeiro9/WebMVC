﻿using SalesWebMVC.Models;
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
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync(); // função sincrona por enquanto
        }
        public async Task InsertAsync(Seller obj) // insert do seller
        {
            /* obj.Department = _context.Department.First(); // pega o id do department para usar como chave estrangeira. Não é mais necessário, pois agora já está instanciado com o department */
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Seller> FindByIDAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }

        }
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException err) // interceptando uma possível exceção do banco e personalizando-a
            {
                throw new DbConcurrencyException(err.Message);
            }

        }
    }
}
