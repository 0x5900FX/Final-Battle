
using System.IO.Pipes;
using System.Reflection.Metadata;
using System.Threading;
using System.Windows.Markup;

public abstract class Character
{
    public string Name { get; set; }
    public int AttackPower { get; set; }
    
    private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }          // returns the actual stored value
        set { currentHealth = Math.Max(0, (Math.Min(value, MaxHealth))); }
    }

    public int MaxHealth { get; set; }
    public Attack StandardAttack { get ; set; } 
    public Attack SecondaryAttack { get; set; }
    public Character(string name, int maxHealth, int attackPower)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth ;
        AttackPower = attackPower;
    }


}

public abstract class Action
{ 

    public abstract void Execute(Character user, Character target);

}

public abstract class Player
{

    public abstract Action SelectAction(Character user, ICollection<Character> party_member, ICollection<Character> Enemy);
}

public abstract class Item
{
    public string Name { get; }
    public Item(string name) { Name = name; }
    public abstract void Use_Item(Character user);

}
public class HealthPotion:Item
{
    public HealthPotion() : base("healh potion") { }
    public override void Use_Item(Character user)
    {

        if (user.CurrentHealth + 10 < user.MaxHealth)
        {
            user.CurrentHealth += 10;
        }
        Console.WriteLine($"used {this.Name} on {user.Name} and increased hp to {user.CurrentHealth}");

    }
}
public class HumanPlayer : Player
{
    public override Action SelectAction(Character user, ICollection<Character> party_member, ICollection<Character> Enemy)
    {
        Console.WriteLine($"{user.Name}'s turn. Choose an action:");
        Console.WriteLine($"1. Attack with {user.StandardAttack}");
        Console.WriteLine($"2. Attack with {user.SecondaryAttack}");
        string choice = Console.ReadLine();
        if (choice == "1")
        {
            return new AttackAction(user.StandardAttack);
        }
        else if(choice=="2"){
            return new AttackAction(user.SecondaryAttack);

        }
        else
        {
            return new DoNothing();
        }
    }
}

public class MonsterPlayer : Player
{
    Random r = new Random();
    public override Action SelectAction(Character user, ICollection<Character> party_member, ICollection<Character> Enemy)
    {
        // For simplicity, monsters always attack
        if (r.Next(2) == 0)
        {
            return new AttackAction(user.StandardAttack);
        }
        else {
            return new AttackAction(user.SecondaryAttack);
                };
    }
}
public class ComputerPlayer : Player {
    Random r = new Random();
    public override Action SelectAction(Character user, ICollection<Character> party_member, ICollection<Character> Enemy)
    {
        //Thread.Sleep(1000);
        if (r.Next(2) == 0)
        {
            return new AttackAction(user.StandardAttack);
        }
        else
        {
            return new AttackAction(user.SecondaryAttack);
        }
        ;
    }
}

public class DoNothing : Action
{
       public override void Execute(Character user, Character target)
    {
        Console.WriteLine($"{user.Name} did Nothing!");
    }
}

public abstract class Attack
{
    public string name { get;  }
    public Attack(string name){
    this.name = name;
    }
    public abstract int DealDamage();

}

public class shield_block: Attack
{
    public shield_block(string name) : base("Shield Block") { }

    public override int DealDamage()
    {
        Random r = new Random();
        return 1; // Random damage between 1 
    }
}

public class Useless_block : Attack
{
    public Useless_block(string name) : base("Shield Block") { }

    public override int DealDamage()
    {
        Random r = new Random();
        return 1; // Random damage between 1
    }
}
public class Slash : Attack
{
    public Slash(string name) : base("Slash") { }

    public override int DealDamage()
    {
        Random r = new Random();
        return r.Next(1, 4); // Random damage between 1 and 3
    }
}

public class Sword_Slash : Attack
{
    public Sword_Slash(string name) : base("Sword Slash") { }

    public override int DealDamage()
    {
        Random r = new Random();
        return r.Next(1, 8); // Random damage between 1 and 3
    }
}

public class Punch : Attack
{

    public  Punch(string name): base("Punch") { }

    public override int DealDamage()
    {
        return 1;
    }

}
public class Super_punch : Attack
{
    public string name { get; }

    public Super_punch(string name) : base("Super Punch") { }
        public override int DealDamage()
    {
        return 5;
    }
}

public class Fireball : Attack
{
    public Fireball(string name): base("Fireball") { }
    public override int DealDamage()
    {
        Random r = new Random();
        return r.Next(8, 15); // Random damage between 8,15
    }
}

public class WaterDrought : Attack
{
    public WaterDrought(string name) : base("WaterDrought") { }
    public override int DealDamage()
    {
        Random r = new Random();
        return r.Next(8, 20); // Random damage between 8-20
    }
}

public class BoneCrunch : Attack
{
    public BoneCrunch(string name ): base("Bone Crunch")
    {
    }

    public override int DealDamage()
    {
        Random r = new Random();
        return r.Next(22); // Random damage between 1 and 2
    }
}

