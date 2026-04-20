using Mandatory2DGameFramework.config;
using Mandatory2DGameFramework.helper.logger;
using Mandatory2DGameFramework.model.attack;
using Mandatory2DGameFramework.model.attack.decorators;
using Mandatory2DGameFramework.model.combat;
using Mandatory2DGameFramework.model.creatures;
using Mandatory2DGameFramework.model.creatures.classes;
using Mandatory2DGameFramework.model.defence;
using Mandatory2DGameFramework.model.defence.decorators;
using Mandatory2DGameFramework.worlds;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== TESTSUITE START ===\n");

        // ---------------------------------------------------------
        // 1) CONFIGURATION TESTS
        // ---------------------------------------------------------
        var config = GameConfigLoader.Load("gameconfig.xml");
        World world = World.FromConfig(config);
        Console.WriteLine($"Loaded world: {world}\n");
        Console.WriteLine(world);

        Debug.Assert(world.MaxX == config.World.MaxX, "World MaxX mismatch");
        Debug.Assert(world.MaxY == config.World.MaxY, "World MaxY mismatch");
        Debug.Assert(world.Difficulty == config.Difficulty, "World Difficulty mismatch");

        // ---------------------------------------------------------
        // 2) LOGGER TESTS
        // ---------------------------------------------------------
        var logger = Logger.Log;

        var consoleListener = new ConsoleTraceListener();
        var fileListener = new TextWriterTraceListener("game.log");
        var fileListener2 = new TextWriterTraceListener("game2.log");

        logger.AddListener(consoleListener);
        logger.AddListener(fileListener);
        logger.AddListener(fileListener2);

        logger.LogInfo("Logger initialized");

        logger.RemoveListener(fileListener2);
        logger.LogInfo("File listener removed");

        // ---------------------------------------------------------
        // 3) ATTACKITEM TESTS
        // ---------------------------------------------------------
        Console.WriteLine("\n=== ATTACK TESTS ===");

        var sword = new AttackItem("Sword", 10, 1, 5);
        var dagger = new AttackItem("Dagger", 5, 1, 2);

        var combo = sword + dagger;
        Console.WriteLine(combo);
        Debug.Assert(combo.Hit == 15, "Sword + Dagger Hit mismatch");
        Debug.Assert(combo.Weight == 7, "Sword + Dagger Weight mismatch");

        // composite tests
        var compA = new AttackComposite([sword, dagger]);
        var compB = new AttackComposite([new AttackItem("Axe", 12, 1, 8)]);
        var compC = sword + dagger;
        var merged = compA + compB;

        Debug.Assert(merged.Hit == 27, "Composite merge Hit mismatch");
        Debug.Assert(merged.Weight == 15, "Composite merge Weight mismatch");
        Debug.Assert(compA.Hit == compC.Hit, "Composite operator Hit mismatch");
        Debug.Assert(compA.Weight == compC.Weight, "Composite operator Weight mismatch");

        // decorator tests
        var buffedSword = new AttackBuffDecorator(sword, 3);
        Console.WriteLine(buffedSword);
        Debug.Assert(buffedSword.Hit == 13, "Decorator buff mismatch");

        var debuffedSword = new AttackDebuffDecorator(new AttackItem("TestSword", 10, 1, 5), 20);
        Console.WriteLine(debuffedSword);
        Debug.Assert(debuffedSword.Hit == 0, "AttackDebuffDecorator should not go below 0");

        var smallDebuff = new AttackDebuffDecorator(new AttackItem("TestSword", 10, 1, 5), 3);
        Console.WriteLine(smallDebuff);
        Debug.Assert(smallDebuff.Hit == 7, "Small AttackDebuffDecorator mismatch");

        // ---------------------------------------------------------
        // 4) DEFENCEITEM TESTS
        // ---------------------------------------------------------
        Console.WriteLine("\n=== DEFENCE TESTS ===");

        var shield = new DefenceItem("Shield", 5, 3);
        var helmet = new DefenceItem("Helmet", 3, 1);

        // composite tests
        var defenceCombo = shield + helmet;
        Console.WriteLine(defenceCombo);
        Debug.Assert(defenceCombo.ReduceDamage == 8, "Defence combo mismatch");

        // decorator tests
        var buffedShield = new DefenceBuffDecorator(new DefenceItem("Shield", 5, 3), 4);
        Console.WriteLine(buffedShield);
        Debug.Assert(buffedShield.ReduceDamage == 9, "DefenceBuffDecorator mismatch");

        var debuffedShield = new DefenceDebuffDecorator(new DefenceItem("Shield", 5, 3), 10);
        Console.WriteLine(debuffedShield);
        Debug.Assert(debuffedShield.ReduceDamage == 0, "DefenceDebuffDecorator should not go below 0");

        var smallDebuffShield = new DefenceDebuffDecorator(new DefenceItem("Shield", 5, 3), 2);
        Console.WriteLine(smallDebuffShield);
        Debug.Assert(smallDebuffShield.ReduceDamage == 3, "Small DefenceDebuffDecorator mismatch");

        // ---------------------------------------------------------
        // 5) CREATURE TESTS
        // ---------------------------------------------------------
        Console.WriteLine("\n=== CREATURE TESTS ===");

        var warrior = new Warrior("Laezel");
        var mage = new Mage("Gale");

        world.AddCreature(warrior);
        world.AddCreature(mage);

        // looting
        var axe = new AttackItem("Axe", 8, 1, 8);
        axe.MoveObject(5, 5);
        world.AddObject(axe);

        warrior.Loot(axe);
        Debug.Assert(!world.ObjectsAt(5, 5).Any(), "Looted axe should be removed from world");
        // attack strategy should give 25% bonus to attack, so 8 * 1.25 = 10
        Debug.Assert(warrior.Strategy.CalculateDamage([axe]) == 10, "Warrior should have looted axe");

        // add defence to mage
        var armor = new DefenceItem("Light armor", 4, 5);
        mage.Loot(armor);
        // defensive strategy should give 25% bonus to defence, so 4 * 1.25 = 5
        Debug.Assert(mage.Strategy.CalculateDefence([armor]) == 5, "Mage should have looted armor");

        // ---------------------------------------------------------
        // 6) STRATEGY TESTS
        // ---------------------------------------------------------
        Console.WriteLine("\n=== STRATEGY TESTS ===");

        // balanced
        warrior.Strategy = new BalancedStrategy();
        Debug.Assert(warrior.Strategy.CalculateDamage([sword]) == 10, "Balanced damage mismatch");

        // aggressive
        warrior.Strategy = new AggressiveStrategy();
        Debug.Assert(warrior.Strategy.CalculateDamage([sword]) == (int)(10 * 1.25), "Aggressive damage mismatch");

        // defensive
        mage.Strategy = new DefensiveStrategy();
        Debug.Assert(mage.Strategy.CalculateDamage([dagger]) == (int)(5 * 0.75), "Defensive damage mismatch");

        // ---------------------------------------------------------
        // 7) COMBAT TESTS
        // ---------------------------------------------------------
        Console.WriteLine("\n=== COMBAT TESTS ===");

        int mageHPBefore = mage.HitPoint;
        int damage = warrior.PerformHit(mage);

        Debug.Assert(damage >= 0, "Damage should never be negative");
        Debug.Assert(mage.HitPoint == mageHPBefore - (damage - mage.Strategy.CalculateDefence([armor])), "Damage application mismatch");

        // kill test
        Console.WriteLine("\n=== DEATH TEST ===");
        while (!mage.IsDead)
        {
            warrior.PerformHit(mage);
        }

        Debug.Assert(mage.IsDead, "Mage should be dead");
        Debug.Assert(mage.HitPoint == 0, "Dead creature should have 0 HP");

        // ---------------------------------------------------------
        // 8) WORLD TESTS
        // ---------------------------------------------------------
        Console.WriteLine("\n=== WORLD TESTS ===");

        var chest = new WorldObject("Chest", lootable: false, removable: false);
        chest.MoveObject(2, 2);
        world.AddObject(chest);
        warrior.Loot(chest); // Should not be able to loot

        Debug.Assert(world.ObjectsAt(2, 2).Count() == 1, "Chest should still be there");

        // ---------------------------------------------------------
        // 9) LOGGING END
        // ---------------------------------------------------------
        Console.WriteLine("\n=== LOGGING END TEST ===");
        logger.RemoveListener(fileListener);

        Console.WriteLine("\n=== TESTSUITE END ===");
    }
}
