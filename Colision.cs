using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using projet.Models;
public class Colision
{
    private Coordonnee topLeftCorner { get; set; }
    private Coordonnee topRightCorner { get; set; }
    private Coordonnee bottomLeftCorner { get; set; }
    private Coordonnee bottomRightCorner { get; set; }
    public Texture2D Texture { get; set; }

    private List<Enemy> inColision { get; set; } = new List<Enemy>();
    private int selfHeight { get; set; }
    private int selfWitdh { get; set; }

    public bool IsHere(Enemy e)
    {
        return inColision.Contains(e);
    }
    public Colision(int height, int witdh, Joueur j)
    {
        topLeftCorner = new Coordonnee(j.Co.VectorLocation.X - witdh, j.Co.VectorLocation.Y + height);
        topRightCorner = new Coordonnee(j.Co.VectorLocation.X + witdh, j.Co.VectorLocation.Y + height);
        bottomLeftCorner = new Coordonnee(j.Co.VectorLocation.X - witdh, j.Co.VectorLocation.Y - height);
        bottomRightCorner = new Coordonnee(j.Co.VectorLocation.X + witdh, j.Co.VectorLocation.Y - height);
        selfWitdh = witdh;
        selfHeight = height;
    }
    public void UpdateSelfCo(Joueur j)
    {
        topLeftCorner = new Coordonnee(j.Co.VectorLocation.X - selfWitdh, j.Co.VectorLocation.Y + selfHeight);
        topRightCorner = new Coordonnee(j.Co.VectorLocation.X + selfWitdh, j.Co.VectorLocation.Y + selfHeight);
        bottomLeftCorner = new Coordonnee(j.Co.VectorLocation.X - selfWitdh, j.Co.VectorLocation.Y - selfHeight);
        bottomRightCorner = new Coordonnee(j.Co.VectorLocation.X + selfWitdh, j.Co.VectorLocation.Y - selfHeight);
    }
    private bool isOut(Coordonnee coord)
    {
        var res = false;
        if (topLeftCorner.Ishigher(coord) || !topLeftCorner.IsWider(coord))
        {
            //  Console.WriteLine("1");
            res = true;
        }
        else
        {
            if (topRightCorner.Ishigher(coord) || topRightCorner.IsWider(coord))
            {
                // Console.WriteLine("2");
                res = true;
            }
            else
            {
                if (!bottomLeftCorner.Ishigher(coord) || !bottomLeftCorner.IsWider(coord))
                {
                    //  Console.WriteLine("3");
                    res = true;
                }
                else
                {
                    if (!bottomRightCorner.Ishigher(coord) || !bottomLeftCorner.IsWider(coord))
                    {
                        //  Console.WriteLine("4");
                        res = true;
                    }
                }
            }
        }
        return res;
    }
    private bool isIn(Coordonnee coord)
    {
        var res = false;
        if (!topLeftCorner.Ishigher(coord) &&
            topLeftCorner.IsWider(coord) &&
            !topRightCorner.Ishigher(coord) &&
            !topRightCorner.IsWider(coord) &&
            bottomLeftCorner.Ishigher(coord) &&
            bottomLeftCorner.IsWider(coord) &&
            bottomRightCorner.Ishigher(coord) &&
            !bottomRightCorner.IsWider(coord)
            )
        {
            //   Console.WriteLine("in");
            res = true;
        }
        return res;
    }
    public List<Enemy> WhoIn()
    {
        return inColision;
    }
    public void GoIn(Enemy entity)
    {
        inColision.Add(entity);
    }
    public void GoOut(Enemy entity)
    {
        inColision.Remove(entity);
    }
    public void UpdateColision(List<Enemy> allEntity)
    {
        if (inColision.Count != 0)
        {
            foreach (Enemy e in inColision)
            {
                Console.WriteLine(e.Id);
            }
        }
        try
        {
            List<Enemy> aSup = new List<Enemy>();
            foreach (Enemy entityIn in inColision)
            {
                switch (entityIn)
                {
                    case (Enemy enemy):
                        if (isOut(enemy.Co))
                        {
                            //  Console.WriteLine("je suis sortie");
                            aSup.Add(entityIn);

                        }
                        break;
                }
            }
            foreach (Enemy entityInsup in aSup)
            {
                switch (entityInsup)
                {
                    case (Enemy enemy):
                        GoOut(entityInsup);
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
                            bool exist = false;
                            foreach (Enemy entity in inColision)
                            {
                                if (entity == entityCurrent)
                                {
                                    exist = true;
                                }
                            }
                            if (!exist)
                            {
                                //  Console.WriteLine("je suis rentrer");
                                GoIn(entityCurrent);
                            }
                        }
                        break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }


    }
}
