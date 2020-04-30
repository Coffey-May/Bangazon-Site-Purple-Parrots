using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models
{
    public class LikeProduct
    {
        [Key]
        public int LikeId { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public bool Like { get; set; }

    }
}
