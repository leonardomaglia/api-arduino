namespace api_arduino.DTOs
{
    public class SettingsDTO
    {
        public int HumidityTrigger { get; set; }
        public List<string> Schedules { get; set; }
    }
}