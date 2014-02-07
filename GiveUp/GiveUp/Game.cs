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
using GiveUp.Classes.Db;
#endregion

namespace GiveUp
{

    // Bjarke test
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Matrix spriteScale { get; set; }

        public static bool ExitGame = false;
        public static ScreenManager ScreenManager;

        public Game1() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width);
            graphics.PreferredBackBufferHeight = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Game1.ScreenManager = new ScreenManager(Content);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteScale = Matrix.CreateScale(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 1600f, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 900f, 1);
        }
        
        protected override void UnloadContent()
        {
            Game1.ScreenManager.UnloadContent();
            try
            {
                DataContext.Current.SaveChanges();
                DataContext.Current.Dispose();
                DataContext.Current = null;
                DataContext.Current.Dispose();
            }
            catch (Exception)
            {
            }
        }
        protected override void OnExiting(object sender, EventArgs args)
        {
            try
            {
                DataContext.Current.SaveChanges();
                DataContext.Current.Dispose();
                DataContext.Current = null;
                DataContext.Current.Dispose();
            }
            catch (Exception)
            {
            }
            base.OnExiting(sender, args);
        }

        protected override void Update(GameTime gameTime)
        {
            Game1.ScreenManager.Update(gameTime);

            if (ExitGame == true)
            {
                this.Exit();
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, spriteScale);
            Game1.ScreenManager.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive, null, null, null, null, spriteScale);
            Game1.ScreenManager.DrawAdditive(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}
