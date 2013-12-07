using GiveUp.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Screens
{
    public class MenuScreen : BaseScreen
    {
        private static List<Button> buttons = new List<Button>();

        public Texture2D Texture;
        public Rectangle menuRectangle;
        public Texture2D testTexture;
        public Rectangle testRectangle;


        public override void LoadContent()
        {
            Texture = Content.Load<Texture2D>("Images/Menu/BackGround/MenuScreen");
            testTexture = Content.Load<Texture2D>("Images/Tiles/IceGround");
            testRectangle = new Rectangle(0, 0, testTexture.Width, testTexture.Height);
            menuRectangle = new Rectangle(0, 0, 1600, 900);
            buttons.Add(new Button(Content.Load<Texture2D>("Images/Tiles/ground"), new Vector2(0, 0), StartGame));
            base.LoadContent();
        }

        public static void StartGame()
        {
            ScreenManager.Current.LoadScreen(new GameScreen());
        }
        
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (Button b in buttons)
            {
                b.Update(gameTime);
            }
            
            //Loads gameScreen
            //ScreenManager.Current.LoadScreen(new GameScreen());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, menuRectangle, Color.White);

            foreach (Button b in buttons)
            {
                b.Draw(spriteBatch);
            }
            
            base.Draw(spriteBatch);
        }
    }
}
