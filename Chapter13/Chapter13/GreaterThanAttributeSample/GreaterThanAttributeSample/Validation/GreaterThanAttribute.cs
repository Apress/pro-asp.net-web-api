using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace GreaterThanAttributeSample.Validation {

    [AttributeUsage(AttributeTargets.Property)]
    public class GreaterThanAttribute : ValidationAttribute {

        public string OtherProperty { get; private set; }

        public override bool RequiresValidationContext {

            get {
                return true;
            }
        }

        public GreaterThanAttribute(string otherProperty) : 
            base(errorMessage: "The {0} field must be greater than the {1} field.") {

            if (string.IsNullOrEmpty(otherProperty)) { 

                throw new ArgumentNullException("otherProperty");
            }

            OtherProperty = otherProperty;
        }

        public override string FormatErrorMessage(string name) {

            return string.Format(
                CultureInfo.CurrentCulture,
                base.ErrorMessageString,
                name,
                OtherProperty);
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext) {

            IComparable firstValue = value as IComparable;
            IComparable secondValue = GetSecondValue(
                validationContext.ObjectType, 
                validationContext.ObjectInstance);

            if (firstValue == null || secondValue == null) {

                throw new InvalidCastException(
                    "The property types must implement System.IComparable");
            }

            if (firstValue.CompareTo(secondValue) < 1) { 

                return new ValidationResult(
                    this.FormatErrorMessage(
                        validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }

        private IComparable GetSecondValue(Type type, object instance) {

            PropertyInfo propertyInfo = 
                type.GetProperty(this.OtherProperty);

            if (propertyInfo == null) {

                throw new Exception(
                    string.Format(
                        "The property named {0} does not exist.", 
                        this.OtherProperty));
            }

            var value = propertyInfo.GetValue(instance, null);
            return value as IComparable;
        }
    }
}