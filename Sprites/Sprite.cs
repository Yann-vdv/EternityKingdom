using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using projet.Models;
using projet.Managers;
using System.Linq;
using System;

namespace projet.Sprites
{
    public class Sprite
    {
        #region Fields
        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;
        protected Vector2 _position;
        protected Texture2D _texture;
        protected Joueur _joueur;
        protected string _lastDirection = "right";
        protected bool isGameOver = false;
        protected bool isAttacking = false;
        #endregion

        #region Properties
        public input Input;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (_animationManager != null)
                {
                    _animationManager.Position = _position;
                }
            }
        }
        public float Speed = 1f;
        public Vector2 Velocity;
        #endregion

        #region Method
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else throw new System.Exception("il y a une erreur");
        }
        protected virtual void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Up))
            {
                if (Keyboard.GetState().IsKeyDown(Input.Left))
                {
                    Velocity.X -= Speed;
                    _lastDirection = "left";
                }
                else if (Keyboard.GetState().IsKeyDown(Input.Right))
                {
                    Velocity.X += Speed;
                    _lastDirection = "right";
                }
                Velocity.Y -= Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
            {
                if (Keyboard.GetState().IsKeyDown(Input.Left))
                {
                    Velocity.X -= Speed;
                    _lastDirection = "left";
                }
                else if (Keyboard.GetState().IsKeyDown(Input.Right))
                {
                    Velocity.X += Speed;
                    _lastDirection = "right";
                }
                Velocity.Y += Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                if (Keyboard.GetState().IsKeyDown(Input.Up))
                {
                    Velocity.Y -= Speed;
                }
                else if (Keyboard.GetState().IsKeyDown(Input.Down))
                {
                    Velocity.Y += Speed;
                }
                Velocity.X -= Speed;
                _lastDirection = "left";
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                if (Keyboard.GetState().IsKeyDown(Input.Up))
                {
                    Velocity.Y -= Speed;
                }
                else if (Keyboard.GetState().IsKeyDown(Input.Down))
                {
                    Velocity.Y += Speed;
                }
                Velocity.X += Speed;
                _lastDirection = "right";
            }
            // else
            // {
            //     Velocity.Y = 0;
            //     Velocity.X = 0;
            // }
        }
        protected virtual void setAnimation()
        {
            if (Velocity.X > 0)
            {
                _animationManager.Play(_animations["Walk_right"]);
            }
            else if (Velocity.X < 0)
            {
                _animationManager.Play(_animations["Walk_left"]);
            }
            else if (Velocity.Y > 0 || Velocity.Y < 0)
            {
                if (_lastDirection == "right")
                {
                    _animationManager.Play(_animations["Walk_right"]);
                }
                else
                {
                    _animationManager.Play(_animations["Walk_left"]);
                }
            }
            else if (Keyboard.GetState().IsKeyUp(Input.Space))
            {
                if (_lastDirection == "right")
                {
                    _animationManager.Play(_animations["Idle_right"]);
                }
                else
                {
                    _animationManager.Play(_animations["Idle_left"]);
                }
                // _animationManager.Stop();
            }
        }
        public virtual void Attack()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Space) && !isAttacking)
            {
                Velocity = Vector2.Zero;
                if (_lastDirection == "right")
                {
                    _animationManager.Play(_animations["Attack_right"]);
                }
                else
                {
                    _animationManager.Play(_animations["Attack_left"]);
                }

                isAttacking = true;
            }
            else if (isAttacking)
            {
                // Console.WriteLine(_animations["Attack_right"].CurrentFrame);
                // Console.WriteLine(_animations["Attack_right"].FrameCount);
                if (_lastDirection == "right")
                {
                    _animationManager.Play(_animations["Attack_right"]);
                    if (_animations["Attack_right"].CurrentFrame == _animations["Attack_right"].FrameCount - 1)
                    {
                        _joueur.Attack();
                        isAttacking = false;
                    }
                }
                else
                {
                    _animationManager.Play(_animations["Attack_left"]);
                    if (_animations["Attack_left"].CurrentFrame == _animations["Attack_left"].FrameCount - 1)
                    {
                        _joueur.Attack();
                        isAttacking = false;
                    }
                }
            }

        }
        public virtual void Die()
        {
            //Console.WriteLine("animatin : " + _animations["Dead_right"].Texture);
            if (_joueur.IsDead)
            {
                Velocity = Vector2.Zero;
                if (_lastDirection == "right")
                {
                    //for (int i = 0; i < _animations["Dead_right"].FrameCount; i++)
                    _animationManager.Play(_animations["Dead_right"]);

                }
                else
                {
                    //for (int i = 0; i < _animations["Dead_left"].FrameCount; i++)
                    _animationManager.Play(_animations["Dead_left"]);
                }
                if (_animations["Dead_right"].CurrentFrame == 12 || _animations["Dead_right"].CurrentFrame == 12)
                    isGameOver = true;
            }
        }
        public Sprite(Joueur j)
        {
            _joueur = j;
            _animations = j.LoadAnimation;
            _animationManager = new AnimationManager(_animations.First().Value);
            _position = j.Co.VectorLocation;
        }
        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }
        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }
        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (!isGameOver)
            {
                Die();

                _animationManager.Update(gameTime);

                if (!_joueur.IsDead)
                {
                    Attack();

                    if (!isAttacking)
                    {
                        _joueur.Colision.UpdateSelfCo(_joueur);
                        Move();

                        setAnimation();

                        Position += Velocity;
                        _joueur.Co.VectorLocation = Position;
                    }
                }
            }

            Velocity = Vector2.Zero;
        }
        #endregion
    }
}