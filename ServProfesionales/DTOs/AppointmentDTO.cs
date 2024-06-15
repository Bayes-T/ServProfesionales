using ServProfesionales.Models;

namespace ServProfesionales.DTOs;

public class AppointmentDTO
{
    public string AppointmentId { get; set; }
    public string Title { get; set; }
    public string StartingDate { get; set; }
    public string EndingDate { get; set; }
    public Client Client { get; set; }
}