using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Projet_2._0;

namespace Kcaspher.Menus
{
    public class Menu_multi2
    {

        Rectangle Bouton_Exit, Bouton_Options, Bouton_Solo, Bouton_Multi, local, host, join, Bouton_Play;
        Texture2D Text_Menu_Play;
        Rectangle mouseClick;
        MouseState mouseState, previousmouseState;
        KeyboardState keyboardstate, previouskeyboardstate;

        public Menu_multi2(Texture2D Text_Menu_Play)
        {
            this.Text_Menu_Play = Text_Menu_Play;
            Bouton_Options = new Rectangle(Res.gI().ScaleX(955), Res.gI().ScaleY(210), Res.gI().ScaleX(225), Res.gI().ScaleY(310));
            Bouton_Solo = new Rectangle(Res.gI().ScaleX(500), Res.gI().ScaleY(525), Res.gI().ScaleX(225), Res.gI().ScaleY(310));
            Bouton_Multi = new Rectangle(Res.gI().ScaleX(755), Res.gI().ScaleY(680), Res.gI().ScaleX(225), Res.gI().ScaleY(310));
            Bouton_Exit = new Rectangle(Res.gI().ScaleX(755), Res.gI().ScaleY(280), Res.gI().ScaleX(165), Res.gI().ScaleY(80));
            Bouton_Play = new Rectangle(Res.gI().ScaleX(500), Res.gI().ScaleY(210), Res.gI().ScaleX(225), Res.gI().ScaleY(310));
            join = new Rectangle(Res.gI().ScaleX(1050), Res.gI().ScaleY(700), Res.gI().ScaleX(170), Res.gI().ScaleY(110));
            host = new Rectangle(Res.gI().ScaleX(1300), Res.gI().ScaleY(700), Res.gI().ScaleX(100), Res.gI().ScaleY(100));
            local = new Rectangle(Res.gI().ScaleX(1050), Res.gI().ScaleY(850), Res.gI().ScaleX(250), Res.gI().ScaleY(107));

        }

        void MouseClicked(int x, int y, ref GameType gameType)
        {
            mouseClick = new Rectangle(x, y, 10, 10);

            if (mouseClick.Intersects(Bouton_Play))
            {
                gameType = GameType.Menu_Play_Type;
            }
            if (mouseClick.Intersects(Bouton_Exit))
            {
                Game1.GetGame().Exit();
            }
            if (mouseClick.Intersects(Bouton_Options))
            {
                gameType = GameType.Menu_Option_Type;
            }

            if (mouseClick.Intersects(Bouton_Solo))
            {
                gameType = GameType.Menu_Play_Solo_Type;
            }
            if (mouseClick.Intersects(host))
            {
                System.Diagnostics.Process.Start("C:/Users/epita/Desktop/Final/zbra/bin/Debug/zbra.exe");
            }
            if (mouseClick.Intersects(join))
            {
                System.Diagnostics.Process.Start("C:/Users/epita/Desktop/Final/Client/bin/Debug/Client.exe");
            }
            if (mouseClick.Intersects(local))
            {
                gameType = GameType.Menu_Play_Multi_Type;
            }
        }

        public void update(GameTime gametime, ref GameType gametype, ref GameType previousgametype)
        {
            keyboardstate = Keyboard.GetState();
            mouseState = Mouse.GetState();
            /// <check if mouseclick>
            if (previousmouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {
                MouseClicked(mouseState.X, mouseState.Y, ref gametype);
            }
            /// </check if mouseclick>
            previousmouseState = mouseState;
            if (keyboardstate.IsKeyDown(Keys.Escape) && previouskeyboardstate.IsKeyUp(Keys.Escape))
            {
                gametype = previousgametype;
            }
            previouskeyboardstate = keyboardstate;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Text_Menu_Play, new Rectangle(0, 0, Res.gI().ScaleX(1680), Res.gI().ScaleY(1050)), Color.White);
        }
    }
}
