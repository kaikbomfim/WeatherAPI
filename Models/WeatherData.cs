namespace WeatherAPI.Models
{
    public class WeatherData
    {
        // Nome da cidade consultada
        public string? City { get; set; }
        // Temperatura em graus Celsius
        public string? Temperature { get; set; }
        // Descrição da condição climática (ex.: nuvens dispersas)
        public string? Condition { get; set; }
        // Umidade relativa do ar em porcentagem
        public string? Humidity { get; set; }
        // Velocidade do vento em m/s
        public string? WindSpeed { get; set; }
    }    
}


