using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace snak_mono
{
    public class Food : DrawableGameComponent
    {
        public int PosX { get; set; }
        public int PosY { get; set; }

        public int PilePosX { get; set; }
        public int PilePosY { get; set; }
        
        public bool active { get; set; } = false;

        public bool PearShow { get; set; }
        public bool AppleShow { get; set; }
        public bool PileShow { get; set; }

        SpriteBatch spriteBatch;
        public Texture2D pixel;
        int size;
        Game1 game;

        public Food(Game game, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, int size)
        : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.size = size;
            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });

        }

        
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (active)
            {
                // spriteBatch.Draw(pixel, new Rectangle(PosX, PosY, size, size), Color.Black);
                // spriteBatch.DrawString(game._font, "1", new Vector2(PosX, PosY), Color.White);

                //spriteBatch.Draw(game.Pear, new Vector2(0 , 0), Color.White);
                //spriteBatch.Draw(game.Pear, new Rectangle(50, 50, 15, 15), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}