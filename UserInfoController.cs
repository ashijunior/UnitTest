using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UnitPractical.DTO;
using UnitPractical.Model;
using UnitPractical.Repository.Interface;

namespace UnitPractical.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoRepo _userInfoRepo;

        public UserInfoController(IUserInfoRepo userInfoRepo)
        {
            _userInfoRepo = userInfoRepo;
        }

        //GET USER INFORMATION BY ID
        [HttpGet("GetUserInfo/{id}")]
        public async Task<IActionResult> GetUserInfo(int id)
        {
            try
            {
                // Retrieve user info by ID using UserInfoRepo
                UserInfo userInfo = await _userInfoRepo.GetUserInfoByIdAsync(id);

                if (userInfo != null)
                {
                    // Map UserInfo to UserInfoDTO if needed
                    UserInfoDTO userInfoDTO = new UserInfoDTO
                    {
                        ID = userInfo.ID,
                        firstName = userInfo.firstName,
                        lastName = userInfo.lastName,
                        email = userInfo.email,
                        phoneNumber = userInfo.phoneNumber,
                        nationality = userInfo.nationality,
                        currentResidence = userInfo.currentResidence,
                        idNumber = userInfo.idNumber,
                        dateOfBirth = userInfo.dateOfBirth,
                        gender = userInfo.gender,
                        personalInfo = userInfo.personalInfo,
                        gradYear = userInfo.gradYear,
                        multipleChoices = userInfo.multipleChoices,
                        rejection = userInfo.rejection,
                        yearExperience = userInfo.yearExperience,
                        dateOfRelocation = userInfo.dateOfRelocation
                    };

                    return Ok(userInfoDTO); // Return user info DTO if found
                }
                else
                {
                    return NotFound("User not found."); // Return not found if user info not found
                }
            }
            catch (Exception)
            {
                // Log the exception or handle it as per your application's requirements
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        //CREATE USER INFORMATION
        [HttpPost("CreateUserInfo")]
        public async Task<IActionResult> CreateUserInfo([FromBody] UserInfoDTO userInfoDTO)
        {
            try
            {
                // Map UserInfoDTO to UserInfo
                UserInfo userInfo = new UserInfo
                {
                    ID = userInfoDTO.ID,
                    firstName = userInfoDTO.firstName,
                    lastName = userInfoDTO.lastName,
                    email = userInfoDTO.email,
                    phoneNumber = userInfoDTO.phoneNumber,
                    nationality = userInfoDTO.nationality,
                    currentResidence = userInfoDTO.currentResidence,
                    idNumber = userInfoDTO.idNumber,
                    dateOfBirth = userInfoDTO.dateOfBirth,
                    gender = userInfoDTO.gender,
                    personalInfo = userInfoDTO.personalInfo,
                    gradYear = userInfoDTO.gradYear,
                    multipleChoices = userInfoDTO.multipleChoices,
                    rejection = userInfoDTO.rejection,
                    yearExperience = userInfoDTO.yearExperience,
                    dateOfRelocation = userInfoDTO.dateOfRelocation
                };

                // Add user info using UserInfoRepo
                UserInfo addedUserInfo = await _userInfoRepo.AddUserInfoAsync(userInfo);

                if (addedUserInfo != null)
                {
                    return Ok(addedUserInfo); // Return the added user info if successful
                }
                else
                {
                    return BadRequest("Failed to add user info."); // Return bad request if addition fails
                }
            }
            catch (Exception)
            {
                // Log the exception or handle it as per your application's requirements
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        [HttpPut("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserInfoDTO userInfoDTO)
        {
            try
            {
                // Check if user exists by ID
                UserInfo existingUser = await _userInfoRepo.GetUserInfoByIdAsync(userInfoDTO.ID);
                if (existingUser == null)
                {
                    return NotFound("User not found."); // Return not found if user does not exist
                }

                // Update user information
                existingUser.firstName = userInfoDTO.firstName;
                existingUser.lastName = userInfoDTO.lastName;
                existingUser.email = userInfoDTO.email;
                existingUser.phoneNumber = userInfoDTO.phoneNumber;
                existingUser.nationality = userInfoDTO.nationality;
                existingUser.currentResidence = userInfoDTO.currentResidence;
                existingUser.idNumber = userInfoDTO.idNumber;
                existingUser.dateOfBirth = userInfoDTO.dateOfBirth;
                existingUser.gender = userInfoDTO.gender;
                existingUser.personalInfo = userInfoDTO.personalInfo;
                existingUser.gradYear = userInfoDTO.gradYear;
                existingUser.multipleChoices = userInfoDTO.multipleChoices;
                existingUser.rejection = userInfoDTO.rejection;
                existingUser.yearExperience = userInfoDTO.yearExperience;
                existingUser.dateOfRelocation = userInfoDTO.dateOfRelocation;

                // Update user info using UserInfoRepo
                UserInfo updatedUserInfo = await _userInfoRepo.UpdateUserAsync(existingUser);

                return Ok(updatedUserInfo); // Return the updated user info
            }
            catch (Exception)
            {
                // Log the exception or handle it as per your application's requirements
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpDelete("DeleteUserInfo/{id}")]
        public async Task<IActionResult> DeleteUserInfo(int id)
        {
            try
            {
                // Check if user exists by ID
                UserInfo existingUser = await _userInfoRepo.GetUserInfoByIdAsync(id);
                if (existingUser == null)
                {
                    return NotFound("User not found."); // Return not found if user does not exist
                }

                // Delete user using UserInfoRepo
                await _userInfoRepo.DeleteUserAsync(id);

                return Ok("User deleted successfully."); // Return success message
            }
            catch (Exception)
            {
                // Log the exception or handle it as per your application's requirements
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


    }
}
