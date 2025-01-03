namespace JobCandidateHubAPI.Repository
{
    public interface ICandidateRepository
    {
        void Add(Candidate candidate); 
        void Update(Candidate candidate);
        bool ExistsByEmail(string email);
    }
}
