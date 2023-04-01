namespace api_arduino.Models
{
    public class SaveSettingsDTO
    {
        public int HumidityLevel { get; set; }
        public List<string> Schedules { get; set; }
    }
}