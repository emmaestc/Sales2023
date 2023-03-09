using Microsoft.EntityFrameworkCore;
using Sales.API.Services;
using Sales.shared.Entities;
using Sales.shared.Responses;

namespace Sales.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;

        public SeedDb(DataContext context, IApiService apiService)
        {
            _context = context;
            _apiService = apiService;
        }

        public async Task SeedAsync()
        {
            //Hace el update-database por codigo
            await _context.Database.EnsureCreatedAsync();
            //await CheckCountriesAsync();
        }

        public async Task SeedCTAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriesAsync();
        }


        private async Task CheckCountriesAsync()
        {
            //if (!_context.Countries.Any())
            //{
                Response responseCountries = await _apiService.GetListAsync<CountryResponse>("/v1", "/countries");
                if (responseCountries.IsSuccess)
                {
                    List<CountryResponse> countries = (List<CountryResponse>)responseCountries.Result!;
                    foreach (CountryResponse countryResponse in countries)
                    {
                        Country country = await _context.Countries!.FirstOrDefaultAsync(c => c.Name == countryResponse.Name!)!;
                        if (country == null)
                        {
                            country = new() { Name = countryResponse.Name!, States = new List<State>() };
                            Response responseStates = await _apiService.GetListAsync<StateResponse>("/v1", $"/countries/{countryResponse.Iso2}/states");
                            if (responseStates.IsSuccess)
                            {
                                List<StateResponse> states = (List<StateResponse>)responseStates.Result!;
                                foreach (StateResponse stateResponse in states!)
                                {
                                    State state = country.States!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
                                    if (state == null)
                                    {
                                        state = new() { Name = stateResponse.Name!, Cities = new List<City>() };
                                        Response responseCities = await _apiService.GetListAsync<CityResponse>("/v1", $"/countries/{countryResponse.Iso2}/states/{stateResponse.Iso2}/cities");
                                        if (responseCities.IsSuccess)
                                        {
                                            List<CityResponse> cities = (List<CityResponse>)responseCities.Result!;
                                            foreach (CityResponse cityResponse in cities)
                                            {
                                                if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
                                                {
                                                    continue;
                                                }
                                                City city = state.Cities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
                                                if (city == null)
                                                {
                                                    state.Cities.Add(new City() { Name = cityResponse.Name! });
                                                }
                                            }
                                        }
                                        if (state.CitiesNumber > 0)
                                        {
                                            country.States.Add(state);
                                        }
                                    }
                                }
                            }
                            if (country.StatesNumber > 0)
                            {
                                _context.Countries.Add(country);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            //}
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
