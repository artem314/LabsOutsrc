//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lab4
{
    using System;
    using System.Collections.Generic;
    
    public partial class Review
    {
        public int Id { get; set; }
        public Nullable<int> Cinema { get; set; }
        public Nullable<int> Movie { get; set; }
        public string Review_text { get; set; }
        public int Rating { get; set; }
    
        public virtual Cinema Cinema1 { get; set; }
        public virtual Movy Movy { get; set; }
    }
}
