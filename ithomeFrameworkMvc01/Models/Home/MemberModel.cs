using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ithomeFrameworkMvc01.Models.Home
{
    public class MemberModel : IValidatableObject
    {
        public MemberModel()
        {

        }

        public string Account { get; set; }
        public string Password { get; set; }
        public string AgainPassword { get; set; }
        public string City { get; set; }
        public string Village { get; set; }
        public string Address { get; set; }

        public class ViewCity
        {
            public string CityId { set; get; }
            public string CityName { set; get; }
        }

        public class ViewVillage
        {
            public string VillageId { get; set; }
            public string VillageName { get; set; }
        }

        /// <summary>
        /// 驗證
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(this.Password != this.AgainPassword)
            {
                yield return new ValidationResult("AgainPassword is not equal to Equre Password");
            }

            var regex = new Regex("^[a-zA-Z0-9]+$");

            if (!regex.IsMatch(this.Account))
            {
                yield return new ValidationResult("English Or Number", new string[] { "Account" });
            }

            if (!regex.IsMatch(this.Password))
            {
                yield return new ValidationResult("English Or Number", new string[] { "Password" });
            }

            if (!regex.IsMatch(this.AgainPassword))
            {
                yield return new ValidationResult("English Or Number", new string[] { "AgainPassword" });
            }

            if (string.IsNullOrWhiteSpace(this.City))
            {
                yield return new ValidationResult("Required", new string[] { "City" });
            }

            if (string.IsNullOrWhiteSpace(this.Village))
            {
                yield return new ValidationResult("Required", new string[] { "Village" });
            }

            if (string.IsNullOrWhiteSpace(this.Address))
            {
                yield return new ValidationResult("Required", new string[] { "Address" });
            }
        }
    }
}