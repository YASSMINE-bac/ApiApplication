﻿namespace API.entities
{
    public class Employer : Person
    {
        public new int Id { get; set; }
        public string EmployerType { get; set; }
        public string TypeEquipe { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
