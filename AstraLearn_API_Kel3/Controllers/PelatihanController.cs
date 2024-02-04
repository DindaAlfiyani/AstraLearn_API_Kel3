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

        [HttpGet("[controller]/GetPelatihanByKlasifikasi")]
        public ActionResult<ResponseModel> GetPelatihanByKlasifikasi(int id, int status)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _pelatihanRepository.GetDataByPelatihan(id, status);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
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

        [HttpPost("[controller]/UploadFile")]
        public ActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine("D:\\SEMESTER 3\\PRG 4\\project astralearn\\SystemAstraLearn_Kelompok3\\SystemAstraLearn_Kelompok3\\wwwroot\\assets\\Thumbnail", fileName); // Sesuaikan dengan direktori yang diinginkan

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return new JsonResult(new { success = true, message = "File uploaded successfully." });
                }
                else
                {
                    return new JsonResult(new { success = false, message = "No file received." });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = $"Error: {ex.Message}" });
            }
        }
    }
}
