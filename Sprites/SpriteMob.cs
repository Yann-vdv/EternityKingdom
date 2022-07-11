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
    public class SpriteMob
    {
        #region Fields
        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;
        protected Vector2 _position;
        protected Texture2D _texture;
        protected Enemy _monstre;
        protected string _lastDirection = "right";
        protected bool isGameOver = false;
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
            if (_monstre._Position.VectorLocation.X < _monstre.Target.VectorLocation.X && _monstre._Position.VectorLocation.Y < _monstre.Target.VectorLocation.Y)
            {

            };
        }
        protected virtual void setAnimation()
        {
            if (Velocity.X > 0)
            {
                _animationManager.Play(_animations[_monstre.Textures[TypeSprite.Walk_right].NomSprite]);
            }
            else if (Velocity.X < 0)
            {
                _animationManager.Play(_animations[_monstre.Textures[TypeSprite.Walk_left].NomSprite]);
            }
            else if (Velocity.Y > 0 || Velocity.Y < 0)
            {
                if (_lastDirection == "right")
                {
                    _animationManager.Play(_animations[_monstre.Textures[TypeSprite.Walk_right].NomSprite]);
                }
                else
                {
                    _animationManager.Play(_animations[_monstre.Textures[TypeSprite.Walk_left].NomSprite]);
                }
            }
            else
            {
                if (_lastDirection == "right")
                {
                    _animationManager.Play(_animations[_monstre.Textures[TypeSprite.Idle_right].NomSprite]);
                }
                else
                {
                    _animationManager.Play(_animations[_monstre.Textures[TypeSprite.Idle_left].NomSprite]);
                }
                // _animationManager.Stop();
            }
        }
        public virtual void Attack()
        {
            // Velocity = Vector2.Zero;
            // if (_lastDirection == "right")
            // {
            //     _animationManager.Play(_animations["Attack_right"]);
            // }
            // else
            // {
            //     _animationManager.Play(_animations["Attack_left"]);
            // }
        }
        public virtual void Die()
        {
            //Console.WriteLine("animatin : " + _animations["Dead_right"].Texture);
            if (_monstre.IsDead)
            {
                Velocity = Vector2.Zero;
                if (_lastDirection == "right")
                {
                    //for (int i = 0; i < _animations["Dead_right"].FrameCount; i++)
                    _animationManager.Play(_animations[_monstre.Textures[TypeSprite.Dead_right].NomSprite]);

                }
                else
                {
                    //for (int i = 0; i < _animations["Dead_left"].FrameCount; i++)
                    _animationManager.Play(_animations[_monstre.Textures[TypeSprite.Dead_left].NomSprite]);
                }
                if (_animations[_monstre.Textures[TypeSprite.Dead_left].NomSprite].CurrentFrame == 12 || _animations[_monstre.Textures[TypeSprite.Dead_right].NomSprite].CurrentFrame == 12)
                    isGameOver = true;
            }
        }
        public SpriteMob(Enemy m)
        {
            _monstre = m;
            _animations = m.LoadAnimation;
            _animationManager = new AnimationManager(_animations.First().Value);
            _position = m.Co.VectorLocation;
        }
        public virtual void Update(GameTime gameTime, List<SpriteMob> sprites)
        {
            if (!isGameOver)
            {
                Die();

                _animationManager.Update(gameTime);

                if (!_monstre.IsDead)
                {
                    Move();

                    Attack();

                    setAnimation();

                    Position += Velocity;
                    _monstre.Co.VectorLocation = Position;
                }

            }

            Velocity = Vector2.Zero;
        }
        #endregion
    }
}