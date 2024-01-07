namespace AstraLearn_API_Kel3.Model
{
    public class ExerciseModel
    {
        public int id_exercise { get; set; }
        public int id_section { get; set; }
        public string soal { get; set; }
        public string pilgan1 { get; set; }
        public string pilgan2 { get; set; }
        public string pilgan3 { get; set; }
        public string pilgan4 { get; set; }
        public string pilgan5 { get; set; }
        public string kunci_jawaban { get; set; }
        public int status { get; set; }
    }
}
