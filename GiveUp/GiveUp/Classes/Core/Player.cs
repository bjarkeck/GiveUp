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
        public ParticleManager ParticleManager;
        private Vector2 diePosition;
        public bool CanJump = true;
        public bool CanDoubleJump = false;
        public bool ReverseControls = false;
        public bool DragActivated = false;
        public float StartJumpSpeed;
        private ParticleEmitter timeEmitter;
        public Vector2 Gravity;
        public float MaxSpeed;
        public float MaxDrag;
        public float Friction;
        public InputHelper InputHelper = new InputHelper();

        private bool isTimeSlowed = false;
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
            ParticleManager = new ParticleManager();

            List<ParticleTexture> l = new List<ParticleTexture>();
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b1"), Color.White, new Color(Color.White, 0), 0.7f, 0.7f));
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b2"), Color.White, new Color(Color.White, 0), 0.5f, 0.5f));
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b3"), Color.White, new Color(Color.White, 0), 0.6f, 0.6f));
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b4"), Color.White, new Color(Color.White, 0), 0.5f, 0.5f));
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b5"), Color.White, new Color(Color.White, 0), 0.5f, 0.5f));
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b6"), Color.White, new Color(Color.White, 0), 0.6f, 0.6f));
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b7"), Color.White, new Color(Color.White, 0), 0.6f, 0.6f));
            l.Add(new ParticleTexture(content.Load<Texture2D>("Images/Player/b8"), Color.White, new Color(Color.White, 0), 0.5f, 0.5f));
            ParticleManager.AddEmitter("BodyParts",
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

            List<ParticleTexture> blood = new List<ParticleTexture>();  //Blod klat størrelse (1 = 100%)
            blood.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/blood1"), new Color(Color.Red, 1f), new Color(Color.Red, 0.1f), 0.4f, 0.5f));
            blood.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/blood2"), new Color(Color.Red, 1f), new Color(Color.Red, 0.4f), 0.2f, 0.2f));
            blood.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/blood3"), new Color(Color.Red, 1f), new Color(Color.Red, 0.1f), 0.2f, 0.3f));
            blood.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/blood4"), new Color(Color.Red, 1f), new Color(Color.Red, 0.2f), 0.2f, 0.3f));
            ParticleManager.AddEmitter("Blood",
                new ParticleEmitter(
                    blood,
                    new Range<float>(100, 200),
                    new Range<float>(-0.003f, -0.003f),
                    new Range<int>(0, 500),
                    0,
                    100,
                    0,
                    10000,
                    Velocity * -1 / 1.3f,
                    Gravity / 8,
                    4000,
                    true,
                    false
                )
            );


            List<ParticleTexture> l2 = new List<ParticleTexture>();
            l2.Add(new ParticleTexture(content.Load<Texture2D>("Images/Particles/wind"), new Color(Color.Black, 0.04f), new Color(Color.White, 0f), 1f, 1f));

            timeEmitter = new ParticleEmitter(
                    l2,
                    new Range<float>(21, 50),
                    new Range<float>(0),
                    new Range<int>(0, 500),
                    0,
                    360,
                    0,
                    1020,
                    this.Velocity,
                    Vector2.Zero
                );

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            ParticleManager.Draw(spriteBatch);
            Animation.Draw(spriteBatch, (isTimeSlowed ? 2f : 1));
        }

        public void DrawAdditive(SpriteBatch spriteBatch)
        {
            ParticleManager.DrawAdditive(spriteBatch);
            timeEmitter.Draw(spriteBatch); ;
        }

        public void Update(GameTime gameTime)
        {
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                this.Position = MouseHelper.Position;
                this.Rectangle.X = (int)MouseHelper.Position.X;
                this.Position.Y = (int)MouseHelper.Position.Y;
            }

            Rectangle = Animation.Rectangle;
            Animation.AnimationSpeed = 150 - Math.Abs(Velocity.X * 30);
            InputHelper.Update();
            Movement(gameTime);
            Animation.Update(gameTime, Position);
            Rectangle = Animation.Rectangle;
            ParticleManager.Update(gameTime, new Rectangle((int)diePosition.X + 5, (int)diePosition.Y + 5, Rectangle.Width - 10, Rectangle.Height - 10));

            ParticleManager.ParticleEmitters["BodyParts"].AddedVelocity = Velocity * -1;
            ParticleManager.ParticleEmitters["Blood"].AddedVelocity = Velocity * -1 / 1.3f;
            ParticleManager.ParticleEmitters["BodyParts"].MaxNumberOfParitcles = 0;

            LevelManagerr l = ((GameScreen)ScreenManager.Current.CurrentScreen).LevelManager;
            var tiles = l.GameObjects.Where(x => x.GetType() == typeof(BoxTile));
            foreach (var item in ParticleManager.ParticleEmitters["BodyParts"].Particles)
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
            if (ParticleManager != null)
            {
                this.diePosition = diePosition;
                ParticleManager.ParticleEmitters["BodyParts"].MaxNumberOfParitcles += 6;
                ParticleManager.ParticleEmitters["Blood"].MaxNumberOfParitcles += 100;
            }
        }

        public void Jump(GameTime time)
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
                this.Velocity.X += this.Acceleration * -1 * gameTime.ElapsedGameTime.Milliseconds * Time.GameSpeed;
            }
            else if (keyState.IsKeyDown(ReverseControls ? Keys.A : Keys.D))
            {
                Animation.PlayAnimation("run");
                this.Velocity.X += this.Acceleration * gameTime.ElapsedGameTime.Milliseconds * Time.GameSpeed;
            }
            else
            {
                Animation.PlayAnimation("stand");
            }
            //Jump
            if (InputHelper.IsNewPress(Keys.Space))
                this.Jump(gameTime);


            if (keyState.IsKeyDown(Keys.LeftShift))
            {
                timeEmitter.MaxNumberOfParitcles = 2000;
                Time.GameSpeed = 0.4f;
                isTimeSlowed = true;
                Game1.AlphaMapAlpha = 1;
            }
            else
            {
                timeEmitter.MaxNumberOfParitcles = 0;
                Time.GameSpeed = 1f;
                isTimeSlowed = false;
                Game1.AlphaMapAlpha = 0.3f;
            }
            Game1.AlphaMapPosition = this.Position;
            timeEmitter.Update(gameTime, this.Rectangle);

            //Friction
            if (Math.Abs(Velocity.X) < Friction)
                Velocity.X = 0;
            else
                Velocity.X += Friction * (Velocity.X > 0 ? -1f : 1f);


            //Gravity
            Velocity += Gravity * gameTime.DeltaTime();

            //Max Speed
            Velocity.X = MathHelper.Clamp(Velocity.X, MaxSpeed * -1, MaxSpeed);

            //Add To Position
            this.Position += this.Velocity * gameTime.DeltaTime();

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