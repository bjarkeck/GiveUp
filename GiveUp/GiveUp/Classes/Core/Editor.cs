using GiveUp.Classes.LevelManager;
using GiveUp.Classes.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GiveUp.Classes.Core
{
    public class Editor
    {
        public static bool IsEnable = true;
        public static bool IsEnable = false;

        private int selectedTile = -1;
        public LevelManagerr LevelManager
        {
            get
            {
                return ((GameScreen)ScreenManager.Current.CurrentScreen).LevelManager;
            }
        }

        Dictionary<int, IGameObject> obsticles = new Dictionary<int, IGameObject>();

        public Editor(ContentManager content)
        {
            Player player = new Player();
            var types = Assembly.GetEntryAssembly().GetTypes().Where(x => typeof(IGameObject).IsAssignableFrom(x) && typeof(IGameObject) != x);

            int i = 0;
            foreach (var item in types)
            {
                obsticles.Add(i, (IGameObject)Activator.CreateInstance(item));
                obsticles[i].Player = player;
                obsticles[i].Initialize(content, new Vector2(i * 32, 930));
                obsticles[i].InitialPosition = new Vector2(i * 32, 930);
                i++;
            }

        }
        public bool IsAddOrRemoveDefined = false;
        public bool add = false;
        private Rectangle saveChanges = new Rectangle(1600 - 32, 900, 32, 100);

        public void Update()
        {


            if (Keyboard.GetState().IsKeyDown(Keys.F) || Keyboard.GetState().IsKeyDown(Keys.R))
                LevelManager.SaveChanges();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
                LevelManager.RestartLevel();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {

                if (MouseHelper.Position.Y > 900)
                {
                    foreach (var item in obsticles)
                    {
                        if ((new Rectangle(item.Key * 32, 930, 32, 32)).Contains(MouseHelper.Position.ToPoint()))
                        {
                            selectedTile = item.Key;
                        }
                    }
                }
                else
                {
                    var grid = GetGridCord();

                    if (IsAddOrRemoveDefined)
                    {
                        if (LevelManager.IsOccupied(new Vector2(grid.X * 32, grid.Y * 32)) == false)
                        {
                            add = true;
                        }
                        else
                        {
                            add = false;
                        }
                        IsAddOrRemoveDefined = false;
                    }
                    if (selectedTile != -1)
                        LevelManager.AddObsticle(obsticles[selectedTile],  new Vector2(grid.X * 32, grid.Y * 32), add);
                }


                if (saveChanges.Contains(MouseHelper.Position.ToPoint()))
                {
                    LevelManager.SaveChanges();
                }

            }
            else
            {
                IsAddOrRemoveDefined = true;
            }



        }

        public Point GetGridCord()
        {
            var mousePos = MouseHelper.Position.ToPoint();

            int xW = 0;
            while (mousePos.X > xW * 32)
                xW += 1;

            int xY = 0;
            while (mousePos.Y > xY * 32)
                xY += 1;

            return new Point(xW - 1, xY - 1);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (selectedTile != -1)
                spriteBatch.Draw(GameLogic.ColorTexture(Color.Green, spriteBatch), new Rectangle(selectedTile * 32, 965, 32, 5), Color.White);

            foreach (var item in obsticles)
                item.Value.Draw(spriteBatch);


            spriteBatch.Draw(GameLogic.ColorTexture(Color.Green, spriteBatch), saveChanges, Color.White);

        }

        

    }
}
