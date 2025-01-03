namespace JobCandidateHubAPI.Repository
{
    public interface ICandidateRepository
    {
        Task Add(Candidate candidate); 
        void Update(Candidate candidate);
        Task<Candidate> ExistsByEmail(string email);
    }
}
