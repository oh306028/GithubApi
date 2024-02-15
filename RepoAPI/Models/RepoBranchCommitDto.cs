namespace RepoAPI.Models
{
    public class RepoBranchCommitDto
    {
        public string RepoName { get; set; }
        public string BranchName { get; set; }
        public string CommitSha { get; set; }   
    }
}
