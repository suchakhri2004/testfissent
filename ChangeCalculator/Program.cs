using System;
using System.Collections.Generic;

class Program
{
            static Dictionary<int, int> cashTotal = new Dictionary<int, int>()
    {
        { 1000, 10 },
        { 500, 5 },
        { 100, 10 },
        { 50, 10 },
        { 10, 1000 },
        { 5, 500 },
        { 1, 1000 }
    };
        static void Main()
        {
            while (true) {
                Console.Write("กรุณาใส่จำนวนเงินที่ต้องการทอน : ");
                int amount;
                if(!int.TryParse(Console.ReadLine(), out amount)){
                    Console.WriteLine("กรุณาใส่ตัวเลขที่ถูกต้อง");
                    continue;
                }
                if (!CanGiveChange(amount)){
                    Console.WriteLine("ไม่สามารถทอนเงินได้ เนื่องจากเงินไม่พอหรือไม่มีเศษ");
                }
                DisplayInventory();
            }
        }

    static bool CanGiveChange(int amount)
    {
        Dictionary<int,int> changeToGive = new Dictionary<int, int>();
        int amountTotal = amount;

        foreach (var bill in cashTotal.Keys){
            if(amountTotal <= 0){
                break;
            }
            int numOfBills = amountTotal / bill;
            if(numOfBills > cashTotal[bill]){
                numOfBills = cashTotal[bill];
            } 
            if(numOfBills > 0){
                changeToGive[bill] = numOfBills;
                amountTotal -= numOfBills * bill;
            }
        }
        if(amountTotal > 0){
            return false;
        }

        foreach(var bill in changeToGive.Keys){
            cashTotal[bill] -= changeToGive[bill];
            Console.WriteLine($"ใช้แบงค์ {bill}: {changeToGive[bill]} เหลือ {cashTotal[bill]}");
        }
        return true;
    }

    static void DisplayInventory()
    {
        Console.WriteLine("\nสถานะเงินคงเหลือ:");
        foreach (var bill in cashTotal)
        {
            Console.WriteLine($"แบงค์ {bill.Key}: {bill.Value}");
        }
        Console.WriteLine();
    }
    
}
