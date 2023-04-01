namespace api_arduino.Models
{
    public class SaveSettingsDTO
    {
        public int HumidityTrigger { get; set; }
        public List<string> Schedules { get; set; }
    }
}