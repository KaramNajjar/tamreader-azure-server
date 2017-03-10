using ContactList.App_Start;
using ContactList.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace ContactList.Controllers
{
    public class SignsController : ApiController
    {

        private const string FILENAME = "Signs.json";
        
        public SignsController()
        {
            SignsLogic.CreateSignsTable("Signs");
            SignsLogic.CreateSignsTable("Users");
        }

        /// <summary>
        /// Gets the list of signs
        /// </summary>
        /// <returns>The signs</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK,
            Type = typeof(IEnumerable<Sign>))]
        [Route("~/signs")]
        public List<Sign> Get()
        {
            return SignsLogic.GetSigns();
        }

        /// <summary>
        /// Gets a specific sign
        /// </summary>
        /// <param name="Name">Identifier for the sign</param>
        /// <returns>The requested sign</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK,
            Description = "OK",
            Type = typeof(IEnumerable<Sign>))]
        [SwaggerResponse(HttpStatusCode.NotFound,
            Description = "Sign not found",
            Type = typeof(IEnumerable<Sign>))]
        [SwaggerOperation("GetSignByName")]
        [Route("~/signs/{name}")]
        public Sign Get([FromUri] String name)
        {
            var signs = SignsLogic.GetSigns();
            return signs.FirstOrDefault(x => x.Name == name);
        }


        /// <summary>
        /// Creates a new sign
        /// </summary>
        /// <param name="sign">The new sign</param>
        /// <returns>The saved sign</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created,
            Description = "Post",
            Type = typeof(userSign))]
        [Route("~/Signs")]
        public userSign Post([FromBody] userSign usign)
        {
            
            SignsLogic.AddSign(usign.Us);
            SignsLogic.AddUserInfo(usign);

            return usign;
        }

        /*
                /// <summary>
                /// Deletes a sign
                /// </summary>
                /// <param name="id">Identifier of the sign to be deleted</param>
                /// <returns>True if the sign was deleted</returns>
                [HttpDelete]
                [SwaggerResponse(HttpStatusCode.OK,
                    Description = "OK",
                    Type = typeof(bool))]
                [SwaggerResponse(HttpStatusCode.NotFound,
                    Description = "Sign not found",
                    Type = typeof(bool))]
                [Route("~/signs/{name}")]
                public async Task<HttpResponseMessage> Delete([FromUri] String name)
                {
                    var signs = await GetSigns();
                    var signsList = signs.ToList();

                    if (!signsList.Any(x => x.Name == name))
                    {
                        return Request.CreateResponse<bool>(HttpStatusCode.NotFound, false);
                    }
                    else
                    {
                        signsList.RemoveAll(x => x.Name == name);
                        await _storage.Save(signsList, FILENAME);
                        return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
                    }
                }

            */



    }
}