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

    private List<Entity> inColision { get; set; } = new List<Entity>();
    private int selfHeight { get; set; }
    private int selfWitdh { get; set; }

    public bool IsHere(Entity e)
    {
        return inColision.Contains(e);
    }
    public Colision(int height, int witdh, Characters j)
    {
        topLeftCorner = new Coordonnee(j.Co.VectorLocation.X - witdh, j.Co.VectorLocation.Y + height);
        topRightCorner = new Coordonnee(j.Co.VectorLocation.X + witdh, j.Co.VectorLocation.Y + height);
        bottomLeftCorner = new Coordonnee(j.Co.VectorLocation.X - witdh, j.Co.VectorLocation.Y - height);
        bottomRightCorner = new Coordonnee(j.Co.VectorLocation.X + witdh, j.Co.VectorLocation.Y - height);
        selfWitdh = witdh;
        selfHeight = height;
    }
    public void UpdateSelfCo(Characters j)
    {
        topLeftCorner = new Coordonnee(j.Co.VectorLocation.X - selfWitdh, j.Co.VectorLocation.Y + selfHeight);
        topRightCorner = new Coordonnee(j.Co.VectorLocation.X + selfWitdh, j.Co.VectorLocation.Y + selfHeight);
        bottomLeftCorner = new Coordonnee(j.Co.VectorLocation.X - selfWitdh, j.Co.VectorLocation.Y - selfHeight);
        bottomRightCorner = new Coordonnee(j.Co.VectorLocation.X + selfWitdh, j.Co.VectorLocation.Y - selfHeight);
    }
    public bool IsOut(Coordonnee coord)
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
    public bool IsIn(Coordonnee coord)
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
    public List<Entity> WhoIn()
    {
        return inColision;
    }
    public void GoIn(Entity entity)
    {
        inColision.Add(entity);
    }
    public void GoOut(Entity entity)
    {
        inColision.Remove(entity);
    }
    public void UpdateColision(List<Entity> allEntity)
    {

        List<Entity> aSup = new List<Entity>();
        foreach (Entity entityIn in inColision)
        {
            switch (entityIn)
            {
                case (Enemy enemy):
                    if (IsOut(enemy.Co))
                    {
                        //  Console.WriteLine("je suis sortie");
                        aSup.Add(enemy);

                    }
                    break;
                case (Joueur j):
                    if (IsOut(j.Co))
                    {
                        //  Console.WriteLine("je suis sortie");
                        aSup.Add(j);

                    }
                    break;
                case (Structure s):
                    if (IsOut(s.Co))
                    {
                        aSup.Add(s);
                    }
                    break;
                    //    default:
                    //        Console.WriteLine(entityIn);
                    //       break;
            }
        }
        foreach (Entity entityInsup in aSup)
        {
            switch (entityInsup)
            {
                case (Enemy enemy):
                    GoOut(enemy);
                    break;
                case (Joueur j):
                    GoOut(j);
                    break;
                case (Structure s):
                    GoOut(s);
                    break;
                    //      default:
                    //          Console.WriteLine(entityInsup);
                    //         break;
            }
        }
        foreach (Entity entityCurrent in allEntity)
        {
            switch (entityCurrent)
            {
                case (Enemy enemy):
                    if (IsIn(enemy.Co))
                    {
                        bool exist = false;
                        foreach (Entity entity in inColision)
                        {
                            if (entity is Enemy)
                            {
                                if (entity == entityCurrent)
                                {
                                    exist = true;
                                }
                            }
                        }
                        if (!exist)
                        {
                            //  Console.WriteLine("je suis rentrer");
                            GoIn(entityCurrent);
                        }
                    }
                    break;
                case (Joueur j):
                    if (IsIn(j.Co))
                    {
                        bool existjouer = false;
                        foreach (Entity entity in inColision)
                        {
                            if (entity is Joueur)
                            {
                                if (entity == j)
                                {
                                    existjouer = true;
                                }
                            }
                        }
                        if (!existjouer)
                        {
                            //  Console.WriteLine("je suis rentrer");
                            GoIn(j);
                        }
                    }
                    break;
                    /*    case (Structure r):
                            if (r.Type != TypeStructure.castle)
                            {
                                if (IsIn(r.Co))
                                {
                                    bool existStructure = false;
                                    foreach (Entity entity in inColision)
                                    {
                                        if (entity is Structure)
                                        {
                                            if (entity == r)
                                            {
                                                existStructure = true;
                                            }
                                        }
                                    }
                                    if (!existStructure)
                                    {
                                        //  Console.WriteLine("je suis rentrer");

                                        GoIn(r);


                                    }
                                }
                            }
                            break;*/
                    //        default:
                    //             Console.WriteLine(entityCurrent);
                    //                break;
            }
        }



    }
}
