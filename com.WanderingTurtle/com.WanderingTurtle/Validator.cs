using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace com.WanderingTurtle.BusinessLogic
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
        /// Created 2015/02/28
        ///
        /// Custom Validator for Company names that can contain certain special characters. Follow industry standards.
        /// </summary>
        /// <param name="inputToValidate">string to validate</param>
        /// <returns>boolean value if string only contains allowed characters</returns>
        public static bool ValidateCompanyName(string inputToValidate, int min, int max)
        {
            if (inputToValidate.Length >= min && inputToValidate.Length <= max)
            {
                return Regex.IsMatch(inputToValidate, @"^[a-zA-Z0-9,.?@&!#'~*\s_;+'-]+$");
            }
            else
            {
                return false;
            }
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
            bool result = Regex.IsMatch(inputToValidate, @"(\([2-9]\d\d\)|[2-9]\d\d) ?[-.,]? ?[2-9]\d\d ?[-.,]? ?\d{4}");
            return result;
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
            //current list from IANA as of 2015/04/24
            string[] temp = { ".abb", ".abbott", ".abogado", ".ac", ".academy", ".accountant", ".accountants", ".active", ".actor", ".ad", ".ads", ".adult", ".ae", ".aero", ".af", ".afl", ".ag", ".agency", ".ai", ".airforce", ".al", ".allfinanz", ".alsace", ".am", ".amsterdam", ".an", ".android", ".ao", ".apartments", ".aq", ".aquarelle", ".ar", ".archi", ".army", ".arpa", ".as", ".asia", ".associates", ".at", ".attorney", ".au", ".auction", ".audio", ".autos", ".aw", ".ax", ".axa", ".az", ".ba", ".band", ".bank", ".bar", ".barclaycard", ".barclays", ".bargains", ".bauhaus", ".bayern", ".bb", ".bbc", ".bd", ".be", ".beer", ".berlin", ".best", ".bf", ".bg", ".bh", ".bi", ".bid", ".bike", ".bingo", ".bio", ".biz", ".bj", ".bl", ".black", ".blackfriday", ".bloomberg", ".blue", ".bm", ".bmw", ".bn", ".bnpparibas", ".bo", ".boats", ".bond", ".boo", ".boutique", ".bq", ".br", ".brussels", ".bs", ".bt", ".budapest", ".build", ".builders", ".business", ".buzz", ".bv", ".bw", ".by", ".bz", ".bzh", ".ca", ".cab", ".cafe", ".cal", ".camera", ".camp", ".cancerresearch", ".canon", ".capetown", ".capital", ".caravan", ".cards", ".care", ".career", ".careers", ".cartier", ".casa", ".cash", ".casino", ".cat", ".catering", ".cbn", ".cc", ".cd", ".center", ".ceo", ".cern", ".cf", ".cfd", ".cg", ".ch", ".channel", ".chat", ".cheap", ".chloe", ".christmas", ".chrome", ".church", ".ci", ".citic", ".city", ".ck", ".cl", ".claims", ".cleaning", ".click", ".clinic", ".clothing", ".club", ".cm", ".cn", ".co", ".coach", ".codes", ".coffee", ".college", ".cologne", ".com", ".community", ".company", ".computer", ".condos", ".construction", ".consulting", ".contractors", ".cooking", ".cool", ".coop", ".country", ".courses", ".cr", ".credit", ".creditcard", ".cricket", ".crs", ".cruises", ".cu", ".cuisinella", ".cv", ".cw", ".cx", ".cy", ".cymru", ".cyou", ".cz", ".dabur", ".dad", ".dance", ".date", ".dating", ".datsun", ".day", ".dclk", ".de", ".deals", ".degree", ".delivery", ".democrat", ".dental", ".dentist", ".desi", ".design", ".dev", ".diamonds", ".diet", ".digital", ".direct", ".directory", ".discount", ".dj", ".dk", ".dm", ".dnp", ".do", ".docs", ".doha", ".domains", ".doosan", ".download", ".durban", ".dvag", ".dz", ".eat", ".ec", ".edu", ".education", ".ee", ".eg", ".eh", ".email", ".emerck", ".energy", ".engineer", ".engineering", ".enterprises", ".epson", ".equipment", ".er", ".erni", ".es", ".esq", ".estate", ".et", ".eu", ".eurovision", ".eus", ".events", ".everbank", ".exchange", ".expert", ".exposed", ".express", ".fail", ".faith", ".fan", ".fans", ".farm", ".fashion", ".feedback", ".fi", ".film", ".finance", ".financial", ".firmdale", ".fish", ".fishing", ".fit", ".fitness", ".fj", ".fk", ".flights", ".florist", ".flowers", ".flsmidth", ".fly", ".fm", ".fo", ".foo", ".football", ".forex", ".forsale", ".foundation", ".fr", ".frl", ".frogans", ".fund", ".furniture", ".futbol", ".ga", ".gal", ".gallery", ".garden", ".gb", ".gbiz", ".gd", ".gdn", ".ge", ".gent", ".gf", ".gg", ".ggee", ".gh", ".gi", ".gift", ".gifts", ".gives", ".gl", ".glass", ".gle", ".global", ".globo", ".gm", ".gmail", ".gmo", ".gmx", ".gn", ".gold", ".goldpoint", ".golf", ".goo", ".goog", ".google", ".gop", ".gov", ".gp", ".gq", ".gr", ".graphics", ".gratis", ".green", ".gripe", ".gs", ".gt", ".gu", ".guge", ".guide", ".guitars", ".guru", ".gw", ".gy", ".hamburg", ".hangout", ".haus", ".healthcare", ".help", ".here", ".hermes", ".hiphop", ".hiv", ".hk", ".hm", ".hn", ".holdings", ".holiday", ".homes", ".horse", ".host", ".hosting", ".house", ".how", ".hr", ".ht", ".hu", ".ibm", ".id", ".ie", ".ifm", ".il", ".im", ".immo", ".immobilien", ".in", ".industries", ".infiniti", ".info", ".ing", ".ink", ".institute", ".insure", ".int", ".international", ".investments", ".io", ".iq", ".ir", ".irish", ".is", ".it", ".iwc", ".java", ".jcb", ".je", ".jetzt", ".jewelry", ".jm", ".jo", ".jobs", ".joburg", ".jp", ".juegos", ".kaufen", ".kddi", ".ke", ".kg", ".kh", ".ki", ".kim", ".kitchen", ".kiwi", ".km", ".kn", ".koeln", ".komatsu", ".kp", ".kr", ".krd", ".kred", ".kw", ".ky", ".kyoto", ".kz", ".la", ".lacaixa", ".land", ".lat", ".latrobe", ".lawyer", ".lb", ".lc", ".lds", ".lease", ".leclerc", ".legal", ".lgbt", ".li", ".lidl", ".life", ".lighting", ".limited", ".limo", ".link", ".lk", ".loan", ".loans", ".london", ".lotte", ".lotto", ".love", ".lr", ".ls", ".lt", ".ltda", ".lu", ".luxe", ".luxury", ".lv", ".ly", ".ma", ".madrid", ".maif", ".maison", ".management", ".mango", ".market", ".marketing", ".markets", ".marriott", ".mc", ".md", ".me", ".media", ".meet", ".melbourne", ".meme", ".memorial", ".menu", ".mf", ".mg", ".mh", ".miami", ".mil", ".mini", ".mk", ".ml", ".mm", ".mma", ".mn", ".mo", ".mobi", ".moda", ".moe", ".monash", ".money", ".mormon", ".mortgage", ".moscow", ".motorcycles", ".mov", ".movie", ".mp", ".mq", ".mr", ".ms", ".mt", ".mtn", ".mtpc", ".mu", ".museum", ".mv", ".mw", ".mx", ".my", ".mz", ".na", ".nagoya", ".name", ".navy", ".nc", ".ne", ".net", ".network", ".neustar", ".new", ".news", ".nexus", ".nf", ".ng", ".ngo", ".nhk", ".ni", ".nico", ".ninja", ".nissan", ".nl", ".no", ".np", ".nr", ".nra", ".nrw", ".ntt", ".nu", ".nyc", ".nz", ".okinawa", ".om", ".one", ".ong", ".onl", ".online", ".ooo", ".org", ".organic", ".osaka", ".otsuka", ".ovh", ".pa", ".page", ".panerai", ".paris", ".partners", ".parts", ".party", ".pe", ".pf", ".pg", ".ph", ".pharmacy", ".photo", ".photography", ".photos", ".physio", ".piaget", ".pics", ".pictet", ".pictures", ".pink", ".pizza", ".pk", ".pl", ".place", ".plumbing", ".plus", ".pm", ".pn", ".pohl", ".poker", ".porn", ".post", ".pr", ".praxi", ".press", ".pro", ".prod", ".productions", ".prof", ".properties", ".property", ".ps", ".pt", ".pub", ".pw", ".py", ".qa", ".qpon", ".quebec", ".racing", ".re", ".realtor", ".recipes", ".red", ".redstone", ".rehab", ".reise", ".reisen", ".reit", ".ren", ".rentals", ".repair", ".report", ".republican", ".rest", ".restaurant", ".review", ".reviews", ".rich", ".rio", ".rip", ".ro", ".rocks", ".rodeo", ".rs", ".rsvp", ".ru", ".ruhr", ".rw", ".ryukyu", ".sa", ".saarland", ".sale", ".samsung", ".sap", ".sarl", ".saxo", ".sb", ".sc", ".sca", ".scb", ".schmidt", ".scholarships", ".school", ".schule", ".schwarz", ".science", ".scot", ".sd", ".se", ".seat", ".services", ".sew", ".sex", ".sexy", ".sg", ".sh", ".shiksha", ".shoes", ".show", ".shriram", ".si", ".singles", ".site", ".sj", ".sk", ".sky", ".sl", ".sm", ".sn", ".so", ".social", ".software", ".sohu", ".solar", ".solutions", ".sony", ".soy", ".space", ".spiegel", ".spreadbetting", ".sr", ".ss", ".st", ".study", ".style", ".su", ".sucks", ".supplies", ".supply", ".support", ".surf", ".surgery", ".suzuki", ".sv", ".sx", ".sy", ".sydney", ".systems", ".sz", ".taipei", ".tatar", ".tattoo", ".tax", ".tc", ".td", ".team", ".tech", ".technology", ".tel", ".temasek", ".tennis", ".tf", ".tg", ".th", ".tickets", ".tienda", ".tips", ".tires", ".tirol", ".tj", ".tk", ".tl", ".tm", ".tn", ".to", ".today", ".tokyo", ".tools", ".top", ".toshiba", ".tours", ".town", ".toys", ".tp", ".tr", ".trade", ".trading", ".training", ".travel", ".trust", ".tt", ".tui", ".tv", ".tw", ".tz", ".ua", ".ug", ".uk", ".um", ".university", ".uno", ".uol", ".us", ".uy", ".uz", ".va", ".vacations", ".vc", ".ve", ".vegas", ".ventures", ".versicherung", ".vet", ".vg", ".vi", ".viajes", ".video", ".villas", ".vision", ".vlaanderen", ".vn", ".vodka", ".vote", ".voting", ".voto", ".voyage", ".vu", ".wales", ".wang", ".watch", ".webcam", ".website", ".wed", ".wedding", ".weir", ".wf", ".whoswho", ".wien", ".wiki", ".williamhill", ".win", ".wme", ".work", ".works", ".world", ".ws", ".wtc", ".wtf", ".xerox", ".xin", ".xxx", ".xyz", ".yachts", ".yandex", ".ye", ".yodobashi", ".yoga", ".yokohama", ".youtube", ".yt", ".za", ".zip", ".zm", ".zone", ".zuerich", ".zw" };
            List<string> tlds = new List<string>(temp);

            if (inputToValidate.Contains("."))
            {
                string sub = inputToValidate.Substring(inputToValidate.LastIndexOf('.'));
                if (tlds.Contains(sub))
                {
                    //continue
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
            //suggested from stack overflow
            try
            {
                var addr = new MailAddress(inputToValidate);
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