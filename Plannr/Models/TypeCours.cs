using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plannr.Models
{
    public class TypeCours
    {
        [Key]
        public int Id {get; set;}
        
        public string Type {get;set;}

      

    }
}