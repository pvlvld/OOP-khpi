namespace lab1_3

{
    public class Program3
    {
        public static void Main()
        {

            // Why is there no bultin deconstructor?
            int[] numbers = ParseInputNumbers();
            int a = numbers[0];
            int b = numbers[1];
            while (b != 0)
            {
                int temp = a % b;
                a = b;
                b = temp;
            }

            System.Console.WriteLine($"Result: {a}");
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

        private static int[] ParseInputNumbers()
        {
            string[] args = Environment.GetCommandLineArgs();
            const string helpMessage = "Usage example: -a=69 -b=420";

            if (args.Length != 3)
            {
                throw new Exception($"Wrong amount of arguments privided.\n{helpMessage}");
            }

            int aNumberArgIdx = Array.FindIndex(args, e => e.StartsWith("-a="));
            int bNumberArgIdx = Array.FindIndex(args, e => e.StartsWith("-b="));
            if (aNumberArgIdx == -1 || bNumberArgIdx == -1)
            {
                throw new Exception($"Invalid arguments.\n{helpMessage}");
            }

            int a, b;
            try
            {
                a = Int32.Parse(args[aNumberArgIdx].Split("=")[1]);
                b = Int32.Parse(args[bNumberArgIdx].Split("=")[1]);

            }
            catch (System.Exception)
            {
                throw new Exception($"Invalid arguments.\n{helpMessage}");
            }

            return [a, b];
        }
    }
}