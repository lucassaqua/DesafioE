using DesafioE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioE.Repository
{
    public interface IWebCrawlerRepository
    {
        Task Save(DBRegisterModel dBRegister);
        Task<DBRegisterModel> GetByIdAsync(int id);
        Task<List<DBRegisterModel>> GetAllAsync();
        Task UpdateAsync(DBRegisterModel dBRegister);
        Task DeleteAsync(int id);
    }
}
