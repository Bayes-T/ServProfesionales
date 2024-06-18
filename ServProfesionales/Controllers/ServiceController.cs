using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServProfesionales.Entities;

namespace ServProfesionales.Controllers;

[ApiController]
[Route("api/services")]
public class ServiceController: ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    public ServiceController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Service>>> GetAll()
    {
        var services = await _dbContext.Services.Include(x => x.Client).Include(x => x.Appointment).ToListAsync();

        return services;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Service>> GetById(string id)
    {
        var service = await _dbContext.Services.Include(x => x.Client).Include(x => x.Appointment).FirstOrDefaultAsync();

        if (service == null)
        {
            return NotFound();
        }

        return service;
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> Post(string id, JsonPatchDocument<Service>? patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }

        var service = await _dbContext.Services.FirstOrDefaultAsync(x => x.ServiceId == id);
        
        if (service == null)
        {
            return BadRequest("Id del paciente no encontrado");
        }

        patchDoc.ApplyTo(service, ModelState);
        
        if (!TryValidateModel(service))
        {
            return BadRequest(ModelState);
        }
        _dbContext.Update(service);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var service = await _dbContext.Services.FirstOrDefaultAsync(x => x.ServiceId == id);
        
        if (service == null)
        {
            return BadRequest("Id del paciente no encontrado");
        }

        _dbContext.Remove(new Service() { ServiceId = id });
        _dbContext.SaveChangesAsync();
        return Ok();
    }
}