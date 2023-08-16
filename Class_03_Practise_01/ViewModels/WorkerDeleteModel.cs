using System.ComponentModel.DataAnnotations;

namespace Class_03_Practise_01.ViewModels
{
    public class WorkerDeleteModel
    {
        public int WorkerId { get; set; }
        public string WorkerName { get; set; } = default!;
        public decimal PayRate { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public IFormFile Picture { get; set; } = default!;
    }
}
