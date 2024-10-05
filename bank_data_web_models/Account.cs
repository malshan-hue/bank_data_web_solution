using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_data_web_models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountHolderName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public DateTime CreatedDateTime { get; set; }
        [DefaultValue("false")]
        public bool IsDeleted { get; set; }

        #region NAVIGATIONAL PROPERTIES
        public UserInformation UserInformation { get; set; } = new UserInformation();
        #endregion
    }
}
