using Data.Models;
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
    public class AdminRepository<T> where T : IdBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> table = null;
        private readonly IHostingEnvironment _env;
        private readonly ApplicationUser _user;
        public AdminRepository(ApplicationDbContext context, Guid userId, IHostingEnvironment env) { _context = context; table = _context.Set<T>(); _env = env; _user = _context.Users.Find(userId.ToString()); }

        public async Task<int> Count() => await table.Where(x => !x.isDeleted).CountAsync();

        public async Task<T> Single(Guid id) => await table.FirstOrDefaultAsync(x => !x.isDeleted && x.Id == id);

        public async Task<List<T>> Multiple(int pageNumber = 1, int pageCount = 20)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageCount = pageCount < 1 ? 1 : pageCount;
            return await table.Where(x => !x.isDeleted).Skip((pageNumber - 1) * pageCount).Take(pageCount).ToListAsync();
        }
        public async Task<List<T>> MultipleExp(Expression<Func<T, bool>> expression, int pageNumber = 1, int pageCount = 20)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageCount = pageCount < 1 ? 1 : pageCount;
            return await table.Where(x => !x.isDeleted).Where(expression).Skip((pageNumber - 1) * pageCount).Take(pageCount).ToListAsync();
        }
        public async Task<List<resT>> MultipleSelect<resT>(Expression<Func<T, bool>> expression, Expression<Func<T, resT>> select)
        {
            return await table.Where(x => !x.isDeleted).Where(expression).Select(select).Cast<resT>().ToListAsync();
        }
        public async Task<bool> Add(T item, bool saveChanges = true)
        {
            try
            {
                item.CreatedById = _user.Id;
                _context.Entry(item).State = EntityState.Added;
                if (saveChanges)
                    await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    throw ex;
                }
                return false;
            }
        }
        public async Task<bool> AddRange(List<T> items)
        {
            try
            {
                foreach (var item in items)
                {
                    Add(item, false);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    throw ex;
                }
                return false;
            }
        }
        public async Task<bool> Update(T item)
        {
            try
            {
                item.ModifiedOn = DateTime.Now;
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    throw ex;
                }
                return false;
            }
        }
        public async Task<bool> Delete(Guid itemId)
        {
            try
            {
                var item = await table.FindAsync(itemId);
                item.DeletedOn = DateTime.Now;
                item.isDeleted = true;
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    throw ex;
                }
                return false;
            }
        }
        public async Task<bool> DeleteRange(List<Guid> itemIds)
        {
            try
            {
                var items = await table.Where(x => itemIds.Contains(x.Id)).ToListAsync();
                foreach (var item in items)
                {
                    item.DeletedOn = DateTime.Now;
                    item.isDeleted = true;
                    _context.Entry(item).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    throw ex;
                }
                return false;
            }
        }
        public async Task<bool> Activity(T item, bool situation)
        {
            try
            {
                item.ModifiedOn = DateTime.Now;
                item.isActive = situation;
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    throw ex;
                }
                return false;
            }
        }
    }
}
