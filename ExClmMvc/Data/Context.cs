using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExClmMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace ExClmMvc.Data
{
     public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<ExpenseSubcategory> ExpenseSubcategories { get; set; }
        public DbSet<ExpenseClaim> ExpenseClaims { get; set; }
    }
}