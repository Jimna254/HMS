using Appointments.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appointments.Models
{
   
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }

        public string PatientId{ get; set; }

        public Guid HospitalID {get; set;}
		public Guid DoctorId { get; set; }

		public Departments Department { get; set; } = Departments.Dental;

        [NotMapped]
        public string Date { get; set; } 
		public string Time  { get; set; } 

        public string Symptoms { get; set; }    


    }
}
