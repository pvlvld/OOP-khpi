using System.Text.RegularExpressions;
namespace lab2_3
{
    public static class String
    {
        // Why not?
        // public static string RemoveExtraSpaces(this string str) => str.Replace(@"\s+", " ");
        public static string RemoveExtraSpaces(this string str) => Regex.Replace(str, @"\s+", " ");
    }

    class Program
    {
        static void Main()
        {
            string input = "  some     spaces ";
            Console.WriteLine($"input: \"{input}\"\noutput: \"{String.RemoveExtraSpaces(input)}\"");
        }
    }
}