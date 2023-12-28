using AstraLearn_API_Kel3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace AstraLearn_API_Kel3.Controllers
{
    [ApiController]
    public class SoalExerciseController : ControllerBase
    {
        private readonly SoalExerciseRepository _soalExerciseRepository;

        public SoalExerciseController(IConfiguration configuration)
        {
            _soalExerciseRepository = new SoalExerciseRepository(configuration);
        }

        [HttpGet("[controller]/GetAllSoalExercises")]
        public ActionResult<ResponseModel> GetAllSoalExercises()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _soalExerciseRepository.GetAllData();
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpGet("[controller]/GetSoalExercise")]
        public ActionResult<ResponseModel> GetSoalExercise(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _soalExerciseRepository.GetData(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpPost("[controller]/InsertSoalExercise")]
        public ActionResult<ResponseModel> InsertSoalExercise([FromBody] SoalExerciseModel soalExerciseModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _soalExerciseRepository.InsertData(soalExerciseModel);
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

        [HttpPost("[controller]/UpdateSoalExercise")]
        public ActionResult<ResponseModel> UpdateSoalExercise([FromBody] SoalExerciseModel soalExerciseModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _soalExerciseRepository.UpdateData(soalExerciseModel);
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

        [HttpPost("[controller]/DeleteSoalExercise")]
        public ActionResult<ResponseModel> DeleteSoalExercise(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _soalExerciseRepository.DeleteData(id);
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
