using AutoMapper;
namespace AjmeraInfoTechAPI.Profiles
{
    public class BooksProfile :Profile
    {
        public BooksProfile()
        {
            CreateMap<Models.Domain.Book, Models.DTO.Book>()
                .ReverseMap();
        }
    }
}
