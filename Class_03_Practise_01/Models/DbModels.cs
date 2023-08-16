using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Class_03_Practise_01.Models
{
    public class Worker
    {
        public int WorkerId { get; set; }
        [Required, StringLength(50), Display(Name = "Worker name")]
        public string WorkerName { get; set; } = default!;
        [Required, Column(TypeName = "money"), DataType(DataType.Currency)]
        public decimal PayRate { get; set; } = default!;
        [Required, StringLength(20)]
        public string Phone { get; set; } = default!;
        [Required, StringLength(100)]
        public string Address { get; set; } = default!;
        [Required, StringLength(50)]
        public string Picture { get; set; } = default!;
        public virtual ICollection<WorkLog> WorkLogs { get; set; } = new List<WorkLog>();
    }
    public class WorkLog
    {
        public int WorkLogId { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Strat Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date"), Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EndDate { get; set; }
        [Required]
        public int WorkerId { get; set; }
        [ForeignKey(nameof(WorkerId))]
        public virtual Worker Worker { get; set; } = default!;
    }
    public class WorkerDbContext : DbContext
    {
        public WorkerDbContext(DbContextOptions<WorkerDbContext> options) : base(options) { }
        public DbSet<Worker> Workers { get; set; } = default!;
        public DbSet<WorkLog> WorkLogs { get; set; } = default!;
    }
}
