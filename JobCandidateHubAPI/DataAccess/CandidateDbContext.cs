using Microsoft.EntityFrameworkCore;

namespace JobCandidateHubAPI.DataAccess
{
    public class CandidateDbContext:DbContext
    {
        public CandidateDbContext(DbContextOptions<CandidateDbContext> options)
            : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }

    }
}
