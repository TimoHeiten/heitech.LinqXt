using heitech.LinqXt.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Text.Encoding;
using static System.Convert;

namespace heitech.LinqXt.String
{
    public static class StringXt
    {
        ///<summary>
        /// Takes a string and replaces every whitespace with the specified character.
        ///</summary>
        public static string ReplaceWhitespaceWith(this string source, char? withChar = null)
        {
            if (withChar.HasValue)
            {
                char[] array = source.ToCharArray();
                for (int i = 0; i < array.Length; i++)
                {
                    char _char = array[i];
                    if (Char.IsWhiteSpace(_char))
                        array[i] = withChar.Value;
                }
                return new string(array);
            } 
            else
                return new string(source.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
        }

        ///<summary>
        /// Transforms a string to camelCasing
        ///</summary>
        public static string ToCamelCase(this string source)
        {
            var array = source.ToCharArray();
            array[0] = (char.ToLower(source[0]));
            
            return new string(array.ToArray());
        }

        ///<summary>
        /// Transforms a string to snake_case
        ///</summary>
        public static string ToSnakeCase(this string source)
        {
            return source.CasingToSeperator(seperator: '_');
        }

        ///<summary>
        /// Transforms the source to a casing with a seperator.
        /// E.g. MyCasingType --> (Default '.') my.casing.type
        /// useful for message queue topics and such
        ///</summary>
        public static string CasingToSeperator(this string source, char seperator = '.')
        {
            var array = new List<char>();
            for (int i = 1; i < source.Length; i++)
            {
                var next = source[i];
                bool isUpper = char.IsUpper(next);
                var lower = char.ToLower(next);
                if (isUpper)
                {
                    array.Add(seperator);
                }
                array.Add(lower);
            }
            array.Insert(0, char.ToLower(source[0]));
            return new string(array.ToArray());
        }

         ///<summary>
        /// Remove all chars of a string starting at the end of the string. Amount is interpreted as Absoulte value (-10 --> 10)
        /// <para>E.g. "abcdef".RemoveCharsFromRight(3) --> "abc"</para>
        ///</summary>
        public static string RemoveCharsFromRight(this string source, int amount=0)
        {
            char[] array = source.ToCharArray();
            int count = array.Length - Math.Abs(amount);
            if (count < 0)
                count = 0;

            var output = new char[count];

            for (int i = 0; i < output.Length; i++)
                output[i] = array[i];

            return new string(output);
        }

        ///<summary>
        /// Remove all chars of a string starting at the end of the string. Amount is interpreted as Absoulte value (-10 --> 10)
        /// <para>E.g. "abcdef".RemoveCharsFromLeft(3) --> "def"</para>
        ///</summary>
        public static string RemoveCharsFromLeft(this string source, int amount=0)
        {
            amount = Math.Abs(amount);
            if (amount > source.Length)
                amount = source.Length;

            return source.Substring(amount, source.Length-amount);
        }

        ///<summary>
        /// Remove all specified chars from a string.
        ///</summary>
        public static string RemoveAll(this string source, params char[] _char)
        {
            if (_char.Length == 0)
                return source;

            var list = new List<char>();
            for (int i = 0; i < source.Length; i++)
            {
                char c = source[i];
                if (_char.NotAny(x => x == c))
                    list.Add(c);
            }
            return new string(list.ToArray());
        }

        ///<summary>
        /// Shorten a string for output purposes to the charAmount limit and add a shortened Expression
        ///</summary>
        public static string Shorten(this string source, int charAmount=77, string shortenedExpression="...")
        {
            if (source.Length < charAmount)
                return source;

            return $"{source.Substring(0, charAmount)}{shortenedExpression}";
        }

        ///<summary>
        /// Change the casing to Inverse: PascalCase --> pASCALcASE 
        ///</summary>
        public static string SwapCase(this string source)
        {
            char[] array = source.ToCharArray();
            for (int i = 0; i < source.Length; i++)
            {
                char c = source[i];
                if (Char.IsLower(c))
                    array[i] = Char.ToUpper(c);
                else if (Char.IsUpper(c))
                    array[i] = Char.ToLower(c);
            }

            return new string(array);
        }

        ///<summary>
        /// like a string.Join but with applying a Func<T,string> toString
        ///</summary>
        public static string FlattenSequence<T>(this IEnumerable<T> source, Func<T, string> toString, char sep=',')
            => source.Convert(x => toString(x)).Aggregate((s1,s2) => $"{s1}{sep}{s2}");

        ///<summary>
        /// Specify Encoding for Bytes from a string
        ///</summary>
        public static byte[] ToBytes(this string source, string encoding = "Unicode")
        {
            if (Comparison(encoding, "UTF-8"))
                return UTF8.GetBytes(source);
            else if (Comparison(encoding, "UTF-32"))
                return UTF32.GetBytes(source);
            else if (Comparison(encoding, "ASCII"))
                return ASCII.GetBytes(source);

            return Unicode.GetBytes(source);
        }

        ///<summary>
        /// Get with encoding from Bytes
        ///</summary>
        public static string FromBytes(this byte[] bytes, string encoding="Unicode")
        {
            if (Comparison(encoding, "UTF-8"))
                return UTF8.GetString(bytes);
            else if (Comparison(encoding, "UTF-32"))
                return UTF32.GetString(bytes);
            else if (Comparison(encoding, "ASCII"))
                return ASCII.GetString(bytes);

            return Unicode.GetString(bytes);
        }

        ///<summary>
        /// Compare with InvariantIgnoreCase as Extension Method
        ///</summary>
        private static bool Comparison(string encoding, string s)
            => encoding.Equals(s, StringComparison.InvariantCultureIgnoreCase);

        public static string ToBase64Encode(this byte[] bytes)
            => ToBase64String(bytes);

        public static string Base64Decode(this string base64String)
            => UTF8.GetString(FromBase64String(base64String));

        ///<summary>
        /// Convenience Method for strings
        ///</summary>
        public static bool IsNullOrEmpty(this string source)
            => string.IsNullOrEmpty(source);

        ///<summary>
        /// Convenience Method for strings
        ///</summary>
        public static bool IsNullOrWhiteSpace(this string source)
            => string.IsNullOrWhiteSpace(source);

    }
}
