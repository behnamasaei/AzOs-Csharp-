using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pattern
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Texture2D RedDiamond { get; set; }
        public Texture2D BlueDiamond { get; set; }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            RedDiamond = Content.Load<Texture2D>("red");
            BlueDiamond = Content.Load<Texture2D>("blue");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        _spriteBatch.Draw(RedDiamond, new Rectangle(j*30+50, i*30+50, 30, 30), Color.White);
                    }
                    else
                    {
                        _spriteBatch.Draw(BlueDiamond, new Rectangle(j*30+50, i*30+50, 30, 30), Color.White);
                    }
                }
            }
            // _spriteBatch.Draw(RedDiamond, new Rectangle(10, 10, 30, 30), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
