using JobCandidateHubAPI.DataAccess;

namespace JobCandidateHubAPI.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CandidateDbContext _context;

        public CandidateRepository(CandidateDbContext context)
        {
            _context = context;
        }
        public Candidate GetByEmail(string email) => _context.Candidates.Find(email);

        public void Add(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            _context.SaveChanges();
        }

        public void Update(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            _context.SaveChanges();
        }

    }
}
