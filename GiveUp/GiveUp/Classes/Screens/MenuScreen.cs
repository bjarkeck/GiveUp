using GiveUp.Classes.Core;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiveUp.Classes.Screens
{
    public class MenuScreen : BaseScreen
    {
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                ScreenManager.Current.LoadScreen(new GameScreen());
            }
        }
    }
}
