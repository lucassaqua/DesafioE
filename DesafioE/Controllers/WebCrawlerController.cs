using DesafioE.Models;
using DesafioE.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebCrawlerController : ControllerBase
    {
        private readonly IWebCrawlerService _webCrawlerService;

        public WebCrawlerController(IWebCrawlerService webCrawlerService)
        {
            _webCrawlerService = webCrawlerService;
        }

        [HttpPost]
        public async Task<IActionResult> GetRegistersTeste()
        {
            var semaphore = new SemaphoreSlim(3);
            await semaphore.WaitAsync();

            try
            {
                //////   Defina abaixo onde esta localizado o arquivo json com os dados a serem lidos. /////////
                var filePathRead = @"C:\Users\lucas\source\repos\DesafioE\DesafioE\Data\dataEntrada.json";

                var content = await System.IO.File.ReadAllTextAsync(filePathRead);
                var dbRegister = await _webCrawlerService.ResolveDesafio(content);

                return Ok(new
                {
                    success = true,
                    data = dbRegister

                }); 
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
            finally
            {
                semaphore.Release();
            }
        }
        


        /// //////////////////////////////////////////////////////////////////////////////////////////
       
        // Os endpoints abaixos não foram pedidos no desafio, mas os criei como forma de 
        // complementar o meu trabalho.

        /// //////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        public async Task<IActionResult> GetWebCrawlerRegisters()
        {
            try
            {
                return Ok(new
                {
                    success = true,
                    data = await _webCrawlerService.GetAll()
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DBRegisterModel>> GetWebCrawlerRegister(int id)   
        {
            try 
            {
                var register = await _webCrawlerService.GetRegister(id);

                if (register == null)
                {
                    return NotFound();
                }

                return register;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWebCrawlerRegister(int id, DBRegisterModel dbRegister)
        {
            if (id != dbRegister.Id)
            {
                return BadRequest();
            }

            try
            {
                var result = await _webCrawlerService.UpdateRegister(dbRegister);
                if (result.success != "true")
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Id not found."
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = dbRegister
                });
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebCrawlerRegister(int id)
        {
            try
            {
                await _webCrawlerService.DeleteRegister(id);
                return Ok(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }

    }
}
