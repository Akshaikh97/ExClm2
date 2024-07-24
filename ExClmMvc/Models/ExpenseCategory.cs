using System.ComponentModel.DataAnnotations;

namespace ExClmMvc.Models
{
    public class ExpenseCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}