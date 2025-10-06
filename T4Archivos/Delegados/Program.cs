namespace Archivos
{
    public class Program
    {
        public static void view(int grade)
        {
            Console.ForegroundColor = grade >= 5 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"Student grade: {grade,3}.");
            Console.ResetColor();
        }
        public static bool pass(int num)
        {
            return num >= 5;
        }
        static void Main(string[] args)
        {
            int[] v = { 2, 2, 6, 7, 1, 10, 3 };
            Array.ForEach(v, view);
            int res = Array.FindIndex(v, pass);
            Console.WriteLine($"The first passing student is number {res + 1} in the list.");
            Console.ReadKey();
        }
    }
}