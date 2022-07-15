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
        public GamePlayInfo Gpi;
        protected Enemy _monstre;
        private bool isAttacking;
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
        public float Speed = 0.3f;
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
            if (_monstre.Target.Co.VectorLocation.X < _monstre.Co.VectorLocation.X)
            {
                Velocity.X -= Speed;
                _lastDirection = "left";
                if (_monstre.Target.Co.VectorLocation.Y > _monstre.Co.VectorLocation.Y)
                {
                    Velocity.Y += Speed;
                }
                if (_monstre.Target.Co.VectorLocation.Y < _monstre.Co.VectorLocation.Y)
                {
                    Velocity.Y -= Speed;
                }
            }
            else if (_monstre.Target.Co.VectorLocation.X >= _monstre.Co.VectorLocation.X)
            {
                Velocity.X += Speed;
                _lastDirection = "right";
                if (_monstre.Target.Co.VectorLocation.Y > _monstre.Co.VectorLocation.Y)
                {
                    Velocity.Y += Speed;
                }
                if (_monstre.Target.Co.VectorLocation.Y < _monstre.Co.VectorLocation.Y)
                {
                    Velocity.Y -= Speed;
                }
            }
            else if (_monstre.Target.Co.VectorLocation.Y > _monstre.Co.VectorLocation.Y)
            {
                Velocity.Y += Speed;
                if (_monstre.Target.Co.VectorLocation.X < _monstre.Co.VectorLocation.X)
                {
                    Velocity.X -= Speed;
                    _lastDirection = "left";
                }
                if (_monstre.Target.Co.VectorLocation.X >= _monstre.Co.VectorLocation.X)
                {
                    Velocity.X += Speed;
                    _lastDirection = "right";
                }
            }
            else if (_monstre.Target.Co.VectorLocation.Y < _monstre.Co.VectorLocation.Y)
            {
                Velocity.Y -= Speed;
                if (_monstre.Target.Co.VectorLocation.X < _monstre.Co.VectorLocation.X)
                {
                    Velocity.X -= Speed;
                    _lastDirection = "left";
                }
                if (_monstre.Target.Co.VectorLocation.X >= _monstre.Co.VectorLocation.X)
                {
                    Velocity.X += Speed;
                    _lastDirection = "right";
                }
            }
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
            }
        }
        public virtual void Attack()
        {
            if (_monstre.Colision.IsIn(_monstre.Target.Co))
            {
                if (!isAttacking)
                {
                    Velocity = Vector2.Zero;
                    if (_lastDirection == "right")
                    {
                        _animationManager.Play(_animations[_monstre.Textures[TypeSprite.Attack_right].NomSprite]);
                    }
                    else
                    {
                        _animationManager.Play(_animations[_monstre.Textures[TypeSprite.Attack_left].NomSprite]);
                    }
                    isAttacking = true;
                }
                else
                {
                    if (_lastDirection == "right")
                    {
                        if ((_animations[_monstre.Textures[TypeSprite.Attack_right].NomSprite].CurrentFrame + 1) >= _animations[_monstre.Textures[TypeSprite.Attack_right].NomSprite].FrameCount - 1)
                        {
                            _monstre.Attack(_monstre, Gpi);
                            isAttacking = false;
                        }
                    }
                    else
                    {
                        if ((_animations[_monstre.Textures[TypeSprite.Attack_left].NomSprite].CurrentFrame + 1) >= _animations[_monstre.Textures[TypeSprite.Attack_left].NomSprite].FrameCount - 1)
                        {
                            _monstre.Attack(_monstre, Gpi);
                            isAttacking = false;
                        }
                    }
                }
            }
            else
            {
                isAttacking = false;
            }
        }
        public virtual void Die()
        {
            if (_monstre.IsDead)
            {
                Velocity = Vector2.Zero;
                if (_lastDirection == "right")
                {
                    _animationManager.Play(_animations[_monstre.Textures[TypeSprite.Dead_right].NomSprite]);

                }
                else
                {
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
        public virtual void Update(GameTime gameTime, List<SpriteMob> sprites, GamePlayInfo gpi)
        {
            if (!isGameOver)
            {
                Die();

                _animationManager.Update(gameTime);

                if (!_monstre.IsDead)
                {
                    if (!isAttacking)
                    {
                        Move();
                        setAnimation();
                        Position += Velocity;
                        _monstre.Co.VectorLocation = Position;
                    }

                    Attack();

                }

                _monstre.Colision.UpdateSelfCo(_monstre);
                _monstre.UpdateTarget(gpi);
            }

            Velocity = Vector2.Zero;
        }
        #endregion
    }
}