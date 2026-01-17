namespace API.DTOs {
    public class CreateCriminalRequest {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int RiskId { get; set; }
        public string? Image { get; set; }
        public DateTime CriminalSince { get; set; }
    }
}
