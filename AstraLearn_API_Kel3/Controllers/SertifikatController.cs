using AstraLearn_API_Kel3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AstraLearn_API_Kel3.Controllers
{
    [ApiController]
    public class SertifikatController : ControllerBase
    {
        private readonly SertifikatRepository _sertifikatRepository;

        public SertifikatController(IConfiguration configuration)
        {
            _sertifikatRepository = new SertifikatRepository(configuration);
        }

        [HttpGet("[controller]/GetAllSertifikat")]
        public ActionResult<ResponseModel> GetAllSertifikat()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _sertifikatRepository.GetAllData();
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpGet("[controller]/GetAllSertifikat2")]
        public ActionResult<ResponseModel> GetAllSertifikat2(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _sertifikatRepository.GetAllData2(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpGet("[controller]/GetSertifikat")]
        public ActionResult<ResponseModel> GetSertifikat(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _sertifikatRepository.GetData(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpPost("[controller]/InsertSertifikat")]
        public ActionResult<ResponseModel> InsertSertifikat([FromBody] SertifikatModel sertifikatModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _sertifikatRepository.InsertData(sertifikatModel);
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

        [HttpPost("[controller]/UpdateSertifikat")]
        public ActionResult<ResponseModel> UpdateSertifikat([FromBody] SertifikatModel sertifikatModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _sertifikatRepository.UpdateData(sertifikatModel);
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

        [HttpPost("[controller]/DeleteSertifikat")]
        public ActionResult<ResponseModel> DeleteSertifikat(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _sertifikatRepository.DeleteData(id);
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
