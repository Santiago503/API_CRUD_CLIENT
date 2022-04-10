using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APICrudClient.Model
{
    public class Client
    {
        public Client()
        {
            ClientAddress = new List<ClientAddress>();
        }

        [Key]
        public int Id { get; set; }
        
        
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        public string Name { get; set; }
        
        
        [Required(ErrorMessage = "Lastname is Required")]
        [StringLength(50, ErrorMessage = "LastName length can't be more than 50.")]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [StringLength(16, ErrorMessage = "CellPhone length can't be more than 16.")]
        public string Cellphone { get; set; }


        public char Gender { get; set; }
        public char Status { get; set; }

        public virtual ICollection<ClientAddress> ClientAddress { get; set; }

    }
}
