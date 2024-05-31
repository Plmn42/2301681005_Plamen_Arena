using System;

namespace SimpleRPG
{
    public class Hero
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public bool IsAlive { get; set; }

        public Hero(string name, int hp, int attack)
        {
            Name = name;
            HP = hp;
            Attack = attack;
            IsAlive = true;
        }

        public virtual void DisplayStats()
        {
            if (IsAlive)
            {
                Console.WriteLine($"Name: {Name}, HP: {HP}, Attack: {Attack}");
            }
            else
            {
                Console.WriteLine($"{Name} is dead.");
            }
        }

        public virtual void DealAttack(Hero target)
        {
            if (IsAlive)
            {
                Console.WriteLine($"{Name} attacks {target.Name} for {Attack} damage!");
                target.ReceiveAttack(Attack);
            }
            else
            {
                Console.WriteLine($"{Name} is dead and cannot attack.");
            }
        }

        public void ReceiveAttack(int damage)
        {
            if (IsAlive)
            {
                HP -= damage;
                Console.WriteLine($"{Name} receives {damage} damage! Remaining HP: {HP}");
                if (HP <= 0)
                {
                    IsAlive = false;
                    Console.WriteLine($"{Name} has died.");
                }
            }
        }

        public virtual void Heal(Hero target)
        {
            if (IsAlive)
            {
                target.ReceiveHealing(10);
            }
            else
            {
                Console.WriteLine($"{Name} is dead and cannot be healed.");
            }
        }

        public void ReceiveHealing(int healAmount)
        {
            if (IsAlive)
            {
                HP += healAmount;
                Console.WriteLine($"{Name} receives {healAmount} healing! Current HP: {HP}");
            }
            else
            {
                Console.WriteLine($"{Name} is dead and cannot be healed.");
            }
        }
    }

    public class Tank : Hero
    {
        public double ShieldMultiplier { get; }

        public Tank(string name, int hp, int attack, double shieldMultiplier)
            : base(name, hp, attack)
        {
            ShieldMultiplier = shieldMultiplier;
            HP = (int)(HP * ShieldMultiplier);
        }

        public override void DisplayStats()
        {
            if (IsAlive)
            {
                Console.WriteLine($"[Tank] Name: {Name}, HP: {HP}, Attack: {Attack}");
            }
            else
            {
                Console.WriteLine($"[Tank] {Name} is dead.");
            }
        }

        public override void DealAttack(Hero target)
        {
            if (IsAlive)
            {
                Console.WriteLine($"{Name} (Tank) attacks {target.Name} for {Attack} damage!");
                base.DealAttack(target);
            }
            else
            {
                Console.WriteLine($"{Name} is dead and cannot attack.");
            }
        }
    }

    public class Mage : Hero
    {
        public int Mana { get; set; }
        public int StaffMultiplier { get; }

        public Mage(string name, int hp, int attack, int mana, int staffMultiplier)
            : base(name, hp, attack)
        {
            Mana = mana;
            StaffMultiplier = staffMultiplier;
        }

        public override void DisplayStats()
        {
            if (IsAlive)
            {
                Console.WriteLine($"[Mage] Name: {Name}, HP: {HP}, Attack: {Attack}, Mana: {Mana}");
            }
            else
            {
                Console.WriteLine($"[Mage] {Name} is dead.");
            }
        }

        public override void DealAttack(Hero target)
        {
            if (IsAlive)
            {
                int effectiveAttack = Attack;
                if (Mana >= 100)
                {
                    effectiveAttack *= StaffMultiplier;
                    Console.WriteLine($"{Name} (Mage) uses magic to attack {target.Name} for {effectiveAttack} damage! Remaining Mana: {Mana}");
                }
                else
                {
                    Console.WriteLine($"{Name} (Mage) attacks {target.Name} for {Attack} damage! Not enough mana for staff multiplier.");
                }
                target.ReceiveAttack(effectiveAttack);
            }
            else
            {
                Console.WriteLine($"{Name} is dead and cannot attack.");
            }
        }
    }

    public class Assassin : Hero
    {
        public double CriticalChanceMultiplier { get; }

        public Assassin(string name, int hp, int attack, double criticalChanceMultiplier)
            : base(name, hp, attack)
        {
            CriticalChanceMultiplier = criticalChanceMultiplier;
        }

        public override void DisplayStats()
        {
            if (IsAlive)
            {
                Console.WriteLine($"[Assassin] Name: {Name}, HP: {HP}, Attack: {Attack}");
            }
            else
            {
                Console.WriteLine($"[Assassin] {Name} is dead.");
            }
        }

        public override void DealAttack(Hero target)
        {
            if (IsAlive)
            {
                double effectiveAttack = Attack;
                if (new Random().NextDouble() < CriticalChanceMultiplier)
                {
                    effectiveAttack *= 2;
                    Console.WriteLine($"{Name} (Assassin) lands a critical hit on {target.Name} for {effectiveAttack} damage!");
                }
                else
                {
                    Console.WriteLine($"{Name} (Assassin) attacks {target.Name} for {Attack} damage!");
                }
                target.ReceiveAttack((int)effectiveAttack);
            }
            else
            {
                Console.WriteLine($"{Name} is dead and cannot attack.");
            }
        }
    }

    public class Cleric : Hero
    {
        public double ManaMultiplier { get; }

        public Cleric(string name, int hp, int attack, double manaMultiplier)
            : base(name, hp, attack)
        {
            ManaMultiplier = manaMultiplier;
        }

        public override void DisplayStats()
        {
            if (IsAlive)
            {
                Console.WriteLine($"[Cleric] Name: {Name}, HP: {HP}, Attack: {Attack}");
            }
            else
            {
                Console.WriteLine($"[Cleric] {Name} is dead.");
            }
        }

        public override void DealAttack(Hero target)
        {
            if (IsAlive)
            {
                Console.WriteLine($"{Name} (Cleric) attacks {target.Name} for {Attack} damage!");
                target.ReceiveAttack(Attack);
            }
            else
            {
                Console.WriteLine($"{Name} is dead and cannot attack.");
            }
        }

        public override void Heal(Hero target)
        {
            if (IsAlive)
            {
                base.Heal(target);
            }
            else
            {
                Console.WriteLine($"{Name} is dead and cannot heal.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Tank garen = new Tank("Garen", 150, 50, 1.5);
            garen.DisplayStats();


            Mage ryze = new Mage("Ryze", 100, 70, 200, 2);
            ryze.DisplayStats();


            Assassin talon = new Assassin("Talon", 80, 90, 2);
            talon.DisplayStats();


            Cleric sarante = new Cleric("Sarante", 120, 40, 1.2);
            sarante.DisplayStats();


            garen.DealAttack(ryze);
            ryze.DealAttack(garen);
            talon.DealAttack(garen);
            talon.DealAttack(ryze);
            sarante.Heal(garen);
            sarante.Heal(ryze);


            garen.DealAttack(talon);
            garen.DealAttack(talon);
            talon.DealAttack(garen);
            sarante.Heal(talon);
            Console.ReadKey();
        }
    }
}
