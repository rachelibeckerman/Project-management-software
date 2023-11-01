
namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome0554();
            Welcome8381();
            Console.ReadKey();
        }

        static partial void Welcome8381();
        private static void Welcome0554()
        {
            Console.Write("Enter your name: ");
            string myString = Console.ReadLine();
            Console.WriteLine($"{myString}, welcome to my first console application");

        }

    }
}
