using System.ComponentModel.DataAnnotations.Schema;
using ServProfesionales.Models;

namespace ServProfesionales.Entities;

public class Service
{
    public string ServiceId { get; set; }
    public DateTime Date { get; set; }
    public ServiceEnum State { get; set; }
    
    public Client Client { get; set; }
    
    public string AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
}