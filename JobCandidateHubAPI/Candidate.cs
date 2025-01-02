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

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
