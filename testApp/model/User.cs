using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace testApp
{
    public class User: ICloneable
    {     

        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool Type { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }     
    }
}
