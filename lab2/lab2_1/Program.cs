namespace lab2_1
{
    public struct Roots
    {
        public int Count { get; set; }
        public double Root1 { get; set; }
        public double Root2 { get; set; }
    }

    public class QuadraticEquationSolver
    {

        private double _root1;
        private double _root2;
        public double Root1 { get; private set; }
        public double Root2 { get; private set; }
        public int Solve(double a, double b, double c, out double x1, out double x2)
        {
            double discriminant = b * b - 4 * a * c;

            if (Math.Abs(a) < double.Epsilon)
            {
                x1 = -1;
                x2 = -1;
                return -1;
            }

            if (discriminant > 0)
            {
                x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                _root1 = x1;
                x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                _root2 = x2;
                return 2;
            }

            if (discriminant == 0)
            {
                x1 = x2 = -b / (2 * a);
                return 1;
            }

            x1 = double.NaN;
            x2 = double.NaN;
            return 0;
        }

        public Roots SolveRoots(double a, double b, double c)
        {
            int result = Solve(a, b, c, out double x1, out double x2);
            return new Roots { Count = result, Root1 = x1, Root2 = x2 };
        }

        public (int, double, double) SolveTuple(double a, double b, double c)
        {
            int result = Solve(a, b, c, out double x1, out double x2);
            return (result, x1, x2);
        }

        public double this[int index]
        {
            get
            {
                if (index == 0) return _root1;
                if (index == 1) return _root2;
                throw new IndexOutOfRangeException();
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            QuadraticEquationSolver solver = new();
            double a = 1, b = -69, c = 420;

            Console.WriteLine("Перша функція повинна отримувати як параметри коефіцієнти рівняння, а також корені як параметри з атрибутом out. Функція повинна повертати кількість коренів та -1, якщо коренів безмежна кількість.");
            int count = solver.Solve(a, b, c, out double x1, out double x2);
            Console.WriteLine($"out змінні:\tКількість коренів: {count}, Корені: {x1}, {x2}");
            Console.WriteLine($"Індекси:\tКількість коренів: {count}, Корені: {solver[0]}, {solver[1]}");

            Console.WriteLine("\n");
            Console.WriteLine("\nДруга функція повинна отримувати як параметри коефіцієнти рівняння та повертати структуру, в якій зберігаються три значення: кількість коренів, перший та другий корінь.");
            Roots roots = solver.SolveRoots(a, b, c);
            Console.WriteLine($"Структура:\tКількість коренів: {roots.Count}, Корені: {roots.Root1}, {roots.Root2}");

            Console.WriteLine("\n");
            Console.WriteLine("Третя функція повинна отримувати як параметри коефіцієнти рівняння та повертати кортеж, який складається з трьох значень: кількість коренів, перший та другий корінь.");
            (int count3, double x1_3, double x2_3) = solver.SolveTuple(a, b, c);
            Console.WriteLine($"Кортеж:\t\tКількість коренів: {count3}, Корені: {x1_3}, {x2_3}");
        }
    }
}