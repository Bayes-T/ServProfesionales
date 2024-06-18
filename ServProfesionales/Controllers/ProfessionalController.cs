using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServProfesionales.Entities;

namespace ServProfesionales.Controllers;

[ApiController]
[Route("api/professional")]
public class ProfessionalController: ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public ProfessionalController( ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<Professional>> Get()
    {
        var professional = await _dbContext.Professionals.FirstOrDefaultAsync();

        if (professional == null)
        {
            return NotFound();
        }
        return professional;
    }

    [HttpPost("registerprofessional")]
    public async Task<ActionResult> Post(Professional professional)
    {
        var registerprofessional = new Professional()
        {
            ProfessionalId = professional.ProfessionalId,
            Name = professional.Name,
            Description = professional.Description,
            ServicesOffer = professional.ServicesOffer,
            MyWork = professional.MyWork,
            ProfileImage = professional.ProfileImage,
            Email = professional.Email,
            PhoneNumber = professional.PhoneNumber,
            LinkedinURL = professional.LinkedinURL,
            HourPrice = professional.HourPrice,
            Offer1 = professional.Offer1,
            Package1Hours = professional.Package1Hours,
            Package1Price = professional.Package1Price,
            Offer2 = professional.Offer2,
            Package2Hours = professional.Package2Hours,
            Package2Price = professional.Package2Price,
            NegotiatePrice = professional.NegotiatePrice
        };

        _dbContext.Add(registerprofessional);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch("editprofessional")]
    public async Task<ActionResult> Patch([FromBody] JsonPatchDocument<Professional>? patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }
        
        var professional = await _dbContext.Professionals.FirstOrDefaultAsync();

        if (professional == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(professional, ModelState);
        if (!TryValidateModel(professional))
        {
            return BadRequest(ModelState);
        }
        
        _dbContext.Update(professional);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    

    //Por defecto no se puede borrar un profesional, el registro de la base de datos se borra al eliminar la cuenta ya que sin un profesional no puede funcionar la aplicación.
}