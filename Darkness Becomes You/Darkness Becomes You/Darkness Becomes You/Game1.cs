using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using SpriteLibrary;

namespace Darkness_Becomes_You
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public KeyboardState currentKeyState;
        public KeyboardState oldKeyState;

        public Sprite playerSprite;
        public Texture2D playerTexture;

        public LinkedList<Sprite> playerBullets = new LinkedList<Sprite>();

        public Sprite playerBulletSprite;
        public Texture2D playerBulletTexture;
        public int playerShotCooldown;

        public LinkedList<Sprite> activeEnemies = new LinkedList<Sprite>();

        public Sprite enemyISprite;
        public Texture2D enemyITexture;

        public LinkedList<Sprite> enemyBullets = new LinkedList<Sprite>();

        public Sprite enemyBulletSprite;
        public Texture2D enemyBulletTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            playerTexture = this.Content.Load<Texture2D>("Textures\\Player");
            playerBulletTexture = this.Content.Load<Texture2D>("Textures\\PlayerBullet");
            enemyITexture = this.Content.Load<Texture2D>("Textures\\Enemy1");
            enemyBulletTexture = this.Content.Load<Texture2D>("Textures\\EnemyBullet");

            playerSprite = new Sprite();
            playerSprite.UpperLeft = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            playerSprite.firingDelay = 30;
            playerSprite.SetTexture(playerTexture);

            enemyISprite = new Sprite();
            enemyISprite.SetTexture(enemyITexture);
            enemyISprite.SetSpeedAndDirection(1, -90);
            enemyISprite.UpperLeft = new Vector2(1920 / 3, 1080 / 4);
            enemyISprite.firingDelay = 120;
            activeEnemies.AddLast(enemyISprite);

            enemyISprite = new Sprite();
            enemyISprite.SetTexture(enemyITexture);
            enemyISprite.SetSpeedAndDirection(1, -90);
            enemyISprite.UpperLeft = new Vector2((1920 / 3) * 2, 1080 / 4);
            enemyISprite.firingDelay = 120;
            activeEnemies.AddLast(enemyISprite);



        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            currentKeyState = Keyboard.GetState();

            if (oldKeyState == null)
                oldKeyState = currentKeyState;


            if (currentKeyState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (currentKeyState.IsKeyDown(Keys.W))
            {
                playerSprite.UpperLeft = playerSprite.UpperLeft + new Vector2(0, -5);
            }

            if (currentKeyState.IsKeyDown(Keys.S))
            {
                playerSprite.UpperLeft = playerSprite.UpperLeft + new Vector2(0, 5);
            }

            if (currentKeyState.IsKeyDown(Keys.A))
            {
                playerSprite.UpperLeft = playerSprite.UpperLeft + new Vector2(-5, 0);
            }

            if (currentKeyState.IsKeyDown(Keys.D))
            {
                playerSprite.UpperLeft = playerSprite.UpperLeft + new Vector2(5, 0);
            }

            if (currentKeyState.IsKeyDown(Keys.Space))
            {
                if (playerSprite.firingTimer <= 0)
                {
                    playerBulletSprite = new Sprite();
                    playerBulletSprite.SetTexture(playerBulletTexture);
                    playerBulletSprite.UpperLeft = new Vector2(playerSprite.UpperLeft.X + (playerSprite.GetWidth() / 2) - (playerBulletSprite.GetWidth() / 2), playerSprite.UpperLeft.Y);
                    playerBulletSprite.SetSpeedAndDirection(10, 90);

                    playerBullets.AddLast(playerBulletSprite);

                    playerSprite.firingTimer = playerSprite.firingDelay - 1;
                }
            }

            foreach (Sprite enemy in activeEnemies)
            {
                if (enemy.IsAlive)
                {
                    if (enemy.firingTimer == 0)
                    {
                        enemyBulletSprite = new Sprite();
                        enemyBulletSprite.SetTexture(enemyBulletTexture);
                        enemyBulletSprite.UpperLeft = new Vector2(enemy.UpperLeft.X + (enemy.GetWidth() / 2) - (enemyBulletSprite.GetWidth() / 2), enemy.UpperLeft.Y + enemy.GetHeight());
                        enemyBulletSprite.SetSpeedAndDirection(-5, -270);

                        enemyBullets.AddLast(enemyBulletSprite);

                        enemy.firingTimer = enemy.firingDelay + 1;
                    }
                    enemy.firingTimer -= 1;
                }
            }
            playerSprite.firingTimer -= 1;

            oldKeyState = currentKeyState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            playerSprite.Draw(spriteBatch);

            foreach (Sprite enemy in activeEnemies)
            {
                enemy.Draw(spriteBatch);
            }

            foreach (Sprite bullet in enemyBullets)
            {
                bullet.MoveAndVanish(1920, 1080);
                bullet.Draw(spriteBatch);

                if (bullet.IsCollided(playerSprite))
                {
                    bullet.IsAlive = false;
                    playerSprite.IsAlive = false;
                }
            }

            foreach (Sprite bullet in playerBullets)
            {
                bullet.MoveAndVanish(1920, 1080);
                bullet.Draw(spriteBatch);

                foreach (Sprite enemy in activeEnemies)
                {
                    if (bullet.IsCollided(enemy))
                    {
                        bullet.IsAlive = false;
                        enemy.IsAlive = false;
                    }
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
