using AjmeraInfoTechAPI.Models.Domain;

namespace AjmeraInfoTechAPI.Repositories
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        Book Get(Guid id);
        Book Add(Book region);

    }
}
