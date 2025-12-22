//Part 1
//using System;
//using System.Collections.Generic;
//using System.Linq;

//class Programm
//{
//    static void Main()
//    {
//        Battery battery = new Battery();
//        Console.WriteLine(battery.SortHighest());
//    }
//}

//class Battery
//{
//    double sumTen = 0;
//    double sumOne = 0;
//    double sum = 0;
//    double[] highestNumbers = new double[12];
//    int[] highestIs = new int[12];

//    public double SortHighest()
//    {
//        string input = System.IO.File.ReadAllText("Day3_Input.txt");
//        string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
//        string result = "";
//        //int highest1 = 0;
//        //int highest2 = 0;
//        //int highestI = 0;

//        foreach (string line in lines)
//        {
//            //highest1 = 0;
//            //highest2 = 0;
//            //highestI = 0;
//            for (int i = 0; i < line.Length; i++)
//            {
//                for (int j = 0; j < highestNumbers.Length; j++)
//                {
//                    if (highestNumbers[j] < line[i] - '0' && i != 88)
//                    {
//                        highestNumbers[j] = line[i] - '0';
//                        highestIs[j] = i;
//                        for (int k = highestIs[i]; k < highestNumbers.Length - j && k+j <= 88 && i >= highestIs[i]; k++)
//                        {
//                            highestNumbers[j+k] = line[i+k] - '0';
//                            highestIs[j+k] = i+k;
//                        }
//                    }
//                    if (line.Length - 1 == i)
//                    {
//                        for (int l = 0; l < highestNumbers.Length; l++)
//                        {
//                            result += Convert.ToString(highestNumbers[l]);
//                        }
//                        sum += double.Parse(result);
//                    }


//                    //if (highest1 < line[i] - '0' && i != 99)
//                    //{
//                    //    highest1 = line[i] - '0';
//                    //    highest2 = line[i + 1] - '0';
//                    //    highestI = i;
//                    //}

//                    //if (highest2 < line[i] - '0' && i > highestI)
//                    //{
//                    //    highest2 = line[i] - '0';
//                    //}

//                    //if (line.Length - 1 == i)
//                    //{
//                    //    Console.WriteLine(highest1 + "" + highest2);
//                    //    //sumTen += highest1;
//                    //    //sumOne += highest2;
//                    //    sum += highest1 * 10 + highest2;
//                    //}
//                }
//            }
//            //string line1result = line.Remove(highestI, 1);
//            //for (int i = 0; i < line.Length; i++)
//            //{
//            //    if (highest2 < Convert.ToInt32(line[i]) && i != highestI)
//            //    {
//            //        highest2 = Convert.ToInt32(line[i]);
//            //    }

//            //    if (line.Length - 1 == i)
//            //    {
//            //        sumOne += highest2;
//            //    }
//            //}
//        }
//        //sumTen *= 10;
//        //sum = sumTen + sumOne;
//        return sum;
//    }
//}
//--------------------------------------------------------------------------
//Part 2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Programm
{
    static void Main()
    {
        Battery battery = new Battery();
        Console.WriteLine(battery.SortHighest());
    }
}

