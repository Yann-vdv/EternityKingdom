using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
public class Region : Entity
{
    public Structure Structure;
    public Region(Coordonnee coo, Structure structure)
    {
        this.Co = coo;
        this.Structure = structure;
        this.Structure.Co = coo;
    }
    public bool clickState = false;

    public void setStructure(Structure structure)
    {
        this.Structure = structure;
        this.Structure.Co = this.Co;
    }
    public bool enterButton()
    {
        var mouseState = Mouse.GetState();
        var mousePosition = new Point(mouseState.X, mouseState.Y);

        if (mousePosition.X < Co.VectorLocation.X + 75 &&
            mousePosition.X > Co.VectorLocation.X &&
            mousePosition.Y < Co.VectorLocation.Y + 75 &&
            mousePosition.Y > Co.VectorLocation.Y)
        {
            return true;
        }
        clickState = false;
        return false;
    }

    public void Update(GameTime gameTime)
    {
        if (Structure.Type == TypeStructure.plain)
        {
            var mouseState = Mouse.GetState();
            if (enterButton())
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    clickState = true;
                }
                if (clickState && mouseState.LeftButton == ButtonState.Released)
                {
                    // Console.WriteLine("click on : " + Co.VectorLocation.X + ", " + Co.VectorLocation.Y);
                    // Structure.Co = this.Co;
                    Structure.Type = TypeStructure.steack;

                    clickState = false;
                }
            }
        }
    }
}