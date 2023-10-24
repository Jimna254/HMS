namespace DoctorApplication.Model.DTO_s
{
	public class ResponseDTO
	{
		public bool IsSuccess { get; set; }

		public string Message { get; set; }

		public object? Result { get; set; }
	}
}
