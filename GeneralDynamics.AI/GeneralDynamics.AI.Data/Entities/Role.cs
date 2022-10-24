namespace GeneralDynamics.AI.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Role()
        {
            //this.Users = new HashSet<User>();
        }
    
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is required"), StringLength(10)]
        public string Code { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<User> Users { get; set; }
    }
}
