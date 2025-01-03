using JobCandidateHubAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Candidate> ExistsByEmail(string email)
        {
            if (!_cache.TryGetValue(EmailCacheKey, out HashSet<string> emailSet))
            {
                emailSet =_context.Candidates.Select(c => c.Email).ToHashSet();
                _cache.Set(EmailCacheKey, emailSet, TimeSpan.FromMinutes(5));
            }
            if (emailSet.Contains(email)) { 
                return  await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
            }
            return null;
        }

        public async Task Add(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
        }

        public void Update(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
            _context.SaveChanges();
        }

    }
}
