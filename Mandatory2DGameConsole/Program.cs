using Mandatory2DGameFramework.config;
using Mandatory2DGameFramework.worlds;

class Program
{
    static void Main()
    {
        var config = GameConfigLoader.Load("gameconfig.xml");
        World world = World.FromConfig(config);

        Console.WriteLine(world);
    }
}