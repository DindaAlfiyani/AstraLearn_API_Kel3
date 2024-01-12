using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AstraLearn_API_Kel3.Model;

namespace AstraLearn_API_Kel3.Controllers
{
    [ApiController]
    public class PenggunaController : Controller
    {
        private readonly PenggunaRepository _penggunaRepository;

        public PenggunaController(IConfiguration configuration)
        {
            _penggunaRepository = new PenggunaRepository(configuration);
        }

        [HttpGet("[controller]/GetAllPengguna")]
        public ResponseModel GetAllPelatihan()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _penggunaRepository.GetAllData();
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        [HttpGet("[controller]/GetPengguna")]
        public ResponseModel GetPengguna(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _penggunaRepository.GetData(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        [HttpGet("[controller]/GetPenggunaUsername")]
        public ResponseModel GetPenggunaUsername(string username)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _penggunaRepository.GetUser(username);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        [HttpPost("[controller]/InsertPengguna")]
        public ResponseModel InsertPengguna([FromBody] PenggunaModel penggunaModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _penggunaRepository.InsertPengguna(penggunaModel);
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

        [HttpPost("[controller]/UpdatePengguna")]
        public ResponseModel UpdatePengguna([FromBody] PenggunaModel penggunaModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _penggunaRepository.UpdatePengguna(penggunaModel);
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

        [HttpPost("[controller]/UpdateHakAkses")]
        public ResponseModel UpdatePengguna2([FromBody] PenggunaModel penggunaModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _penggunaRepository.UpdateHakAkses(penggunaModel.id_pengguna, penggunaModel.hak_akses);
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
    }
}
