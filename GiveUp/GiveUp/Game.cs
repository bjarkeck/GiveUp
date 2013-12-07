#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using GiveUp.Classes.LevelManager;
using System.IO;
using GiveUp.Classes.Core;
#endregion

namespace GiveUp
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        public static ScreenManager ScreenManager;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.ApplyChanges();


        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //graphics.PreferredBackBufferWidth = 1024;
            //graphics.PreferredBackBufferHeight = 768;


            graphics.ApplyChanges();



            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Game1.ScreenManager = new ScreenManager(Content);


            spriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteScale = Matrix.CreateScale(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 1600f, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 900f, 1);
        }
        
        protected override void UnloadContent()
        {
            Game1.ScreenManager.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Game1.ScreenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, SpriteScale);
            Game1.ScreenManager.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        public Matrix SpriteScale { get; set; }
    }
}
