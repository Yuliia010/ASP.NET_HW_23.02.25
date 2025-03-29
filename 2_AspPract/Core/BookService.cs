using _2_AspPract.Abstract;
using _2_AspPract.Models;
using AspSecond.DAL.Abstract;
using AspSecond.DAL.Entities;

namespace _2_AspPract.Core
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(BookDTO book)
        {
            book.Id = Guid.NewGuid();

            var bk = new Book
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Style = book.Style,
                PublicationDate = book.PublicationDate,
                OtherInfo = book.OtherInfo
            };
            await _repository.AddAsync(bk);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);

        }

        public async Task<List<BookDTO>> GetAllAsync()
        {
            var booksDto = new List<BookDTO>();

            var result = await _repository.GetAllAsync();

            foreach (var book in result)
            {
                booksDto.Add(new BookDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Style = book.Style,
                    PublicationDate = book.PublicationDate,
                    OtherInfo = book.OtherInfo
                });
            }

            return booksDto;
        }

        public async Task UpdateAsync(BookDTO book)
        {
            var bk = new Book
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Style = book.Style,
                PublicationDate = book.PublicationDate,
                OtherInfo = book.OtherInfo
            };

            await _repository.UpdateAsync(bk);
        }
    }
}
