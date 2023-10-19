using System.ComponentModel.DataAnnotations;

namespace TickitingSystems.Models
{
    public class IssueDetailsModel
    {
        public int IssueDetailsId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public string IssueDescription { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public int ProjectId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public int ServerityId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public int IssueStatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public DateTime? ClosedDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public string EnviromentType { get; set; }

        public string ProjectName { get; set; }

        public string ServerityName { get; set; }

        public string IssueStatusName { get; set; }
    }
}
