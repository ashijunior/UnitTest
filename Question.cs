

namespace UnitPractical.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int QuestTypeID { get; set; } // Add property for QuestionType Id
    }
}
