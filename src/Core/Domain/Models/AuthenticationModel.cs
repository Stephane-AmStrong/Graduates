using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AuthenticationModel
    {
        public Dictionary<string, string> UserInfo { get; set; }
        public virtual Student AppUser { get; set; }
        public string Token { get; set; }
        //[JsonIgnore]
        public RefreshToken RefreshToken { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> ErrorDetails { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
