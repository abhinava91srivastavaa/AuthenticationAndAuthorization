//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;
namespace AuthenticationAndAuthorization
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserprofileandReg
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Required")]
        [RegularExpression(".+@.+\\.[a-z]+", ErrorMessage = "Must be a valid EmailID")]
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string CommunicationAddress { get; set; }
        public string Status { get; set; }
        public Nullable<int> UserType { get; set; }
    }
}
