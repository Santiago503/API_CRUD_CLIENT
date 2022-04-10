using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICrudClient.Model
{
    public class ClientAddress
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Country is Required")]
        [StringLength(50, ErrorMessage = "Country length can't be more than 50.")]
        public int Country { get; set; }

        [Required(ErrorMessage = "City is Required")]
        [StringLength(50, ErrorMessage = "City length can't be more than 50.")]
        public int City { get; set; }


        [Required(ErrorMessage = "Address is Required")]
        [StringLength(50, ErrorMessage = "Address length can't be more than 50.")]
        public int Address { get; set; }
    }
}
