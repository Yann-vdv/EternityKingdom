using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
public class GamePlayInfo
{
    public List<Enemy> enemys;

    public Joueur j1;
    public Texture2D _GameBackground;
    public GamePlayInfo()
    {
        this.j1 = new Joueur("inconnue", new Coordonnee());
        this._GameBackground = null;
        this.enemys = new List<Enemy>();
    }
}