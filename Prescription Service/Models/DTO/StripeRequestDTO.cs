namespace Prescription_Service.Models.DTO
{
	public interface StripeRequestDTO
	{
		public string? StripeSessionUrl { get; set; }
		public string? StripeSessionId { get; set; }
		public string ApprovedUrl { get; set; }
		public string CancelUrl { get; set; }
		public Prescription Prescription { get; set; }
	}
}
