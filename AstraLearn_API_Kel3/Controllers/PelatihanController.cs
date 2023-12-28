using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AstraLearn_API_Kel3.Model;

namespace AstraLearn_API_Kel3.Controllers
{
    [ApiController]
    public class PelatihanController : Controller
    {
        private readonly PelatihanRepository _pelatihanRepository;

        public PelatihanController(IConfiguration configuration)
        {
            _pelatihanRepository = new PelatihanRepository(configuration);
        }

        [HttpGet("[controller]/GetAllPelatihan")]
        public ResponseModel GetAllPelatihan()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _pelatihanRepository.GetAllData();
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        [HttpGet("[controller]/GetPelatihan")]
        public ResponseModel GetPelatihan(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _pelatihanRepository.GetData(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        [HttpGet("[controller]/GetPelatihan2")]
        public ResponseModel GetPelatihan2(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _pelatihanRepository.GetAllData2(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        [HttpPost("[controller]/InsertPelatihan")]
        public ResponseModel InsertPelatihan([FromBody] PelatihanModel pelatihanModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _pelatihanRepository.InsertData(pelatihanModel);
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

        [HttpPost("[controller]/UpdatePelatihan")]
        public ResponseModel UpdatePelatihan([FromBody] PelatihanModel pelatihanModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _pelatihanRepository.UpdateData(pelatihanModel);
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

        [HttpPost("[controller]/UpdateNilai")]
        public ResponseModel UpdateNilai(int id, int nilai)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _pelatihanRepository.UpdateNilai(id, nilai);
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

        [HttpPost("[controller]/DeletePelatihan")]
        public ResponseModel DeletePelatihan(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _pelatihanRepository.DeleteData(id);
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
