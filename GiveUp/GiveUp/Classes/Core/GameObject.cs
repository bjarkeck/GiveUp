using Tempus.Classes.Core;
using Tempus.Classes.LevelManager;
using Tempus.Classes.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameObject
{
    public Rectangle Rectangle;
    public Vector2 InitialPosition { get; set; }

    public IEnumerable<T> GetAllGameObjects<T>()
    {
        if (LevelManager != null)
        {
            if (typeof(T) == typeof(IGameObject))
            {
                return LevelManager.GameObjects.Select(c => (T)c).ToList();
            }
            return LevelManager.GameObjects.Where(x => x.GetType() == typeof(T) || x.GetType().BaseType == typeof(T)).Select(x => ((T)x)).ToList();
        }
        return new List<T>();
    }


    public Player Player { get; set; }

    public LevelManagerr LevelManager
    {
        get
        {
            if (ScreenManager.Current.CurrentScreen.GetType() == typeof(GameScreen))
                return ((GameScreen)ScreenManager.Current.CurrentScreen).LevelManager;
            else
                return null;
        }
    }

    public virtual void Initialize(ContentManager content, Vector2 position)
    {
    }

    public virtual void Update(GameTime gameTime)
    {

    }

    public virtual void CollisionLogic()
    {

    }

    public virtual void DrawAdditive(SpriteBatch spriteBatch)
    {

    }
    public virtual void Draw(SpriteBatch spriteBatch)
    {
    }

}