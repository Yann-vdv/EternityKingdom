using Microsoft.Xna.Framework.Graphics;
public class Structure : Entity
{
    public TypeStructure Type;
    public Texture2D Texture;
    public int OriginalWidth;
    public int OriginalHeight;

    public Structure(TypeStructure type, int hp, int maxHp, Texture2D texture, int originalWidth, int originalHeight, Coordonnee co)
    {
        this.Pv = hp;
        this.PvMax = maxHp;
        this.Texture = texture;
        this.Type = type;
        this.OriginalWidth = originalWidth;
        this.OriginalHeight = originalHeight;
        this.Co = co;
    }
    public Structure(TypeStructure type, int hp, int maxHp, Texture2D texture, int originalWidth, int originalHeight)
    {
        this.Pv = hp;
        this.PvMax = maxHp;
        this.Texture = texture;
        this.Type = type;
        this.OriginalWidth = originalWidth;
        this.OriginalHeight = originalHeight;
    }
}