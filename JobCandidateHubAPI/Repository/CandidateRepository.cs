using JobCandidateHubAPI.DataAccess;
using Microsoft.Extensions.Caching.Memory;

namespace JobCandidateHubAPI.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CandidateDbContext _context;
        private readonly IMemoryCache _cache;
        private const string EmailCacheKey = "CandidateEmails";

        public CandidateRepository(CandidateDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;

        }
        public bool ExistsByEmail(string email)
        {
            if (!_cache.TryGetValue(EmailCacheKey, out HashSet<string> emailSet))
            {
                emailSet = _context.Candidates.Select(c => c.Email).ToHashSet();
                _cache.Set(EmailCacheKey, emailSet, TimeSpan.FromMinutes(10));
            }
            return emailSet.Contains(email);
        }

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
