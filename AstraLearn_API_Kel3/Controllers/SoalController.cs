using AstraLearn_API_Kel3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AstraLearn_API_Kel3.Controllers
{
    [ApiController]
    public class SoalController : ControllerBase
    {
        private readonly SoalRepository _soalRepository;

        public SoalController(IConfiguration configuration)
        {
            _soalRepository = new SoalRepository(configuration);
        }

        [HttpGet("[controller]/GetAllSoal")]
        public ActionResult<ResponseModel> GetAllSoal()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _soalRepository.GetAllData();
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpGet("[controller]/GetSoal")]
        public ActionResult<ResponseModel> GetSoal(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _soalRepository.GetData(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpPost("[controller]/InsertSoal")]
        public ActionResult<ResponseModel> InsertSoal([FromBody] SoalModel soalModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _soalRepository.InsertData(soalModel);
                responseModel.message = "Data berhasil ditambahkan";
                responseModel.status = 200;
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpPost("[controller]/UpdateSoal")]
        public ActionResult<ResponseModel> UpdateSoal([FromBody] SoalModel soalModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _soalRepository.UpdateData(soalModel);
                responseModel.message = "Data berhasil diupdate";
                responseModel.status = 200;
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpPost("[controller]/DeleteSoal")]
        public ActionResult<ResponseModel> DeleteSoal(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _soalRepository.DeleteData(id);
                responseModel.message = "Data berhasil dihapus";
                responseModel.status = 200;
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
