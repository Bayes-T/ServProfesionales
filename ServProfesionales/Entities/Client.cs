namespace ServProfesionales.Entities;

public class Client
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public bool test { get; set; }
    

    public List<Service> Services { get; set; }
    public List<Appointment> Appointments { get; set; }
}