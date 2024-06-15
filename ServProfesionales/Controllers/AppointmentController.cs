using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServProfesionales.DTOs;
using ServProfesionales.Entities;
using ServProfesionales.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace ServProfesionales.Controllers;

[ApiController]
[Route("api/appointment")]
public class AppointmentController: ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    
    public AppointmentController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<AppointmentDTO>>> GetAll()
    {
        var appointments = await _dbContext.Appointments.Include(x => x.Client).ToListAsync();
        var appointmentsDtos = new List<AppointmentDTO>();

        foreach (var appointment in appointments)
        {
            var appointmentDto = new AppointmentDTO()
            {
                AppointmentId = appointment.AppointmentId,
                Client = appointment.Client,
                Title = appointment.Title,
                StartingDate = appointment.StartingDate,
                EndingDate = appointment.EndingDate
                
            };
            
            appointmentsDtos.Add(appointmentDto);
        }

        return appointmentsDtos;
    }

    [HttpGet("/{id}")]
    public async Task<ActionResult<AppointmentDTO>> GetById(string id)
    {
        var appointment = await _dbContext.Appointments.FirstOrDefaultAsync(x => x.AppointmentId == id);
        
        if (appointment == null)
        {
            return BadRequest();
        }

        var appointmentDto = new AppointmentDTO()
        {
            AppointmentId = appointment.AppointmentId,
            Client = appointment.Client,
            Title = appointment.Title,
            StartingDate = appointment.StartingDate,
            EndingDate = appointment.EndingDate
        };

        return appointmentDto;
    }

    [HttpPost("agregarappointment")]
    public async Task<ActionResult> Post(PostAppointmentDTO postAppointmentDto)
    {
        var existeAppointment = await _dbContext.Appointments.AnyAsync(x => x.AppointmentId == postAppointmentDto.AppointmentId);
        
        if (existeAppointment)
        {
            return BadRequest("Ya existe un appointment con este Id");
        }

        var client = new Client()
        {
            ClientId = postAppointmentDto.ClientId,
            Name = postAppointmentDto.Name,
            Email = postAppointmentDto.Email
        };
        
        var appointment = new Appointment()
        {
            AppointmentId = postAppointmentDto.AppointmentId,
            Title = postAppointmentDto.Title,
            StartingDate = postAppointmentDto.StartingDate,
            EndingDate = postAppointmentDto.EndingDate,
            Client = client
        };
        
        _dbContext.Add(appointment);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchAppointment(string id, [FromBody] JsonPatchDocument<Appointment>? patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }

        var appointment = await _dbContext.Appointments.FirstOrDefaultAsync(x => x.AppointmentId == id);

        if (appointment == null)
        {
            return BadRequest("Id no encontrado");
        }
        
        patchDoc.ApplyTo(appointment, ModelState);

        if (!TryValidateModel(appointment))
        {
            return BadRequest(ModelState);
        }
        
        _dbContext.Update(appointment);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutAppointment(AppointmentDTO appointmentDto)
    {
        var existe =
            await _dbContext.Appointments.Include(x => x.Client).FirstOrDefaultAsync(x => x.AppointmentId == appointmentDto.AppointmentId);

        if (existe == null)
        {
            return BadRequest("Id no encontrado");
        }

        var appointment = new Appointment()
        {
            AppointmentId = appointmentDto.AppointmentId,
            Title = appointmentDto.Title,
            StartingDate = appointmentDto.StartingDate,
            EndingDate = appointmentDto.EndingDate,
        };
        
        var client = new Client()
        {
            ClientId = appointmentDto.Client.ClientId,
            Name = appointmentDto.Client.Name,
            Email = appointmentDto.Client.Email
        };

        appointment.Client = client;

        _dbContext.Update(appointment);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAppointment(string id)
    {
        var appointment = await _dbContext.Appointments.AnyAsync(x => x.AppointmentId == id);

        if (appointment == false)
        {
            return BadRequest("No se encontró el Id");
        }

        _dbContext.Remove(new Appointment() { AppointmentId = id });
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    
}