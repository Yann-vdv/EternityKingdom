using Microsoft.Xna.Framework;
using System;
public class Coordonnee
{
    public Vector2 VectorLocation;
    public Coordonnee(float ux, float uy)
    {
        this.VectorLocation = new Vector2(ux, uy);
    }
    public Coordonnee()
    {
        this.VectorLocation = new Vector2(0, 0);
    }
    public bool Ishigher(Coordonnee coord)
    {
        try
        {
            return coord.VectorLocation.Y > VectorLocation.Y;
        }
        catch (InvalidCastException e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
    public bool IsWider(Coordonnee coord)
    {
        try
        {
            return coord.VectorLocation.X > VectorLocation.X;
        }
        catch (InvalidCastException e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}