namespace Automation.Engine.Application.DataTransferObjects
{
    public class JobRequestDto
    {
        public string RobotId { get; set; } = string.Empty;
        public JobPayloadDto Payload { get; set; } = null!;
        public DateTime Timestamp { get; set; }
    }

}
