using System.ComponentModel.DataAnnotations;

namespace Class_03_Practise_01.ViewModels
{
    public class WorkLogViewModel
    {
        public int WorkLogId { get; set; }
        [Display(Name = "Strat Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EndDate { get; set; }
        [Required]
        public int WorkerId { get; set; }
        [Display(Name = "Worker name")]
        public string? WorkerName { get; set; }
        public int? DayWorked
        {
            get
            {
                return this.EndDate.HasValue ? ((this.EndDate.Value - this.StartDate).Days + 1) : null;
            }
        }
        public decimal? PayRate { get; set; }
        public decimal? Payable
        {
            get
            {
                return this.EndDate != null ? ((this.EndDate.Value - this.StartDate).Days + 1) * this.PayRate : null;
            }
        }

    }
}
