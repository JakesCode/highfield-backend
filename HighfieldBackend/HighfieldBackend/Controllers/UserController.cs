using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using HighfieldBackend.Classes;
using System.Web.Http.Cors;

namespace HighfieldBackend.Controllers
{
    public class UserController : ApiController
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<List<UserEntity>> GetAPIResponse()
        {
            var userInformation = await client.GetAsync("https://recruitment.highfieldqualifications.com/api/test");
            if (userInformation.IsSuccessStatusCode)
            {
                var userContent = await userInformation.Content.ReadAsStringAsync();
                List<UserEntity> userEntities = JsonConvert.DeserializeObject<List<UserEntity>>(userContent);
                return userEntities;
            } else
            {
                // Handle Error //
                Console.WriteLine("An error has occurred whilst trying to access to HighFields API.");
                return null;
            }
        }

        public List<TopColoursDTO> CalculateTopColours(List<UserEntity> users)
        {
            Dictionary<string, TopColoursDTO> topColours = new Dictionary<string, TopColoursDTO>();
            foreach(UserEntity user in users)
            {
                if(topColours.ContainsKey(user.favouriteColour))
                {
                    // Add to existing TopColoursDTO //
                    topColours[user.favouriteColour].count += 1;
                } else
                {
                    // Make a new one and add it to the dictionary //
                    topColours.Add(user.favouriteColour, new TopColoursDTO() { colour = user.favouriteColour, count = 1 });
                }
            }

            return topColours.OrderBy(topColour => topColour.Value.count)
                    .ThenBy(topColour => topColour.Value.colour)
                    .Select(topColour => topColour.Value)
                    .ToList();
        }

        public List<AgePlusTwentyDTO> CalculateAgePlusTwenty(List<UserEntity> users)
        {
            List<AgePlusTwentyDTO> agePlusTwenty = new List<AgePlusTwentyDTO>();
            int currentYear = DateTime.Now.Year;
            foreach(UserEntity user in users)
            {
                int originalAge = currentYear - user.dob.Year;
                agePlusTwenty.Add(new AgePlusTwentyDTO() { userId = user.id, originalAge = originalAge, agePlusTwenty = originalAge + 20 });
            }

            return agePlusTwenty;
        }

        [Route("api/users")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserInformation()
        {
            List<UserEntity> userEntities = await GetAPIResponse();

            if(userEntities != null)
            {
                // Get top colours //
                List<TopColoursDTO> topColours = CalculateTopColours(userEntities);

                // Get age plus twenty //
                List<AgePlusTwentyDTO> agePlusTwenty = CalculateAgePlusTwenty(userEntities);

                return Ok(new ResponseDTO() { ages = agePlusTwenty, topColours = topColours, users = userEntities });
            } else
            {
                return Ok("An error occurred whilst accessing the HighFields API.");
            }
            
        }

        [Route("")]
        [HttpGet]
        public string Default()
        {
            return "The API is now running, and can be accessed using /api/users.";
        }
    }
}
