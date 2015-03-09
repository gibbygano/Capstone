using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace com.WanderingTurtle
{
    public class Validator
    {
        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/28
        /// 
        /// Custom Validator for Company names that can contain certain special characters. Follow industry standards.
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <returns>boolean value if string only contains allowed characters</returns>
        public static bool ValidateCompanyName(string inputToValidate)
        {
            return Regex.IsMatch(inputToValidate, @"^[a-zA-Z0-9,.?@&!#'~*\s_;+'-]+$");
        }
       
        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that a string only contains letters or an apostrophe
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <returns>boolean value if string only contains allowed characters</returns>
        public static bool ValidateString(string inputToValidate)
        {
            return Regex.IsMatch(inputToValidate, @"^[a-zA-Z'-]+$");
        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that a string only contains letters or an apostrophe and meets minimum length requirements
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <param name="min">minimum length</param>
        /// <returns>boolean value if string only contains allowed characters</returns>
        public static bool ValidateString(string inputToValidate, int minNumOfChars)
        {
            if (inputToValidate.Length >= minNumOfChars)
            {
                return Regex.IsMatch(inputToValidate, @"^[a-zA-Z'-]+$");
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that a string only contains letters or an apostrophe and meets min/mex length requirements
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <param name="min">minimum length</param>
        /// <param name="max">maximum length</param>
        /// <returns>boolean value if string only contains allowed characters</returns>
        public static bool ValidateString(string inputToValidate, int minNumOfChars, int maxNumOfChars)
        {
            if (inputToValidate.Length >= minNumOfChars && inputToValidate.Length <= maxNumOfChars)
            {
                return Regex.IsMatch(inputToValidate, @"^[a-zA-Z'-]+$");
            }
            else
            {
                return false;
            }
        }

        
        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string is numeric (numbers)
        /// Does NOT check if it is an int, double, etc
        /// Will return false on negatives and decimals - use other provided methods for those cases
        /// also should be used if leading zeros are important
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <returns>boolean value if string only contains allowed characters</returns>
        public static bool ValidateNumeric(string inputToValidate)
        {
            return Regex.IsMatch(inputToValidate, @"^[0-9]+$");
        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string is numeric (numbers) & meets minimum length
        /// Does NOT check if it is an int, double, etc
        /// Will return false on negatives and decimals - use other provided methods for those cases
        /// also should be used if leading zeros are important
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <param name="min">minimum length</param>
        /// <returns>boolean value if string only contains allowed characters</returns>
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
        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string is numeric (numbers) & meets minimum & max length requirements
        /// Does NOT check if it is an int, double, etc
        /// Will return false on negatives and decimals - use other provided methods for those cases
        /// also should be used if leading zeros are important
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <param name="min">minimum length</param>
        /// <param name="max">maximum length</param>
        /// <returns>boolean value if string only contains allowed characters</returns>
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

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string is alphanumeric (only numbers & letters -- and spaces)
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <returns>boolean value if string only contains allowed characters</returns>
        public static bool ValidateAlphaNumeric(string inputToValidate)
        {
            return Regex.IsMatch(inputToValidate, @"^['a-zA-Z0-9\s-]+$");
        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string is alphanumeric (only numbers & letters -- and spaces) & meets minimum length
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <param name="min">minimum length</param>
        /// <returns>boolean value if string only contains allowed characters</returns>
        public static bool ValidateAlphaNumeric(string inputToValidate, int minNumOfChars)
        {
            if (inputToValidate.Length >= minNumOfChars)
            {
                return Regex.IsMatch(inputToValidate, @"^['a-zA-Z0-9\s-]+$");
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string is alphanumeric (only numbers & letters -- and spaces) & meets minimum & max length requirements
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <param name="min">minimum length</param>
        /// <param name="max">maximum length</param>
        /// <returns>boolean value if string only contains allowed characters</returns>
        public static bool ValidateAlphaNumeric(string inputToValidate, int minNumOfChars, int maxNumOfChars)
        {
            if (inputToValidate.Length >= minNumOfChars && inputToValidate.Length <= maxNumOfChars)
            {
                return Regex.IsMatch(inputToValidate, @"^['a-zA-Z0-9\s-]+$");
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string is an int
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <returns>boolean value if string is an int-- DOES NOT RETURN AN INT</returns>
        public static bool ValidateInt(string inputToValidate)
        {
            int num;
            return int.TryParse(inputToValidate, out num);
        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string is an int & meets minimum value requirements
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <param name="min">minimum value</param>
        /// <returns>boolean value if string is an int-- DOES NOT RETURN AN INT</returns>
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

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string is an int & meets minimum & max value requirements
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <param name="min">minimum value</param>
        /// <param name="max">maximum value</param>
        /// <returns>boolean value if string is an int-- DOES NOT RETURN AN INT</returns>
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

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string is a double
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <returns>boolean value if string is a double-- DOES NOT RETURN A DOUBLE</returns>
        public static bool ValidateDouble(string inputToValidate)
        {
            double num;
            return double.TryParse(inputToValidate, out num);
        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string is a double & meets minimum value requirements
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <param name="min">minimum value</param>
        /// <returns>boolean value if string is a double-- DOES NOT RETURN A DOUBLE</returns>
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

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string is a double & meets minimum & max value requirements
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <param name="min">minimum value</param>
        /// <param name="max">maximum value</param>
        /// <returns>boolean value if string is a double-- DOES NOT RETURN A DOUBLE</returns>
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

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string is a decimal
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <returns>boolean value if string is a decimal-- DOES NOT RETURN A DECIMAL</returns>
        public static bool ValidateDecimal(string inputToValidate)
        {
            decimal num;
            return decimal.TryParse(inputToValidate, out num);
        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string is a decimal & meets minimum value requirements
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <param name="min">minimum value</param>
        /// <returns>boolean value if string is a decimal-- DOES NOT RETURN A DECIMAL</returns>
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

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string is a decimal & meets minimum & max value requirements
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <param name="min">minimum value</param>
        /// <param name="max">maximum value</param>
        /// <returns>boolean value if string is a decimal-- DOES NOT RETURN A DECIMAL</returns>
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

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string can be converted to a DateTime object
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <returns>boolean value if string can be converted-- DOES NOT RETURN A DATETIME</returns>
        public static bool ValidateDateTime(string inputToValidate)
        {
            DateTime date;
            return DateTime.TryParse(inputToValidate, out date);
        }


        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string can be converted to a DateTime object & meets minimum value requirements
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <param name="min">minimum date in DateTime form</param>
        /// <returns>boolean value if string can be converted-- DOES NOT RETURN A DATETIME</returns>
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
        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates that the given string can be converted to a DateTime object & meets minimum & max value requirements
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <param name="min">minimum date in DateTime form</param>
        /// <param name="max">maximum date in DateTime form</param>
        /// <returns>boolean value if string can be converted-- DOES NOT RETURN A DATETIME</returns>
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
        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates the given password meets password complexity requirements
        /// minimum of 8 characters
        /// at least 1 each of 3 of the following 4:
        /// lowercase letter
        /// UPPERCASE LETTER
        /// Number
        /// Special Character (not space)
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <returns>returns boolean value if stirng meets the above parameters</returns>
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

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates whether a string can be converted to a valid boolean object
        /// does not return the converted value!
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <returns>returns a bool if it can be converted -- does not return the converted value</returns>
        public static bool ValidateBool(string inputToValidate)
        {
            bool output;
            return bool.TryParse(inputToValidate, out output);
        }

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates whether a string is in a valid phone format
        /// accepts: 2222222222. 222.222.2222, 222-222-2222, (222) 222-2222 etc
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <returns>a boolean value if the string contains only the allow characters</returns>
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

        /// <summary>
        /// Matt Lapka
        /// Created 2015/02/01
        /// Validates whether string is in a valid e-mail format
        /// Does not check whether is actually valid or working
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <returns>a boolean value if the string is a vlid email format</returns>
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

    public static class StringTool
    {
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/03/07
        /// 
        /// Extention method to truncate a string to the specified character length
        /// and add an ellipses to indicate it had been truncated.
        /// </summary>
        /// <param name="source">string that needs to be truncated</param>
        /// <param name="length">length to truncate the string to</param>
        /// <returns>shortened string</returns>
        public static string Truncate(this string source, int length)
        {
            if (source.Length > length)
            {
                source = source.Substring(0, length) + "...";
            }
            return source;
        }

    }
}
