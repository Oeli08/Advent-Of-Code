class Programm
{
    static void Main(string[] args)
    {
        Id id = new Id();
        id.parse();
        Console.WriteLine(id.counter);
    }
}
class Id
{
    public int counter = 0;
    public void parse()
    {
        string input = System.IO.File.ReadAllText("Input.txt");
        string inputId = System.IO.File.ReadAllText("InputId.txt");
        string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        string[] linesIdarray = inputId.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        List<string> linesIdList = new List<string>(linesIdarray);

        foreach (string line in lines)
        {
            string[] range = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
            int j = linesIdList.Count;
            for (int i = 0 ; i < j; i++)
            {
                if (Convert.ToInt64(linesIdList[i]) >= Convert.ToInt64(range[0]) && Convert.ToInt64(linesIdList[i]) <= Convert.ToInt64(range[1]))
                {
                    counter++;
                    linesIdList.RemoveAt(i);
                    i--;
                    j = linesIdList.Count;
                }
            }
   

        }
    }
}