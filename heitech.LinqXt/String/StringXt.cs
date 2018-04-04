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
        public static string ReplaceWhitespaceWith(this string source, char withChar)
        {
            throw new NotImplementedException();
        }

        public static string RemoveIndexFromRight(this string source, int start, int amount=0)
        {
            throw new NotImplementedException();
        }

        public static string RemoveIndexFromLeft(this string source, int start, int amount=0)
        {
            throw new NotImplementedException();
        }

        public static string RemoveAll(this string source, char _char)
        {
            throw new NotImplementedException();
        }

        public static string Shorten(this string source, int charAmount=80, string shortenedExpression="...")
        {
            throw new NotImplementedException();
        }

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

        public static string FlattenSequence<T>(this IEnumerable<T> source, Func<T, string> toString, char sep=',')
            => source.Convert(x => toString(x)).Aggregate((s1,s2) => $"{s1}{sep}{s2}");

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

        private static bool Comparison(string encoding, string s)
            => encoding.Equals(s, StringComparison.InvariantCultureIgnoreCase);

        public static string ToBase64Encode(this byte[] bytes)
            => ToBase64String(bytes);

        public static string Base64Decode(this string base64String)
            => UTF8.GetString(FromBase64String(base64String));
    }
}
