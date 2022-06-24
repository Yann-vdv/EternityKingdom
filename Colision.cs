using System.Collections.Generic;
using System;
public class Colision
{
    private Coordonnee topLeftCorner { get; set; }
    private Coordonnee topRightCorner { get; set; }
    private Coordonnee bottomLeftCorner { get; set; }
    private Coordonnee bottomRightCorner { get; set; }
    private List<Enemy> inColision { get; set; } = new List<Enemy>();
    public Colision(int height, int witdh, Coordonnee co)
    {
        topLeftCorner = new Coordonnee(co.VectorLocation.X - witdh, co.VectorLocation.Y + height);
        topRightCorner = new Coordonnee(co.VectorLocation.X + witdh, co.VectorLocation.Y + height);
        bottomLeftCorner = new Coordonnee(co.VectorLocation.X - witdh, co.VectorLocation.Y - height);
        bottomRightCorner = new Coordonnee(co.VectorLocation.X + witdh, co.VectorLocation.Y - height);

    }
    private bool isOut(Coordonnee coord)
    {
        var res = false;
        if (topLeftCorner.Ishigher(coord) && !topLeftCorner.IsWider(coord))
        {
            res = true;
        }
        else
        {
            if (topRightCorner.Ishigher(coord) && topRightCorner.IsWider(coord))
            {
                res = true;
            }
            else
            {
                if (!bottomLeftCorner.Ishigher(coord) && !bottomLeftCorner.IsWider(coord))
                {
                    res = true;
                }
                else
                {
                    if (!bottomRightCorner.Ishigher(coord) && bottomLeftCorner.IsWider(coord))
                    {
                        res = true;
                    }
                }
            }
        }
        return res;
    }
    private bool isIn(Coordonnee coord)
    {
        var res = true;
        if (topLeftCorner.Ishigher(coord) && !topLeftCorner.IsWider(coord))
        {
            res = false;
        }
        else
        {
            if (topRightCorner.Ishigher(coord) && topRightCorner.IsWider(coord))
            {
                res = false;
            }
            else
            {
                if (!bottomLeftCorner.Ishigher(coord) && !bottomLeftCorner.IsWider(coord))
                {
                    res = false;
                }
                else
                {
                    if (!bottomRightCorner.Ishigher(coord) && bottomLeftCorner.IsWider(coord))
                    {
                        res = false;
                    }
                }
            }
        }
        return res;
    }
    public List<Enemy> WhoIn()
    {
        return inColision;
    }
    public void GoIn(Enemy entity)
    {
        Console.WriteLine("je suis dedans");
        inColision.Add(entity);
    }
    public void GoOut(Enemy entity)
    {
        Console.WriteLine("je suis sortie");
        inColision.Remove(entity);
    }
    public void UpdateColision(List<Enemy> allEntity)
    {
        foreach (Enemy entityIn in inColision)
        {
            switch (entityIn)
            {
                case (Enemy enemy):
                    if (isOut(enemy.Co))
                    {
                        GoOut(entityIn);
                    }
                    break;
            }
        }
        foreach (Enemy entityCurrent in allEntity)
        {
            switch (entityCurrent)
            {
                case (Enemy enemy):
                    if (isIn(enemy.Co))
                    {
                        GoIn(entityCurrent);
                    }
                    break;
            }
        }


    }
}
