using Data.Models.BaseModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UserRepository<T> where T : IdBase
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> table = null;
        private IHostingEnvironment _env;
        public UserRepository(ApplicationDbContext context, IHostingEnvironment env) { _context = context; table = _context.Set<T>(); _env = env; }

        public async Task<T> Single(Guid id) => await table.FirstOrDefaultAsync(x => !x.isDeleted && x.isActive && x.Id == id);
        public async Task<List<T>> Multiple(int pageNumber = 1, int pageCount = 20)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageCount = pageCount < 1 ? 1 : pageCount;
            return await table.Where(x => !x.isDeleted && x.isActive).Skip((pageNumber - 1) * pageCount).Take(pageCount).ToListAsync();
        }

        public async Task<T> SingleExp(Expression<Func<T, bool>> expression) {
            var debug = await table.FirstOrDefaultAsync(expression); ;
            return debug;
        }
        public async Task<List<T>> MultipleExp(Expression<Func<T, bool>> expression, int pageNumber = 1, int pageCount = 20)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageCount = pageCount < 1 ? 1 : pageCount;
            return await table.Where(expression).Skip((pageNumber - 1) * pageCount).Take(pageCount).ToListAsync();
        }
    }
}
