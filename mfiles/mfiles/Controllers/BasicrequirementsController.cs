using Mfiles.Models;
using Mfiles.Models.model1;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using public_link.Models.models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicrequirementsController : ControllerBase
    {
        private IConfiguration Configuration;
        public BasicrequirementsController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        // GET: api/<BasicrequirementsController>
        [HttpGet]
        public ActionResult Get([FromQuery] int Applicantid, [FromQuery] string Username, [FromQuery] string Password)
        {
            string connString = this.Configuration.GetConnectionString("MfilesUrl");

            if (!string.IsNullOrWhiteSpace(Username))
                Username = Username.Trim();
            if (!string.IsNullOrWhiteSpace(Password))
                Password = Password.Trim();
            if (string.IsNullOrWhiteSpace(Applicantid.ToString()))
            {
                return BadRequest("Applicantid is required");
            }
			var jsonSerializer = JsonSerializer.CreateDefault();
			var username = Username;
			var password = Password;
			var guid = "200DC8E4-F0F2-431D-BDAB-FBA1231C2B8C";
			var ptt = "";
			// Create the authentication details.
			var auth = new
			{
				Username = Username,
				Password = Password,
				VaultGuid = "{200DC8E4-F0F2-431D-BDAB-FBA1231C2B8C}" // Use GUID format with {braces}.
			};

			var authenticationRequest = (HttpWebRequest)WebRequest.Create($"{connString}/REST/server/authenticationtokens.aspx");
			authenticationRequest.Method = "POST";
			using (var streamWriter = new StreamWriter(authenticationRequest.GetRequestStream()))
			{
				using (var jsonTextWriter = new JsonTextWriter(streamWriter))
				{
					jsonSerializer.Serialize(jsonTextWriter, auth);
				}
			}

			// Execute the request.
			var authenticationResponse = (HttpWebResponse)authenticationRequest.GetResponse();

			// Extract the authentication token.
			string authenticationToken = null;
			using (var streamReader = new StreamReader(authenticationResponse.GetResponseStream()))
			{
				using (var jsonTextReader = new JsonTextReader(streamReader))
				{
					authenticationToken = ((dynamic)jsonSerializer.Deserialize(jsonTextReader)).Value;
				}
			}
			List<string> itemtop = new List<string>();
			//itemtop.Add("7");
			//itemtop.Add("2");
			itemtop.Add("1308");

			var commands = new List<Example>();
			foreach (var p in itemtop)
			{
				var clientz = new RestClient($"{connString}/REST/objects/184/e{Applicantid}/latest");
				clientz.Timeout = -1;
				var requestz = new RestRequest(Method.GET);
				requestz.AddHeader("X-Authentication", $"{authenticationToken}");
				IRestResponse responsez = clientz.Execute(requestz);
				pike accountp = JsonConvert.DeserializeObject<pike>(responsez.Content);

                var client = new RestClient($"{connString}/REST/objects?o=0&p100=2&p1308={accountp.ObjVer.ID}");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("X-Authentication", $"{authenticationToken}");
                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() == "Forbidden")
                {
                    return Unauthorized(response.Content);
                }
                else
                {
                    Example account = JsonConvert.DeserializeObject<Example>(response.Content);
                    if (account.Items == null)
                    {
                        return NoContent();
                    }
                    else
                    {
                        foreach (var item in account.Items)
                        {
                            int fileID = 0;
                            int objid = item.ObjVer.ID;
                            int fileFileVersionType = 0;
                            int objtype = item.ObjVer.Type;
                            int objversion = item.ObjVer.Version;
                            string str1 = "";
                            foreach (var item1 in item.Files)
                            {
                                fileID = item1.ID;
                                fileFileVersionType = item1.FileVersionType;
                                str1 = item1.ID.ToString();
                            }
                            if (!string.IsNullOrWhiteSpace(str1))
                            {
                                public_link.Models.models.FileVer fileVer = new public_link.Models.models.FileVer();
                                public_link.Models.models.ObjVer objVer = new public_link.Models.models.ObjVer();

                                fileVer.ID = fileID;
                                fileVer.Version = -1;
                                fileVer.FileVersionType = fileFileVersionType;
                                fileVer.ExpirationTime = "2021-06-01T00:00:00Z";
                                fileVer.Description = "Created By " + Username;

                                objVer.ID = objid;
                                objVer.Type = objtype;
                                objVer.Version = objversion;

                                Root root = new Root();
                                root.FileVer = fileVer;
                                root.objVer = objVer;
                                string output = JsonConvert.SerializeObject(root);

                                var client1 = new RestClient($"{connString}/REST/sharedlinks?");
                                client1.Timeout = -1;
                                var request1 = new RestRequest(Method.POST);
                                request1.AddHeader("Content-Type", "application/json");
                                request1.AddHeader("X-Authentication", $"{authenticationToken}");
                                request1.AddParameter("application/json", output, ParameterType.RequestBody);
                                IRestResponse response1 = client1.Execute(request1);
                                string stres = response1.Content.Substring(14, 64);
                                item.Fileurl = $"{connString}/REST/sharedlinks/{guid}/{stres}/content";
                            }

                        }
                    }
                    commands = new List<Example> { account };
                }

            }

			return new JsonResult(commands);
        }

        // GET api/<BasicrequirementsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BasicrequirementsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BasicrequirementsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BasicrequirementsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
