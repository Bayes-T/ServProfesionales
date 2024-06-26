using System.ComponentModel.DataAnnotations.Schema;
using ServProfesionales.Models;

namespace ServProfesionales.Entities;

public class Appointment
{
    public string AppointmentId { get; set; }
    public string Title { get; set; }
    public string StartingDate { get; set; }
    public string EndingDate { get; set; }
    public Client Client { get; set; }
    
    public Service? Service { get; set; }
}

