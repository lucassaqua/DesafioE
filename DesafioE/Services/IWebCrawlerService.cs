using DesafioE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioE.Services
{
    public interface IWebCrawlerService
    {
        Task<Result> ResolveDesafio(string content);
        Task<List<DBRegisterModel>> GetAll();
        Task<DBRegisterModel> GetRegister(int id);
        Task<Result> UpdateRegister(DBRegisterModel dbRegister);
        Task<Result> DeleteRegister(int id);
    }
}
