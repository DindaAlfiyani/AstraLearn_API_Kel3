using AstraLearn_API_Kel3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AstraLearn_API_Kel3.Controllers
{
    [ApiController]
    public class DetailExamController : ControllerBase
    {
        private readonly DetailExamRepository _DetailExamRepository;

        public DetailExamController(IConfiguration configuration)
        {
            _DetailExamRepository = new DetailExamRepository(configuration);
        }

        [HttpGet("[controller]/GetAllDetailExam")]
        public ActionResult<ResponseModel> GetAllDetail(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _DetailExamRepository.GetAllData();
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpGet("[controller]/GetDetailExam")]
        public ActionResult<ResponseModel> GetExam(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _DetailExamRepository.GetData(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpPost("[controller]/InsertDetailExam")]
        public ActionResult<ResponseModel> InsertDetailExam([FromBody] DetailExamModel DetailExamModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _DetailExamRepository.InsertData(DetailExamModel);
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

        [HttpPost("[controller]/UpdateDetailExam")]
        public ActionResult<ResponseModel> UpdateDetailExam([FromBody] DetailExamModel DetailExamModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _DetailExamRepository.UpdateData(DetailExamModel);
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

        [HttpPost("[controller]/DeleteDetailExam")]
        public ActionResult<ResponseModel> DeleteDetailExam(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _DetailExamRepository.DeleteData(id);
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
