using PresentationPlanner.Areas.Contacts.Models;
using PresentationPlanner.Areas.Contacts.Repository.Interface;
using PresentationPlanner.Areas.Contacts.Services.Interfaces;

namespace PresentationPlanner.Areas.Contacts.Services;

public class ContactService : IContactService
{
    private readonly IWebHostEnvironment _env;
    private readonly IContactRepository _repo;

    public ContactService(IWebHostEnvironment env, IContactRepository repo)
    {
        _env = env;
        _repo = repo;
    }

    public async Task<bool> AddAsync(Contact contact, CancellationToken cancellationToken)
    {
        contact.Id = Guid.NewGuid();
        await UploadPhoto(contact, cancellationToken);

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

    public async Task<bool> UpdateAsync(Contact contact, CancellationToken cancellationToken)
    {
        if (contact.Picture != null)
        {
            await UploadPhoto(contact, cancellationToken).ConfigureAwait(false);
            _repo.Update(contact);
        }
        else
        {
            _repo.UpdateContactWithoutPic(contact);
        }
        contact.UpdateDate = DateTime.UtcNow;
        return await _repo.CommitAsync(cancellationToken).ConfigureAwait(false);
    }

    //public async Task<bool> ChangeFavorite(Guid id, CancellationToken cancellationToken)
    //{
    //    var contato = await _genericRepository.GetByKeysAsync(cancellationToken, id).ConfigureAwait(false);
    //    contato.Favorito = !contato.Favorito;
    //    _genericRepository.Update(contato);
    //    return await _genericRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
    //}



    //public async Task<int> CountContacts(CancellationToken cancellationToken)
    //{
    //    return await _repo.GetAll().CountAsync(cancellationToken).ConfigureAwait(false);
    //}

    public async Task<Contact> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _repo.GetByKeysAsync(cancellationToken, id).ConfigureAwait(false);
    }



    //// métodos privados
    //private async Task UploadFoto(Contato contato, CancellationToken cancellationToken)
    //{
    //    if (contato.Foto != null)
    //    {
    //        contato.FotoUrl = Path.Combine("Images", "Contatos", $"{contato.Id}-{contato.Foto.FileName}");
    //        var fulPath = Path.Combine(_env.WebRootPath, contato.FotoUrl);
    //        using (var fileStream = new FileStream(fulPath, FileMode.Create))
    //        {
    //            await contato.Foto.CopyToAsync(fileStream, cancellationToken).ConfigureAwait(false);
    //        }
    //    }

    //}

    private async Task UploadPhoto(Contact contact, CancellationToken cancellationToken)
    {
        if (contact.Picture is null)
        {
            return;
        }

        contact.PhotoUrl = Path.Combine("Images", "Contacts", $"{contact.Id}-{contact.Picture.FileName}");
        var fullpath = Path.Combine(_env.WebRootPath, contact.PhotoUrl);
        using (var fileStream = new System.IO.FileStream(fullpath, FileMode.Create))
        {
            await contact.Picture.CopyToAsync(fileStream, cancellationToken);
        }
    }
}
