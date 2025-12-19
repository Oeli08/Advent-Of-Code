//class Lock
//{
//    public int current = 50;
//    public int password = 0;
//    public void Increase(int value)
//    {
//        current += value;
//        Check();
//    }
//    public void Decrease(int value)
//    {
//        current -= value;
//        Check();
//    }
//    public void Check()
//    {
//        if (current > 99)
//        {
//            password += 1;
//            int excess = current - 100;
//            current = 0;
//            Increase(excess);
//            if (current > 99)
//            {
//                Check();
//            }
//        }
//        else if (current < 0)
//        {
//            password += 1;
//            int deficit = 0 - current;
//            current = 99;
//            Decrease(deficit);
//            if (current < 0)
//            {
//                Check();
//            }
//        }
//        else if (current == 0)
//        {
//            password += 1;
//        }

//    }
//    public void ParseRotations(string input)
//    {
//        string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

//        foreach (string line in lines)
//        {
//            char dir = line[0];
//            int value = int.Parse(line.Substring(1));
//            if (dir == 'R')
//            {
//                Increase(value);
//            }
//            else if (dir == 'L')
//            {
//                Decrease(value);
//            }
//        }
//    }
//}
class Program
{
    static void Main(string[] args)
    {
        string input = System.IO.File.ReadAllText("Input_Day1.txt");
        Lock myLock = new Lock();
        myLock.ParseRotations(input);
        Console.WriteLine($"The final lock position is: {myLock.password}");
        Console.WriteLine(myLock.current);
    }
}
class Lock
{
    public int current = 50;
    public int password = 0;
    public List<Number> numbers = new List<Number>();
    public void Increase(int value)
    {
        password += value / 100;

        if (current + (value % 100) >= 100)
        {
            password += 1;
        }

        current = (current + value) % 100;
        numbers.Add(new Number { value = current, counter = password, toNull = (100 - current) % 100 });

    }

    public void Decrease(int value)
    {
        password += value / 100;

        int rem = value % 100;
        if (current != 0 && rem >= current)
        {
            password += 1;
        }

        current = ((current - value) % 100 + 100) % 100;
    }

    public void ParseRotations(string input)
    {
        string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        Console.WriteLine(lines.Length);
        foreach (string line in lines)
        {
            char dir = line[0];
            int value = int.Parse(line.Substring(1));

            if (dir == 'R')
                Increase(value);
            else
                Decrease(value);
        }
    }
}
class Number
{
    public int value;
    public int counter;
    public int toNull;
}