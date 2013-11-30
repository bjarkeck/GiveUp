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
            graphics.IsFullScreen = false;
            //graphics.PreferredBackBufferHeight = 960;
            //graphics.PreferredBackBufferWidth = 1280;
            //graphics.PreferredBackBufferHeight = 860;
            //graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Game1.ScreenManager = new ScreenManager(Content);
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
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            Game1.ScreenManager.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
