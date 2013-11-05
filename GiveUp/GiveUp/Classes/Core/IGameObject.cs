using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Core
{
    public interface IGameObject
    {
        void Initialize(ContentManager content, Vector2 position);


        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}
