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
    public class QuestionTypeController : ControllerBase
    {
        private readonly IQuestionRepo _questionRepo;

        public QuestionTypeController(IQuestionRepo questionRepo)
        {
            _questionRepo = questionRepo;
        }


        //GET ALL QUESTION TYPE

        [HttpGet("GetAllQuestionTypes")]
        public async Task<IActionResult> GetAllQuestionTypes()
        {
            try
            {
                // Retrieve all question types using the repository
                var questionTypes = await _questionRepo.GetQuestionTypeAsync();

                return Ok(questionTypes); // Return the list of question types
            }
            catch (Exception)
            {
                // Log the exception or handle it as per your application's requirements
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        //CREATE TYPE OF QUESTION TYPE
        [HttpPost("CreateQuestionType")]
        public async Task<IActionResult> CreateQuestionType([FromBody] QuestionTypeDTO questionTypeDTO)
        {
            try
            {

                // Map QuestionTypeDTO to QuestionType
                QuestionType questionType = new QuestionType
                {
                    QuestionID = questionTypeDTO.QuestionID,
                    QuestionTypeName = questionTypeDTO.QuestionTypeName
                };

                // Create question type using QuestionTypeRepo
                QuestionType createdQuestionType = await _questionRepo.CreateQuestionTypeAsync(questionType);

                return Ok(createdQuestionType); // Return the created question type
            }
            catch (Exception)
            {
                // Log the exception or handle it as per your application's requirements
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        //EDIT QUESTION TYPES
        [HttpPut("EditQuestionType")]
        public async Task<IActionResult> UpdateQuestionType([FromBody] QuestionTypeDTO questionTypeDTO)
        {
            try
            {
                // Check if the question type exists by ID
                QuestionType existingQuestionType = await _questionRepo.GetQuestionTypeByIdAsync(questionTypeDTO.QuestionID);
                if (existingQuestionType == null)
                {
                    return NotFound("Question type not found."); // Return not found if question type does not exist
                }

                // Update question type information
                existingQuestionType.QuestionTypeName = questionTypeDTO.QuestionTypeName;

                // Update question type using QuestionTypeRepo
                QuestionType updatedQuestionType = await _questionRepo.UpdateQuestionTypeAsync(existingQuestionType);

                return Ok(updatedQuestionType); // Return the updated question type
            }
            catch (Exception)
            {
                // Log the exception or handle it as per your application's requirements
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        //DELETE QUESTION TYPE
        [HttpDelete("DeleteQuestionType/{id}")]
        public async Task<IActionResult> DeleteQuestionType(int id)
        {
            try
            {
                // Check if the question type exists by ID
                QuestionType existingQuestionType = await _questionRepo.GetQuestionTypeByIdAsync(id);
                if (existingQuestionType == null)
                {
                    return NotFound("Question type not found."); // Return not found if question type does not exist
                }

                // Delete question type using QuestionTypeRepo
                await _questionRepo.DeleteQuestionTypeAsync(id);

                return Ok("Question type deleted successfully."); // Return success message
            }
            catch (Exception)
            {
                // Log the exception or handle it as per your application's requirements
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }



    }
}



