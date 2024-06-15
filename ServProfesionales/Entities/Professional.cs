using System.Net.Mime;
using Microsoft.AspNetCore.Identity;

namespace ServProfesionales.Entities;

public class Professional
{
    public string ProfessionalId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ServicesOffer { get; set; }
    public string? MyWork { get; set; }
    public string? ProfileImage { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? LinkedinURL { get; set; }
    public string PersonalWeb { get; set; }

    public string PriceHour { get; set; }
    public bool? Offer1 { get; set; }
    public string? Package1Hours { get; set; }
    public string? Package1Price { get; set; }
    public bool? Offer2 { get; set; }
    public string? Package2Hours { get; set; }
    public string? Package2Price { get; set; }
    public bool? NegotiatePrice { get; set; }
}

