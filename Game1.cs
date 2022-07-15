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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private FunctionGame _functionGame = new FunctionGame();
        private GamePlayInfo _gamePlayInfo = new GamePlayInfo();
        private List<Sprite> _spritePlayers;
        private List<RoundedButton> _menuButtons;
        private RoundedButton _backMenuButton;
        private Joueur _player;
        private SpriteFont _font;
        private List<SpriteMob> _spriteEnemys;
        private gameState _gs = gameState.mainMenu;
        private Texture2D _castle;
        private GameTime _gt;
        private Texture2D _steack;
        private Texture2D _buttonTexture;
        private Texture2D _gameOver;
        private Texture2D _hpBar;
        private Texture2D _hpMaxBar;
        private int _tileSize = 75;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            if (GraphicsDevice == null)
            {
                _graphics.ApplyChanges();
            }
            _graphics.PreferredBackBufferWidth = GraphicsDevice.Adapter.CurrentDisplayMode.Width - GraphicsDevice.Adapter.CurrentDisplayMode.Width % 50;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height - GraphicsDevice.Adapter.CurrentDisplayMode.Height % 50 - 25;
            Window.AllowUserResizing = false;
            _graphics.ApplyChanges();
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Random rnd = new Random();
            int xx = -100;
            int yy = -100;
            List<Coordonnee> l = new List<Coordonnee>();
            l.Add(new Coordonnee(-100, -100));
            l.Add(new Coordonnee(-100, 2000));
            l.Add(new Coordonnee(2000, 2000));
            l.Add(new Coordonnee(2000, -100));
            int indexRandom;
            for (var i = 0; i < 100; i++)
            {
                indexRandom = rnd.Next(l.Count);
                _functionGame.SpawnEnemy(TypeMobs.Golem, (int)((rnd.NextDouble() * 10) + 1), _gamePlayInfo, new Coordonnee(-100 - xx, -100 - yy));
                _functionGame.SpawnEnemy(TypeMobs.Golem, (int)((rnd.NextDouble() * 10) + 1), _gamePlayInfo, new Coordonnee(-100 - xx, 500 + (yy * -1)));
                _functionGame.SpawnEnemy(TypeMobs.Golem, (int)((rnd.NextDouble() * 10) + 1), _gamePlayInfo, new Coordonnee(500 + (xx * -1), 500 + (yy * -1)));
                _functionGame.SpawnEnemy(TypeMobs.Golem, (int)((rnd.NextDouble() * 10) + 1), _gamePlayInfo, new Coordonnee(500 + (xx * -1), -100 - yy));
                xx -= 300;
                yy -= 300;

            }

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _gamePlayInfo.GameBackground = Content.Load<Texture2D>("grass");
            _player = _gamePlayInfo.J1;
            _castle = Content.Load<Texture2D>("Structure/castle-nobg");
            _steack = Content.Load<Texture2D>("Structure/steack-nobg");
            _buttonTexture = Content.Load<Texture2D>("btn/button2-nobg");
            _gameOver = Content.Load<Texture2D>("gameOver-nobg");
            _hpMaxBar = new Texture2D(GraphicsDevice, 1, 1);
            _hpMaxBar.SetData(new[] { Color.Gray });
            _hpBar = new Texture2D(GraphicsDevice, 1, 1);
            _hpBar.SetData(new[] { Color.Red });

            for (var y = 0; y < GraphicsDevice.Adapter.CurrentDisplayMode.Height; y = y + _tileSize)
            {
                for (var x = 0; x < GraphicsDevice.Adapter.CurrentDisplayMode.Width; x = x + _tileSize)
                {
                    Coordonnee c = new Coordonnee(x, y);
                    _gamePlayInfo.Regions.Add(new Region(c, new Structure(TypeStructure.plain, 0, 0, _gamePlayInfo.GameBackground, 512, 512, c)));
                }
            }
            _gamePlayInfo.Regions[(int)_gamePlayInfo.Regions.Count / 2 - (int)GraphicsDevice.Adapter.CurrentDisplayMode.Width / (_tileSize * 2) - 1]
                .setStructure(new Structure(TypeStructure.castle, 100, 100, _castle, 592, 421, new Coordonnee()));

            var animTemp = new Dictionary<string, Animation>();
            foreach (var t in _player.Textures)
            {
                animTemp.Add(t.Value.NomSprite, new Animation(Content.Load<Texture2D>(t.Value.PathSprite), t.Value.NbFrame));
            }
            _player.LoadAnimation = animTemp;
            _spriteEnemys = new List<SpriteMob>();
            foreach (var m in _gamePlayInfo.Entity)
            {
                if (m is Enemy)
                {
                    Enemy monstre = (Enemy)m;
                    foreach (var t in monstre.Textures)
                    {
                        animTemp.Add(t.Value.NomSprite, new Animation(Content.Load<Texture2D>(t.Value.PathSprite), t.Value.NbFrame));
                    }

                    monstre.LoadAnimation = animTemp;
                    _spriteEnemys.Add(new SpriteMob(monstre) { Position = monstre.Co.VectorLocation, Gpi = _gamePlayInfo });
                }
            }
            _player.Colision.Texture = new Texture2D(GraphicsDevice, 1, 1);
            _player.Colision.Texture.SetData(new Color[] { Color.DarkSlateGray });

            _spritePlayers = new List<Sprite>()
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
                    Gpi= _gamePlayInfo
                }
            };

            _font = Content.Load<SpriteFont>("btn/Font");

            var buttonCenter = GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2 - _buttonTexture.Width / 2;

            _menuButtons = new List<RoundedButton>()
            {
                new RoundedButton(_buttonTexture, _font)
                {
                    Position = new Vector2(buttonCenter, 50),
                    Text = "JOUER",
                    _newGameState = gameState.gamePlay
                },
                new RoundedButton(_buttonTexture, _font)
                {
                    Position = new Vector2(buttonCenter, 150),
                    Text = "SCORE",
                    _newGameState = gameState.score
                }
            };
            if (_gs == gameState.score)
            {
                _backMenuButton = new RoundedButton(_buttonTexture, _font)
                {
                    Position = new Vector2(buttonCenter, 20),
                    Text = "MENU",
                    _newGameState = gameState.mainMenu
                };
            }
            else
            {
                _backMenuButton = new RoundedButton(_buttonTexture, _font)
                {
                    Position = new Vector2(buttonCenter, 100),
                    Text = "MENU",
                    _newGameState = gameState.mainMenu
                };
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (_gs == gameState.gamePlay)
            {
                _gt = gameTime;
                // test colision a voir
                if (!_gamePlayInfo.pause || _gamePlayInfo.J1.IsDead)
                {
                    foreach (Region region in _gamePlayInfo.Regions)
                        region.Update(gameTime);



                    _player.Colision.UpdateColision(_gamePlayInfo.Entity);
                    foreach (var m in _gamePlayInfo.Entity)
                    {
                        if (m is Enemy)
                        {
                            Enemy e = (Enemy)m;
                        }
                    }

                    foreach (SpriteMob enemySprite in _spriteEnemys)
                        enemySprite.Update(gameTime, _spriteEnemys, _gamePlayInfo);

                    foreach (var sprite in _spritePlayers)
                    {
                        sprite.Update(gameTime, _spritePlayers);
                    }
                }
                if (_gamePlayInfo.J1.IsDead)
                {
                    _gamePlayInfo.Scores.Add(_gamePlayInfo.Score);
                    _gamePlayInfo.Scores.Sort();
                    _gs = gameState.gameOver;
                    //reset game :
                    _gamePlayInfo.J1 = new Joueur("Golem", new Coordonnee(200, 200));
                    _gamePlayInfo.J1.Pv = 100;
                    _gamePlayInfo.Entity = new List<Entity>();
                    _gamePlayInfo.Entity.Add(_gamePlayInfo.J1);
                    _gamePlayInfo.Regions = new List<Region>();
                    _spritePlayers = new List<Sprite>()
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
                            Gpi= _gamePlayInfo
                        }
                    };
                    _spriteEnemys = new List<SpriteMob>();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    _gamePlayInfo.pause = !_gamePlayInfo.pause;
                }
            }
            else if (_gs == gameState.mainMenu)
            {
                foreach (RoundedButton button in _menuButtons)
                {
                    button.Update(gameTime);
                    if (button.Clicked())
                    {
                        _gs = button._newGameState;
                    }
                }
            }
            else if (_gs == gameState.score || _gs == gameState.gameOver)
            {
                _backMenuButton.Update(gameTime);
                if (_backMenuButton.Clicked())
                {
                    _gs = _backMenuButton._newGameState;
                }
            }
            base.Update(gameTime);
        }
        private void save()
        {
            string serializedText;
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            switch (_gs)
            {
                case gameState.mainMenu:
                    DrawMainMenu(gameTime);
                    break;
                case gameState.gamePlay:
                    DrawGamePlay();
                    break;
                case gameState.gameOver:
                    DrawGameOver();
                    break;
                case gameState.score:
                    DrawScore();
                    break;
            }

            base.Draw(gameTime);
        }

        void DrawGamePlay()
        {
            _spriteBatch.Begin();
            for (var y = 0; y < GraphicsDevice.Adapter.CurrentDisplayMode.Height; y = y + _tileSize)
            {
                for (var x = 0; x < GraphicsDevice.Adapter.CurrentDisplayMode.Width; x = x + _tileSize)
                {
                    var sourceRectangle = new Rectangle(0, 0, 512, 512);
                    var destinationRectangle = new Rectangle(x, y, _tileSize, _tileSize);
                    _spriteBatch.Draw(_gamePlayInfo.GameBackground, destinationRectangle, sourceRectangle, Color.White);
                }
            }

            foreach (Region region in _gamePlayInfo.Regions)
            {
                switch (region.Structure.Type)
                {
                    case (TypeStructure.steack):
                        var s = new Structure(TypeStructure.steack, 2, 30, _steack, 228, 212, region.Co);
                        _gamePlayInfo.Entity.Add(s);

                        region.Structure = s;
                        break;
                    case (TypeStructure.castle):
                        var st = new Structure(TypeStructure.castle, 30, 30, _castle, 592, 421, region.Co);
                        _gamePlayInfo.Entity.Add(st);
                        region.Structure = st;
                        _gamePlayInfo.Castel = st;
                        break;
                }
                _spriteBatch.Draw(
                    region.Structure.Texture,
                    new Rectangle((int)region.Co.VectorLocation.X, (int)region.Co.VectorLocation.Y, _tileSize, _tileSize),
                    new Rectangle(0, 0, region.Structure.OriginalWidth, region.Structure.OriginalHeight),
                    Color.White);
            }

            foreach (var enemySprite in _spriteEnemys)
            {
                enemySprite.Draw(_spriteBatch);
            }
            foreach (var sprite in _spritePlayers)
                sprite.Draw(_spriteBatch);

            _spriteBatch.DrawString(_font, "Score : " + _gamePlayInfo.Score, new Vector2(0, 0), Color.White);

            _spriteBatch.Draw(_hpMaxBar, new Rectangle(500, 0, 200, 30), Color.Gray);
            _spriteBatch.Draw(_hpBar, new Rectangle(500, 0, _gamePlayInfo.J1.Pv * 2, 30), Color.Red);
            _spriteBatch.DrawString(_font, "Vie : " + _gamePlayInfo.J1.Pv, new Vector2(510, 5), Color.White);
            _spriteBatch.End();
        }

        void DrawMainMenu(GameTime gameTime)
        {
            _spriteBatch.Begin();
            foreach (RoundedButton button in _menuButtons)
            {
                button.Draw(_spriteBatch);
            };
            var horizontalCenter = GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2 - (_castle.Width / 2);
            _spriteBatch.Draw(_castle, new Vector2(horizontalCenter, GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2), new Rectangle(0, 0, _castle.Width, _castle.Height), Color.White);
            var text = "Vous chevalier saint, vous retrouvez en plein milieu d'un raid de golems";
            var text2 = "sur le chateau fort fort Lointain, les vagues d'ennemies sembles sans fin,";
            var text3 = "pendant combien de temps s'aurez-vous proteger la veuve et l'orphelin ?";
            _spriteBatch.DrawString(_font, text, new Vector2(GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2 - (_font.MeasureString(text).X / 2), GraphicsDevice.Adapter.CurrentDisplayMode.Height - 540), Color.White);
            _spriteBatch.DrawString(_font, text2, new Vector2(GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2 - (_font.MeasureString(text2).X / 2), GraphicsDevice.Adapter.CurrentDisplayMode.Height - 520), Color.White);
            _spriteBatch.DrawString(_font, text3, new Vector2(GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2 - (_font.MeasureString(text3).X / 2), GraphicsDevice.Adapter.CurrentDisplayMode.Height - 500), Color.White);
            _spriteBatch.End();
        }

        void DrawGameOver()
        {
            _spriteBatch.Begin();
            _backMenuButton.Draw(_spriteBatch);
            var horizontalCenter = GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2 - (_gameOver.Width / 2);
            var verticalCenter = GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2 - (_gameOver.Height / 2);
            _spriteBatch.Draw(_gameOver, new Vector2(horizontalCenter, verticalCenter), new Rectangle(0, 0, _gameOver.Width, _gameOver.Height), Color.White);
            var text = "SCORE : " + _gamePlayInfo.Score;
            _spriteBatch.DrawString(_font, text, new Vector2(GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2 - (_font.MeasureString(text).X / 2), GraphicsDevice.Adapter.CurrentDisplayMode.Height - 200), Color.White);
            _spriteBatch.End();
        }

        void DrawScore()
        {
            _spriteBatch.Begin();
            _backMenuButton.Draw(_spriteBatch);
            var orderedScores = _gamePlayInfo.Scores;
            orderedScores.Sort();
            var l = 10;
            if (orderedScores.Count < 10)
            {
                l = orderedScores.Count;
            }
            for (int index = 0; index < l; index++)
            {
                var text = index + 1 + "   SCORE : " + orderedScores[index];
                _spriteBatch.DrawString(_font, text, new Vector2(GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2 - (_font.MeasureString(text).X / 2), 200 + 50 * (index + 1)), Color.White);
            }
            _spriteBatch.End();
        }
    }
}
