using System.ComponentModel.DataAnnotations;

namespace JobCandidateHubAPI
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"^(\d+)\s*(hours?|h)\s*(\d+)\s*(minutes?|m)$", ErrorMessage = "Time Interval is in invalid formate")]
        public string TimeInterval { get; set; }
        public string LinkedInProfileURL { get; set; }
        public string GitHubProfileURL { get; set; }
        
        [Required]
        public string Comment { get; set; }
    }
}
