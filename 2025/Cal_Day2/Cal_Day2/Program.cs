using System;
using System.Collections.Generic;
using System.Linq;

class Programm
{
    static void Main(string[] args)
    {
        IdHandler handler = new IdHandler();
        handler.IdParser();
        Console.WriteLine(handler.counter);
        //Console.WriteLine(handler.counter2);
        //handler.NumberCounter();
    }
}

class IdHandler
{
    public long counter = 0;
    public long counter2 = 0;

    public void IdParser()
    {
        string input = System.IO.File.ReadAllText("input.txt");
        string[] lines = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            string[] splitLine = line.Split('-', StringSplitOptions.RemoveEmptyEntries);

            long start = long.Parse(splitLine[0]);
            long end = long.Parse(splitLine[1]);

            long number = Math.Abs(end - start + 1);

            for (long i = 0; i < number; i++)
            {
                counter += IdProcesser((start + i).ToString());
            }
        }
    }

    public long IdProcesser(string id)
    {
        long checkingId = Convert.ToInt64(id);

        string firsthalf = id.Substring(0, id.Length / 2);
        string secondhalf = id.Substring(firsthalf.Length);

        if (firsthalf == secondhalf)
        {
            return checkingId;
        }
        //else
        //{
        //    return 0;
        //}
        for (int k = 0; k <= firsthalf.Length; k++)
        {
            for (int i = 0; id.Length > i; i++)
            {
                string number = "";
                for (int j = 0; j < i; j++)
                {
                    counter2++;
                    number += id[j];
                }
                //long number = Convert.ToInt64(id[i]);
                if (i == 0)
                    continue;
                long number2 = id.Length / i;
                string result = string.Concat(Enumerable.Repeat(number, (int)number2));
                if (result == id)
                {
                    return Convert.ToInt64(id);
                }
            }
        }
        return 0;
    }

    //------------------------------

    public void NumberCounter()
    {
        string input = System.IO.File.ReadAllText("Day2_Input.txt");
        string[] lines = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            string[] splitLine = line.Split('-', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < splitLine.Length; i++)
            {
                Console.WriteLine(splitLine[i] + ": " + splitLine[i].Length);
            }
        }
    }
}

//if (line.length % 2 == 5)
//{}
//int lineLength = line.length / 2

//if (