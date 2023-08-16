using System.ComponentModel.DataAnnotations;

namespace Class_03_Practise_01.ViewModels
{
    public class WorkerInputModel
    {
        public int WorkerId { get; set; }
        [Required, StringLength(50), Display(Name = "Worker name")]
        public string WorkerName { get; set; } = default!;
        [Required, DataType(DataType.Currency)]
        public decimal PayRate { get; set; } = default!;
        [Required, StringLength(20)]
        public string Phone { get; set; } = default!;
        [Required, StringLength(100)]
        public string Address { get; set; } = default!;
        [Required]
        public IFormFile Picture { get; set; } = default!;
    }
}
