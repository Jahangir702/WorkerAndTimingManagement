using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Class_03_Practise_01.ViewModels
{
    public class WorkLogInputModel
    {
        public int WorkLogId { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Strat Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date"), Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EndDate { get; set; }
        [Required]
        public int WorkerId { get; set; }
    }
}
