using Mandatory2DGameFramework.config;
using Mandatory2DGameFramework.helper.logger;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.creatures.classes;
using Mandatory2DGameFramework.worlds;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        // testing configuration and loading of world
        var config = GameConfigLoader.Load("gameconfig.xml");
        World world = World.FromConfig(config);

        Console.WriteLine(world);

        // testing logging 
        var logger = Logger.Log;

        // Console output
        logger.AddListener(new ConsoleTraceListener());

        // log to file
        logger.AddListener(new TextWriterTraceListener("game.log"));

        logger.LogInfo("Game started");
        logger.LogWarning("Low health warning");
        logger.LogError("Critical failure");

        // testing AttackItem
        var sword = new AttackItem("Sword", 10, 1, 5);
        var dagger = new AttackItem("Dagger", 5, 1, 2);

        var combo = sword + dagger;

        Console.WriteLine(combo.Hit);
        Console.WriteLine(combo.Weight);

        // testing battle classes and world object added to the world
        var hero = new Warrior("Conan");
        var mage = new Mage("Merlin");

        world.AddCreature(hero);
        world.AddCreature(mage);

        var axe = new AttackItem("Axe", 12, 1, 8) { Lootable = true };
        axe.MoveObject(5, 5);
        world.AddObject(axe);

        hero.Loot(axe);
        hero.PerformHit(mage);
        Console.WriteLine(hero);
        Console.WriteLine(mage);
    }
}