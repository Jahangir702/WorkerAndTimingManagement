using Class_03_Practise_01.Models;

namespace Class_03_Practise_01.HostedServices
{
    public class DbInitializer
    {
        readonly WorkerDbContext db;
        public DbInitializer(WorkerDbContext db)
        {
            this.db = db;

        }
        public async Task SeedAsync()
        {
            if (!(await this.db.Database.CanConnectAsync()))
            {
                await this.db.Database.EnsureCreatedAsync();
            }
            if (!db.Workers.Any())
            {
                Worker w = new Worker { WorkerName = "Worker 1", Phone = "01932XXXXXX", Address = "Mirpur Section 1, Dhaka", PayRate = 2100.00M, Picture = "1.jpg" };
                w.WorkLogs.Add(new WorkLog { StartDate = DateTime.Today.AddDays(-14), EndDate = DateTime.Today.AddDays(-11) });
                w.WorkLogs.Add(new WorkLog { StartDate = DateTime.Today.AddDays(-7) });
                await this.db.Workers.AddAsync(w);
                await this.db.SaveChangesAsync();
            }
        }
    }
}
