using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WeatherAPI.Models;

namespace WeatherAPI.Controllers
{
    public class WeatherController : Controller
    {
        private readonly HttpClient _httpClient; // Cliente HTTP para chamar a API externa
        private readonly string? _apiKey;        // Chave da API lida da configuração/env

        public WeatherController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient; // Injeção de dependência do HttpClient
            _apiKey = configuration["WeatherApi:ApiKey"]; // Busca a chave no appsettings/env
        }

        public async Task<IActionResult> Index(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return View(); // Renderiza a busca vazia
            }

            var weatherData = await GetWeatherData(city); // Chama a API e mapeia o resultado

            if (weatherData == null)
            {
                ViewBag.ErrorMessage = "Erro ao buscar dados do tempo"; // Exibe erro para o usuário
                return View(); // Volta para a view sem modelo
            }

            return View(weatherData); // Exibe os dados do clima
        }

        private async Task<WeatherData> GetWeatherData(string city)
        {
            try
            {
                // Monta a URL com cidade, chave, unidades em °C e idioma PT-BR
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric&lang=pt_br";

                var response = await _httpClient.GetStringAsync(url); // Chamada HTTP GET

                if (response.Contains("city not found"))
                {
                    return null!; // Cidade não encontrada
                }

                var data = JObject.Parse(response); // Faz o parse do JSON

                var weather = new WeatherData
                {
                    City = data["name"]?.ToString(),                 // Nome da cidade
                    Temperature = data["main"]?["temp"]?.ToString(), // Temperatura em °C
                    Condition = data["weather"]?[0]?["description"]?.ToString(), // Descrição
                    Humidity = data["main"]?["humidity"]?.ToString(), // Umidade %
                    WindSpeed = data["wind"]?["speed"]?.ToString()    // Vento m/s
                };

                return weather; // Retorna o modelo preenchido
            } catch {
                return null!; // Em caso de erro, retorna nulo
            }
           
        }
    }
}
