using AstraLearn_API_Kel3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AstraLearn_API_Kel3.Controllers
{
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly SectionRepository _sectionRepository;

        public SectionController(IConfiguration configuration)
        {
            _sectionRepository = new SectionRepository(configuration);
        }

        [HttpGet("[controller]/GetAllSections")]
        public ActionResult<ResponseModel> GetAllSections()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _sectionRepository.GetAllData();
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpGet("[controller]/GetAllSectionsById")]
        public ActionResult<ResponseModel> GetAllSections2(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _sectionRepository.GetDataByPelatihan(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpGet("[controller]/GetSection")]
        public ActionResult<ResponseModel> GetSection(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _sectionRepository.GetData(id);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

        [HttpPost("[controller]/InsertSection")]
        public ActionResult<ResponseModel> InsertSection([FromBody] SectionModel sectionModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _sectionRepository.InsertData(sectionModel);
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

        [HttpPost("[controller]/UpdateSection")]
        public ActionResult<ResponseModel> UpdateSection([FromBody] SectionModel sectionModel)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _sectionRepository.UpdateData(sectionModel);
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

        [HttpPost("[controller]/DeleteSection")]
        public ActionResult<ResponseModel> DeleteSection(int id)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                _sectionRepository.DeleteData(id);
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

        [HttpPost("[controller]/UploadFile")]
        public ActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine("D:\\SEMESTER 3\\PRG 4\\project astralearn\\SystemAstraLearn_Kelompok3\\SystemAstraLearn_Kelompok3\\wwwroot\\assets\\Upload", fileName); // Sesuaikan dengan direktori yang diinginkan

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
