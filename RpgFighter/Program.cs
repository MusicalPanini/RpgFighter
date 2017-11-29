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
            Tuple<int, int, int> stats;
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
                    stats = StatCreation(20, 31, 10, 21, 6, 9);
                    break;
                }
                else if (mnu == "2")
                {
                    //Ranged
                    type = 2;
                    stats = StatCreation(20, 21, 25, 36, 4, 7);
                    break;
                }
                else if (mnu == "3")
                {
                    //Magic
                    type = 3;
                    stats = StatCreation(10, 21, 20, 26, 3, 5);
                    break;
                }
                else { }
            }
            Console.Clear();
            Console.Write("What is the name of your weapon: ");
            string wepname = Console.ReadLine();
            Person player = new Person(stats.Item1, stats.Item2, stats.Item3, type, name, wepname);
            Console.Clear();
            Person enemy = new Person(0, 0, 0, 1, "name", "weapon");
            //End Character Creation

            

            //LORE
            Console.WriteLine("A group of enemies approche in the distance and they are blocking your path.");
            Console.WriteLine("Looks like you will have to fight your way through!");
            Console.ReadLine();


            while (player.health > 0)
            {
                kills++;
                //Enemy Creation
                name = enemyNames[rand.Next(0, enemyNames.Length)];
                type = rand.Next(1, 4);
                if (type == 1)
                {
                    //Melee
                    type = 1;
                    stats = StatCreation(20, 31, 10, 21, 6, 9);
                    wepname = meleeNames[rand.Next(0, meleeNames.Length)];
                }
                else if (type == 2)
                {
                    //Ranged
                    type = 2;
                    stats = StatCreation(20, 26, 25, 36, 4, 7);
                    wepname = rangeNames[rand.Next(0, rangeNames.Length)];
                }
                else if (type == 3)
                {
                    //Magic
                    type = 3;
                    stats = StatCreation(10, 21, 20, 26, 3, 5);
                    wepname = magicNames[rand.Next(0, magicNames.Length)];
                }
                else { }
                enemy.baseDamage = stats.Item1;
                enemy.attackRange = stats.Item2;
                enemy.speed = stats.Item3;
                enemy.weaponType = type;
                enemy.name = name;
                enemy.weaponName = wepname;
                enemy.health = 100 + (10 * kills);

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

                    ///Players Turn

                    //Channeled Attack
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
                            Menu(turn, phases[currentphase]);
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
                                player.health += Block(player);
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
                                currentphase = 1;
                            }
                            else { }
                        }

                        while (currentphase == 1 && enemy.health > 0 && player.health > 0)
                        {
                            Console.Clear();
                            Stats(player.speed, player.baseDamage, player.attackRange, player.health, player.name, wepType[player.weaponType]);
                            Stats(enemy.speed, enemy.baseDamage, enemy.attackRange, enemy.health, enemy.name, wepType[enemy.weaponType]);
                            Console.WriteLine("[Distance: " + distance + "] ~~~ [Kills: " + kills + "]");
                            Menu(turn, phases[currentphase]);
                            choice = Console.ReadLine();

                            if (choice == "1")
                            {
                                Console.Clear();
                                dam = Attack(player, enemy, distance);
                                enemy.health -= Convert.ToInt32(dam * 0.75);
                                HealthCheck(player, enemy);
                                Console.WriteLine("While exhausted, " + player.name + " deals " + dam + " damage to " + enemy.name + " with " + player.weaponName);
                                Console.ReadKey();
                                player.health -= ColateralCheck(player, enemy, dam);

                                currentphase = 2;
                            }
                            else if (choice == "2")
                            {
                                Console.Clear();
                                player.health += Block(player);
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
                                Console.WriteLine("You are too exhausted to move");
                                Console.ReadKey();
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
                        while (currentphase == 0)
                        {
                            enemy.currentStance = 1;
                            Console.WriteLine(enemy.name + "'s Turn!");
                            Console.ReadKey();
                            Console.Clear();
                            if (enemy.weaponType == 2 && distance < 5)
                            {
                                //Attempt to leave close range
                                enemy.position += Move(enemy, 1);
                                distance = enemy.position - player.position;
                                Console.ReadKey();
                                currentphase = 1;
                            }
                            else if (enemy.attackRange < distance)
                            {
                                //Attempt to get in range of player
                                enemy.position += Move(enemy, -1);
                                distance = enemy.position - player.position;
                                Console.ReadKey();
                                currentphase = 1;
                            }
                            else if (player.currentStance == 3)
                            {
                                if (enemy.health < player.health)
                                {
                                    //Attempt to block channeled attack
                                    enemy.health += Block(enemy);
                                    Console.ReadKey();
                                    currentphase = 2;
                                }
                                else
                                {
                                    //Attempt to fight through channeled attack
                                    EnemyAttack(enemy, player, distance, currentphase);
                                    currentphase = 2;
                                }
                            }
                            else if (enemy.health < player.health)
                            {
                                if (enemy.weaponType >= 2)
                                {
                                    if (enemy.health < player.health * 0.6 && player.attackRange < distance)
                                    {
                                        //Attempt to back off of player becuase of much lower health than player
                                        enemy.position += Move(enemy, 1);
                                        distance = enemy.position - player.position;
                                        Console.ReadKey();
                                        currentphase = 1;
                                    }
                                    else
                                    {
                                        if (rand.Next(1, 4) == 1)
                                        {
                                            //RNG choose to back off of player becuase of slightly lower health than player
                                            enemy.position += Move(enemy, 1);
                                            distance = enemy.position - player.position;
                                            Console.ReadKey();
                                            currentphase = 1;
                                        }
                                        else
                                        {
                                            //RNG choose to attack player dispite lower health
                                            EnemyAttack(enemy, player, distance, currentphase);
                                            currentphase = 2;
                                        }
                                    }
                                }
                                else
                                {
                                    if (rand.Next(1, 3) == 1)
                                    {
                                        //RNG choose to channel
                                        Channel(enemy);
                                        Console.ReadKey();
                                        currentphase = 1;
                                    }
                                    else
                                    {
                                        //RNG choose to attack
                                        EnemyAttack(enemy, player, distance, currentphase);
                                        currentphase = 2;
                                    }
                                }
                            }
                            else if (enemy.weaponType !=2 && player.weaponType == 2 && distance > 5)
                            {
                                //Attempt to get within close range of Ranged player
                                enemy.position += Move(enemy, -1);
                                distance = enemy.position - player.position;
                                Console.ReadKey();
                                currentphase = 1;
                            }
                            else
                            {
                                //Attack
                                EnemyAttack(enemy, player, distance, currentphase);
                                currentphase = 2;
                            }
                        }

                        //Mid Phase
                        while (currentphase == 1 && player.health > 0 && enemy.health > 0)
                        {
                            Console.Clear();
                            if (player.currentStance == 3 && player.attackRange > distance)
                            {
                                if(enemy.health >= player.health && enemy.attackRange >= distance)
                                {
                                    //Attack
                                    EnemyAttack(enemy, player, distance, currentphase);
                                    currentphase = 2;
                                }
                                else
                                {
                                    enemy.health += Block(enemy);
                                    Console.ReadKey();
                                    currentphase = 2;
                                }
                            }
                            else if (enemy.attackRange < distance)
                            {
                                enemy.health += Block(enemy);
                                Console.ReadKey();
                                currentphase = 2;
                            }
                            else if (enemy.health < player.health)
                            {
                                if (rand.Next(1, 3) == 1)
                                {
                                    Channel(enemy);
                                    Console.ReadKey();
                                    currentphase = 2;
                                }
                                else
                                {
                                    EnemyAttack(enemy, player, distance, currentphase);
                                    currentphase = 2;
                                }
                            }
                            else
                            {
                                EnemyAttack(enemy, player, distance, currentphase);
                                currentphase = 2;
                            }
                        }
                    }
                    else { }
                }
                if (enemy.health <= 0)
                {
                    Console.Clear();

                    dam = rand.Next(1, 4);
                    rng = rand.Next(0, 3);
                    spd = rand.Next(0, 2);
                    player.baseDamage += dam;
                    player.attackRange += rng;
                    player.speed += spd;
                    Console.WriteLine(player.name + " has slain " + enemy.name);
                    Console.WriteLine("They gain 50 health");
                    Console.WriteLine(dam + " damage");
                    Console.WriteLine(rng + " range");
                    Console.WriteLine(spd + " speed");
                    Console.WriteLine("A sense of accomplishment");
                    Console.WriteLine("and blood on their hands!");
                    player.health += 50;
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(enemy.name + " has slain " + player.name);
                    Console.WriteLine("Game Over");
                    Console.ReadKey();
                }
            }
        }

        static void EnemyAttack(Person enemy, Person player, double distance, int currentphase)
        {
            int dam = Attack(enemy, player, distance);
            player.health -= dam;
            HealthCheck(player, enemy);
            if(currentphase == 0)
            {
                Console.WriteLine(enemy.name + " deals " + dam + " damage to " + player.name + " with " + enemy.weaponName);
            }
            else
            {
                Console.WriteLine("While exhausted, " + enemy.name + " deals " + dam + " damage to " + player.name + " with " + enemy.weaponName);
            }
            Console.ReadKey();
            enemy.health -= ColateralCheck(enemy, player, dam);
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

        static Tuple<int, int, int> StatCreation(int dam1, int dam2, int rng1, int rng2, int spd1, int spd2)
        {
            Random rand = new Random();
            return Tuple.Create(rand.Next(dam1, dam2), rand.Next(rng1, rng2), rand.Next(spd1, spd2));
        }

        static int Move(Person attacker, int direction)
        {
            Random rand = new Random();
            int moved = direction * rand.Next(3, attacker.speed + 1);
            Console.WriteLine("Moved " + moved + " units!");
            return moved;
        }

        static void Menu(int turn, string phase)
        {
            Console.WriteLine("[Turn " + turn + "] [" + phase + "]");
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
                    attacker.position += Move(attacker, 1);
                    break;
                }
                if (dir == "2")
                {
                    attacker.position += Move(attacker, -1);
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
                    CtD = 2.5;
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
                    ChD = 2.25;
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
                MeD = 0.8;
            }
            else
            {
                MeD = 1;
            }

            return Convert.ToInt32(dam * CtD * ChD * DfD * MeD);
        }

        static int Block(Person attacker)
        {
            attacker.currentStance = 2;
            if (attacker.weaponType == 3)
            {
                Console.WriteLine(attacker.name + " summons a strong reflective sheild and gains 10 health");
                return 10;
            }
            else if (attacker.weaponType == 1)
            {
                Console.WriteLine(attacker.name + " summons a resistant reflective sheild");
                return 0;
            }
            else
            {
                Console.WriteLine(attacker.name + " summons a weak reflective sheild and gains 5 health");
                return 5;
            }
            
        }

        static void Channel(Person attacker)
        {
            attacker.currentStance = 3;
            if (attacker.weaponType == 3)
            {
                Console.WriteLine(attacker.name + " starts to gather magic from around them");
            }
            else if (attacker.weaponType == 1)
            {
                Console.WriteLine(attacker.name + " prepares a lethal strike");
            }
            else
            {
                Console.WriteLine(attacker.name + " takes a deep breath and focuses");
            }
        }

        static int ColateralCheck(Person attacker, Person defender, int damage)
        {
            Random rand = new Random();
            if (defender.currentStance == 2 && rand.Next(1, 3) == 1)
            {
                if(defender.weaponType == 3)
                {
                    Console.WriteLine(attacker.name + " takes " + Convert.ToInt32(damage * 0.75) + " recoil damage from the reflected attack");
                    Console.ReadKey();
                    return Convert.ToInt32(damage * 0.75);
                }
                else
                {
                    Console.WriteLine(attacker.name + " takes " + Convert.ToInt32(damage * 0.5) + " recoil damage from the reflected attack");
                    Console.ReadKey();
                    return Convert.ToInt32(damage * 0.5);
                }
                
            }
            else { return 0; }
        }

        static void HealthCheck(Person player,Person enemy)
        {
            Console.WriteLine(player.name + " [Health: " + player.health + "]");
            Console.WriteLine(enemy.name + " [Health: " + enemy.health + "]");
        }
    }
}
