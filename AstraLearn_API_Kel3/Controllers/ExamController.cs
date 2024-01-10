using AstraLearn_API_Kel3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AstraLearn_API_Kel3.Controllers
{
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ExamRepository _examRepository;

        public ExamController(IConfiguration configuration)
        {
            _examRepository = new ExamRepository(configuration);
        }

        [HttpGet("[controller]/GetAllExams")]
        public ResponseModel GetAllExam()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _examRepository.GetAllData();
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        [HttpGet("[controller]/GetExam")]
        public ResponseModel GetExam(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _examRepository.GetData(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        [HttpPost("[controller]/InsertExam")]
        public ResponseModel InsertExam([FromBody] ExamModel examModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _examRepository.InsertData(examModel);
                responseModel.message = "Data berhasil ditambahkan";
                responseModel.status = 200;
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        [HttpPost("[controller]/UpdateExam")]
        public ResponseModel UpdateExam([FromBody] ExamModel examModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _examRepository.UpdateData(examModel);
                responseModel.message = "Data berhasil diupdate";
                responseModel.status = 200;
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        [HttpPost("[controller]/DeleteExam")]
        public ResponseModel DeleteExam(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _examRepository.DeleteData(id);
                responseModel.message = "Data berhasil dihapus";
                responseModel.status = 200;
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }
    }
}
