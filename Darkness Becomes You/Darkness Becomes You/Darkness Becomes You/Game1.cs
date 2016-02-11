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

        public SpriteFont Calibri;

        public KeyboardState currentKeyState;
        public KeyboardState oldKeyState;

        public int lastSpriteCode = 0;

        public Sprite playerSprite;
        public Texture2D playerTexture;

        public LinkedList<Sprite> playerBullets = new LinkedList<Sprite>();

        public Sprite playerBulletSprite;
        public Texture2D playerBulletTexture;
        public int playerShotCooldown;

        public LinkedList<LinkedList<Sprite>> activeEnemies = new LinkedList<LinkedList<Sprite>>();

        public Sprite enemy1Sprite;
        public Texture2D enemy1Texture;
        public LinkedList<Sprite> activeEnemy1 = new LinkedList<Sprite>();

        public LinkedList<Sprite> enemyBullets = new LinkedList<Sprite>();

        public Sprite enemyBulletSprite;
        public Texture2D enemyBulletTexture;

        public double level = 0.5;
        public int elapsedTime = 1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            activeEnemies.AddFirst(activeEnemy1);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            Calibri = this.Content.Load<SpriteFont>("Fonts\\Calibri");
            playerTexture = this.Content.Load<Texture2D>("Textures\\Player");
            playerBulletTexture = this.Content.Load<Texture2D>("Textures\\PlayerBullet");
            enemy1Texture = this.Content.Load<Texture2D>("Textures\\Enemy1");
            enemyBulletTexture = this.Content.Load<Texture2D>("Textures\\EnemyBullet");

            playerSprite = new Sprite();
            playerSprite.SetTexture(playerTexture);
            playerSprite.UpperLeft = new Vector2((1920 / 2) - (playerSprite.GetWidth() / 2), (1080 / 2) - (playerSprite.GetHeight() / 2));
            playerSprite.LayerDepth = 0;
            playerSprite.firingDelay = 30;
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
            if (level == 0.5)
            {
                enemy1Sprite = new Sprite();
                enemy1Sprite.SetTexture(enemy1Texture);
                enemy1Sprite.Origin = enemy1Sprite.GetCenter();
                enemy1Sprite.UpperLeft = new Vector2(0 - enemy1Sprite.GetWidth(), 0 - enemy1Sprite.GetHeight());
                enemy1Sprite.firingDelay = 120;
                enemy1Sprite.firingSpeed = 5;
                enemy1Sprite.firingDirection = 270;
                enemy1Sprite.patternNum = 8;
                enemy1Sprite.movementPatterns = new double[enemy1Sprite.patternNum];
                enemy1Sprite.movementSpeeds = new double[enemy1Sprite.patternNum];
                enemy1Sprite.movementTimings = new double[enemy1Sprite.patternNum];
                enemy1Sprite.uniqueMovements = new bool[enemy1Sprite.patternNum];
                enemy1Sprite.movementRotation = new bool[enemy1Sprite.patternNum];

                enemy1Sprite.movementTimings[0] = 0;
                enemy1Sprite.uniqueMovements[0] = false;

                enemy1Sprite.movementPatterns[1] = -30;
                enemy1Sprite.movementSpeeds[1] = 3;
                enemy1Sprite.movementTimings[1] = 300;
                enemy1Sprite.movementRotation[1] = false;
                enemy1Sprite.uniqueMovements[1] = false;

                enemy1Sprite.movementPatterns[2] = -2;
                enemy1Sprite.movementSpeeds[2] = 3;
                enemy1Sprite.movementTimings[2] = 365;
                enemy1Sprite.movementRotation[2] = true;
                enemy1Sprite.uniqueMovements[2] = false;

                enemy1Sprite.movementPatterns[3] = 0;
                enemy1Sprite.movementSpeeds[3] = 3;
                enemy1Sprite.movementTimings[3] = 550;
                enemy1Sprite.movementRotation[3] = true;
                enemy1Sprite.uniqueMovements[3] = false;

                enemy1Sprite.movementPatterns[4] = -2;
                enemy1Sprite.movementSpeeds[4] = 3;
                enemy1Sprite.movementTimings[4] = 603;
                enemy1Sprite.movementRotation[4] = true;
                enemy1Sprite.uniqueMovements[4] = false;

                enemy1Sprite.movementPatterns[5] = 0;
                enemy1Sprite.movementSpeeds[5] = 2;
                enemy1Sprite.movementTimings[5] = 653;
                enemy1Sprite.movementRotation[5] = true;
                enemy1Sprite.uniqueMovements[5] = false;

                enemy1Sprite.movementPatterns[6] = 0;
                enemy1Sprite.movementSpeeds[6] = 0;
                enemy1Sprite.movementTimings[6] = 910;
                enemy1Sprite.movementRotation[6] = true;
                enemy1Sprite.uniqueMovements[6] = true;

                enemy1Sprite.movementPatterns[7] = 0;
                enemy1Sprite.movementSpeeds[7] = 2;
                enemy1Sprite.movementTimings[7] = 10000;
                enemy1Sprite.movementRotation[7] = true;
                enemy1Sprite.uniqueMovements[7] = false;

                activeEnemy1.AddLast(enemy1Sprite);


                enemy1Sprite = new Sprite();
                enemy1Sprite.SetTexture(enemy1Texture);
                enemy1Sprite.Origin = enemy1Sprite.GetCenter();
                enemy1Sprite.UpperLeft = new Vector2(1920, 0 - enemy1Sprite.GetHeight());
                enemy1Sprite.firingDelay = 120;
                enemy1Sprite.firingSpeed = 5;
                enemy1Sprite.firingDirection = 270;
                enemy1Sprite.patternNum = 8;
                enemy1Sprite.movementPatterns = new double[enemy1Sprite.patternNum];
                enemy1Sprite.movementSpeeds = new double[enemy1Sprite.patternNum];
                enemy1Sprite.movementTimings = new double[enemy1Sprite.patternNum];
                enemy1Sprite.uniqueMovements = new bool[enemy1Sprite.patternNum];
                enemy1Sprite.movementRotation = new bool[enemy1Sprite.patternNum];

                enemy1Sprite.movementTimings[0] = 0;
                enemy1Sprite.uniqueMovements[0] = false;

                enemy1Sprite.movementPatterns[1] = -150;
                enemy1Sprite.movementSpeeds[1] = 3;
                enemy1Sprite.movementTimings[1] = 300;
                enemy1Sprite.movementRotation[1] = false;
                enemy1Sprite.uniqueMovements[1] = false;

                enemy1Sprite.movementPatterns[2] = 2;
                enemy1Sprite.movementSpeeds[2] = 3;
                enemy1Sprite.movementTimings[2] = 365;
                enemy1Sprite.movementRotation[2] = true;
                enemy1Sprite.uniqueMovements[2] = false;

                enemy1Sprite.movementPatterns[3] = 0;
                enemy1Sprite.movementSpeeds[3] = 3;
                enemy1Sprite.movementTimings[3] = 550;
                enemy1Sprite.movementRotation[3] = true;
                enemy1Sprite.uniqueMovements[3] = false;

                enemy1Sprite.movementPatterns[4] = 2;
                enemy1Sprite.movementSpeeds[4] = 3;
                enemy1Sprite.movementTimings[4] = 603;
                enemy1Sprite.movementRotation[4] = true;
                enemy1Sprite.uniqueMovements[4] = false;

                enemy1Sprite.movementPatterns[5] = 0;
                enemy1Sprite.movementSpeeds[5] = 2;
                enemy1Sprite.movementTimings[5] = 653;
                enemy1Sprite.movementRotation[5] = true;
                enemy1Sprite.uniqueMovements[5] = false;

                enemy1Sprite.movementPatterns[6] = 0;
                enemy1Sprite.movementSpeeds[6] = 0;
                enemy1Sprite.movementTimings[6] = 910;
                enemy1Sprite.movementRotation[6] = true;
                enemy1Sprite.uniqueMovements[6] = true;

                enemy1Sprite.movementPatterns[7] = 0;
                enemy1Sprite.movementSpeeds[7] = 2;
                enemy1Sprite.movementTimings[7] = 10000;
                enemy1Sprite.movementRotation[7] = true;
                enemy1Sprite.uniqueMovements[7] = false;

                activeEnemy1.AddLast(enemy1Sprite);


                level = 1;
            }
            if (level == 1)
            {

            }

            foreach (Sprite enemy in activeEnemy1)
            {
                if (enemy.IsAlive)
                {
                    for (int i = 1; i < enemy.movementPatterns.Length; i++)
                    {
                        if ((elapsedTime <= enemy.movementTimings[i]) && ( elapsedTime >= enemy.movementTimings[i-1]))
                        {
                            if (enemy.movementRotation[i])
                            {
                                enemy.SetSpeedAndDirection(enemy.movementSpeeds[i], enemy.GetDirectionAngle() + enemy.movementPatterns[i]);
                            }
                            else
                            {
                                enemy.SetSpeedAndDirection(enemy.movementSpeeds[i], enemy.movementPatterns[i]);
                            }

                            if (enemy.uniqueMovements[i])
                                UniqueMonsterMovements(enemy, i);
                        }
                    }
                    if (enemy.firingTimer == 0)
                    {
                        enemyBulletSprite = new Sprite();
                        enemyBulletSprite.SetTexture(enemyBulletTexture);
                        enemyBulletSprite.Origin = enemyBulletSprite.GetCenter();
                        enemyBulletSprite.UpperLeft = new Vector2((enemy.UpperLeft.X + enemy.GetCenter().X) - (enemyBulletSprite.GetWidth() / 2), (enemy.UpperLeft.Y + enemy.GetCenter().Y) - (enemyBulletSprite.GetHeight() / 2));
                        enemyBulletSprite.SetSpeedAndDirection(enemy.firingSpeed, enemy.firingDirection);

                        enemyBullets.AddLast(enemyBulletSprite);

                        enemy.firingTimer = enemy.firingDelay + 1;
                    }
                    enemy.Move();

                    enemyBulletSprite.RotationAngle = enemyBulletSprite.GetDirectionAngle() + 90;
                    enemy.RotationAngle = enemy.GetDirectionAngle() + 90;

                    enemy.firingTimer -= 1;
                }
            }
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

            playerSprite.firingTimer -= 1;
            elapsedTime += 1;
            oldKeyState = currentKeyState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            foreach (Sprite bullet in playerBullets)
            {
                bullet.MoveAndVanish(1920, 1080);
                bullet.Draw(spriteBatch);

                foreach (LinkedList<Sprite> list in activeEnemies)
                {
                    foreach (Sprite enemy in list)
                    {
                        if (bullet.IsCollided(enemy))
                        {
                            bullet.IsAlive = false;
                            enemy.IsAlive = false;
                        }
                    }
                }
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

            foreach (LinkedList<Sprite> list in activeEnemies)
            {
                foreach (Sprite enemy in list)
                {
                    enemy.Draw(spriteBatch);
                }
            }

            playerSprite.Draw(spriteBatch);

            spriteBatch.DrawString(Calibri, elapsedTime.ToString(), new Vector2(10, 10), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }


        public void UniqueMonsterMovements(Sprite enemy, int i)
        {
            if (enemy.firingDelay == 60)
                return;

            int q = (int)enemy.UpperLeft.X;
            int r = (int)enemy.UpperLeft.Y;
            if (((q == 1798) && (r == 494)) || (q == 71) && (r == 494))
            {
                if (i == 6)
                {
                    if ((enemy.firingDelay != 180) && (enemy.firingDelay != 15))
                    {
                        enemy.firingDelay = 180;
                        enemy.firingTimer = enemy.firingDelay;
                        enemy.firingDirection = 15;
                    }

                    if (enemy.firingTimer == 0)
                    {
                        enemyBulletSprite = new Sprite();
                        enemyBulletSprite.SetTexture(enemyBulletTexture);
                        enemyBulletSprite.Origin = enemyBulletSprite.GetCenter();
                        enemyBulletSprite.UpperLeft = new Vector2((enemy.UpperLeft.X + enemy.GetCenter().X) - (enemyBulletSprite.GetWidth() / 2), (enemy.UpperLeft.Y + enemy.GetCenter().Y) - (enemyBulletSprite.GetHeight() / 2));
                        enemyBulletSprite.SetSpeedAndDirection(enemy.firingSpeed, Sprite.CalculateDirectionAngle(new Vector2((playerSprite.UpperLeft.X + playerSprite.GetWidth()) - (enemyBulletSprite.UpperLeft.X + enemyBulletSprite.GetWidth()), (playerSprite.UpperLeft.Y + playerSprite.GetWidth() / 2) - (enemyBulletSprite.UpperLeft.Y + enemyBulletSprite.GetWidth() / 2))) + enemy.firingDirection);
                        enemyBulletSprite.RotationAngle = enemyBulletSprite.GetDirectionAngle() + 90;

                        enemyBullets.AddLast(enemyBulletSprite);

                        if (enemy.firingDelay != 15)
                            enemy.firingDelay = 15;

                        enemy.firingDirection -= 15;
                        enemy.firingTimer = enemy.firingDelay;

                        if (enemy.firingDirection == -30)
                        {
                            enemy.firingDelay = 60;
                            enemy.firingDirection = 270;
                            enemy.firingTimer = enemy.firingDelay;
                        }
                    }
                }
            }
        }
    }
}
