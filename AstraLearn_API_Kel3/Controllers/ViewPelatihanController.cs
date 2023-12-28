using AstraLearn_API_Kel3.Model;
using Microsoft.AspNetCore.Mvc;

namespace AstraLearn_API_Kel3.Controllers
{
    [ApiController]
    public class ViewPelatihanController : Controller
    {
        private readonly ViewPelatihanRepository _viewPelatihanRepository;

        public ViewPelatihanController(IConfiguration configuration)
        {
            _viewPelatihanRepository = new ViewPelatihanRepository(configuration);
        }

        [HttpGet("[controller]/GetAllPelatihans")]
        public ActionResult<ResponseModel> GetAllPelatihans()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _viewPelatihanRepository.GetAllData();
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
