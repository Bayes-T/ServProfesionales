using System.ComponentModel.DataAnnotations.Schema;
using ServProfesionales.Models;

namespace ServProfesionales.Entities;

public class Service
{
    public string ServiceId { get; set; }
    public string Title { get; set; }
    public string startingDate { get; set; }
    public string EndingDate { get; set; }
    public ServiceEnum State { get; set; }
    public Client Client { get; set; }
    
    public string AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
    
}