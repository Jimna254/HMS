using Newtonsoft.Json;
using RecordsManagement.Models.DTO;
using System.Collections.Generic;

namespace RecordsManagement.Services
{
	public class PrescriptionService : IPrescriptionInterface
	{
		private readonly IHttpClientFactory _httpClientFactory;
        public PrescriptionService(IHttpClientFactory httpClientFactory)
        {
			_httpClientFactory = httpClientFactory;
            
        }
        public async Task<List<Prescription>> GetPrescriptionbyId(Guid id)
		{
			try
			{
				var client = _httpClientFactory.CreateClient("Prescription");
				var response = await client.GetAsync("/api/Prescription/{id}");
				var content = await response.Content.ReadAsStringAsync();
				var responseDto = JsonConvert.DeserializeObject<ResponseDTO>(content);

				if (responseDto.IsSuccess)
				{
					return JsonConvert.DeserializeObject<List<Prescription>>(Convert.ToString(responseDto.Result));
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return new List<Prescription>();

		}
		
	}
}
