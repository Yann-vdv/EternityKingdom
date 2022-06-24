using Microsoft.Xna.Framework.Graphics;
public class Joueur : Characters
{
    public int points { get; set; }
    public string userName { get; set; }
    public Joueur(string nom, Coordonnee coordonne) : base(coordonne)
    {
        this.userName = nom;
        this.points = 0;
    }
}