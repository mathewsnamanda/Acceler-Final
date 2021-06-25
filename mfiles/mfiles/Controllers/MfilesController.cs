using mfiles.Entities;
using mfiles.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Net;
using Mfiles.Models;
using public_link.Models.models;
using Mfiles.Models.model1;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mfiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MfilesController : ControllerBase
    {
        private readonly IBidCategory _repo;

        private IConfiguration Configuration;

        public MfilesController(IBidCategory bidCategory, IConfiguration _configuration)
        {
            _repo = bidCategory ?? throw new ArgumentNullException(nameof(bidCategory));
            Configuration = _configuration ?? throw new ArgumentNullException(nameof(_configuration));
            
        }
      
        // GET: api/<MfilesController>
        [HttpGet]
        public ActionResult Get([FromQuery]int Applicantid, [FromQuery] string Startdate, [FromQuery] string enddate, [FromQuery] string Username, [FromQuery] string Password)
        {
            var test = 1;
            string connString = this.Configuration.GetConnectionString("MfilesUrl");
            string authentication = "";
			if (!string.IsNullOrWhiteSpace(Username))
				Username = Username.Trim();
			if (!string.IsNullOrWhiteSpace(Password))
				Password = Password.Trim();
			if (string.IsNullOrWhiteSpace(Applicantid.ToString()))
			{
				return BadRequest("Applicantid is required");
			}

            if (Applicantid <= 0)
                throw new ArgumentNullException(nameof(Applicantid));
            var responsepp = _repo.GetCore_Bids(Applicantid);

            List<core_bid> core_Bids = new List<core_bid>(responsepp);
            IDictionary<string, List<Example>> numberNames = new Dictionary<string, List<Example>>();


            foreach (var iteminlist in core_Bids)
            {
                var piter = _repo.GetCore_Category(iteminlist.category_id);
                var jsonSerializer = JsonSerializer.CreateDefault();
                var username = Username;
                var password = Password;
                var guid = "200DC8E4-F0F2-431D-BDAB-FBA1231C2B8C";
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

                itemtop.Add("1308");
                authentication = authenticationToken;
                var commands = new List<Example>();
                foreach (var p in itemtop)
                {
                    var clientz = new RestClient($"{connString}/REST/objects/104/e{iteminlist.id}/latest");
                    clientz.Timeout = -1;
                    var requestz = new RestRequest(Method.GET);
                    requestz.AddHeader("X-Authentication", $"{authenticationToken}");
                    IRestResponse responsez = clientz.Execute(requestz);
                    pike accountp = JsonConvert.DeserializeObject<pike>(responsez.Content);



                    var client = new RestClient($"{connString}/REST/objects.aspx?o=0&p1030={accountp.ObjVer.ID}");
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
                        numberNames.Add(piter.name, commands);
                    }

                }


            }

            return new JsonResult(numberNames);
		}

        // GET api/<MfilesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MfilesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MfilesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MfilesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
