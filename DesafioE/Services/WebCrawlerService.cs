using DesafioE.Models;
using DesafioE.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioE.Services
{
    public class WebCrawlerService: IWebCrawlerService
    {
        private readonly IWebCrawlerRepository _webCrawlerRepository;
        public WebCrawlerService(IWebCrawlerRepository webCrawlerRepository)
        {
            _webCrawlerRepository = webCrawlerRepository;
        }

        public async Task<Result> ResolveDesafio(string content)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<List<FilePathReadItemModel>>(content);

                List<ItemForJsonCreatedModel> ListToSave = new List<ItemForJsonCreatedModel>();
                DBRegisterModel dbRegister = new DBRegisterModel();
                int NumPages = (data.Count() % 20) == 0 ? (data.Count() / 20) : (data.Count() / 20) + 1;

                dbRegister.NumberOfPages = NumPages;
                dbRegister.NumberOfLines = data.Count();
                dbRegister.StartTimeExecution = DateTime.Now;

                foreach (FilePathReadItemModel item in data)
                {
                    ListToSave.Add(new ItemForJsonCreatedModel
                    {
                        ip = item.ip,
                        port = item.port,
                        country_name = item.country_name,
                        http = item.http
                    });
                }

                var json = JsonConvert.SerializeObject(ListToSave);

                ////////  Defina abaixo o caminho do  arquivo Json que será criado localmente  //////////////
                var filePathCreated = @"C:\Users\lucas\source\repos\DesafioE\DesafioE\Data\dataSaida.json";

                using (var streamWriter = new System.IO.StreamWriter(filePathCreated))
                {
                    streamWriter.Write(json);
                }

                dbRegister.ListToSave = json;
                dbRegister.ExecutionEndTime = DateTime.Now; 

                for (int i = 0; i < NumPages; i++)
                {
                    StringBuilder html = new StringBuilder();

                    html.Append("<html>");
                    html.Append("<head>");
                    html.Append("<title>Lista de Registros - Pagina " + (i + 1) + "</title>");
                    html.Append("</head>");
                    html.Append("<body>");

                    html.Append("<h1>Lista de Registros - Pagina " + (i + 1) + "</h1>");

                    for (int j = i * 20; j < (i + 1) * 20 && j < ListToSave.Count(); j++)
                    {
                        html.Append("<p> IP Adress " + ListToSave[j].ip + " - "
                                    + "Port " + ListToSave[j].port + " - "
                                    + "Country " + ListToSave[j].country_name + " - "
                                    + "Protocol " + ListToSave[j].http + " </p>");
                    }

                    html.Append("</body>");
                    html.Append("</html>");

                    /////// Defina abaixo o diretorio onde as paginas HTML serao criadas.   ///////////////////
                    ////// Sugiro que crie uma pasta para isso.                            ///////////////////
                    string caminhoArquivo = @"C:\Users\lucas\source\repos\DesafioE\DesafioE\Data\Paginas HTML\pagina" + (i + 1) + ".html";

                    using (var streamWriter = new StreamWriter(caminhoArquivo))
                    {
                        streamWriter.Write(html.ToString());
                    }
                }

                await _webCrawlerRepository.Save(dbRegister);
              
                return new Result
                {
                    success = "true",
                    data = dbRegister
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Result
                {
                    success = "false. An error occurred while processing your request."
                };
            }
            
        }
        

        public async Task<List<DBRegisterModel>> GetAll()
        {
            return await _webCrawlerRepository.GetAllAsync();
        }

        public async Task<DBRegisterModel> GetRegister(int id)
        {
            return await _webCrawlerRepository.GetByIdAsync(id);
        }

        public async Task<Result> UpdateRegister(DBRegisterModel dbRegister)
        {
            try
            {
                await _webCrawlerRepository.UpdateAsync(dbRegister);
                return new Result
                {
                    success = "true",
                    data = dbRegister
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Result
                {
                    success = "false. An error occurred while processing your request."
                };
            }
        }

        public async Task<Result> DeleteRegister(int id)
        {
            try
            {
                var register = await _webCrawlerRepository.GetByIdAsync(id);

                if (register == null)
                {
                    return new Result
                    {
                        success = "Id Not found."
                    };
                }

                await _webCrawlerRepository.DeleteAsync(id);
                return new Result
                {
                    success = "Deleted register.",
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Result
                {
                    success = "false. An error occurred while processing your request."
                };
            }
        }

    }
}
