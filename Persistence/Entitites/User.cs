﻿using TP_Complot_Rest.Persistence.Entitites;

namespace TP_Complot_Rest.Persistence.Entities

{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public List<Complot> Complots { get; set; }
        public string Email { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; }
    }
}