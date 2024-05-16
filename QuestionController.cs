using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UnitPractical.DTO;
using UnitPractical.Model;
using UnitPractical.Repository.Interface;

namespace UnitPractical.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepo _questionRepo;

        public QuestionController(IQuestionRepo questionRepo)
        {
            _questionRepo = questionRepo;
        }


        //GET QUESTION BY ID
        [HttpGet("GetQuestionsByTypeId/{questionTypeId}")]
        public async Task<IActionResult> GetQuestionsByTypeId(int questionTypeId)
        {
            try
            {
                var questions = await _questionRepo.GetQuestionsByTypeIdAsync(questionTypeId);
                return Ok(questions);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        //CREATE QUESTION
        [HttpPost("CreateQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionDTO questionDTO)
        {
            try
            {
                // Validate the incoming QuestionTypeID
                if (questionDTO.QuestTypeID <= 0)
                {
                    return BadRequest("Invalid Question Type ID.");
                }

                // Create the question using the repository
                Question createdQuestion = await _questionRepo.CreateQuestionAsync(questionDTO);

                return Ok(createdQuestion); // Return the created question
            }
            catch (Exception)
            {
                // Log the exception or handle it as per your application's requirements
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        //UPDATE QUESTION
        [HttpPut("UpdateQuestion/{questionId}")]
        public async Task<IActionResult> UpdateQuestion(int questionId, [FromBody] QuestionDTO questionDTO)
        {
            try
            {
                // Validate the incoming QuestionTypeID
                if (questionDTO.QuestTypeID <= 0)
                {
                    return BadRequest("Invalid Question Type ID.");
                }

                // Retrieve the existing question from the repository
                var existingQuestion = await _questionRepo.GetQuestionByIdAsync(questionId);

                if (existingQuestion == null)
                {
                    return NotFound("Question not found.");
                }

                // Update the properties of the existing question
                existingQuestion.QuestionText = questionDTO.QuestionText;
                existingQuestion.QuestTypeID = questionDTO.QuestTypeID; // Update QuestTypeID if needed

                // Update the question using the repository
                Question updatedQuestion = await _questionRepo.UpdateQuestionAsync(existingQuestion);

                return Ok(updatedQuestion); // Return the updated question
            }
            catch (Exception)
            {
                // Log the exception or handle it as per your application's requirements
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }





        //DELETE QUESTION
        [HttpDelete("DeleteQuestion/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            try
            {
                // Delete the question using the repository
                await _questionRepo.DeleteQuestionAsync(questionId);

                return Ok("Question deleted successfully.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }



    }
}
