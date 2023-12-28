using Microsoft.AspNetCore.Mvc;

namespace AstraLearn_API_Kel3.Model
{
    public class ResponseModel
    {
        public int status { get; set; }
        public string message { get; set; }
        public Object data { get; set; }
    }
}
