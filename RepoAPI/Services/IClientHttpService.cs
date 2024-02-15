using RepoAPI.Entities;
using RepoAPI.Models;

namespace RepoAPI.Services
{
    public interface IClientHttpService
    {
        Task<List<RepoBranchCommitDto>> GetAllReposAsync(string userName);
            
    }
}