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
        private List<Sprite> _Players;
        private gameState gs = gameState.gamePlay;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            // _graphics.PreferredBackBufferWidth = 1920;
            // _graphics.PreferredBackBufferHeight = 1080;
            // _graphics.IsFullScreen = true;
            // _graphics.ApplyChanges();
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

            var Player1 = new Dictionary<string, Animation>()
            {
                { "Walk_right", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Walking_right_001"), 17 ) },
                { "Walk_left", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Walking_left_001"), 17 ) },
                { "Attack_left", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Attacking_Left_000"), 12 ) },
                { "Attack_right", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Attacking_Right_000"), 12 ) },
                { "Dead_left", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Dying_Left_000"), 15 ) },
                { "Dead_right", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Dying_Right_000"), 15 ) },
                { "Idle_left", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Idle_Left_000"), 12 ) },
                { "Idle_right", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Idle_Right_000"), 12 ) },
            };
            // var animations2 = new Dictionary<string, Animation>()
            // {
            //     { "Walk_right", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Walking_right_001"), 17 ) },
            //     { "Walk_left", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Walking_left_001"), 17 ) },
            //     { "Attack_left", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Attacking_Left_000"), 12 ) },
            //     { "Attack_right", new Animation(Content.Load<Texture2D>("Golem01/Golem_01_Attacking_Right_000"), 12 ) },
            // };

            _Players = new List<Sprite>()
            {
                new Sprite(Player1)
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
                },
                // new Sprite(animations2)
                // {
                //     Position = new Vector2(200,100),
                //     Input = new input()
                //     {
                //         Up = Keys.Up,
                //         Down = Keys.Down,
                //         Left = Keys.Left,
                //         Right = Keys.Right,
                //         Space = Keys.RightShift,
                //     },
                // },
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

            foreach (var sprite in _Players)
                sprite.Update(gameTime, _Players);

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

            foreach (var sprite in _Players)
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
