using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Projet_2._0;
using Microsoft.Xna.Framework;

namespace Kcaspher
{
    public class DyingScreen
    {
        KeyboardState key, prevk;

        public DyingScreen()
        {}

        public void update(GameType nextgametype)
        {
            key = Keyboard.GetState();
            if (key.IsKeyUp(Keys.Space) && prevk.IsKeyDown(Keys.Space))
            {
                Game1.GetGame().screenmanager.gametype = nextgametype;
            }
            prevk = key;
        }

        public void Draw(SpriteBatch spritebatch, Texture2D texture)
        {
            spritebatch.Draw(texture, new Rectangle(0, 0, Res.gI().ScaleX(1680), Res.gI().ScaleY(1050)), Color.White);
        }
    }
}
