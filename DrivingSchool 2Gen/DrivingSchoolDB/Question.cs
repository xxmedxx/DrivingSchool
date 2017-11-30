namespace DrivingSchoolDB
{
    public class Question
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Audio { get; set; }
        public string Image { get; set; }
        public string Answeres { get; set; }
        public string CorrectAnswer { get; set; }

        public int SerieId { get; set; }

    }
}
