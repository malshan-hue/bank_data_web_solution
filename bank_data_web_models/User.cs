using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace bank_data_web_models
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public string? PasswordSalt { get; set; }

        public int ActivationCode { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastLogginDate { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
		[DefaultValue("false")]
		public bool IsAdmin { get; set; }

		public Guid UserGlobalIdentity { get; set; }

        #region NAVIGATIONAL PROPERTIES
        public UserInformation UserInformation { get; set; }
        #endregion

    }
}
