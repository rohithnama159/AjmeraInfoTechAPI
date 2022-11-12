 using AjmeraInfoTechAPI.Models.DTO;
using AjmeraInfoTechAPI.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AjmeraInfoTechAPI.Controllers
{
    [ApiController]
    [Route("Books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository bookRepository;
        private readonly IMapper mapper;
        private readonly ILogger<BooksController> logger;
  

        public BooksController(IBookRepository bookRepository, IMapper mapper, ILogger<BooksController> logger)
        {
            this.bookRepository = bookRepository;
            this.mapper = mapper;
            this.logger = logger;
        }



        [HttpGet]
        public IActionResult GetAllBooks()
        {
            logger.LogInformation("Getting All Books");
            var books = bookRepository.GetAll();

            var booksDTO = mapper.Map<List<Book>>(books);
            return Ok(booksDTO);
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetBook")]
        public IActionResult GetBook(Guid id)
        {
            logger.LogInformation("Getting Books by ID");
            var book = bookRepository.Get(id);

            if (book == null)
            {
                logger.LogWarning("Book With The ID Not Found");
                return NotFound();
            }
                

            var regionDTO = mapper.Map<Models.DTO.Book>(book);

            return Ok(regionDTO);
        }

        [HttpPost]
        public IActionResult AddBook(AddBookRequest bookRequest)
        {
            logger.LogInformation("Adding Book in to database");

            if (!ValidateBooknAsync(bookRequest))
            {
                logger.LogError("Invalid Feilds");
                return BadRequest(ModelState);
            }

            var book = new Models.Domain.Book()
            {
                Name = bookRequest.Name,
                AuthorName = bookRequest.AuthorName
            };
     
            var response = bookRepository.Add(book);


            logger.LogInformation("Book Added Sucessfully to database");
            var bookDTO = mapper.Map<Models.DTO.Book>(response);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, bookDTO);

        }


        private bool ValidateBooknAsync(AddBookRequest addBookRequest)
        {
            if (addBookRequest == null)
            {
                ModelState.AddModelError(nameof(addBookRequest), $"{nameof(addBookRequest)} cannot be null or empty");
                return true;
            }

            if (string.IsNullOrWhiteSpace(addBookRequest.Name))
                ModelState.AddModelError(nameof(addBookRequest.Name), $"{nameof(addBookRequest.Name)} cannot be null or empty");

            if (string.IsNullOrWhiteSpace(addBookRequest.AuthorName))
                ModelState.AddModelError(nameof(addBookRequest.AuthorName), $"{nameof(addBookRequest.AuthorName)} cannot be null or empty");

            if (ModelState.ErrorCount > 0)
                return false;

            return true;

        }
    }
}
