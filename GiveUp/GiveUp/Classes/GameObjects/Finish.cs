using Tempus.Classes.Core;
using Tempus.Classes.GameObjects;
using Tempus.Classes.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class Finish : GameObject, IGameObject
    {
        public Player Player { get; set; }
        public Texture2D closedDoor { get; set; }
        public Texture2D openDoor { get; set; }
        public Vector2 Position { get; set; }
        public const byte LoadOrder = 1;

        //Hejsa 1

        public const char TileChar = 'D';
        List<ButtonActivator> buttonActivators;

        public void Initialize(ContentManager content, Vector2 position)
        {
            Position = position;
            closedDoor = content.Load<Texture2D>("Images/Tiles/ClosedDoor");
            openDoor = content.Load<Texture2D>("Images/Tiles/OpenDoor");
            Rectangle = new Rectangle((int)position.X, (int)position.Y, closedDoor.Width, closedDoor.Height);

            buttonActivators = GetAllGameObjects<ButtonActivator>().ToList();
        }

        public void Update(GameTime gameTime)
        {
            // Sætter døren til at være åben
            if (buttonActivators.Any(x => x.isLocked == false) == false)
            {
                closedDoor = openDoor;
            }

            if (Player.InputHelper.IsNewPress(Keys.Enter))
            {
                this.LevelManager.StartNextLevel();
            }
        }

        public void CollisionLogic()
        {
            if (Player.Rectangle.Intersects(Rectangle))
            {
                // Tjekker om alle knapper er tændte
                if (buttonActivators.Any(x => x.isLocked == false) == false)
                {
                    this.LevelManager.StartNextLevel(true);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(closedDoor, Position, Color.White);
        }
    }