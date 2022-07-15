using System;
public class Entity
{
    public Coordonnee Co { get; set; }
    public int PvMax { get; set; } = 5;

    public bool IsDead { get; set; } = false;

    public int Pv { get; set; } = 2;

    public int Damage { get; set; } = 1;
    public void TakeDamage(Entity c, int damage, GamePlayInfo gpi)
    {
        int s = 0;
        if (c is Joueur)
        {
            if (gpi.J1.Pv - damage > 0)
            {
                Console.WriteLine("j'ai mal");
                gpi.J1.Pv = gpi.J1.Pv - damage;
            }
            else
            {
                gpi.J1.Pv = 0;
                gpi.J1.IsDead = true;
                Console.WriteLine("mort");
            }
        }
        else
        {
            if (c.Pv - damage > 0)
            {
                c.Pv = c.Pv - damage;
                gpi.Score += 10;
                s = 100;
            }
            else
            {

                c.Pv = 0;
                c.IsDead = true;

                gpi.Score += s;
                //    Console.WriteLine("détruit");
                s = 0;
            }
        }
    }
    public void TakeDamageE(Entity c, int damage)
    {
        Console.WriteLine("j'ai mal");
        if (c.Pv - damage > 0)
        {
            c.Pv = c.Pv - damage;

        }
        else
        {
            c.Pv = 0;
            c.IsDead = true;
            Console.WriteLine("détruit");
        }
    }
    public void Attack(Characters Current, GamePlayInfo gpi)
    {
        switch (Current)
        {
            case (Joueur):
                foreach (Entity enemy in Current.Colision.WhoIn())
                {
                    if (enemy is Enemy)
                    {
                        Enemy e = (Enemy)enemy;
                        // Console.WriteLine("pv= " + e.Pv);
                        TakeDamage(e, Current.Damage, gpi);
                    }
                }
                break;
            case (Enemy):
                //   Console.WriteLine("je suis un monstre");

                TakeDamage(gpi.J1, Current.Damage, gpi);
                /*  else if (enemy is Structure)
                  {
                      Structure e = (Structure)enemy;
                      //Console.WriteLine("pv= " + e.Pv);
                      TakeDamageE(e, Current.Damage);
                  }*/

                break;
        }
    }
}