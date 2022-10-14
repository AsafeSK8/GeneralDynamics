namespace GeneralDynamics.AI.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Role()
        {
            //this.Users = new HashSet<User>();
        }
    
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<User> Users { get; set; }
    }
}
