

using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using UnitPractical.Context;
using UnitPractical.DTO;
using UnitPractical.Model;
using UnitPractical.Repository.Interface;

namespace UnitPractical.Repository.Implementation
{
    public class QuestionRepo : IQuestionRepo
    {
        private readonly AppDBContext _context;
        
        public QuestionRepo(AppDBContext context)
        {
            _context = context;
        }

        //QUESTION TYPES
        public async Task<QuestionType> GetQuestionTypeByIdAsync(int id)
        {
            return await _context.QuestionTypes.FirstOrDefaultAsync(q => q.QuestionID == id);
        }

        public async Task<QuestionType> CreateQuestionTypeAsync(QuestionType questionType)
        {            
            QuestionType questionTypes = new QuestionType
            {
                QuestionID = questionType.QuestionID,
                QuestionTypeName = questionType.QuestionTypeName
            };

            _context.QuestionTypes.Add(questionType);
            await _context.SaveChangesAsync();
            return questionType;
        }

        public async Task<QuestionType> UpdateQuestionTypeAsync(QuestionType questionType)
        {
            _context.QuestionTypes.Update(questionType);
            await _context.SaveChangesAsync();
            return questionType;
        }

        public async Task DeleteQuestionTypeAsync(int id)
        {
            var questionType = await _context.QuestionTypes.FindAsync(id);
            if (questionType != null)
            {
                _context.QuestionTypes.Remove(questionType);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<List<QuestionType>> GetQuestionTypeAsync()
        {
            return await _context.QuestionTypes.ToListAsync();
        }



        //QUESTIONS
        public async Task<List<Question>> GetQuestionsByTypeIdAsync(int questionTypeId)
        {
            return await _context.Questions.Where(x => x.QuestTypeID == questionTypeId).ToListAsync();
        }

        public async Task<Question> CreateQuestionAsync(QuestionDTO questionDTO)
        {
            // Map QuestionDTO to Question
            Question question = new Question
            {
                QuestionText = questionDTO.QuestionText,
                QuestTypeID = questionDTO.QuestTypeID // Assign QuestTypeID from DTO
            };

            // Add question to context and save changes
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return question;
        }
        public async Task<Question> UpdateQuestionAsync(Question updatedQuestion)
        {
            // Retrieve the existing question from the database
            var existingQuestion = await _context.Questions.FindAsync(updatedQuestion.Id);

            if (existingQuestion == null)
            {
                throw new InvalidOperationException("Question not found.");
            }

            // Update the properties of the existing question based on the updatedQuestion entity
            existingQuestion.QuestionText = updatedQuestion.QuestionText;
            existingQuestion.QuestTypeID = updatedQuestion.QuestTypeID;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return existingQuestion;
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            var questionToDelete = await _context.Questions.FindAsync(questionId);

            if (questionToDelete == null)
            {
                throw new ArgumentException("Question not found.");
            }

            _context.Questions.Remove(questionToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Question> GetQuestionByIdAsync(int questionId)
        {
            return await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
        }

    }
}
