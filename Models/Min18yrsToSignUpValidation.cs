using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MScBank.Models {
    public class Min18yrsToSignUpValidation : ValidationAttribute{

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

            var user = (ApplicationUser) validationContext.ObjectInstance;

            if(CalculateAgeCorrect(user.DateOfBirth, DateTime.Now) >= 18) {
                return ValidationResult.Success;
            }

            return new ValidationResult("You must be at least 18 years old to sign up!");
        }

        private int CalculateAgeCorrect(DateTime birthDate, DateTime now) {
            int age = now.Year - birthDate.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                age--;

            return age;
        }
    }
}