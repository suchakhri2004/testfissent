using System;

namespace BankAccountApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int balance = 100;
            int minimumBalance = 100;
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Account");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Check Balance");
                Console.WriteLine("0. Exit");
                Console.Write("Select number: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter amount to deposit: ");
                        int deposit = int.Parse(Console.ReadLine());
                        balance += deposit;
                        Console.WriteLine($"New balance is: {balance}");
                        break;

                    case "2":
                        Console.Write("Enter amount to withdraw: ");
                        int withdraw = int.Parse(Console.ReadLine());

                        if (balance - withdraw >= minimumBalance)
                        {
                            balance -= withdraw;
                            Console.WriteLine($"New balance is: {balance}");
                        }
                        else
                        {
                            Console.WriteLine("Cannot withdraw because balance will be less than minimum required.");
                        }
                        break;

                    case "3":
                        Console.WriteLine($"Your balance is: {balance}");
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Invalid choice, please select again.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}