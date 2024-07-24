using System.ComponentModel.DataAnnotations;

namespace ExClmMvc.Models
{
    public class ExpenseSubcategory
    {
        [Key]
        public int SubcategoryId { get; set; }
        public int CategoryId { get; set; }
        public string? SubcategoryName { get; set; }

        public virtual ExpenseCategory? Category { get; set; }
    }
}