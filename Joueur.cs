using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
public class Joueur : Characters
{
    public int points { get; set; }
    public string userName { get; set; }
    public Joueur(string nom, Coordonnee coordonne) : base(coordonne)
    {
        this.Colision = new Colision(100, 50, this);
        this.userName = nom;
        this.points = 0;
    }

}