public class BoneThrow : Attack
{
    public BoneThrow(string name) : base("Bone Crunch")
    {
    }

    public override int DealDamage()
    {
        Random r = new Random();
        return r.Next(12); // Random damage between 1 and 2
    }
}

public class DoomOfDarkness: Attack
{
    public DoomOfDarkness(string name): base("Doom of Darkness") { }
    public override int DealDamage()
    {
        Random r = new Random();
        return r.Next(20, 22); // Random damage between 12,30
    }
}

public class EternalFlame: Attack
{
    public EternalFlame(string name) : base("Eternal Flame") { }
    public override int DealDamage()
    {
        Random r = new Random();
        return r.Next(9, 20); // Random damage between 5 and 10
    }
}


    public class Skeleton : Character
{
     Random r = new Random();

    public Skeleton(string name, int health, int attackPower)
        : base(name, 5, attackPower)
    {
        MaxHealth = 5;
        int attackpower = r.Next(2);
        StandardAttack = new BoneCrunch("Bone Crunch");
        SecondaryAttack = new BoneThrow("Bone Throw");

    }

}

public class THE_UNCODED_ONE : Character
{
    public THE_UNCODED_ONE(string name, int health, int attackPower)
        : base(name, 100, attackPower)
    {
        MaxHealth = 100;
        StandardAttack = new DoomOfDarkness("Doom of Darkness");
        SecondaryAttack = new EternalFlame("Eternal Flame");
    }
}

public class Hero : Character
{
    public Hero(string name, int health, int attackPower)
        : base(name, health, attackPower)
    {
        MaxHealth = 8;
        StandardAttack = new Slash("Slash");
        SecondaryAttack = new Sword_Slash("Sword Slash");
    }

}
public class Tank: Character
{
    public Tank(string name, int health, int attackPower) : base(name, health, attackPower) {
        MaxHealth = 30;
        StandardAttack = new shield_block("Shield Block");
        SecondaryAttack = new Useless_block("useless Block");
    }
}
public class Mage : Character
{
    public Mage(string name, int health, int attackPower)
        : base(name, health, attackPower)
    {
        MaxHealth = 10;
        StandardAttack = new Fireball("Fireball");
        SecondaryAttack = new WaterDrought("WaterDrought");
    }
}

public class TrueProgrammer : Character
{
    public TrueProgrammer(string name, int health, int attackPower)
        : base(name, 25, attackPower)
    {
        MaxHealth = 25;
        StandardAttack = new Punch("Punch");
        SecondaryAttack = new Super_punch("Super Punch");

    }
}

public class AttackAction : Action
{
    Attack ChoosenAttack { get; set; }
    public AttackAction(Attack attack)
    {
        ChoosenAttack = attack;
    }
    public override void Execute(Character current_attacker, Character target)
    {

        Console.WriteLine($"{this.ChoosenAttack.name}");

        Console.WriteLine($"{current_attacker.Name} used {current_attacker.StandardAttack.name} on {target.Name}");


        if (target.CurrentHealth <= 0)
        {
            Console.WriteLine($"{target.Name} has already been defeated!");
            return;
        }
        else {
            int damage = ChoosenAttack.DealDamage();
            target.CurrentHealth -= damage;


            if (target.CurrentHealth <= 0)
            {
                Console.WriteLine($"{target.Name} has been defeated!");
                


            }
            else
            {
                Console.WriteLine($"{target.Name} took {damage} damage and now has {target.CurrentHealth} health left.");
            }


        }




    }
}

class FinalBattle
{
    public static void Main(string[] args)
    {
        
        //Action action = new DoNothing();
        //Action attack = new AttackAction(DoNothing);

        //Character hero = new Hero("Hero", 100, 20);
        //Character tank = new Hero("Tank", 150, 10);
        //Character skelly1 = new Skeleton("Skeleton1", 50, 10);
        //Character skelly2 = new Skeleton("Skeleton2", 50, 10);
        Character trueprogrammer = new TrueProgrammer("UserName Holder", 20, 1);
        Character Monlina = new Mage("Monlina", 10, 5);
        Character Keith = new Tank("Keith",50,1);
        Character Pogi = new Tank("Pogi", 40, 1);
        Character Luhou = new Mage("luhou", 10, 5);




        Console.Clear();

        Console.WriteLine("Welcome to the Final Battle!");
        Console.WriteLine("Enter the True Hero's name:");
        trueprogrammer.Name = Console.ReadLine();

        var players = GameController();
        Console.Clear();
        List<Item> HeroItem = new List<Item>()
        {
            new Item("Hp")
        };
        List<Item> MonsterItem = new List<Item>();


        List<ICollection<Character>> Monster_Party = new List<ICollection<Character>>()
        {
        new List<Character>()
        {
            new Skeleton("Skeleton1", 5, 10)
        },

        new List<Character>()
        {
            new Skeleton("Skeleton1", 5, 10),
            new Skeleton("Skeleton2", 5, 10),
            new Skeleton("Skeleton1", 5, 10),
            new Skeleton("Skeleton2", 5, 10),
            new Skeleton("Skeleton1", 5, 10),
            new Skeleton("Skeleton2", 5, 10),
            new Skeleton("MageSkelly", 12 ,5)

        },
        new List<Character>
        {
            new THE_UNCODED_ONE("THE_UNCODED_ONE", 100, 10) 
        }
        };


        ICollection<Character> Hero_Party = new List<Character>()
        {
        Keith,
        Pogi,
        trueprogrammer,
        Monlina,
        Luhou
        
        
        };



        for (int i = 0; i < Monster_Party.Count; i++)
        {
            bool heroesWon = RunBattle(Hero_Party, Monster_Party[i], players.FirstPlayer,players.AnotherPlayer);
            if (!heroesWon)
            {
                Console.WriteLine("You have been defeated!. You lost");
                break;
            }

            Console.WriteLine($"Yay you won Wave {i + 1}");

            if (i == Monster_Party.Count - 1)
            {
                Console.WriteLine("You won all Monster Party");
            }
            else
            {
                Console.WriteLine("Prepare for Battle. Strong one !");
                Thread.Sleep(1400);
            }

        }
    }



