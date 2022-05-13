using Microsoft.Xna.Framework.Graphics;
public class Characters
{
    public Coordonnee co { get; set; }
    public Texture2D textureRightWalk { get; set; }
    public Texture2D textureLeftWalk { get; set; }
    public Texture2D textureRightAttack { get; set; }
    public Texture2D textureLeftAttack { get; set; }
    public int pvMax { get; set; }
    public int pv { get; set; }
    public int attack { get; set; }

    public Characters(Coordonnee c)
    {
        this.co = c;
    }
}