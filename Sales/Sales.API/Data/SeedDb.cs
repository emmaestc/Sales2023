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

        public async Task SeedCTAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriesAsync();
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

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any()) 
            {
                _context.Categories.Add(new Category { Name = "Accesorios para vehiculos" });
                _context.Categories.Add(new Category { Name = "Agro" });
                _context.Categories.Add(new Category { Name = "Alimentos y Bebidas" });
                _context.Categories.Add(new Category { Name = "Animales y Mascotas" });
                _context.Categories.Add(new Category { Name = "Bebés" });
                _context.Categories.Add(new Category { Name = "Belleza y Cuidado ¨Personal" });
                _context.Categories.Add(new Category { Name = "Celulares y Telefonos" });
                _context.Categories.Add(new Category { Name = "Consolas y Videojuegos" });
                _context.Categories.Add(new Category { Name = "Construcción" });
                _context.Categories.Add(new Category { Name = "Deportes y Fitness" });
                _context.Categories.Add(new Category { Name = "Electrodomésticos" });
                _context.Categories.Add(new Category { Name = "Hogar y muebles" });
                _context.Categories.Add(new Category { Name = "Instrumentos musicales" });
                _context.Categories.Add(new Category { Name = "Inmuebles" });
                await _context.SaveChangesAsync();
            }

        }
    }
}
