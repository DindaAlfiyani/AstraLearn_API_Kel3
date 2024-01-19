using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AstraLearn_API_Kel3.Model;

namespace AstraLearn_API_Kel3.Controllers
{
    [ApiController]
    public class MengikutiPelatihanController : Controller
    {
        private readonly MengikutiPelatihanRepository _mengikutiPelatihanRepository;

        public MengikutiPelatihanController(IConfiguration configuration)
        {
            _mengikutiPelatihanRepository = new MengikutiPelatihanRepository(configuration);
        }

        [HttpGet("[controller]/GetAllMengikutiPelatihan")]
        public ResponseModel GetAllMengikutiPelatihan()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _mengikutiPelatihanRepository.GetAllData();
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        [HttpGet("[controller]/GetMengikutiPelatihan")]
        public ResponseModel GetMengikutiPelatihan(int id_pengguna, int id_pelatihan)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _mengikutiPelatihanRepository.GetData(id_pengguna, id_pelatihan);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return responseModel;
        }

        /*[HttpGet("[controller]/GetFollowingsForUser")]
        public ActionResult<IEnumerable<MengikutiPelatihanModel>> GetFollowingsForUser(int userId)
        {
            var followings = _mengikutiPelatihanRepository.GetFollowingsForUser(userId);
            return Ok(followings);
        }*/

        [HttpPost("[controller]/InsertMengikutiPelatihan")]
        public ResponseModel InsertMengikutiPelatihan([FromBody] MengikutiPelatihanModel mengikutiPelatihanModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _mengikutiPelatihanRepository.InsertData(mengikutiPelatihanModel);
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

        // Contoh action untuk update nilai
        [HttpPost("[controller]/UpdateMengikutiPelatihan")]
        public ResponseModel UpdateNilai([FromBody] MengikutiPelatihanModel mengikutiPelatihanModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _mengikutiPelatihanRepository.UpdateData(mengikutiPelatihanModel);
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
