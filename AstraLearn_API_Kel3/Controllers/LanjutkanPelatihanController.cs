using AstraLearn_API_Kel3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AstraLearn_API_Kel3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LanjutkanPelatihanController : ControllerBase
    {
        private readonly LanjutkanPelatihanRepository _lanjutkanPelatihanRepository;

        public LanjutkanPelatihanController(IConfiguration configuration)
        {
            _lanjutkanPelatihanRepository = new LanjutkanPelatihanRepository(configuration);
        }

        [HttpGet("GetLanjutkanPelatihan")]
        public ActionResult<ResponseModel> GetLanjutkanPelatihan(int idPengguna)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _lanjutkanPelatihanRepository.GetLanjutkanPelatihan(idPengguna);
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
