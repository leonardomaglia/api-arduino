namespace api_arduino.Models
{
    public class Schedules
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string Time { get; set; }
        public string JobId { get; set; }
    }
}