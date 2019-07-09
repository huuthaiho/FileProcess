using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DataModel
{
    public class Employeer
    {
        public Employeer() { }
        [RegularExpression(@"(?i)(company|Inc|LLC|Corp)")]
        [MinLength(0)]
        public string CompanyName { get; set; }

        [RegularExpression(@"^[0-9]{1,4}$")]
        [MinLength(1)]
        public string YearOfBusiness { get; set; }

        [RegularExpression(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})")]
        [MinLength(0)]
        public string Email { get; set; }

        [RegularExpression(@"^([\+][0-9]{1,3}([ \.\-])?)?([\(]{1}[0-9]{3}[\)])?([0-9A-Z \.\-]{1,32})((x|ext|extension)?[0-9]{1,4}?)$")]
        [MinLength(8)]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^[a-zA-Z][0-9a-zA-Z .,'-]*$")]
        [MinLength(0)]
        public string Contactor { get; set; }

        //test
    }
}