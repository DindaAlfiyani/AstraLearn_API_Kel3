using AstraLearn_API_Kel3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace AstraLearn_API_Kel3.Controllers
{
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly ExerciseRepository _exerciseRepository;

        public ExerciseController(IConfiguration configuration)
        {
            _exerciseRepository = new ExerciseRepository(configuration);
        }

        [HttpGet("[controller]/GetAllExercises")]
        public ResponseModel GetAllExercises()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _exerciseRepository.GetAllData();
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        [HttpGet("[controller]/GetExercise")]
        public ResponseModel GetExercise(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _exerciseRepository.GetData(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        [HttpPost("[controller]/InsertExercise")]
        public ResponseModel InsertExercise([FromBody] ExerciseModel exerciseModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _exerciseRepository.InsertData(exerciseModel);
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

        [HttpPost("[controller]/UpdateExercise")]
        public ResponseModel UpdateExercise([FromBody] ExerciseModel exerciseModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _exerciseRepository.UpdateData(exerciseModel);
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

        [HttpPost("[controller]/DeleteExercise")]
        public ResponseModel DeleteExercise(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _exerciseRepository.DeleteData(id);
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
