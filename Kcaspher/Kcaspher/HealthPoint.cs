using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Projet_2._0.Menus;

namespace Projet_2._0
{
    public class HealthPoint
    {
        public int healthpoint;
        public Boolean respawn;
        public ParticleEngine Hp1, Hp2, Hp3, Hp4, Hp5, Hp12, Hp22, Hp32, Hp42, Hp52;
        public List<Texture2D> Text, textures, textures2;
        bool world1;
        public HealthPoint(int healthpoint)
        {
            this.healthpoint = healthpoint;
            respawn = false;
            List<Texture2D> textures = new List<Texture2D>();
            List<Texture2D> textures2 = new List<Texture2D>();
            textures.Add(Content_Manager.getInstance().Textures["etoile"]);
            textures2.Add(Content_Manager.getInstance().Textures["heart"]);
            textures2.Add(Content_Manager.getInstance().Textures["heart2"]);
            Hp1 = Hp2 = Hp3 = Hp4 = Hp5 = new ParticleEngine(textures, new Vector2(0, 0));
            Hp12 = Hp22 = Hp32 = Hp42 = Hp52 = new ParticleEngine(textures2, new Vector2(0, 0));
            world1 = true;
        }

        public void update(Casper casper, IEnumerable<Rectangle> enemies, GameType gametype)
        {
            if (gametype == GameType.Menu_Play_Solo_world1_lvl1 || gametype == GameType.Menu_Play_Solo_world1_lvl2 || gametype == GameType.Menu_Play_Solo_world1_lvl3 || gametype == GameType.Menu_Play_Multi_Type)
                world1 = true;
            else
                world1 = false;

            foreach (Rectangle rect in enemies)
            {
                if (casper.Hitbox.Intersects(rect))
                {
                    //Game1.GetGame().Exit();
                    respawn = true;
                    healthpoint += -1;
                    SoundManager.hp.Play();
                }
                if (healthpoint <= 0)
                {
                    Game1.GetGame().screenmanager.gametype = GameType.Dead;
                }
            }
        }

        public void draw(SpriteBatch spritebatch, Camera camera)
        {
            if (world1)
            {
                Hp1.EmitterLocation = new Vector2((int)camera.centre.X + Res.gI().ScaleX(44), (int)camera.centre.Y + Res.gI().ScaleX(44));
                Hp1.Update();
                Hp1.Draw(spritebatch);
                if (healthpoint > 3)
                {
                    Hp2.EmitterLocation = new Vector2((int)camera.centre.X + Res.gI().ScaleX(44) * 2, (int)camera.centre.Y + Res.gI().ScaleX(44));
                    Hp2.Update();
                    Hp2.Draw(spritebatch);
                }
                if (healthpoint > 6)
                {
                    Hp3.EmitterLocation = new Vector2((int)camera.centre.X + Res.gI().ScaleX(44) * 3, (int)camera.centre.Y + Res.gI().ScaleX(44));
                    Hp3.Update();
                    Hp3.Draw(spritebatch);
                }
                if (healthpoint > 9)
                {
                    Hp4.EmitterLocation = new Vector2((int)camera.centre.X + Res.gI().ScaleX(44) * 4, (int)camera.centre.Y + Res.gI().ScaleX(44));
                    Hp4.Update();
                    Hp4.Draw(spritebatch);
                }
                if (healthpoint > 12)
                {
                    Hp5.EmitterLocation = new Vector2((int)camera.centre.X + Res.gI().ScaleX(44) * 5, (int)camera.centre.Y + Res.gI().ScaleX(44));
                    Hp5.Update();
                    Hp5.Draw(spritebatch);
                }
            }
            else
            {

                Hp12.EmitterLocation = new Vector2((int)camera.centre.X + Res.gI().ScaleX(44), (int)camera.centre.Y + Res.gI().ScaleX(44));
                Hp12.Update();
                Hp12.Draw(spritebatch);
                if (healthpoint > 1)
                {
                    Hp22.EmitterLocation = new Vector2((int)camera.centre.X + Res.gI().ScaleX(44) * 2, (int)camera.centre.Y + Res.gI().ScaleX(44));
                    Hp22.Update();
                    Hp22.Draw(spritebatch);
                }
                if (healthpoint > 2)
                {
                    Hp32.EmitterLocation = new Vector2((int)camera.centre.X + Res.gI().ScaleX(44) * 3, (int)camera.centre.Y + Res.gI().ScaleX(44));
                    Hp32.Update();
                    Hp32.Draw(spritebatch);
                }
                if (healthpoint > 3)
                {
                    Hp42.EmitterLocation = new Vector2((int)camera.centre.X + Res.gI().ScaleX(44) * 4, (int)camera.centre.Y + Res.gI().ScaleX(44));
                    Hp42.Update();
                    Hp42.Draw(spritebatch);
                }
                if (healthpoint > 4)
                {
                    Hp52.EmitterLocation = new Vector2((int)camera.centre.X + Res.gI().ScaleX(44) * 5, (int)camera.centre.Y + Res.gI().ScaleX(44));
                    Hp52.Update();
                    Hp52.Draw(spritebatch);
                }

            }
        }

    }
}
