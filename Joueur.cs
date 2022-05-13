using Microsoft.Xna.Framework.Graphics;
public class Joueur : Characters
{
    public int points { get; set; }
    public string userName { get; set; }
    public Joueur(string n, Coordonnee c) : base(c)
    {
        this.userName = n;
        this.points = 0;
    }
}