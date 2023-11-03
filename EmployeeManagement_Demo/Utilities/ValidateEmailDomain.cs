using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement_Demo.Utilities
{
    public class ValidateEmailDomain: ValidationAttribute
    {
        private readonly string[] allowedDomain;

        public ValidateEmailDomain(params string[] allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }
        public override bool IsValid(object? value)
        {
            if (value == null || value is not string)
            {
                return false;
            }

            else
            {
                var emailString = value.ToString().Split('@');

                if(emailString.Length != 2)
                    return false;

                var domain = emailString[1];
                return allowedDomain.Any(allowedDomain => 
                string.Equals(domain, allowedDomain, StringComparison.OrdinalIgnoreCase));

            }
            
            
            //return emailString[1].ToUpper() == allowedDomain[1].ToUpper();
        }
    }
}
