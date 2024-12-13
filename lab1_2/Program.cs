namespace lab1_2

{
    public class Program2
    {
        public static void Main()
        {
            System.Console.WriteLine($"Result: {Sqrt(ParseInputNumber())}");
        }

        public static double? Sqrt(double x)
        {
            if (x < 0)
            {
                return null;
            }

            if (x == 0)
            {
                return 0;
            }
            double epsilon = .0001;
            double left = 1;
            double right = x;
            double mid;
            double sqr;

            while ((right - left) > epsilon)
            {
                mid = (left + right) / 2;
                sqr = mid * mid;

                if (Math.Abs(sqr - x) < epsilon)
                {
                    return mid;
                }
                else if (sqr > x)
                {
                    right = mid;
                }
                else
                {
                    left = mid;
                }
            }

            return (left + right) / 2;
        }

        private static int ParseInputNumber()
        {
            string[] args = Environment.GetCommandLineArgs();
            const string helpMessage = "Usage example: -x=10\nx - a number to calculate it's square root";

            if (args.Length != 2)
            {
                throw new Exception($"Wrong amount of arguments privided.\n{helpMessage}");
            }

            int imputNumberArgIdx = Array.FindIndex(args, e => e.StartsWith("-x="));
            if (imputNumberArgIdx == -1)
            {
                throw new Exception($"Invalid arguments.\n{helpMessage}");
            }

            int targetNumber;
            try
            {
                targetNumber = Int32.Parse(args[imputNumberArgIdx].Split("=")[1]);

            }
            catch (System.Exception)
            {
                throw new Exception($"Invalid arguments.\n{helpMessage}");
            }

            return targetNumber;
        }
    }
}