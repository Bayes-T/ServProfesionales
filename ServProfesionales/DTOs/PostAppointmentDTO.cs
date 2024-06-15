namespace ServProfesionales.DTOs;

public class PostAppointmentDTO
{
    public string AppointmentId { get; set; }
    public string Title { get; set; }
    public string StartingDate { get; set; }
    public string EndingDate { get; set; }
    
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; } 
}