using GiveUp.Classes.GameObjects.Tiles;
using GiveUp.Classes.LevelManager;
using GiveUp.Classes.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public class Player
    {
        public Vector2 Position = Vector2.Zero;
        public Vector2 Velocity;
        public float Acceleration;
        public Rectangle Rectangle;
        public SpriteAnimation Animation;
        private ParticleManager particleManager;
        private Vector2 diePosition;
        public bool CanJump = true;
        public bool CanDoubleJump = false;
        public bool ReverseControls = false;
        public bool DragActivated = false;
        public float StartJumpSpeed;

        public Vector2 Gravity;
        public float MaxSpeed;
        public float MaxDrag;
        public float Friction;
        public InputHelper InputHelper = new InputHelper();


        public Player()
        {
            this.Acceleration = 0.2f;
            this.Position = Vector2.Zero;
            this.StartJumpSpeed = -8f;
            this.Gravity = new Vector2(0, 0.44f);
            this.MaxSpeed = 3.2f;
            this.MaxDrag = 3.1f;
            this.Friction = 0.9f;
        }

        public void LoadContent(ContentManager content)
        {
            Animation = new SpriteAnimation(content.Load<Texture2D>("Images/Player/playerAnimation"), Position, 46, 60, 5);
            Animation.AddRow("stand", 0, 1);
            Animation.AddRow("run", 1, 15);
            Animation.AddRow("jump", 2, 1);
            Animation.AddRow("slide", 3, 1);
            Animation.AddRow("push", 4, 1);
            particleManager = new ParticleManager();

            List<ParticleTexture> l = new List<ParticleTexture>();
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b1"), Color.White, new Color(Color.White, 0), 0.4f, 0.4f));
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b2"), Color.White, new Color(Color.White, 0), 0.4f, 0.4f));
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b3"), Color.White, new Color(Color.White, 0), 0.4f, 0.4f));
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b4"), Color.White, new Color(Color.White, 0), 0.4f, 0.4f));
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b5"), Color.White, new Color(Color.White, 0), 0.4f, 0.4f));
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b6"), Color.White, new Color(Color.White, 0), 0.4f, 0.4f));
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b7"), Color.White, new Color(Color.White, 0), 0.4f, 0.4f));
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b8"), Color.White, new Color(Color.White, 0), 0.4f, 0.4f));
            particleManager.AddEmitter("BodyParts",
                new ParticleEmitter(
                    l,
                    new Range<float>(50, 100),
                    new Range<float>(-0.003f, -0.003f),
                    new Range<int>(1100),
                    0,
                    100,
                    0,
                    1000,
                    Velocity * -1,
                    Gravity / 6
                )
            );

            List<ParticleTexture> blood = new List<ParticleTexture>();
            blood.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/blood1"), new Color(Color.Red, 1f), new Color(Color.Red, 0.1f), 0.2f, 0.4f));
            blood.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/blood2"), new Color(Color.Red, 1f), new Color(Color.Red, 0.2f), 0.2f, 0.3f));
            blood.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/blood3"), new Color(Color.Red, 1f), new Color(Color.Red, 0.1f), 0.2f, 0.2f));
            blood.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/blood4"), new Color(Color.Red, 1f), new Color(Color.Red, 0.2f), 0.2f, 0.3f));
            particleManager.AddEmitter("Blood",
                new ParticleEmitter(
                    blood,
                    new Range<float>(100, 200),
                    new Range<float>(-0.003f, -0.003f),
                    new Range<int>(0, 500),
                    0,
                    100,
                    0,
                    10000,
                    Velocity * -1,
                    Gravity / 8
                ) { DrawAdditive = false, StickyParticles = true }
            );

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Animation.Draw(spriteBatch);
            particleManager.Draw(spriteBatch);
        }

        public void DrawAdditive(SpriteBatch spriteBatch)
        {
            particleManager.DrawAdditive(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            Rectangle = Animation.Rectangle;
            Animation.AnimationSpeed = 150 - Math.Abs(Velocity.X * 30);
            InputHelper.Update();
            Movement(gameTime);
            Animation.Update(gameTime, Position);
            Rectangle = Animation.Rectangle;
            particleManager.Update(gameTime, new Rectangle((int)diePosition.X + 5, (int)diePosition.Y + 5, Rectangle.Width - 10, Rectangle.Height - 10));
            particleManager.ParticleEmitters["BodyParts"].AddedVelocity = Velocity * -1;
            particleManager.ParticleEmitters["Blood"].AddedVelocity = Velocity * -1;
            particleManager.ParticleEmitters["BodyParts"].MaxNumberOfParitcles = 0;

            LevelManagerr l = ((GameScreen)ScreenManager.Current.CurrentScreen).LevelManager;
            var tiles = l.GameObjects.Where(x => x.GetType() == typeof(BoxTile));
            foreach (var item in particleManager.ParticleEmitters["BodyParts"].Particles)
            {
                foreach (var t in tiles)
                {
                    if (((BoxTile)t).Rectangle.Contains(item.Position.ToPoint()))
                    {
                        item.Velocity.Y *= -1 / 1.2f;
                        if (item.Velocity.X > 0)
                            item.Velocity.X -= 0.3f;
                        if (item.Velocity.X < 0)
                            item.Velocity.X += 0.3f;
                    }
                }
            }
        }

        public void Die(Vector2 diePosition)
        {
            if (particleManager != null)
            {
                this.diePosition = diePosition;
                particleManager.ParticleEmitters["BodyParts"].MaxNumberOfParitcles += 8;
                particleManager.ParticleEmitters["Blood"].MaxNumberOfParitcles += 100;
            }
        }

        public void Jump()
        {
            if (this.CanJump || this.CanDoubleJump)
            {
                CanDoubleJump = CanJump;
                CanJump = false;
                this.Velocity.Y = this.StartJumpSpeed;
            }
        }

        public void Movement(GameTime gameTime)
        {

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(ReverseControls ? Keys.D : Keys.A))
            {
                Animation.PlayAnimation("run");
                this.Velocity.X += this.Acceleration * -1 * gameTime.ElapsedGameTime.Milliseconds;
            }
            else if (keyState.IsKeyDown(ReverseControls ? Keys.A : Keys.D))
            {
                Animation.PlayAnimation("run");
                this.Velocity.X += this.Acceleration * gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                Animation.PlayAnimation("stand");
            }
            //Jump
            if (InputHelper.IsNewPress(Keys.Space))
                this.Jump();


            //Friction
            if (Math.Abs(Velocity.X) < Friction)
                Velocity.X = 0;
            else
                Velocity.X += Friction * (Velocity.X > 0 ? -1f : 1f);


            //Gravity
            Velocity += Gravity;

            //Max Speed
            Velocity.X = MathHelper.Clamp(Velocity.X, MaxSpeed * -1, MaxSpeed);

            //Add To Position
            this.Position += this.Velocity;

            //Animation Direction
            if (Velocity.X > 0)
                Animation.FlipImage = false;
            else if (Velocity.X < 0)
                Animation.FlipImage = true;

            //Animation state
            if (CanJump == false || Math.Abs(Velocity.Y) > 1)
                Animation.PlayAnimation("jump");
        }
    }
}