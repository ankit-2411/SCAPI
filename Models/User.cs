using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCAPI.Models
{
    /// <summary>
    /// Main user model for the operations.
    /// </summary>
    public class UserBase
    {
        /// <summary>
        /// Value represents the amount requested by the user.
        /// </summary>
        public int RequestedAmount { get; set; }

        /// <summary>
        /// Value represents the incorporation date by the user.
        /// </summary>
        public DateTime IncorporationDate { get; set; }

        /// <summary>
        /// Value represents the purpose of the loan requested by the user.
        /// </summary>
        public string LoanPurpose { get; set; }

        /// <summary>
        /// Value represents the industry the user is from.
        /// </summary>
        public string Industry { get; set; }

        /// <summary>
        /// Value represents the revenue of the user.
        /// </summary>
        public int Revenue { get; set; }
    }

    /// <summary>
    /// Extends UserBase to add additional UserId.
    /// </summary>
    public class User : UserBase
    {
        /// <summary>
        /// System generated id when the user is created.
        /// </summary>
        public int UserId { get; set; }

    }
}