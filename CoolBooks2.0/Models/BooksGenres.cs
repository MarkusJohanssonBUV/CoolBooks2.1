﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CoolBooks.Models
{
    public partial class BooksGenres
    {

        public int BooksID { get; set; }
        public Books Books { get; set; }

        public int GenreID { get; set; }
        public Genres Genre { get; set; }
    }
}