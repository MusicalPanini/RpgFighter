using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgFighter
{
    class Program
    {
        static void Main(string[] args)
        {
            int turn = 0;
            string[] phases = { "Start Phase", "Mid Phase", "End Phase" };
            string[] enemyNames = { "Goblin", "Imp", "Orc", "Lance", "Thot" };
            string[] meleeNames = { "Sword", "Dagger", "Battle Axe", "War Hammer", "Bare Fists" };
            string[] rangeNames = { "Bow", "Blow Dart", "Throwing Knife", "AK47", "Crossbow" };
            string[] magicNames = { "Fireball", "Blizzard", "Water Jet", "Blinding Light", "Void Tear" };
            string[] wepType = { "null", "Melee", "Ranged", "Magic" };
            int currentphase;
            string choice;
            Random rand = new Random();
            int dam = 0;
            double rng = 0;
            int spd = 0;
            int kills = -1;

            //Creating Character
            Console.Write("What is your name: ");
            string name = Console.ReadLine();
            Console.Clear();
            int type;
            while (true)
            {
                Console.WriteLine("What kind of weapon do you have?");
                Console.WriteLine("1) Melee");
                Console.WriteLine("2) Ranged");
                Console.WriteLine("3) Magic");
                string mnu = Console.ReadLine();
                if (mnu == "1")
                {
                    //Melee
                    type = 1;
                    dam = rand.Next(20, 31);
                    rng = rand.Next(10, 21);
                    spd = rand.Next(5, 9);
                    break;
                }
                else if (mnu == "2")
                {
                    //Ranged
                    type = 2;
                    dam = rand.Next(20, 21);
                    rng = rand.Next(25, 36);
                    spd = rand.Next(6, 9);
                    break;
                }
                else if (mnu == "3")
                {
                    //Magic
                    type = 3;
                    dam = rand.Next(10, 21);
                    rng = rand.Next(20, 26);
                    spd = rand.Next(2, 5);
                    break;
                }
                else { }
            }
            Console.Clear();
            Console.Write("What is the name of your weapon: ");
            string wepname = Console.ReadLine();
            Person player = new Person(dam, rng, spd, type, name, wepname);
            Console.Clear();
            Person enemy = new Person(dam, rng, spd, type, name, wepname);
            //End Character Creation

            

            //LORE
            Console.WriteLine("A group of enemies approche in the distance and they are blocking your path.");
            Console.WriteLine("Looks like you will have to fight your way through!");
            Console.ReadLine();


            while (player.health > 0)
            {
                kills++;
                //Enemy Creation
                name = enemyNames[rand.Next(0, 5)];
                type = rand.Next(1, 4);
                if (type == 1)
                {
                    //Melee
                    type = 1;
                    dam = rand.Next(20, 31);
                    rng = rand.Next(10, 21);
                    spd = rand.Next(5, 9);
                    wepname = meleeNames[rand.Next(0, 5)];
                }
                else if (type == 2)
                {
                    //Ranged
                    type = 2;
                    dam = rand.Next(20, 21);
                    rng = rand.Next(25, 36);
                    spd = rand.Next(6, 9);
                    wepname = rangeNames[rand.Next(0, 5)];
                }
                else if (type == 3)
                {
                    //Magic
                    type = 3;
                    dam = rand.Next(10, 21);
                    rng = rand.Next(20, 26);
                    spd = rand.Next(2, 5);
                    wepname = magicNames[rand.Next(0, 5)];
                }
                else { }
                enemy.baseDamage = dam;
                enemy.attackRange = rng;
                enemy.speed = spd;
                enemy.weaponType = type;
                enemy.name = name;
                enemy.weaponName = wepname;
                enemy.health = 100;

                //End Enemy Creation

                //Set Positions
                player.position = 0;
                enemy.position = Convert.ToInt32(5 + (player.attackRange + enemy.attackRange) / 2);
                double distance = enemy.position - player.position;

                //Gameplay
                while (enemy.health > 0 && player.health > 0)
                {
                    currentphase = 0;
                    turn++;

                    //Players Turn

                    if (player.currentStance == 3)
                    {
                        Console.Clear();
                        Console.WriteLine(player.name + "'s Turn!");
                        Console.ReadKey();
                        Console.Clear();
                        dam = Attack(player, enemy, distance);
                        enemy.health -= dam;
                        HealthCheck(player, enemy);
                        Console.WriteLine(player.name + " unleashes their charged attack and deals " + dam + " damage to " + enemy.name + " with " + player.weaponName);
                        Console.ReadKey();
                        player.health -= ColateralCheck(player, enemy, dam);
                        currentphase = 2;
                        player.currentStance = 1;
                    }
                    else
                    {
                        Console.Clear();
                        player.currentStance = 1;
                        Console.WriteLine(player.name + "'s Turn!");
                        Console.ReadKey();
                        while (currentphase == 0 && enemy.health > 0 && player.health > 0)
                        {
                            Console.Clear();
                            Stats(player.speed, player.baseDamage, player.attackRange, player.health, player.name, wepType[player.weaponType]);
                            Stats(enemy.speed, enemy.baseDamage, enemy.attackRange, enemy.health, enemy.name, wepType[enemy.weaponType]);
                            Console.WriteLine("[Distance: " + distance + "] ~~~ [Kills: " + kills + "]");
                            menu(turn, phases[currentphase]);
                            choice = Console.ReadLine();

                            if (choice == "1")
                            {
                                Console.Clear();
                                dam = Attack(player, enemy, distance);
                                enemy.health -= dam;
                                HealthCheck(player, enemy);
                                Console.WriteLine(player.name + " deals " + dam + " damage to " + enemy.name + " with " + player.weaponName);
                                Console.ReadKey();
                                player.health -= ColateralCheck(player, enemy, dam);

                                currentphase = 2;
                            }
                            else if (choice == "2")
                            {
                                Console.Clear();
                                Block(player);
                                Console.ReadKey();
                                currentphase = 2;
                            }
                            else if (choice == "3")
                            {
                                Console.Clear();
                                Channel(player);
                                Console.ReadKey();
                                currentphase = 2;
                            }
                            else if (choice == "4")
                            {
                                Console.Clear();
                                MoveMenu(player);
                                distance = enemy.position - player.position;
                                Console.ReadKey();
                                currentphase = 2;
                            }
                            else { }
                        }
                    }

                    //Enemy's Turn
                    currentphase = 0;
                    Console.Clear();

                    //Channeled Attack
                    if (enemy.currentStance == 3 && enemy.health > 0 && player.health > 0)
                    {
                        Console.WriteLine(enemy.name + "'s Turn!");
                        Console.ReadKey();
                        Console.Clear();
                        dam = Attack(enemy, player, distance);
                        player.health -= dam;
                        HealthCheck(player, enemy);
                        Console.WriteLine(enemy.name + " unleashes their charged attack and deals " + dam + " damage to " + player.name + " with " + enemy.weaponName);
                        Console.ReadKey();
                        enemy.health -= ColateralCheck(enemy, player, dam);
                        currentphase = 2;
                        enemy.currentStance = 1;
                    }

                    //Enemy AI
                    else if (enemy.health > 0 && player.health > 0)
                    {
                        enemy.currentStance = 1;
                        Console.WriteLine(enemy.name + "'s Turn!");
                        Console.ReadKey();
                        Console.Clear();
                        if (enemy.weaponType == 2 && distance < 5)
                        {
                            enemy.position += move(enemy, 1);
                            distance = enemy.position - player.position;
                            Console.ReadKey();
                            currentphase = 2;
                        }
                        else if (enemy.attackRange < distance)
                        {
                            enemy.position += move(enemy, -1);
                            distance = enemy.position - player.position;
                            Console.ReadKey();
                            currentphase = 2;
                        }
                        else if (player.currentStance == 3)
                        {
                            if (enemy.health < player.health)
                            {
                                Block(enemy);
                                Console.ReadKey();
                                currentphase = 2;
                            }
                            else
                            {
                                EnemyAttack(enemy, player, distance, currentphase);
                            }
                        }
                        else if (enemy.health < player.health)
                        {
                            if (enemy.weaponType >= 2)
                            {
                                if (enemy.health < player.health * 0.6 && player.attackRange < distance)
                                {
                                    enemy.position += move(enemy, 1);
                                    distance = enemy.position - player.position;
                                    Console.ReadKey();
                                    currentphase = 2;
                                }
                                else
                                {
                                    if (rand.Next(1, 4) == 1)
                                    {
                                        enemy.position += move(enemy, 1);
                                        distance = enemy.position - player.position;
                                        Console.ReadKey();
                                        currentphase = 2;
                                    }
                                    else
                                    {
                                        EnemyAttack(enemy, player, distance, currentphase);
                                    }
                                }
                            }
                            else
                            {
                                if (rand.Next(1, 3) == 1)
                                {
                                    Channel(enemy);
                                    Console.ReadKey();
                                    currentphase = 1;
                                }
                                else
                                {
                                    EnemyAttack(enemy, player, distance, currentphase);
                                }
                            }
                        }
                        else
                        {
                            EnemyAttack(enemy, player, distance, currentphase);
                        }
                    }
                    else { }
                }
                if (enemy.health <= 0)
                {
                    Console.Clear();
                    Console.WriteLine(player.name + " has slain " + enemy.name);
                    player.health += 50;
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(enemy.name + " has slain " + player.name);
                    Console.ReadKey();
                }
            }
        }

        static void EnemyAttack(Person enemy, Person player, double distance, int currentphase)
        {
            int dam = Attack(enemy, player, distance);
            player.health -= dam;
            HealthCheck(player, enemy);
            Console.WriteLine(enemy.name + " deals " + dam + " damage to " + player.name + " with " + enemy.weaponName);
            Console.ReadKey();
            enemy.health -= ColateralCheck(enemy, player, dam);
            currentphase = 2;
        }

        static void Stats(int spd, int dam, double rng, int hp, string name, string wep)
        {
            Console.Write(name + " ");
            Console.Write("[Health: " + hp + "] ~~~ ");
            Console.Write("[Speed: " + spd + "] ~~~ ");
            Console.Write("[Attack Range: " + rng + "] ~~~ ");
            Console.Write("[Damage: " + dam + "] ~~~ ");
            Console.WriteLine("[Weapon Type: " + wep + "]");
        }

        static int move(Person attacker, int direction)
        {
            Random rand = new Random();
            int moved = direction * rand.Next(3, attacker.speed + 1);
            Console.WriteLine("Moved " + moved + " units!");
            return moved;
        }

        static void menu(int turn, string phase)
        {
            Console.WriteLine("Turn " + turn);
            Console.WriteLine();

            Console.WriteLine("1) Attack");
            Console.WriteLine("2) Block");
            Console.WriteLine("3) Channel");
            Console.WriteLine("4) Move");
        }

        static void MoveMenu(Person attacker)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Move");
                Console.WriteLine();
                Console.WriteLine("1) Advance");
                Console.WriteLine("2) Fall Back");
                string dir = Console.ReadLine();
                if (dir == "1")
                {
                    attacker.position += move(attacker, 1);
                    break;
                }
                if (dir == "2")
                {
                    attacker.position += move(attacker, -1);
                    break;
                }
                else { }
            }
        }

        static int Attack(Person attacker, Person defender, double distance)
        {
            int dam = attacker.Damage(distance);
            bool crit = attacker.CritChance(distance);
            bool hit = attacker.HitChance(distance);
            double CtD;
            double DfD;
            double ChD;
            double MeD;

            //Hit
            if (hit)
            {
                dam = attacker.Damage(distance);
            }
            else
            {
                dam = 0;
            }

            //Crit
            if (crit)
            {
                if(attacker.weaponType == 2)
                {
                    CtD = 3;
                }
                else
                {
                    CtD = 2;
                }
            }
            else
            {
                CtD = 1;
            }

            //Channel
            if (attacker.currentStance == 3)
            {
                if (attacker.weaponType == 3)
                {
                    ChD = 2;
                }
                else
                {
                    ChD = 1.5;
                }

            }
            else
            {
                ChD = 1;
            }

            //Block
            if (defender.currentStance == 2)
            {
                if (attacker.weaponType == 1)
                {
                    DfD = 0.3;
                }
                else
                {
                    DfD = 0.5;
                }
            }
            else
            {
                DfD = 1;
            }

            //Melee
            if (defender.weaponType == 1)
            {
                MeD = 0.75;
            }
            else
            {
                MeD = 1;
            }

            return Convert.ToInt32(dam * CtD * ChD * DfD * MeD);
        }

        static void Block(Person attacker)
        {
            attacker.currentStance = 2;
            if (attacker.weaponType == 3)
            {
                Console.WriteLine(attacker.name + " summons a strong reflective sheild");
            }
            else
            {
                Console.WriteLine(attacker.name + " summons a weak reflective sheild");
            }
            
        }

        static void Channel(Person attacker)
        {
            attacker.currentStance = 3;
            Console.WriteLine(attacker.name + " starts channeling a strong attack!");
        }

        static int ColateralCheck(Person attacker, Person defender, int damage)
        {
            Random rand = new Random();
            if (defender.currentStance == 2 && rand.Next(1, 3) == 1)
            {
                if(defender.weaponType == 3)
                {
                    Console.WriteLine(attacker.name + " Takes " + Convert.ToInt32(damage * 0.75) + " recoil damage from the defended attack");
                    Console.ReadKey();
                    return Convert.ToInt32(damage * 0.75);
                }
                else
                {
                    Console.WriteLine(attacker.name + " Takes " + Convert.ToInt32(damage * 0.5) + " recoil damage from the defended attack");
                    Console.ReadKey();
                    return Convert.ToInt32(damage * 0.5);
                }
                
            }
            else { return 0; }
        }

        static void HealthCheck(Person player,Person enemy)
        {
            Console.WriteLine(player.name + "'s Health: " + player.health);
            Console.WriteLine(enemy.name + "'s Health: " + enemy.health);
        }
    }
}
