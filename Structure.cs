using Microsoft.Xna.Framework.Graphics;

public class Structure : Entity
{
    public string name;
    public int hp;
    public int maxHp;
    public Texture2D texture;

    public Structure(string name, int hp, int maxHp, Texture2D texture)
    {
        this.name = name;
        this.hp = hp;
        this.maxHp = maxHp;
        this.texture = texture;
    }
}