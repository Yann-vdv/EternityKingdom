using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using projet.Sprites;
using projet.Models;
namespace projet
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private FunctionGame _FunctionGame = new FunctionGame();
        private GamePlayInfo _GamePlayInfo = new GamePlayInfo();
        private List<Sprite> _sprites;
        private gameState gs = gameState.gamePlay;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 500;
            _graphics.PreferredBackBufferHeight = 400;
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _FunctionGame.SpawnEnemy(TypeMobs.Golem, 10, _GamePlayInfo);
            _GamePlayInfo._GameBackground = Content.Load<Texture2D>("grass");
            // _GamePlayInfo.j1.t2d = Content.Load<Texture2D>("Golem01/Golem_01_Walking_right_001");

            var animations = new Dictionary<string, Animation>()
            {
                { "Walk_right", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Walking_right_001"), 17 ) },
                { "Walk_left", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Walking_left_001"), 17 ) },
                { "Attack_left", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Attacking_Left_000"), 12 ) },
                { "Attack_right", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Attacking_Right_000"), 12 ) },
            };

            _sprites = new List<Sprite>()
            {
                new Sprite(animations)
                {
                    Position = new Vector2(100,100),
                    Input = new input()
                    {
                        Up = Keys.Z,
                        Down = Keys.S,
                        Left = Keys.Q,
                        Right = Keys.D,
                        Space = Keys.Space,
                    },
                }
            };

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //     Exit();

            // if (Keyboard.GetState().IsKeyDown(Keys.Up))
            // {
            //     _GamePlayInfo.j1.co.y -= 1;
            // }
            // if (Keyboard.GetState().IsKeyDown(Keys.Down))
            // {
            //     _GamePlayInfo.j1.co.y += 1;
            // }
            // if (Keyboard.GetState().IsKeyDown(Keys.Left))
            // {
            //     _GamePlayInfo.j1.co.x -= 1;
            // }
            // if (Keyboard.GetState().IsKeyDown(Keys.Right))
            // {
            //     _GamePlayInfo.j1.co.x += 1;
            // }

            foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            switch (gs)
            {
                case gameState.mainMenu:
                    DrawMainMenu();
                    break;
                case gameState.gamePlay:
                    DrawGamePlay();
                    break;
                case gameState.gameOver:
                    DrawGameOver();
                    break;
            }

            base.Draw(gameTime);
        }

        void DrawGamePlay()
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_GamePlayInfo._GameBackground, new Vector2(0, 0), Color.White);
            //_spriteBatch.Draw(_GamePlayInfo.j1.t2d, new Vector2(_GamePlayInfo.j1.co.x, _GamePlayInfo.j1.co.y), Color.White);
            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);
            _spriteBatch.End();
        }

        void DrawMainMenu()
        {
            _spriteBatch.Begin();

            _spriteBatch.End();
        }

        void DrawGameOver()
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_GamePlayInfo._GameBackground, new Vector2(0, 0), Color.White);
            _spriteBatch.End();
        }
    }
}
