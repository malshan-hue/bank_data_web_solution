using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace bank_data_web_models
{
    public class UserInformation
    {
        public int UserInformationId { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public IFormFile? Picture { get; set; }

    }
}
