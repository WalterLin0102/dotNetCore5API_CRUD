﻿using System;
using System.Collections.Generic;

#nullable disable

namespace dotNetCore5API_CRUD.Models
{
    public partial class Roysched
    {
        public string TitleId { get; set; }
        public int? Lorange { get; set; }
        public int? Hirange { get; set; }
        public int? Royalty { get; set; }

        public virtual Title Title { get; set; }
    }
}
