namespace lab2_2
{
    public class Complex(double real, double imaginary)
    {
        public double Real { get; set; } = real;
        public double Imaginary { get; set; } = imaginary;

        // Перевантаження оператора +
        public static Complex operator +(Complex c1, Complex c2) => new(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);

        // Перевантаження оператора -
        public static Complex operator -(Complex c1, Complex c2) => new(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);

        // Перевантаження оператора *
        public static Complex operator *(Complex c1, Complex c2) => new(c1.Real * c2.Real - c1.Imaginary * c2.Imaginary,
                                c1.Real * c2.Imaginary + c1.Imaginary * c2.Real);

        // Перевантаження оператора /
        public static Complex operator /(Complex c1, Complex c2)
        {
            double denominator = c2.Real * c2.Real + c2.Imaginary * c2.Imaginary;
            return new Complex((c1.Real * c2.Real + c1.Imaginary * c2.Imaginary) / denominator,
                                (c1.Imaginary * c2.Real - c1.Real * c2.Imaginary) / denominator);
        }

        // Неявне приведення до типу string
        public static implicit operator string(Complex c) => $"{c.Real}{(c.Imaginary >= 0 ? "+" : "-")}{Math.Abs(c.Imaginary)}i";
        public override string ToString() => $"{this.Real.ToString()} {(this.Imaginary < 0 ? "-" : "+")} {Math.Abs(this.Imaginary)}i";
    }

    class Program
    {
        static void Main()
        {
            Complex c1 = new(2, 3);
            Complex c2 = new(-1, 4);

            Complex diff = c1 - c2;
            Console.WriteLine($"c1 - c2 = {diff}");
            Complex sum = c1 + c2;
            Console.WriteLine($"c1 + c2 = {sum}");
            Complex mult = c1 * c2;
            Console.WriteLine($"c1 * c2 = {mult}");
            Complex div = c1 / c2;
            Console.WriteLine($"c1 / c2 = {div}");
        }
    }
}