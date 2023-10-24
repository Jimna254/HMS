using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prescription_Service.Data;
using Prescription_Service.Models;
using Prescription_Service.Models.DTO;
using Stripe;
using Stripe.Checkout;

namespace Prescription_Service.Service
{
    public class PrescriptionService : IPrescriptionService
	{
		private readonly IMapper _mapper;
		private readonly AppDbContext _context;
        public PrescriptionService(IMapper mapper, AppDbContext appDbContext)
        {
			_mapper = mapper;
			_context = appDbContext;
            
        }
        public async Task<string> CreatePrescription(Prescription prescription)
		{
			_context.Prescriptions.Add(prescription);
			await _context.SaveChangesAsync();
			return "Prescription Created Successfully";
		}

		public async Task<string> DeletePrescription(Prescription prescription)
		{
			_context.Prescriptions.Remove(prescription);
			await _context.SaveChangesAsync();
			return "Prescription Deleted Succesfully";

		}

		public async Task <List<Prescription>> GetAllPrescriptions()
		{
			return await _context.Prescriptions.ToListAsync();

		}

		public async Task<Prescription> GetPrescription(Guid id)
		{
			return await _context.Prescriptions.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<Prescription> GetPrescriptionbyAppointment(Guid appointmentid)
		{
			return await _context.Prescriptions.FirstOrDefaultAsync(p => p.AppointmentId == appointmentid);
		}

		public async Task<string> UpdatePrescription(Prescription prescription)
		{
			_context.Prescriptions.Update(prescription);
			await _context.SaveChangesAsync();
			return "Prescription Updated Succesfully";
		}
		public async Task<StripeRequestDTO> StripePayment(StripeRequestDTO stripeRequestDto)
		{
			var options = new SessionCreateOptions()
			{
				SuccessUrl = stripeRequestDto.ApprovedUrl,
				CancelUrl = stripeRequestDto.CancelUrl,
				Mode = "payment",
				LineItems = new List<SessionLineItemOptions>()
			};


			
			var sessionLineItem = new SessionLineItemOptions()
			{
				PriceData = new SessionLineItemPriceDataOptions()
				{
					UnitAmount = (long)(stripeRequestDto.Prescription.Price * 100),
					Currency = "kes",

					ProductData = new SessionLineItemPriceDataProductDataOptions()
					{
						Name = stripeRequestDto.Prescription.Illness
					},


				},
				Quantity = 1
			};
			options.LineItems.Add(sessionLineItem);
			

			var service = new SessionService();
			Session session = service.Create(options);

			//URL
			//Session ID- portion of the URL
			stripeRequestDto.StripeSessionId = session.Id;
			stripeRequestDto.StripeSessionUrl = session.Url;

			Prescription prepscription = await _context.Prescriptions.FirstOrDefaultAsync(x => x.Id == stripeRequestDto.Prescription.Id);
			prepscription.Illness = stripeRequestDto.Prescription.Illness;
			prepscription.Diagnosis = stripeRequestDto.Prescription.Diagnosis;
			prepscription.Drugs = stripeRequestDto.Prescription.Drugs;

			prepscription.StripeSessionId = session.Id;
			await _context.SaveChangesAsync();

			return stripeRequestDto;

		}

	}
}
