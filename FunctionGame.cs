using System;
using System.Collections.Generic;
public class FunctionGame
{
    public FunctionGame()
    {
    }
    public void SpawnEnemy(TypeMobs t, int q, GamePlayInfo gpf)
    {
        var lastCoo = new Coordonnee(0, 0);
        for (int i = 0; i < q; i++)
        {
            Enemy monstre = new Enemy(lastCoo, t);
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
            monstre.Pv = 20;
            monstre.Attack = 2;
            //gpf.Entity.Add(monstre);
            gpf.Enemys.Add(monstre);
            lastCoo = new Coordonnee(lastCoo.VectorLocation.X + 100, lastCoo.VectorLocation.Y);
        }
    }
}