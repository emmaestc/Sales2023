using Sales.shared.Entities;

namespace Sales.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            //Hace el update-database por codigo
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();

        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())  // Si no hay paises, ingresa
            {
                _context.Countries.Add(new Country { Name = "Colombia" });
                _context.Countries.Add(new Country { Name = "Estados Unidos" });
                _context.Countries.Add(new Country { Name = "perú" });
                await _context.SaveChangesAsync();
            }

        }
    }
}
