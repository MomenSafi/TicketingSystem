using System.ComponentModel.DataAnnotations;

namespace TickitingSystems.Models
{
    public class ProjectModel
    {
        public int ProjectId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public string ProjectName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public DateTime DeliverDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public string ClientName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public string ContactPerson { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is Required")]
        public bool IsUnderSupport { get; set; }
    }
}
