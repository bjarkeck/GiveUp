using GiveUp.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.GameObjects.Obstacles
{
    public class Testicle : GameObject, IGameObject
    {
        Vector2 position;
        SpriteAnimation animation;
        public const char TileChar = 't';

        public override void Initialize(ContentManager content, Vector2 position)
        {
            this.position = position;

            this.animation = new SpriteAnimation(content.Load<Texture2D>("Images/Player/PlayerAnimation"), position, 23, 30, 50);
            animation.AddRow("stand", 0, 1);
            animation.AddRow("run", 1, 8);
            animation.PlayAnimation("run");
        }

        public override void Update(GameTime gameTime)
        {
            position.X += 3f;
            animation.Update(gameTime, position);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }
    }
}
