namespace LecX.Domain.Entities
{
    public class Question
    {
        public int QuestionID { get; set; }
        public int TestID { get; set; }
        public string QuestionContent { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public string CorrectAnswer { get; set; }

        public string? ImagePath { get; set; }
        public virtual Test Test { get; set; }
    }
}