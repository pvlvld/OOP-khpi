namespace lab2_4
{
    public struct Point3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public readonly double DistanceToOrigin
        {
            get { return Math.Sqrt(X * X + Y * Y + Z * Z); }
        }
    }

    class Program
    {
        static void Main()
        {
            Point3D point = new Point3D { X = 3, Y = 4, Z = 12 };
            Console.WriteLine($"Відстань від точки ({point.X}, {point.Y}, {point.Z}) до початку координат: {point.DistanceToOrigin}");
        }
    }
}