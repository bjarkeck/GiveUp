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
using System.Linq;
using GiveUp.Classes.Db;
using System.Diagnostics;
#endregion

namespace GiveUp
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Matrix spriteScale { get; set; }
        public static bool ExitGame = false;
        public static ScreenManager ScreenManager;
        Texture2D alphaTexture;
        Effect alphaMap;
        public static float AlphaMapAlpha = 0;
        private static float alphaMapAlpha = 0;
        public static Vector2 AlphaMapPosition = Vector2.Zero;
        private static Vector2 alphaMapPosition = Vector2.Zero;

        public Game1()
            : base()
        {
            CompileFxFiles();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width);
            graphics.PreferredBackBufferHeight = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            graphics.ApplyChanges();


        }

        private void CompileFxFiles()
        {
            var q = new DirectoryInfo("~/");
            string programPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"MSBuild\MonoGame\v3.0\2MGFX.exe");
            DirectoryInfo effectsFolder = new DirectoryInfo(Path.Combine(new DirectoryInfo("./").FullName, "Content\\Effects"));
            foreach (var item in effectsFolder.GetFiles())
            {
                if (item.Extension.Contains("fx"))
                {
                    string fxPath = item.FullName;
                    string compiledVersion = string.Join(".", fxPath.Split('.').Take(fxPath.Split('.').Count() - 1)) + ".mgfxo";
                    if (File.Exists(compiledVersion))
                    {
                        File.Delete(compiledVersion);
                    }

                    System.Diagnostics.Process.Start(programPath, "\"" + fxPath + "\" " + "\"" + compiledVersion + "\" /DEBUG");
                }
            }
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
            spriteScale = Matrix.CreateScale(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 1600f, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / (Editor.IsEnable ? 1000f : 900f), 1);
            alphaMap = Content.Load<Effect>("Effects/AlphaMap.mgfxo");

            alphaTexture = new Texture2D(GraphicsDevice, 1600, 900);
            Color[] tc = new Color[1600 * 900];
            for (int i = 0; i < tc.Length; i++)
                tc[i] = Color.Black;
            alphaTexture.SetData(tc);



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

            alphaMapPosition = Vector2.Lerp(alphaMapPosition, AlphaMapPosition, 0.3f);
            alphaMapAlpha = MathHelper.Lerp(alphaMapAlpha, AlphaMapAlpha, 0.08f);

            alphaMap.Parameters["PositionX"].SetValue(1600f * (alphaMapPosition.X / 1600f));
            alphaMap.Parameters["PositionY"].SetValue(900f * (alphaMapPosition.Y / 900f));
            alphaMap.Parameters["Procentage"].SetValue(alphaMapAlpha);

            

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, spriteScale);
            Game1.ScreenManager.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Additive, null, null, null, null, spriteScale);
            Game1.ScreenManager.DrawAdditive(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, null, null, null, alphaMap, spriteScale);
            spriteBatch.Draw(alphaTexture, new Rectangle(0, 0, 1600, 900), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
