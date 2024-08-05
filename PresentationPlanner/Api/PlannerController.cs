using Microsoft.AspNetCore.Mvc;
using PresentationPlanner.Areas.Contacts.Services.Interfaces;

namespace PresentationPlanner.Api;

[ApiController]
[Route("api/[controller]")]
public class PlannerController : Controller
{
    private readonly IContactService _serv;

    public PlannerController(IContactService serv)
    {
        _serv = serv;
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteContact(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _serv.DeleteAsync(id, cancellationToken);
        if (!deleted)
        {
            return BadRequest();
        }
        return Ok(deleted);
    }
}
