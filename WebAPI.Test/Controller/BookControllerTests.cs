using AjmeraInfoTechAPI.Controllers;
using AjmeraInfoTechAPI.Models.Domain;
using AjmeraInfoTechAPI.Models.DTO;
using AjmeraInfoTechAPI.Repositories;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace WebAPI.Test.Controller
{
    public class BookControllerTests
    {
        private readonly IBookRepository bookRepository;
        private readonly IMapper mapper;
        public BookControllerTests()
        {
            bookRepository = A.Fake<IBookRepository>();
            mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void BooksController_GetAllBooks_ReturnSucess()
        {
            var books = A.Fake<ICollection<AjmeraInfoTechAPI.Models.DTO.Book>>();
            var booksList = A.Fake<List<AjmeraInfoTechAPI.Models.DTO.Book>>();
            var logger = A.Fake<ILogger<BooksController>>();
            A.CallTo( () => mapper.Map<List<AjmeraInfoTechAPI.Models.DTO.Book>>(books)).Returns(booksList);
            var controller = new BooksController(bookRepository,mapper,logger);
            var result = controller.GetAllBooks();
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }


        [Fact]
        public void BooksController_CreateBook_ReturnOk()
        {
            Guid id = new Guid();
            var bookRequest = A.Fake<AjmeraInfoTechAPI.Models.DTO.AddBookRequest>();
            var logger  = A.Fake<ILogger<BooksController>>();
            var book = A.Fake<AjmeraInfoTechAPI.Models.Domain.Book>();
            var bookCreate = A.Fake<AjmeraInfoTechAPI.Models.DTO.Book>();
            var books = A.Fake<ICollection<AjmeraInfoTechAPI.Models.DTO.Book>>();
            var bookList = A.Fake<List<AjmeraInfoTechAPI.Models.DTO.Book>>();

            A.CallTo(() => bookRepository.Get(bookCreate.Id)).Returns(book);

            A.CallTo(() => mapper.Map<AjmeraInfoTechAPI.Models.Domain.Book>(bookCreate)).Returns(book);

            A.CallTo(() => bookRepository.Add(book)).Returns(book);
            

            var controller = new BooksController(bookRepository,mapper, logger);

            var result = controller.AddBook(bookRequest);

            result.Should().NotBeNull();

        }

        [Fact]
        public void BooksController_GetBook_ReturnSucess()
        {
            var logger = A.Fake<ILogger<BooksController>>();
            var book = A.Fake<AjmeraInfoTechAPI.Models.Domain.Book>();
            var books = A.Fake<ICollection<AjmeraInfoTechAPI.Models.DTO.Book>>();
            var booksList = A.Fake<List<AjmeraInfoTechAPI.Models.DTO.Book>>();
            A.CallTo(() => mapper.Map<List<AjmeraInfoTechAPI.Models.DTO.Book>>(books)).Returns(booksList);
            var controller = new BooksController(bookRepository, mapper, logger);
            var result = controller.GetBook(book.Id);
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
