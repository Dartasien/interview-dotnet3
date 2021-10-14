using GroceryStoreAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace tests.Data.Repositories
{
    public class BaseRepositoryTests
    {
        protected BaseRepositoryTests(DbContextOptions<CustomerContext> contextOptions)
        {
            ContextOptions = contextOptions;

        }

        protected DbContextOptions<CustomerContext> ContextOptions { get; }
    }
}