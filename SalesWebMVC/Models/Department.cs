using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>(); // implementando associação entre seller e department

        public Department()
        {
        }

        public Department(int id, string name) // não incluir collections
        {
            Id = id;
            Name = name;
        }
        void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }
        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial,final)); // soma todas as vendas de todos os vendedores do departamento
        }
    }
}
