namespace AwesomeDevEvents.API.Models
{
    public class DevEventSpeakerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TalkTitle { get; set; }
        public string TalkDescription { get; set; }
        public string LinkedinProfile { get; set; }
    }
}
