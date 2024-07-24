using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExClmMvc.Models
{
    public class ExpenseCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}