class Battery
{
    BigInteger sum = BigInteger.Zero;
    int[] highestNumbers = new int[12];  
    int[] highestIs = new int[12];
    static BigInteger Greedy(string line)
    {
        int toRemove = line.Length - 12;
        var stack = new List<char>();

        foreach (char c in line)
        {
            while (stack.Count > 0 &&
                   toRemove > 0 &&
                   stack[^1] < c)
            {
                stack.RemoveAt(stack.Count - 1);
                toRemove--;
            }
            stack.Add(c);
        }

        while (stack.Count > 12)
            stack.RemoveAt(stack.Count - 1);

        return BigInteger.Parse(new string(stack.ToArray()));
    }
    static BigInteger OldLogic(string line)
    {
        int[] highestNumbers = new int[12];
        int[] highestIs = new int[12];
        string result = "";

        for (int i = 0; i < line.Length; i++)
        {
            for (int j = 0; j < highestNumbers.Length; j++)
            {
                if (i + j < line.Length)
                {
                    if (highestNumbers[j] < line[i + j] - '0' && i <= line.Length - 12)
                    {
                        if (i >= line.Length - 12)
                        {

                        }
                        highestNumbers[j] = line[i + j] - '0';
                        highestIs[j] = i;

                        for (int k = 0; k < highestNumbers.Length - j &&
                                        k + j <= line.Length - 12 ; k++)
                        {
                            highestNumbers[j + k] = line[i + j + k] - '0';
                            highestIs[j + k] = i + k;
                        }
                    }
                }
            }
        }

        for (int l = 0; l < highestNumbers.Length; l++)
            result += highestNumbers[l];

        return BigInteger.Parse(result);
    }

    public BigInteger SortHighest()
    {
        string input = System.IO.File.ReadAllText("Input.txt");
        string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        string result = "";
        //int highest1 = 0;
        //int highest2 = 0;
        //int highestI = 0;

        //foreach (string line in lines)
        //{
        //    //highest1 = 0;
        //    //highest2 = 0;
        //    //highestI = 0;
        //    for (int i = 0; i < line.Length; i++)
        //    {
        //        for (int j = 0; j < highestNumbers.Length; j++)
        //        {
        //            if (i + j < line.Length)
        //            {
        //                if (highestNumbers[j] < line[i + j] - '0' && i <= line.Length - 12)
        //                {
        //                    highestNumbers[j] = line[i + j] - '0';
        //                    highestIs[j] = i;
        //                    for (int k = 0; k < highestNumbers.Length - j && k + j <= line.Length - 12 && i + k >= highestIs[k + j]; k++)
        //                    {
        //                        highestNumbers[j + k] = line[i + j + k] - '0';
        //                        highestIs[j + k] = i + k;
        //                    }
        //                }
        //            }
        //            if (line.Length - 1 == i)
        //            {
        //                for (int l = 0; l < highestNumbers.Length; l++)
        //                {
        //                    result += Convert.ToString(highestNumbers[l]);
        //                }
        //                BigInteger bigInteger = BigInteger.Parse(result);
        //                sum += bigInteger;//noch zu kleiner Datentyp vllt
        //                result = "";
        //                highestNumbers = new int[12];
        //                highestIs = new int[12];
        //                break;
        //            }
        //        }
        //    }
        //}
        //result = "";



        //string input = System.IO.File.ReadAllText("Input.txt");
        //string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        //BigInteger sum = BigInteger.Zero;

        //foreach (string line in lines)
        //{
        //    int toRemove = line.Length - 12;
        //    var stack = new List<char>();

        //    foreach (char c in line)
        //    {
        //        while (stack.Count > 0 &&
        //               toRemove > 0 &&
        //               stack[^1] < c)
        //        {
        //            stack.RemoveAt(stack.Count - 1);
        //            toRemove--;
        //        }
        //        stack.Add(c);
        //    }

        //    // falls noch zu viele Ziffern
        //    while (stack.Count > 12)
        //        stack.RemoveAt(stack.Count - 1);

        //    BigInteger value = BigInteger.Parse(new string(stack.ToArray()));
        //    sum += value;
        //}
        for (int i = 0; i < lines.Length; i++)
        {
            BigInteger oldValue = OldLogic(lines[i]);
            sum += oldValue;
            BigInteger correctValue = Greedy(lines[i]);

            if (oldValue != correctValue)
            {
                Console.WriteLine($"❌ Unterschied in Zeile {i + 1}");
                Console.WriteLine(lines[i]);
                Console.WriteLine($"Alt     : {oldValue}");
                Console.WriteLine($"Richtig : {correctValue}");
                Console.WriteLine();
            }
        }

        return sum;
    }
}
