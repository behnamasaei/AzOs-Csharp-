using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace snak_mono
{
    public class Snake : DrawableGameComponent
    {
        const int updateInterval = 70;

        public int Score { get; set; } = 0;
        public bool Run { get; set; } = true;
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int DirX { get; set; } = 1;
        public int DirY { get; set; } = 0;
        int size = 0;
        int milisSecondsSinceLastUpdate = 0;

        int oldPosX = 0;
        int oldPosY = 0;

        SpriteBatch spriteBatch;
        Texture2D pixel;
        public List<Rectangle> tiles;

        public Snake(Game game, GraphicsDevice graphics, SpriteBatch spriteBatch, int size)
        : base(game)
        {
            this.size = size;
            this.spriteBatch = spriteBatch;
            pixel = new Texture2D(graphics, 1, 1);
            pixel.SetData(new[] { Color.White });

            PosX = graphics.Viewport.Width / 2;
            PosY = graphics.Viewport.Height / 2;

            tiles = new List<Rectangle>();
            tiles.Add(new Rectangle(PosX, PosY, size, size));
        }

        public override void Update(GameTime gameTime)
        {
            milisSecondsSinceLastUpdate += gameTime.ElapsedGameTime.Milliseconds;
            if (milisSecondsSinceLastUpdate >= updateInterval && Run)
            {  
                milisSecondsSinceLastUpdate = 0;
                oldPosX = PosX;
                oldPosY = PosY;

                PosX = PosX + DirX * size;
                PosY = PosY + DirY * size;

                if (tiles.Count > 1)
                {
                    for (int i = tiles.Count - 1; i > 0; i--)
                    {
                        tiles[i] = new Rectangle(tiles[i - 1].X, tiles[i - 1].Y, size, size);
                    }
                }
            }
            tiles[0] = new Rectangle(PosX, PosY, size, size);
            base.Update(gameTime);
        }


        public void AddTail()
        {
            tiles.Add(new Rectangle(PosX, PosY, size, size));
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (Run)
            {
                foreach (Rectangle tile in tiles)
                {
                    spriteBatch.Draw(pixel,
                    new Rectangle(tile.X - 1, tile.Y - 1, size + 2, size + 2), Color.Gray);

                    spriteBatch.Draw(pixel, tile, Color.White);
                }
            }
            else
            {
                foreach (Rectangle tile in tiles)
                {
                    spriteBatch.Draw(pixel,
                    new Rectangle(tile.X - 1, tile.Y - 1, size + 2, size + 2), Color.Red);

                    spriteBatch.Draw(pixel, tile, Color.White);

                }
            }

            spriteBatch.End();
        }
    }
}