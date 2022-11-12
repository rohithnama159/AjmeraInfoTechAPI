using AjmeraInfoTechAPI.Data;
using AjmeraInfoTechAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AjmeraInfoTechAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AjmeraInfoTechDbContext context;

        public BookRepository(AjmeraInfoTechDbContext context)
        {
            this.context = context;
        }
        public  Book Add(Book book)
        {
            book.Id = Guid.NewGuid();
            context.Add(book);
            context.SaveChanges();
            return book;
        }

        public Book Get(Guid id)
        {
            return context.Books.Where(x=>x.Id==id).FirstOrDefault();
        }

        public List<Book> GetAll()
        {
            return  context.Books.ToList();
        }

     
    }
}
