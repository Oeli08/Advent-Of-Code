class Programm
{
    static void Main(string[] args)
    {
        Paper paper = new Paper();
        paper.Parse();
        Console.WriteLine(paper.counter);
    }
}
class Paper
{
    public int counter = 0;
    public void Parse()
    {
        string input = System.IO.File.ReadAllText("Input.txt");
        string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        string[,] map = new string[lines[0].Count(), lines.Length];
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Count(); x++)
            {
                map[x, y] = lines[y][x].ToString();
            }
        }

        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                if (map[x, y] == "@")
                {
                    int paperCounter = 0;
                    for (int yy = y - 1; yy < y + 2; yy++)
                    {
                        if ( yy < 0) continue;
                        for (int xx = x - 1; xx < x + 2; xx++)
                        {
                            if (xx == x && yy == y) continue;
                            if (xx < 0) continue;
                            if (xx < map.GetLength(0) && yy < map.GetLength(1))
                            {
                                if (map[xx, yy] == "@")
                                {
                                    paperCounter++;
                                }
                            }
                        }
                    }
                    if (paperCounter < 4)
                    {
                        counter++;
                    }
                }
                else
                {
                }
            }
        }
    }
}