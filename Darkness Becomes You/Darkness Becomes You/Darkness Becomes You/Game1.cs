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

        public Sprite playerBulletSprite;
        public Texture2D playerBulletTexture;
        public LinkedList<Sprite> playerBullets = new LinkedList<Sprite>();
        public int playerShotCooldown;

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
            playerBulletTexture = this.Content.Load<Texture2D>("Textures\\Bullet");

            playerSprite = new Sprite();
            playerSprite.UpperLeft = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            playerSprite.SetTexture(playerTexture);

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
                if (playerShotCooldown == 0)
                {
                    playerBulletSprite = new Sprite();
                    playerBulletSprite.SetTexture(playerBulletTexture);
                    playerBulletSprite.UpperLeft = new Vector2(playerSprite.UpperLeft.X + (playerSprite.GetWidth() / 2) - (playerBulletSprite.GetWidth() / 2), playerSprite.UpperLeft.Y);
                    playerBulletSprite.SetSpeedAndDirection(10, 90);

                    playerBullets.AddLast(playerBulletSprite);

                    playerShotCooldown = 30;
                }
                else
                    playerShotCooldown -= 1;

            }


            oldKeyState = currentKeyState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            playerSprite.Draw(spriteBatch);

            foreach (Sprite bullet in playerBullets)
            {
                bullet.MoveAndVanish(1920, 1080);
                bullet.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
