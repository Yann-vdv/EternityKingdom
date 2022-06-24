using Microsoft.Xna.Framework;
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
        return coord.VectorLocation.Y > VectorLocation.Y;
    }
    public bool IsWider(Coordonnee coord)
    {
        return coord.VectorLocation.X > VectorLocation.X;
    }
}