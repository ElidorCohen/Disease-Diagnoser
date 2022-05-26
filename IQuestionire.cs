using System.Windows.Forms;

namespace WindowsFormsApp1
{ 
    public enum QuestionType
    {
        IsEasternen,
        IsSmoking,
        HasFever,
        SmokingLungs,
        DiarrheaVomits,
        isEthyopian,
        isPregnant

    }
    public interface IQuestionire
    {
        Question Question { get; set; }
        PatientDiagnoser PatientDiagnoser { get; set; }
    }  

    public class Question
    {
        public QuestionType QuestionType { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DialogResult Result;

        public Question(QuestionType type, string text, string title = "Question")
        {
            QuestionType = type;
            Title = title;
            Text = text;
            Result = DialogResult.None;
        }
    }
}
