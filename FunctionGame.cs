using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
public class FunctionGame
{
    public void SetSpawnRandom(GamePlayInfo gpf)
    {
        Random rnd = new Random();
        List<Coordonnee> l = new List<Coordonnee>();
        l.Add(new Coordonnee(-10, -10));
        l.Add(new Coordonnee(-10, 500));
        l.Add(new Coordonnee(500, 500));
        l.Add(new Coordonnee(500, -10));
        int indexRandom = rnd.Next(l.Count);
        SpawnEnemy(TypeMobs.Golem, (int)(rnd.NextDouble() * 10), gpf, l[indexRandom]);
        SpawnEnemy(TypeMobs.Golem, (int)(rnd.NextDouble() * 10), gpf, l[rnd.Next(l.Count)]);
        SpawnEnemy(TypeMobs.Golem, (int)(rnd.NextDouble() * 10), gpf, l[rnd.Next(l.Count)]);
        SpawnEnemy(TypeMobs.Golem, (int)(rnd.NextDouble() * 10), gpf, l[rnd.Next(l.Count)]);
    }
    public void SpawnEnemy(TypeMobs t, int q, GamePlayInfo gpf, Coordonnee c)
    {
        for (int i = 0; i < q; i++)
        {
            Enemy monstre = new Enemy(c, t);
            switch (t)
            {
                case (TypeMobs.Golem):
                    monstre.setTextures(TypeSprite.Walk_right, monstre.SetNameSprite(TypeSprite.Walk_right), "Golem01/Golem_01_Walking_right_001", 17);
                    monstre.setTextures(TypeSprite.Walk_left, monstre.SetNameSprite(TypeSprite.Walk_left), "Golem01/Golem_01_Walking_left_001", 17);
                    monstre.setTextures(TypeSprite.Attack_left, monstre.SetNameSprite(TypeSprite.Attack_left), "Golem01/Golem_01_Attacking_Left_000", 12);
                    monstre.setTextures(TypeSprite.Attack_right, monstre.SetNameSprite(TypeSprite.Attack_right), "Golem01/Golem_01_Attacking_Right_000", 12);
                    monstre.setTextures(TypeSprite.Dead_left, monstre.SetNameSprite(TypeSprite.Dead_left), "Golem01/Golem_01_Dying_Left_000", 15);
                    monstre.setTextures(TypeSprite.Dead_right, monstre.SetNameSprite(TypeSprite.Dead_right), "Golem01/Golem_01_Dying_Right_000", 15);
                    monstre.setTextures(TypeSprite.Idle_left, monstre.SetNameSprite(TypeSprite.Idle_left), "Golem01/Golem_01_Idle_Left_000", 12);
                    monstre.setTextures(TypeSprite.Idle_right, monstre.SetNameSprite(TypeSprite.Idle_right), "Golem01/Golem_01_Idle_Right_000", 12);
                    break;
            }

            monstre.PvMax = 20;
            monstre.Pv = 3;
            monstre.Damage = 2;
            monstre.SetTarget(gpf.J1);
            gpf.Entity.Add(monstre);
            c = new Coordonnee(c.VectorLocation.X + 50, c.VectorLocation.Y);
        }
    }
}