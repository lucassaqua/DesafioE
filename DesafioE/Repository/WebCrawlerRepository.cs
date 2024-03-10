using DesafioE.Context;
using DesafioE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioE.Repository
{
    public class WebCrawlerRepository: IWebCrawlerRepository
    {
        private readonly AppDbContext _appDbContext;
        public WebCrawlerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<DBRegisterModel> GetByIdAsync(int id)
        {
            return await _appDbContext.WebCrawlers.FindAsync(id);
        }

        public async Task<List<DBRegisterModel>> GetAllAsync()
        {
            return await _appDbContext.WebCrawlers.ToListAsync();
        }

        public async Task Save (DBRegisterModel dBRegister)
        {
            _appDbContext.WebCrawlers.Add(dBRegister);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(DBRegisterModel dBRegister)
        {
            _appDbContext.WebCrawlers.Update(dBRegister);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dBRegister = await _appDbContext.WebCrawlers.FindAsync(id);
            _appDbContext.WebCrawlers.Remove(dBRegister);
            await _appDbContext.SaveChangesAsync();
        }

    }
}
