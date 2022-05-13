using System;
using System.Collections.Generic;
public class FunctionGame
{
    public FunctionGame()
    {
    }
    public void SpawnEnemy(TypeMobs t, int q, GamePlayInfo gpf)
    {
        for (int i = 0; i < q; i++)
        {
            Enemy monstre = new Enemy(new Coordonnee(i + 5, 0), t);
            switch (t)
            {
                case (TypeMobs.Golem):

                    break;
            }

            monstre.pvMax = 20;
            monstre.pv = 20;
            monstre.attack = 2;
            gpf.enemys.Add(monstre);
        }
    }
}