class Programm
{
    static void Main(string[] args)
    {
        Paper paper = new Paper();
        paper.Parse();
    }
}
class Paper
{
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
    }
}