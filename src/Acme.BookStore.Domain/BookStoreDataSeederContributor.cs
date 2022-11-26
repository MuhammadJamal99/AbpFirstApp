using Acme.BookStore.Books;
using System.Threading.Tasks;
using System;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Acme.BookStore;
public class BookStoreDataSeederContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Book, Guid> BookRepository;

    public BookStoreDataSeederContributor(IRepository<Book, Guid> bookRepository)
    {
        BookRepository = bookRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await BookRepository.GetCountAsync() <= 10)
        {
            List<Book> BooksList = new();
            for (int i = 1; i < 1_000; i++)
            {
                BooksList.Add(new Book()
                {
                    Name = $"Book {i}",
                    Type = BookType.Biography,
                    PublishDate = DateTime.Now,
                    Price = i + 100
                });
            }
            await BookRepository.InsertManyAsync(BooksList.AsEnumerable());
        }
    }
}
