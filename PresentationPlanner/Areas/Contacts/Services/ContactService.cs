using PresentationPlanner.Areas.Contacts.Models;
using PresentationPlanner.Areas.Contacts.Services.Interfaces;
using PresentationPlanner.Shared.Data;

namespace PresentationPlanner.Areas.Contacts.Services;

public class ContactService : IContactService
{
    private readonly IWebHostEnvironment _env;
    private readonly IGenericRepository<Contact> _repo;

    public ContactService(IWebHostEnvironment env, IGenericRepository<Contact> repo)
    {
        _env = env;
        _repo = repo;
    }

    public async Task<bool> AddAsync(Contact contact, CancellationToken cancellationToken)
    {
        contact.Id = Guid.NewGuid();
        contact.PhotoUrl = Path.Combine("Images", "Contacts", $"{contact.Id}-{contact.Picture.FileName}");
        var fullpath = Path.Combine(_env.WebRootPath, contact.PhotoUrl);
        using (var fileStream = new System.IO.FileStream(fullpath, FileMode.Create))
        {
            await contact.Picture.CopyToAsync(fileStream, cancellationToken);
        }

        await _repo.AddAsync(contact, cancellationToken).ConfigureAwait(false);
        return await _repo.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<IEnumerable<Contact>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repo.GetAllAsync(
            noTracking: true,
            cancellationToken: cancellationToken
            );
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var ent = await _repo.GetByKeysAsync(cancellationToken, id).ConfigureAwait(false);
        _repo.Delete(ent);
        return await _repo.CommitAsync(cancellationToken).ConfigureAwait(false);
    }
}
