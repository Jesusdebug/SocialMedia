﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SocialMedia.Core.Entities
{
    public partial class Comment:BaseEntity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Descripcion { get; set; }
        public DateTime Date { get; set; }
        public bool IsActivo { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}