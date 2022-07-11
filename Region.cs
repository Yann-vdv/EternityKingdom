using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class Region
{
    public Vector2 coo;
    public Structure structure;

    public Region(Vector2 coo)
    {
        this.coo = coo;
    }

    public void SetStructure(Structure structure)
    {
        this.structure = structure;
    }
}