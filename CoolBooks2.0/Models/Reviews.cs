﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using CoolBooks.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CoolBooks.Models
{
    public partial class Reviews
    {
        public int ReviewsID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Rating { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? Created { get; set; }
        public int BookID { get; set; }
        public Books Book { get; set; }
        public ICollection<ReviewComents> ReviewComent{ get; set; }

        public string ClientId { get; set; }

        public ApplicationUser Client { get; set; }

        //public int UserID { get; set; }
        //public UserInfo User { get; set; }
    }
}