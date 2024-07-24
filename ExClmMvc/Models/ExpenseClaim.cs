using System.ComponentModel.DataAnnotations;

namespace ExClmMvc.Models
{
    public class ExpenseClaim
    {
        [Key]
        public int ExpenseClaimId { get; set; }
        public int EmployeeId { get; set; }
        public int CategoryId { get; set; }
        public string? SubcategoryIds { get; set; }
        public decimal ClaimAmount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string? ExpenseLocation { get; set; }
        public string? BillAttachment { get; set; }
        public string? Remarks { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual ExpenseCategory? Category { get; set; }
        public string? SubcategoryNames { get; set; }
    }
}