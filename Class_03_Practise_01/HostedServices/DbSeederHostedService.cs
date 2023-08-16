namespace Class_03_Practise_01.HostedServices
{
    public class DbSeederHostedService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        public DbSeederHostedService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<DbInitializer>();
            await seeder.SeedAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
