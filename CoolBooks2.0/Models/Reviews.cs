// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using CoolBooks.Areas.Identity;

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoolBooks.Models
{
    public partial class Reviews
    {
        public int ReviewsID { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string? Title { get; set; }

        [StringLength(1000, MinimumLength = 3)]
        [Required]
        public string? Text { get; set; }

        public int? Rating { get; set; }
        public bool IsDeleted { get; set; }

        public bool Flag { get; set; }


        [DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyy}", ApplyFormatInEditMode = true)]
        public DateTime? Created { get; set; }
        public int BookID { get; set; }
        public Books Book { get; set; }
        public ICollection<ReviewComents> ReviewComent{ get; set; }

        public string UserName{ get; set; }

        public string ClientId { get; set; }


        public ApplicationUser Client { get; set; }

        //public int UserID { get; set; }
        //public UserInfo User { get; set; }
    }
}