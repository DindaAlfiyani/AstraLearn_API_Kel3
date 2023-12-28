using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AstraLearn_API_Kel3.Model;

namespace AstraLearn_API_Kel3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewPesertaController : ControllerBase
    {
        private readonly ViewPesertaRepository _viewPesertaRepository;

        public ViewPesertaController(IConfiguration configuration)
        {
            _viewPesertaRepository = new ViewPesertaRepository(configuration);
        }

        [HttpGet("GetAllPeserta")]
        public ActionResult<ResponseModel> GetAllPeserta()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _viewPesertaRepository.GetAllData();
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }
    }
}
