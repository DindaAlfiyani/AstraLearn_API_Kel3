using AstraLearn_API_Kel3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AstraLearn_API_Kel3.Controllers
{
    [ApiController]
    public class DetailExerciseController : ControllerBase
    {
        private readonly DetailExerciseRepository _DetailExerciseRepository;

        public DetailExerciseController(IConfiguration configuration)
        {
            _DetailExerciseRepository = new DetailExerciseRepository(configuration);
        }

        [HttpGet("[controller]/GetAllDetailExercise")]
        public ActionResult<ResponseModel> GetAllDetail(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _DetailExerciseRepository.GetAllData();
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpGet("[controller]/GetDetailExercise")]
        public ActionResult<ResponseModel> GetExercise(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _DetailExerciseRepository.GetData(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpPost("[controller]/InsertDetailExercise")]
        public ActionResult<ResponseModel> InsertDetailExercise([FromBody] DetailExerciseModel DetailExerciseModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _DetailExerciseRepository.InsertData(DetailExerciseModel);
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

        [HttpPost("[controller]/UpdateDetailExercise")]
        public ActionResult<ResponseModel> UpdateDetailExercise([FromBody] DetailExerciseModel DetailExerciseModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _DetailExerciseRepository.UpdateData(DetailExerciseModel);
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

        [HttpPost("[controller]/DeleteDetailExercise")]
        public ActionResult<ResponseModel> DeleteDetailExercise(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _DetailExerciseRepository.DeleteData(id);
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
