using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MScBank.Models {
    public class Min18yrsToSignUpValidation : ValidationAttribute{

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            return base.IsValid(value, validationContext);
        }
    }
}