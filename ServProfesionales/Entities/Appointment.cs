using System.ComponentModel.DataAnnotations.Schema;

namespace ServProfesionales.Entities;

public class Appointment
{
    public string AppointmentId { get; set; }
    public DateTime startingDate { get; set; }
    public DateTime EndingDate { get; set; }
    
    public Client Client { get; set; }
    
    public Service Service { get; set; }
}

