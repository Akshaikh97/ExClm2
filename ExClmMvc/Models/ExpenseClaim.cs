using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExClmMvc.Models
{
    public class ExpenseClaim
    {
        [Key]
        public int ExpenseClaimId { get; set; }

        [Required(ErrorMessage = "Employee is required.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "At least one subcategory must be selected.")]
        public string? SubcategoryIds { get; set; }

        [Required(ErrorMessage = "Claim amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Claim amount must be greater than zero.")]
        public decimal ClaimAmount { get; set; }

        [Required(ErrorMessage = "Expense date is required.")]
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set; }

        [StringLength(100, ErrorMessage = "Expense location cannot exceed 100 characters.")]
        public string? ExpenseLocation { get; set; }

        [DataType(DataType.Upload)]
        public string? BillAttachment { get; set; }

        [StringLength(500, ErrorMessage = "Remarks cannot exceed 500 characters.")]
        public string? Remarks { get; set; }

        //one to many
        public virtual Employee? Employee { get; set; }
        public virtual ExpenseCategory? Category { get; set; }
        public string? SubcategoryNames { get; set; }
    }
}