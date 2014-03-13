using Tempus.Classes.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IGameObject
{

    Player Player { get; set; }

    Vector2 InitialPosition { get; set; }

    void Initialize(ContentManager content, Vector2 position);

    void CollisionLogic();

    void Update(GameTime gameTime);

    void Draw(SpriteBatch spriteBatch);

    void DrawAdditive(SpriteBatch spriteBatch);

}
