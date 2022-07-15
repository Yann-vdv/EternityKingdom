using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class RoundedButton
{
    #region Fields
    private MouseState _currentMousse;
    private SpriteFont _font;
    private bool _isHovering;
    private MouseState _previousMousse;
    private Texture2D _texture;
    #endregion

    #region Properties
    public Color PenColor { get; set; }
    public Vector2 Position { get; set; }
    public Rectangle Rectangle
    {
        get
        {
            return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
        }
    }
    public string Text { get; set; }
    public gameState _newGameState { get; set; }
    #endregion

    public RoundedButton(Texture2D texture, SpriteFont font)
    {
        _texture = texture;
        _font = font;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var color = Color.White;

        if (_isHovering)
            color = Color.Gray;

        spriteBatch.Draw(_texture, Rectangle, color);

        if (!string.IsNullOrEmpty(Text))
        {
            var x = (Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(Text).X / 2));
            var y = (Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(Text).Y / 2));

            spriteBatch.DrawString(_font, Text, new Vector2(x, y), Color.Black);
        }
    }

    public void Update(GameTime gameTime)
    {
        _previousMousse = _currentMousse;
        _currentMousse = Mouse.GetState();

        var mouseRectangle = new Rectangle(_currentMousse.X, _currentMousse.Y, 1, 1);

        _isHovering = false;

        if (mouseRectangle.Intersects(Rectangle))
        {
            _isHovering = true;
        }
    }

    public bool Clicked()
    {
        if (_isHovering && _currentMousse.LeftButton == ButtonState.Released && _previousMousse.LeftButton == ButtonState.Pressed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}