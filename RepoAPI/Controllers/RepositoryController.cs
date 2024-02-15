using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoAPI.Entities;
using RepoAPI.Models;
using RepoAPI.Services;

namespace RepoAPI.Controllers
{
    [Route("api/repos")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {

        IClientHttpService _httpClient;
        public RepositoryController(IClientHttpService httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<List<RepoBranchCommitDto>>> GetRepos([FromRoute] string userName)
        {
            var repositories = await _httpClient.GetAllReposAsync(userName);    

            return Ok(repositories);
        }
    }
}
