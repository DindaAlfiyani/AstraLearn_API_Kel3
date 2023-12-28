using AstraLearn_API_Kel3.Model;
using Microsoft.AspNetCore.Mvc;
using AstraLearn_API_Kel3.Model;

namespace AstraLearn_API_Kel3.Controllers
{
    [ApiController]
    public class KlasifikasiPelatihanController : Controller
    {
        private readonly KlasifikasiPelatihanRepository _klasifikasiPelatihanRepository;

        public KlasifikasiPelatihanController(IConfiguration configuration)
        {
            _klasifikasiPelatihanRepository = new KlasifikasiPelatihanRepository(configuration);
        }

        [HttpGet("[controller]/GetAllKlasifikasiPelatihan")]
        public ResponseModel GetAllKlasifikasiPelatihan()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _klasifikasiPelatihanRepository.GetAllData();
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        [HttpGet("[controller]/GetKlasifikasiPelatihan")]
        public ResponseModel GetKlasifikasiPelatihan(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _klasifikasiPelatihanRepository.GetData(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        [HttpPost("[controller]/InsertKlasifikasiPelatihan")]
        public ResponseModel InsertKlasifikasiPelatihan([FromBody] KlasifikasiPelatihanModel klasifikasiPelatihanModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _klasifikasiPelatihanRepository.InsertData(klasifikasiPelatihanModel);
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

        [HttpPost("[controller]/UpdateKlasifikasiPelatihan")]
        public ResponseModel UpdateKlasifikasiPelatihan([FromBody] KlasifikasiPelatihanModel klasifikasiPelatihanModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _klasifikasiPelatihanRepository.UpdateData(klasifikasiPelatihanModel);
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

        [HttpPost("[controller]/DeleteKlasifikasiPelatihan")]
        public ResponseModel DeleteKlasifikasiPelatihan(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _klasifikasiPelatihanRepository.DeleteData(id);
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
