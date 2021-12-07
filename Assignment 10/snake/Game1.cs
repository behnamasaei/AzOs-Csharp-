using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace snak_mono
{
    public class Game1 : Game
    {
        int score = 0;
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch { get; set; }
        public SpriteFont _font;

        const int snakeSize = 10;
        const int gameWidth = 50;
        const int gameHeight = 100;

        Snake snake;
        Food food;
        Random random;
        public Texture2D Pear { get; set; }
        public Texture2D Pile { get; set; }
        public Texture2D Apple { get; set; }
        public Texture2D GameOver { get; set; }

        FoodType foodType;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = gameWidth * snakeSize;
            _graphics.PreferredBackBufferHeight = gameHeight * snakeSize;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            random = new Random();
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            snake = new Snake(this, GraphicsDevice, _spriteBatch, snakeSize);
            food = new Food(this, _spriteBatch, GraphicsDevice, snakeSize);

            _font = Content.Load<SpriteFont>("font");
            Pear = Content.Load<Texture2D>("pear");
            Pile = Content.Load<Texture2D>("pile");
            Apple = Content.Load<Texture2D>("apple");
            GameOver = Content.Load<Texture2D>("gameover");

            this.Components.Add(snake);
            this.Components.Add(food);
        }

        public void SetFoodLocation()
        {
            food.PosX = random.Next(0, GraphicsDevice.Viewport.Width / snakeSize) * snakeSize;
            food.PosY = random.Next(0, GraphicsDevice.Viewport.Height / snakeSize) * snakeSize;
            Array values = Enum.GetValues(typeof(FoodType));
            foodType = (FoodType)values.GetValue(random.Next(values.Length));
            food.active = true;
        }

        public void CheckSnakeFood()
        {
            if (snake.PosX == food.PosX && snake.PosY == food.PosY)
            {
                if (foodType == FoodType.Pear)
                {
                    score += 2;
                    snake.Score++;
                    food.active = false;
                    snake.AddTail();
                }
                else if (foodType == FoodType.Apple)
                {
                    score++;
                    snake.Score++;
                    food.active = false;
                    snake.AddTail();
                }
                else if (foodType == FoodType.Pile)
                {
                    score--;
                    snake.Score++;
                    food.active = false;
                    snake.AddTail();
                }

            }

            if (score < 0)
            {
                snake.Run = false;
            }

        }

        public void CheckGameOver()
        {
            if (snake.PosX < 0 ||
            snake.PosX >= GraphicsDevice.Viewport.Width ||
            snake.PosY < 0 ||
             snake.PosY >= GraphicsDevice.Viewport.Height)
            {
                snake.Run = false;
            }
        }

        public void CheckBodyKill(int x, int y)
        {
            x = snake.PosX + x * 10;
            y = snake.PosY + y * 10;
            for (int i = 0; i < snake.tiles.Count; i++)
            {
                if (x == snake.tiles[i].X && y == snake.tiles[i].Y)
                {
                    snake.Run = false;

                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (!food.active)
            {
                SetFoodLocation();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                snake.DirX = 0;
                snake.DirY = -1;
                CheckBodyKill(0, -1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                snake.DirX = 0;
                snake.DirY = 1;
                CheckBodyKill(0, 1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                snake.DirX = -1;
                snake.DirY = 0;
                CheckBodyKill(-1, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                snake.DirX = 1;
                snake.DirY = 0;
                CheckBodyKill(1, 0);
            }

            CheckSnakeFood();
            CheckGameOver();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, $"Score: {score}", new Vector2(snakeSize), Color.Black);

            if (foodType == FoodType.Pear)
                _spriteBatch.Draw(Pear, new Rectangle(food.PosX, food.PosY, snakeSize, snakeSize), Color.White);

            if (foodType == FoodType.Pile)
                _spriteBatch.Draw(Pile, new Rectangle(food.PosX, food.PosY, snakeSize, snakeSize), Color.White);

            if (foodType == FoodType.Apple)
                _spriteBatch.Draw(Apple, new Rectangle(food.PosX, food.PosY, snakeSize, snakeSize), Color.White);

            if (!snake.Run)
                _spriteBatch.Draw(GameOver, new Rectangle(350, 200, 100, 100), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