    public static bool RunBattle(ICollection<Character> Hero_Party ,
                                ICollection<Character> Monster_Party,
                                Player HeroPlayer,
                                Player MonsterController)
    {
        
        bool awoken = false;

        while (true)
        {
            Show_hp(Hero_Party,Monster_Party);

            var alive_heroes = Hero_Party.Where(h => h.CurrentHealth > 0).ToList();
            if (!alive_heroes.Any())
            {
                Console.WriteLine("All heroes have been defeated! Game Over.");
                return false;
            }

            var alive_monsters = Monster_Party.Where(h => h.CurrentHealth > 0).ToList();
            if (!alive_monsters.Any())
            {
                Console.WriteLine("All monsters have been defeated! Heroes win!");
                return true;
            }


            foreach (var member in alive_heroes)
            {
                Console.WriteLine($"It is {member.Name}'s turn...");
                Action act = HeroPlayer.SelectAction(member, Hero_Party, Monster_Party);
                act.Execute(member, alive_monsters.FirstOrDefault());



                Hero_Party = Hero_Party.Where(h => h.CurrentHealth > 0).ToList();

                if (!Hero_Party.Any())
                {
                    Console.WriteLine("All heroes have been defeated! Game Over.");
                    return false;
                }
            }

            Console.WriteLine("");
            //Thread.Sleep(200);


            foreach (var monster in alive_monsters)
            {
                

                Console.WriteLine($"It is {monster.Name}'s turn...");
                Action act = MonsterController.SelectAction(monster, Monster_Party, Hero_Party);
                act.Execute(monster, alive_heroes.FirstOrDefault());

                if (awoken  == false)
                {
                    if (monster.Name == "THE_UNCODED_ONE")
                    {
                        Console.WriteLine("\n\n\n =========== THE_UNCODED_ONE Has Awoken. Final Battle Beginsss =========== \n\n\n");
                        awoken = true;
                    }
                }

                Monster_Party = Monster_Party.Where(h => h.CurrentHealth > 0).ToList();
                if (!Monster_Party.Any())
                {
                    Console.WriteLine("All monsters have been defeated! Heroes win!");
                    return true;
                }
            }


            Console.WriteLine("\n");
            //Thread.Sleep(1000);
        }

    }

    public static(Player FirstPlayer , Player AnotherPlayer )GameController()
    {
        Console.WriteLine("=================================");
        Console.WriteLine("How would you like to play?"      );
        Console.WriteLine("1.      Player vs Computer    ");
        Console.WriteLine("2.    Computer vs Computer    ");
        Console.WriteLine("3.      Player vs Player      ");

        Console.WriteLine("=================================");

        Console.Write("Enter your choice (1-3): ");


        string UserInput = Console.ReadLine().Trim();

        switch (UserInput)
        {
            case "1":
                return (new HumanPlayer(), new ComputerPlayer());

            case "2":
                return (new  ComputerPlayer() ,new ComputerPlayer());

            case "3":
                return (new HumanPlayer(), new HumanPlayer());

            default:
                Console.WriteLine("Invalid choice. Defaulting to Player vs Computer.");
                return(new ComputerPlayer(), new ComputerPlayer());

        }


    }

    public static void Show_hp(ICollection<Character> Hero_Party,
                                ICollection<Character> Monster_Party)
    {
        Console.WriteLine("===============================================Battle Status====================================================\n");

        foreach (var hero in Hero_Party)
        {
            Console.WriteLine($"{hero.Name}\t {hero.CurrentHealth}/{hero.MaxHealth}");
        }
        Console.WriteLine("====================================================V/S=========================================================\n");
        foreach (var Monster in Monster_Party)
        {
            Console.WriteLine($"\t\t\t\t\t\t\t\t{Monster.Name}     {Monster.CurrentHealth}/{Monster.MaxHealth}");
        }
        Console.WriteLine("====================================================V/S=========================================================\n");

    }
    //
}