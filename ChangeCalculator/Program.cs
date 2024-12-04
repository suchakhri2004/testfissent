using System;
using System.Collections.Generic;

class Program
{
    static Dictionary<int, int> cashTotal = new Dictionary<int, int>();

    static void Main()
    {
        InitializeInventory(); 
        while (true)
        {
            Console.Write("กรุณาใส่จำนวนเงินที่ต้องการทอน: ");
            int amount;
            if (!int.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("กรุณาใส่ตัวเลขที่ถูกต้อง");
                continue;
            }

            if (!CanGiveChange(amount))
            {
                Console.WriteLine("ไม่สามารถทอนเงินได้ เนื่องจากเงินไม่พอหรือไม่มีเศษ");
            }
            DisplayInventory();
        }
    }

    static void InitializeInventory()
    {
        int[] denominations = { 1000, 500, 100, 50, 10, 5, 1 };

        Console.WriteLine("กรุณากรอกจำนวนเงินคงเหลือในระบบ:");

        foreach (var denomination in denominations)
        {
            Console.Write(denomination >= 10 ? $"จำนวนแบงค์ {denomination}: " : $"จำนวนเหรียญ {denomination}: ");
            int count;
            while (!int.TryParse(Console.ReadLine(), out count) || count < 0)
            {
                Console.WriteLine("กรุณาใส่จำนวนที่ถูกต้อง (จำนวนเต็มบวกเท่านั้น)");
                Console.Write(denomination >= 10 ? $"จำนวนแบงค์ {denomination}: " : $"จำนวนเหรียญ {denomination}: ");
            }
            cashTotal[denomination] = count;
        }
    }

    static bool CanGiveChange(int amount)
    {
        Dictionary<int, int> changeToGive = new Dictionary<int, int>();
        int amountTotal = amount;

        foreach (var bill in cashTotal.Keys)
        {
            if (amountTotal <= 0)
            {
                break;
            }
            int numOfBills = amountTotal / bill;
            if (numOfBills > cashTotal[bill])
            {
                numOfBills = cashTotal[bill];
            }
            if (numOfBills > 0)
            {
                changeToGive[bill] = numOfBills;
                amountTotal -= numOfBills * bill;
            }
        }

        if (amountTotal > 0)
        {
            return false; // เงินไม่พอทอน
        }

        foreach (var bill in changeToGive.Keys)
        {
            cashTotal[bill] -= changeToGive[bill];
            Console.WriteLine(bill >= 10
                ? $"ใช้แบงค์ {bill}: {changeToGive[bill]} เหลือ {cashTotal[bill]}"
                : $"ใช้เหรียญ {bill}: {changeToGive[bill]} เหลือ {cashTotal[bill]}");
        }
        return true;
    }

    static void DisplayInventory()
    {
        Console.WriteLine("\nสถานะเงินคงเหลือ:");
        foreach (var bill in cashTotal)
        {
            Console.WriteLine(bill.Key >= 10
                ? $"แบงค์ {bill.Key}: {bill.Value}"
                : $"เหรียญ {bill.Key}: {bill.Value}");
        }
        Console.WriteLine();
    }
}
