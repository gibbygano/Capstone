using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace com.WanderingTurtle
{
    public class Validator
    {
        //Validates that the given string only contains letters (no numbers)
        //Created by Matt Lapka 2/1/15
        public static bool ValidateString(string inputToValidate)
        {
            return Regex.IsMatch(inputToValidate, @"^[a-zA-Z]+$");
        }

        //Validates that the given string only contains letters (no numbers)
        //and meets a minimum length requirement
        //Created by Matt Lapka 2/1/15
        public static bool ValidateString(string inputToValidate, int minNumOfChars)
        {
            if (inputToValidate.Length >= minNumOfChars)
            {
                return Regex.IsMatch(inputToValidate, @"^[a-zA-Z]+$");
            }
            else
            {
                return false;
            }
        }

        //Validates that the given string only contains letters (no numbers)
        //and meets a min/max length requirement
        //Created by Matt Lapka 2/1/15
        public static bool ValidateString(string inputToValidate, int minNumOfChars, int maxNumOfChars)
        {
            if (inputToValidate.Length >= minNumOfChars && inputToValidate.Length <= maxNumOfChars)
            {
                return Regex.IsMatch(inputToValidate, @"^[a-zA-Z]+$");
            }
            else
            {
                return false;
            }
        }

        //Validates that the given string is numeric (numbers)
        //Does NOT check if it is an int, double, etc
        //Will return false on negatives and decimals - use other provided methods for those cases
        //also should be used if leading zeros are important
        //Created by Matt Lapka 2/1/15
        public static bool ValidateNumeric(string inputToValidate)
        {
            return Regex.IsMatch(inputToValidate, @"^[0-9]+$");
        }

        //Validates that the given string is numeric (numbers) & meets minimum length
        //Does NOT check if it is an int, double, etc
        //Will return false on negatives and decimals - use other provided methods for those cases
        //also should be used if leading zeros are important
        //Created by Matt Lapka 2/1/15
        public static bool ValidateNumeric(string inputToValidate, int minNumOfChars)
        {
            if (inputToValidate.Length >= minNumOfChars)
            {
                return Regex.IsMatch(inputToValidate, @"^[0-9]+$");
            }
            else
            {
                return false;
            }

        }
        //Validates that the given string is numeric (numbers) & meets minimum & max length requirements
        //Does NOT check if it is an int, double, etc
        //Will return false on negatives and decimals - use other provided methods for those cases
        //also should be used if leading zeros are important
        //Created by Matt Lapka 2/1/15
        public static bool ValidateNumeric(string inputToValidate, int minNumOfChars, int maxNumOfChars)
        {
            if (inputToValidate.Length >= minNumOfChars && inputToValidate.Length <= maxNumOfChars)
            {
                return Regex.IsMatch(inputToValidate, @"^[0-9]+$");
            }
            else
            {
                return false;
            }

        }

        //Validates that the given string is alphanumeric (only numbers & letters)
        //Created by Matt Lapka 2/1/15
        public static bool ValidateAlphaNumeric(string inputToValidate)
        {
            return Regex.IsMatch(inputToValidate, @"^[a-zA-Z0-9]+$");
        }

        //Validates that the given string is alphanumeric (only numbers & letters) & meets minimum length
        //Created by Matt Lapka 2/1/15
        public static bool ValidateAlphaNumeric(string inputToValidate, int minNumOfChars)
        {
            if (inputToValidate.Length >= minNumOfChars)
            {
                return Regex.IsMatch(inputToValidate, @"^[a-zA-Z0-9]+$");
            }
            else
            {
                return false;
            }

        }
        //Validates that the given string is alphanumeric (only numbers & letters) & meets minimum & max length requirements
        //Created by Matt Lapka 2/1/15
        public static bool ValidateAlphaNumeric(string inputToValidate, int minNumOfChars, int maxNumOfChars)
        {
            if (inputToValidate.Length >= minNumOfChars && inputToValidate.Length <= maxNumOfChars)
            {
                return Regex.IsMatch(inputToValidate, @"^[a-zA-Z0-9]+$");
            }
            else
            {
                return false;
            }

        }

        //Validates that the given string is an int 
        //Created by Matt Lapka 2/1/15
        public static bool ValidateInt(string inputToValidate)
        {
            int num;
            return int.TryParse(inputToValidate, out num);
        }

        //Validates that the given string is an int & meets minimum value requirements
        //Created by Matt Lapka 2/1/15
        public static bool ValidateInt(string inputToValidate, int min)
        {
            int num;
            if (int.TryParse(inputToValidate, out num))
            {
                if (num >= min)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        //Validates that the given string is an int & meets minimum & max value requirements
        //Created by Matt Lapka 2/1/15
        public static bool ValidateInt(string inputToValidate, int min, int max)
        {
            int num;
            if (int.TryParse(inputToValidate, out num))
            {
                if (num >= min && num <= max)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        //Validates that the given string is a double 
        //Created by Matt Lapka 2/1/15
        public static bool ValidateDouble(string inputToValidate)
        {
            double num;
            return double.TryParse(inputToValidate, out num);
        }

        //Validates that the given string is a double & meets minimum value requirements
        //Created by Matt Lapka 2/1/15
        public static bool ValidateDouble(string inputToValidate, double min)
        {
            double num;
            if (double.TryParse(inputToValidate, out num))
            {
                if (num >= min)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        //Validates that the given string is a double & meets minimum & max value requirements
        //Created by Matt Lapka 2/1/15
        public static bool ValidateDouble(string inputToValidate, double min, double max)
        {
            double num;
            if (double.TryParse(inputToValidate, out num))
            {
                if (num >= min && num <= max)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        //Validates that the given string is a decimal
        //Created by Matt Lapka 2/1/15
        public static bool ValidateDecimal(string inputToValidate)
        {
            decimal num;
            return decimal.TryParse(inputToValidate, out num);
        }

        //Validates that the given string is a decimal & meets minimum value requirements
        //Created by Matt Lapka 2/1/15
        public static bool ValidateDecimal(string inputToValidate, decimal min)
        {
            decimal num;
            if (decimal.TryParse(inputToValidate, out num))
            {
                if (num >= min)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        //Validates that the given string is a decimal & meets minimum & max value requirements
        //Created by Matt Lapka 2/1/15
        public static bool ValidateDecimal(string inputToValidate, decimal min, decimal max)
        {
            decimal num;
            if (decimal.TryParse(inputToValidate, out num))
            {
                if (num >= min && num <= max)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        //Validates that the given string is a valid DateTime
        //Created by Matt Lapka 2/1/15
        public static bool ValidateDateTime(string inputToValidate)
        {
            DateTime date;
            return DateTime.TryParse(inputToValidate, out date);
        }

        //Validates that the given string is a valid DateTime & meets minimum value requirements
        //Created by Matt Lapka 2/1/15
        public static bool ValidateDateTime(string inputToValidate, DateTime min)
        {
            DateTime date;
            if (DateTime.TryParse(inputToValidate, out date))
            {
                if (date >= min)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        //Validates that the given string is a valid DateTime & meets minimum & max value requirements
        //Created by Matt Lapka 2/1/15
        public static bool ValidateDateTime(string inputToValidate, DateTime min, DateTime max)
        {
            DateTime date;
            if (DateTime.TryParse(inputToValidate, out date))
            {
                if (date >= min && date <= max)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        //Validates the given password meets password complexity requirements
        // minimum of 8 characters
        // at least 1 each of 3 of the following 4:
        // lowercase letter
        // UPPERCASE LETTER
        // Number
        // Special Character (not space)
        public static bool ValidatePassword(string inputToValidate)
        {
            if (inputToValidate.Length >= 8)
            {
                int requirements = 0;

                if (Regex.IsMatch(inputToValidate, @"[^a-zA-Z0-9]"))
                {
                    //password contains a special character
                    requirements++;
                }
                if (Regex.IsMatch(inputToValidate, @"[a-z]"))
                {
                    //password contains a lowercase letter
                    requirements++;
                }
                if (Regex.IsMatch(inputToValidate, @"[A-Z]"))
                {
                    //password contains an uppercase letter
                    requirements++;
                }
                if (Regex.IsMatch(inputToValidate, @"[0-9]"))
                {
                    //password contains a number
                    requirements++;
                }

                if (requirements >= 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //Validates whether a string is a valid boolean
        //Created by Matt Lapka 2/1/15
        public static bool ValidateBool(string inputToValidate)
        {
            bool output;
            return bool.TryParse(inputToValidate, out output);
        }

        //Validates whether a string is in a valid phone format
        //accepts: 2222222222. 222.222.2222, 222-222-2222, (222) 222-2222 etc
        //Created by Matt Lapka 2/1/15
        public static bool ValidatePhone(string inputToValidate)
        {
            return Regex.IsMatch(inputToValidate, @"(\([2-9]\d\d\)|[2-9]\d\d) ?[-.,]? ?[2-9]\d\d ?[-.,]? ?\d{4}");
        }

        //Not sure what needs to be here for an address
        //are we checking with the postal service or something
        public static bool ValidateAddress(string inputToValidate)
        {
            return true;
        }

        //Validates whether string is in a valid e-mail format
        //Does not check whether is actually valid or working
        //Created by Matt Lapka 2/1/15
        public static bool ValidateEmail(string inputToValidate)
        {
            //suggested from stack overflow
            try
            {
                var addr = new System.Net.Mail.MailAddress(inputToValidate);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
