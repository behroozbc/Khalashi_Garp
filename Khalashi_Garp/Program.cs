using Khalashi_Garp;

Console.WriteLine("Hello, World!");
int n = int.Parse(Console.ReadLine() ?? "0");
GenerateRandomPufferArray(n);
Console.WriteLine(new Prufer(new uint[] { 4, 4, 4, 5 }).ToTree().ToPruer().ToString());

static void GenerateRandomPufferArray(int pufferTreeLenght)
{

    Random rand = new();
    int length = pufferTreeLenght - 2;
    var arr = new uint[length];

    // Loop to Generate Random Array
    for (int i = 0; i < length; i++)
    {
        arr[i] = (uint)(rand.Next(length + 1) + 1);
    }
    Console.WriteLine("actual value:" + String.Join(',', arr));
    new Prufer(arr).PrintTreeEdges();
    Console.WriteLine(new Prufer(arr).ToTree().ToPruer().ToString());
}
