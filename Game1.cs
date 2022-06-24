using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using projet.Sprites;
using projet.Models;
using System;
namespace projet
{
    public class Game1 : Game
    {
        // a voir pour l'incorporer car faut le faire
        //   https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/query-syntax-and-method-syntax-in-linq

        //  sa c bien pour match des class avec enfant parents
        // https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private FunctionGame _FunctionGame = new FunctionGame();
        private GamePlayInfo _GamePlayInfo = new GamePlayInfo();
        private List<Sprite> _SpritePlayers;
        private Joueur _player;
        private List<SpriteMob> _SpriteEnemys;
        private gameState gs = gameState.gamePlay;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
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
            _player = _GamePlayInfo.J1;
            var animTemp = new Dictionary<string, Animation>();
            // à optimiser plus tard on peux charger plu
            foreach (var t in _player.Textures)
            {
                animTemp.Add(t.Value.NomSprite, new Animation(Content.Load<Texture2D>(t.Value.PathSprite), t.Value.NbFrame));
            }
            _player.LoadAnimation = animTemp;
            _SpriteEnemys = new List<SpriteMob>();
            foreach (var m in _GamePlayInfo.Enemys)
            {
                foreach (var t in m.Textures)
                {
                    animTemp.Add(t.Value.NomSprite, new Animation(Content.Load<Texture2D>(t.Value.PathSprite), t.Value.NbFrame));
                }

                m.LoadAnimation = animTemp;
                _SpriteEnemys.Add(new SpriteMob(m) { Position = m.Co.VectorLocation });
            }


            _SpritePlayers = new List<Sprite>()
            {
                new Sprite(_player)
                {
                    Position = _player.Co.VectorLocation,
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
            //// test colision a voir
            _player.Colision.UpdateColision(_GamePlayInfo.Enemys);




            //Console.WriteLine(_player.co.vectorLocation);
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
            foreach (var enemySprite in _SpriteEnemys)
                enemySprite.Update(gameTime, _SpriteEnemys);

            foreach (var sprite in _SpritePlayers)
            {


                sprite.Update(gameTime, _SpritePlayers);
            }

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
            foreach (var enemySprite in _SpriteEnemys)
            {
                enemySprite.Draw(_spriteBatch);
            }
            foreach (var sprite in _SpritePlayers)
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
