using Newtonsoft.Json;
using RepoAPI.Entities;
using RepoAPI.Exceptions;
using RepoAPI.Models;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace RepoAPI.Services
{
    public class ClientHttpService : IClientHttpService
    {

        
        public async Task<List<RepoBranchCommitDto>> GetAllReposAsync(string userName)   
        {


            var reposResults = new List<RepoBranchCommitDto>();
            string url = $"https://api.github.com/users/{userName}/repos";

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("User-Agent", "C# HttpClient");
               

                var result = await client.GetAsync(url);
            

                if (result.IsSuccessStatusCode)
                {             
                                  
                    var repos = await result.Content.ReadAsStringAsync();                

                    var finalRepos = JsonConvert.DeserializeObject<List<Repository>>(repos);

                    var reposNotForks = finalRepos.Where(x => x.Forks == 0).ToList();
                    
                    
                    foreach(var repo in reposNotForks)
                    {
                        url = $"https://api.github.com/repos/{userName}/{repo.Name}/branches";

                        var branches = await client.GetAsync(url);

                        if (branches.IsSuccessStatusCode)
                        {
                           var branchesString = await branches.Content.ReadAsStringAsync();                        

                            var branchesJson = JsonConvert.DeserializeObject<List<Branch>>(branchesString);

                            foreach(var branch in branchesJson)
                            {
                                reposResults.Add(new RepoBranchCommitDto() { RepoName = repo.Name, BranchName = branch.Name, CommitSha = branch.Commit.Sha });
                            }


                        }
                        else
                        {
                            throw new ReadDataException("Cannot read the data");
                        }
                    

                    }
                    

                    return reposResults;

                }
                else
                {
                    throw new ReadDataException("User not found");
                }

            }

        }
    }
}
