using Newtonsoft.Json;

namespace AIDraw;

class Program
{
    static void Main()
    {
        Console.SetWindowSize(60, 25);
        Console.SetBufferSize(60, 25);

        (int[,] coordinates, int[] center) = 
            new Picture(Console.WindowWidth / 2, Console.WindowHeight / 2)
                .GetPicture();

        Console.SetWindowSize(160, 45);
        Console.SetBufferSize(160, 45);

        File.Create(@"C:\Users\Vlad\Desktop\C#\MyProjects\MyProjects\AIDraw\Coordiantes\Coordinates.JSON");

        File.WriteAllText(@"C:\Users\Vlad\Desktop\C#\MyProjects\MyProjects\AIDraw\Coordiantes\Coordinates.JSON", JsonConvert.SerializeObject(coordinates));

        JsonConvert.SerializeObject(center);
    }
}