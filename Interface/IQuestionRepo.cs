using UnitPractical.DTO;
using UnitPractical.Model;

namespace UnitPractical.Repository.Interface
{
    public interface IQuestionRepo
    {
        //QUESTION TYPE
        Task<List<QuestionType>> GetQuestionTypeAsync();
        Task<QuestionType> GetQuestionTypeByIdAsync(int id);
        Task<QuestionType> CreateQuestionTypeAsync(QuestionType questionType);
        Task<QuestionType> UpdateQuestionTypeAsync(QuestionType questionType);
        Task DeleteQuestionTypeAsync(int id);


        //QUESTIONS
        Task<List<Question>> GetQuestionsByTypeIdAsync(int questionTypeId);
        Task<Question> GetQuestionByIdAsync(int questionId);
        Task<Question> CreateQuestionAsync(QuestionDTO questionDTO);
        Task<Question> UpdateQuestionAsync(Question updatedQuestion);
        Task DeleteQuestionAsync(int questionId);
    }
}
