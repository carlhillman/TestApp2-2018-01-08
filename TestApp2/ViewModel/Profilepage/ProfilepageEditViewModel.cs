using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestApp2.Models;


namespace TestApp2.ViewModel.Profilepage
{
    public class ProfilepageEditViewModel
    {   
    public string Id { get; set; }
    [Required]
    [RegularExpression("^[A-ZÅÄÖ][a-zåäö]+$", ErrorMessage = "Får inte bestå av siffror och mellanslag")]
    public string Stad { get; set;}
    [Required]
    [Range(18, 120, ErrorMessage = "Ålder får vara minst 18 och högst 120")]
    public int Alder { get; set;}
    public byte[]Bild { get; set;}   
    public Synlighet Synlighet { get; set;  }
        
    }
}