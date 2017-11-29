using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgFighter
{
    public class Person
    {
        Random rand = new Random();
        public int baseDamage;
        public double attackRange;
        public int speed;
        public double position;
        public int health = 100;
        public string name;
        public string weaponName;
        //1 = Melee 2 = Ranged 3 = Magic
        public int weaponType;
        //1 = Attack 2 = Block 3 = Channel
        public int currentStance = 1;

        public int Damage(double distance)
        {
            //Melee
            if(weaponType == 1)
            {
                if (Math.Sqrt(Math.Pow(distance, 2)) < attackRange * 0.5)
                {
                    return Convert.ToInt32(baseDamage * 1.2);
                }
                else
                {
                    return baseDamage;
                }
            }
            //Ranged
            else if (weaponType == 2)
            {
                if (Math.Sqrt(Math.Pow(distance, 2)) >= 5 && Math.Sqrt(Math.Pow(distance, 2)) <= attackRange * 0.5)
                {
                    return baseDamage;
                }
                else
                {
                    return Convert.ToInt32(baseDamage * ((rand.Next(30, 101) * 0.01)));
                }
            }
            //Magic
            else if (weaponType == 3)
            {
                if (Math.Sqrt(Math.Pow(distance, 2)) > attackRange)
                {
                    return 0;
                }
                else
                {
                    return baseDamage;
                }
            }
            else
            {
                return 1;
            }
        }

        public bool CritChance(double distance)
        {
            int crit = rand.Next(1, 101);
            //Melee
            if (weaponType == 1)
            {
                if(crit <= 5)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //Ranged
            else if (weaponType == 2)
            {
                if (currentStance == 3)
                {
                    if (crit <= 25)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (Math.Sqrt(Math.Pow(distance, 2)) >= 5 && Math.Sqrt(Math.Pow(distance, 2)) <= attackRange * 0.5)
                {
                    if (crit <= 15)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (Math.Sqrt(Math.Pow(distance, 2)) > 5 && Math.Sqrt(Math.Pow(distance, 2)) > attackRange * 0.75)
                {
                    if (crit <= 5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            //Magic
            else if (weaponType == 3)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        public bool HitChance(double distance)
        {
            int hit = rand.Next(1, 101);
            //Melee
            if (weaponType == 1)
            {
                if (Math.Sqrt(Math.Pow(distance, 2)) > attackRange)
                {
                    return false;
                }
                else if (Math.Sqrt(Math.Pow(distance, 2)) < attackRange * 0.3)
                {
                    return true;
                }
                else if (Math.Sqrt(Math.Pow(distance, 2)) > attackRange * 0.8)
                {
                    if (hit <= 70)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (hit <= 85)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            //Ranged
            else if (weaponType == 2)
            {
                if (Math.Sqrt(Math.Pow(distance, 2)) > attackRange)
                {
                    if (hit <= 75 - (2 * attackRange - Math.Sqrt(Math.Pow(distance, 2))))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (Math.Sqrt(Math.Pow(distance, 2)) < 5)
                {
                    return false;
                }
                else
                {
                    if (hit <= 80)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            //Magic
            else if (weaponType == 3)
            {
                if (Math.Sqrt(Math.Pow(distance, 2)) > attackRange)
                {
                    return false;
                }
                else if (health < 75 && health >= 50)
                {
                    if (hit <= 80)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (health < 50)
                {
                    if (hit <= 60)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public Person(int basedamage, double atkrange, int spd, int weptype, string nm, string wepnm)
        {
            baseDamage = basedamage;
            attackRange = atkrange;
            speed = spd;
            weaponType = weptype;
            name = nm;
            weaponName = wepnm;
        }
    }
}
