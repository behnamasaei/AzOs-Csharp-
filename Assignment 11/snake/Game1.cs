using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;
using System.Collections.Generic;

namespace snak_mono
{
    public class Game1 : Game
    {
        int pileConuter = 0;
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

        #region check type of food
        FoodType foodType;
        string pileType;

        #endregion


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

            food.PilePosX = random.Next(0, GraphicsDevice.Viewport.Width / snakeSize) * snakeSize;
            food.PilePosY = random.Next(0, GraphicsDevice.Viewport.Height / snakeSize) * snakeSize;
            if (pileConuter == 3)
            {
                pileType = "Pile";
                pileConuter = 0;
            }
            else
            {
                pileType = null;
                pileConuter++;
            }
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
                    pileType = null;
                    snake.AddTail();
                }
                else if (foodType == FoodType.Apple)
                {
                    score++;
                    snake.Score++;
                    food.active = false;
                    pileType = null;
                    snake.AddTail();
                }
            }

            if (snake.PosX == food.PilePosX && snake.PosY == food.PilePosY)
            {
                score--;
                snake.Score++;
                food.active = false;
                snake.AddTail();
            }

            if (score < 0)
            {
                snake.Run = false;
            }
        }

        public void CheckGameOver()
        {
            if (
                snake.PosX < 0
                || snake.PosX >= GraphicsDevice.Viewport.Width
                || snake.PosY < 0
                || snake.PosY >= GraphicsDevice.Viewport.Height
            )
            {
                snake.Run = false;
            }
        }

        public void CheckBodyKill(int x, int y)
        {
            x = snake.PosX + x * 10;
            y = snake.PosY + y * 10;
            List<int> bodyXSnake = new List<int>();
            List<int> bodyYSnake = new List<int>();
            foreach (var item in snake.tiles)
            {
                bodyXSnake.Add(item.X);
                bodyYSnake.Add(item.Y);
            }

            var indexX = bodyXSnake.IndexOf(x);
            var indexY = bodyYSnake.IndexOf(y);

            if (indexX > -1 && indexY > -1)
                if (
                    bodyXSnake[bodyXSnake.IndexOf(x)] == x && bodyYSnake[bodyYSnake.IndexOf(y)] == y
                )
                    snake.Run = false;
        }

        public void BuildTree()
        {
            var headSnake = snake.tiles[0].Center;
            var bodySnake = snake.tiles;

            List<TreeModel> tree = new List<TreeModel>();

            var NodeCheck = headSnake.Y;

            while (NodeCheck - 10 > 0)
            {
                foreach (var body in snake.tiles)
                    if (NodeCheck - 10 == body.Center.Y)
                        break;

                tree.Add(
                    new TreeModel { X = headSnake.X, Y = NodeCheck - 10, Parent = 0, Child = 1 }
                );
                NodeCheck = NodeCheck - 10;
            }

            NodeCheck = headSnake.Y;
            while (NodeCheck + 10 < 480)
            {
                foreach (var body in snake.tiles)
                    if (NodeCheck + 10 == body.Center.Y)
                        break;

                tree.Add(
                    new TreeModel { X = headSnake.X, Y = NodeCheck + 10, Parent = 1, Child = 1 }
                );
                NodeCheck += 10;
            }

            NodeCheck = headSnake.X;
            while (NodeCheck - 10 > 0)
            {
                foreach (var body in snake.tiles)
                    if (NodeCheck - 10 == body.Center.X)
                        break;

                tree.Add(
                    new TreeModel { X = NodeCheck - 10, Y = headSnake.Y, Parent = 1, Child = 1 }
                );
                NodeCheck -= 10;
            }

            NodeCheck = headSnake.X;
            while (NodeCheck + 10 < 800)
            {
                foreach (var body in snake.tiles)
                    if (NodeCheck + 10 == body.Center.X)
                        break;

                tree.Add(
                    new TreeModel { X = NodeCheck + 10, Y = headSnake.Y, Parent = 1, Child = 1 }
                );
                NodeCheck += 10;
            }

            for (int y = headSnake.Y - 10; y > 0; y -= 10)
            {
                NodeCheck = y;
                var NodeXCheck = headSnake.X;

                while (NodeXCheck - 10 > 0)
                {
                    foreach (var body in snake.tiles)
                        if (NodeXCheck - 10 == body.Center.X)
                            break;

                    tree.Add(new TreeModel { X = NodeXCheck - 10, Y = y, Parent = 1, Child = 1 });
                    NodeCheck -= 10;
                }

                NodeCheck = headSnake.X;
                while (NodeCheck + 10 < 800)
                {
                    foreach (var body in snake.tiles)
                        if (NodeCheck + 10 == body.Center.X)
                            break;

                    tree.Add(
                        new TreeModel { X = NodeCheck + 10, Y = headSnake.Y, Parent = 1, Child = 1 }
                    );
                    NodeCheck += 10;
                }
            }
            while (true) { }
        }

        protected override void Update(GameTime gameTime)
        {
            if (
                GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape)
            )
                Exit();

            // TODO: Add your update logic here

            if (!food.active)
            {
                SetFoodLocation();
            }

            List<int> bodyXSnake = new List<int>();
            List<int> bodyYSnake = new List<int>();
            foreach (var body in snake.tiles)
            {
                bodyXSnake.Add(body.X);
                bodyYSnake.Add(body.Y);
            }

            if (food.PosX > snake.PosX)
            {
                //check right is body
                if (
                    bodyXSnake.Contains(snake.tiles[0].X + 10)
                    && bodyYSnake.Contains(snake.tiles[0].Y)
                )
                {
                    // check down is body
                    if (
                        !bodyXSnake.Contains(snake.tiles[0].X)
                        && !bodyYSnake.Contains(snake.tiles[0].Y + 10)
                    )
                    {
                        // gp Down
                        snake.DirX = 0;
                        snake.DirY = 1;
                        base.Update(gameTime);
                    }
                    // check up is body
                    if (
                        !bodyXSnake.Contains(snake.tiles[0].X)
                        && !bodyYSnake.Contains(snake.tiles[0].Y - 10)
                    )
                    {
                        // go Up
                        snake.DirX = 0;
                        snake.DirY = -1;
                        base.Update(gameTime);
                    }
                    if (
                        !bodyXSnake.Contains(snake.tiles[0].X - 10)
                        && !bodyYSnake.Contains(snake.tiles[0].Y)
                    )
                    {
                        // go left
                        snake.DirX = -1;
                        snake.DirY = 0;
                        base.Update(gameTime);
                    }
                }

                // go right
                snake.DirX = 1;
                snake.DirY = 0;
                base.Update(gameTime);
            }

            if (food.PosX < snake.PosX)
            {
                //check left is body
                if (
                    bodyXSnake.Contains(snake.tiles[0].X - 10)
                    && bodyYSnake.Contains(snake.tiles[0].Y)
                )
                {
                    // check down is body
                    if (
                        !bodyXSnake.Contains(snake.tiles[0].X)
                        && !bodyYSnake.Contains(snake.tiles[0].Y + 10)
                    )
                    {
                        // gp Down
                        snake.DirX = 0;
                        snake.DirY = 1;
                        base.Update(gameTime);
                    }
                    // check up is body
                    if (
                        !bodyXSnake.Contains(snake.tiles[0].X)
                        && !bodyYSnake.Contains(snake.tiles[0].Y - 10)
                    )
                    {
                        // go Up
                        snake.DirX = 0;
                        snake.DirY = -1;
                        base.Update(gameTime);
                    }
                    if (
                        !bodyXSnake.Contains(snake.tiles[0].X + 10)
                        && !bodyYSnake.Contains(snake.tiles[0].Y)
                    )
                    {
                        // go right
                        snake.DirX = 1;
                        snake.DirY = 0;
                        base.Update(gameTime);
                    }
                }

                // go left
                snake.DirX = -1;
                snake.DirY = 0;
                base.Update(gameTime);
            }

            if (food.PosY > snake.PosY)
            {
                //check Down is body
                if (
                    bodyXSnake.Contains(snake.tiles[0].X)
                    && bodyYSnake.Contains(snake.tiles[0].Y - 10)
                )
                {
                    // check down is body
                    if (
                        !bodyXSnake.Contains(snake.tiles[0].X)
                        && !bodyYSnake.Contains(snake.tiles[0].Y + 10)
                    )
                    {
                        // go Down
                        snake.DirX = 0;
                        snake.DirY = 1;
                        base.Update(gameTime);
                    }
                    // check up is body
                    if (
                        !bodyXSnake.Contains(snake.tiles[0].X)
                        && !bodyYSnake.Contains(snake.tiles[0].Y - 10)
                    )
                    {
                        // go Up
                        snake.DirX = 0;
                        snake.DirY = -1;
                        base.Update(gameTime);
                    }
                    if (
                        !bodyXSnake.Contains(snake.tiles[0].X + 10)
                        && !bodyYSnake.Contains(snake.tiles[0].Y)
                    )
                    {
                        // go right
                        snake.DirX = 1;
                        snake.DirY = 0;
                        base.Update(gameTime);
                    }
                    if (
                        !bodyXSnake.Contains(snake.tiles[0].X - 10)
                        && !bodyYSnake.Contains(snake.tiles[0].Y)
                    )
                    {
                        // go left
                        snake.DirX = -1;
                        snake.DirY = 0;
                        base.Update(gameTime);
                    }
                }

                // go Down
                snake.DirX = 0;
                snake.DirY = 1;
                base.Update(gameTime);
            }

            if (food.PosY < snake.PosY)
            {
                //check Down is body
                if (
                    bodyXSnake.Contains(snake.tiles[0].X)
                    && bodyYSnake.Contains(snake.tiles[0].Y + 10)
                )
                {
                    // check down is body
                    if (
                        !bodyXSnake.Contains(snake.tiles[0].X)
                        && !bodyYSnake.Contains(snake.tiles[0].Y + 10)
                    )
                    {
                        // go Down
                        snake.DirX = 0;
                        snake.DirY = 1;
                        base.Update(gameTime);
                    }
                    // check up is body
                    if (
                        !bodyXSnake.Contains(snake.tiles[0].X)
                        && !bodyYSnake.Contains(snake.tiles[0].Y - 10)
                    )
                    {
                        // go Up
                        snake.DirX = 0;
                        snake.DirY = -1;
                        base.Update(gameTime);
                    }
                    if (
                        !bodyXSnake.Contains(snake.tiles[0].X + 10)
                        && !bodyYSnake.Contains(snake.tiles[0].Y)
                    )
                    {
                        // go right
                        snake.DirX = 1;
                        snake.DirY = 0;
                        base.Update(gameTime);
                    }
                    if (
                        !bodyXSnake.Contains(snake.tiles[0].X - 10)
                        && !bodyYSnake.Contains(snake.tiles[0].Y)
                    )
                    {
                        // go left
                        snake.DirX = -1;
                        snake.DirY = 0;
                        base.Update(gameTime);
                    }
                }


                // go Up
                snake.DirX = 0;
                snake.DirY = -1;
                base.Update(gameTime);
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
                _spriteBatch.Draw(
                    Pear,
                    new Rectangle(food.PosX, food.PosY, snakeSize, snakeSize),
                    Color.White
                );

            if (pileType == "Pile")
                _spriteBatch.Draw(
                    Pile,
                    new Rectangle(food.PilePosX, food.PilePosY, snakeSize, snakeSize),
                    Color.White
                );

            if (foodType == FoodType.Apple)
                _spriteBatch.Draw(
                    Apple,
                    new Rectangle(food.PosX, food.PosY, snakeSize, snakeSize),
                    Color.White
                );

            if (!snake.Run)
                _spriteBatch.Draw(GameOver, new Rectangle(350, 200, 100, 100), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
