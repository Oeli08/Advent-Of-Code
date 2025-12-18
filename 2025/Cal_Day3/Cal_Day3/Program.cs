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
    double[] highestNumbers = new double[12];
    int[] highestIs = new int[12];

    public BigInteger SortHighest()
    {
        string input = System.IO.File.ReadAllText("Input.txt");
        string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        string result = "";
        //int highest1 = 0;
        //int highest2 = 0;
        //int highestI = 0;

        foreach (string line in lines)
        {
            //highest1 = 0;
            //highest2 = 0;
            //highestI = 0;
            for (int i = 0; i < line.Length; i++)
            {
                for (int j = 0; j < highestNumbers.Length; j++)
                {
                    if (i+j < line.Length)
                    {
                        if (highestNumbers[j] < line[i + j] - '0' && i <= 88)
                        {
                            highestNumbers[j] = line[i + j] - '0';
                            highestIs[j] = i;
                            for (int k = highestIs[j]; k < highestNumbers.Length - j && k + j <= 88 && i >= highestIs[i]; k++) //For (int k = highestIs[j] muss geändert werden (Fehler Unbekannt)
                            {
                                highestNumbers[j + k] = line[i + j + k] - '0';
                                highestIs[j + k] = i + k;
                            }
                        }
                    }
                    if (line.Length - 1 == i)
                    {
                        for (int l = 0; l < highestNumbers.Length; l++)
                        {
                            result += Convert.ToString(highestNumbers[l]);
                        }
                        sum += BigInteger.Parse(result);//noch zu kleiner Datentyp vllt
                    }
                }
            }
        }
        result = "";
        return sum;
    }
}