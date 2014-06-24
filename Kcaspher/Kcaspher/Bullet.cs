using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Projet_2._0;

namespace Kcaspher
{
    public class Bullet
    {
        public Vector2 Direction, Position, Velocity, Origin;
        public Rectangle Hitbox, isVisible;
        public Boolean canTouch;

        public Bullet(Vector2 Origin, Vector2 Direction)
        {
            canTouch = true;
            this.Origin = Origin;
            this.Position = Origin;
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Res.gI().ScaleX(15), Res.gI().ScaleX(15));
            Velocity = new Vector2(7f, 7f);
            this.Direction = Direction - Origin;
            this.Direction.Normalize();
            isVisible = new Rectangle((int)Origin.X - Res.gI().ScaleX(340), (int)Origin.Y - Res.gI().ScaleY(340), Res.gI().ScaleX(680), Res.gI().ScaleY(680));
        }

        public void update()
        {
            Position += Velocity * Direction;
            Hitbox.X = (int)Position.X;
            Hitbox.Y = (int)Position.Y;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Content_Manager.getInstance().Textures["Bullet"], Hitbox, Color.White);
        }
    }
}
