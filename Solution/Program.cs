using System;

namespace Solution
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Ingrese un numero menor a 21");
            int number = int.Parse(Console.ReadLine());

            if (number<21) {
                Int64 value = 1;

                for (int i = 1; i <= number; i++)
                {
                    value = value * i;
                }

                Console.WriteLine("El factorial de {0} es = {1} ", number, value);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("El valor es mayor a 20");
            }
        }
    }
}
