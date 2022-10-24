namespace GeneralDynamics.AI.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class User
    {

        public User()
        { }

        //public override string UID => string.Format("{0}", Id);
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required"), StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "LastName is required"), StringLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required"), StringLength(320), RegularExpression(@"^$|^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$")]
        public string Email { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "UserName is required"), StringLength(50), RegularExpression(@"^([A-Za-z0-9_-]){3,20}$")]
        public string UserName { get; set; }
        [StringLength(60)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required"), StringLength(60), 
            RegularExpression(@"^(?=.*\w)(?=.*[0-9])(?=.*[A-Z])(?=.*[+=.\?\-,_!@#\$%&\*;:&])(?=.{6,40}$)((?!\(|\)|\^| |<|>|{|}|\[|\]|~|[\u0080-\u0100]|\/|'|\||\\).)*$")]
        [NotMapped]
        public string PasswordValidationInput { get; set; }
        public string Token { get; set; }
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
