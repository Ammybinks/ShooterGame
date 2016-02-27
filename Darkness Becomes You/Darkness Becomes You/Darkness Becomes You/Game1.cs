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

        public LinkedList<LinkedList<LinkedList<Sprite>>> activeSprites = new LinkedList<LinkedList<LinkedList<Sprite>>>();

        public LinkedList<LinkedList<Sprite>> activeFriendlies = new LinkedList<LinkedList<Sprite>>();

        public LinkedList<Sprite> activePlayers = new LinkedList<Sprite>();

        public LinkedList<LinkedList<Sprite>> activeBullets = new LinkedList<LinkedList<Sprite>>();

        public LinkedList<Sprite> friendlyBullets = new LinkedList<Sprite>();
        public LinkedList<Sprite> enemyBullets = new LinkedList<Sprite>();

        public LinkedList<LinkedList<Sprite>> activeEnemies = new LinkedList<LinkedList<Sprite>>();

        public LinkedList<Sprite> activeEnemy1 = new LinkedList<Sprite>();


        public SpriteFont Calibri;


        public KeyboardState currentKeyState;
        public KeyboardState oldKeyState;


        public Friendly playerSprite;
        public Texture2D playerTexture;


        public Bullet playerBulletSprite;
        public Texture2D playerBulletTexture;

        public EnemyBullet enemyBulletSprite;
        public Texture2D enemyBulletTexture;


        public Enemy enemy1Sprite;
        public Texture2D enemy1Texture;


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

            activeSprites.AddLast(activeFriendlies);

            activeFriendlies.AddLast(activePlayers);

            activeSprites.AddLast(activeBullets);

            activeBullets.AddLast(friendlyBullets);
            activeBullets.AddLast(enemyBullets);


            activeSprites.AddLast(activeEnemies);

            activeEnemies.AddLast(activeEnemy1);


            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();


            Calibri = this.Content.Load<SpriteFont>("Fonts\\Calibri");
            playerTexture = this.Content.Load<Texture2D>("Textures\\Player");
            playerBulletTexture = this.Content.Load<Texture2D>("Textures\\PlayerBullet");
            enemy1Texture = this.Content.Load<Texture2D>("Textures\\Enemy1");
            enemyBulletTexture = this.Content.Load<Texture2D>("Textures\\EnemyBullet");


            playerSprite = new Friendly();
            playerSprite.SetTexture(playerTexture);
            playerSprite.UpperLeft = new Vector2((1920 / 2) - (playerSprite.GetWidth() / 2), (1080 / 2) - (playerSprite.GetHeight() / 2));
            playerSprite.LayerDepth = 0;
            playerSprite.firingDelay = 30;

            activePlayers.AddLast(playerSprite);
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
                enemy1Sprite = new Enemy();
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
                enemy1Sprite.movementSpeeds[1] = 2;
                enemy1Sprite.movementTimings[1] = 235;
                enemy1Sprite.movementRotation[1] = false;
                enemy1Sprite.uniqueMovements[1] = false;

                enemy1Sprite.movementPatterns[2] = -1;
                enemy1Sprite.movementSpeeds[2] = 2;
                enemy1Sprite.movementTimings[2] = 300;
                enemy1Sprite.movementRotation[2] = true;
                enemy1Sprite.uniqueMovements[2] = false;

                enemy1Sprite.movementPatterns[3] = 0;
                enemy1Sprite.movementSpeeds[3] = 2;
                enemy1Sprite.movementTimings[3] = 440;
                enemy1Sprite.movementRotation[3] = true;
                enemy1Sprite.uniqueMovements[3] = false;

                enemy1Sprite.movementPatterns[4] = -1;
                enemy1Sprite.movementSpeeds[4] = 2;
                enemy1Sprite.movementTimings[4] = 495;
                enemy1Sprite.movementRotation[4] = true;
                enemy1Sprite.uniqueMovements[4] = false;

                enemy1Sprite.movementPatterns[5] = 90;
                enemy1Sprite.movementSpeeds[5] = 2;
                enemy1Sprite.movementTimings[5] = 535;
                enemy1Sprite.movementRotation[5] = false;
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


                enemy1Sprite = new Enemy();
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
                enemy1Sprite.movementSpeeds[1] = 2;
                enemy1Sprite.movementTimings[1] = 235;
                enemy1Sprite.movementRotation[1] = false;
                enemy1Sprite.uniqueMovements[1] = false;

                enemy1Sprite.movementPatterns[2] = 1;
                enemy1Sprite.movementSpeeds[2] = 2;
                enemy1Sprite.movementTimings[2] = 300;
                enemy1Sprite.movementRotation[2] = true;
                enemy1Sprite.uniqueMovements[2] = false;

                enemy1Sprite.movementPatterns[3] = 0;
                enemy1Sprite.movementSpeeds[3] = 2;
                enemy1Sprite.movementTimings[3] = 440;
                enemy1Sprite.movementRotation[3] = true;
                enemy1Sprite.uniqueMovements[3] = false;

                enemy1Sprite.movementPatterns[4] = 1;
                enemy1Sprite.movementSpeeds[4] = 2;
                enemy1Sprite.movementTimings[4] = 495;
                enemy1Sprite.movementRotation[4] = true;
                enemy1Sprite.uniqueMovements[4] = false;

                enemy1Sprite.movementPatterns[5] = 90;
                enemy1Sprite.movementSpeeds[5] = 2;
                enemy1Sprite.movementTimings[5] = 535;
                enemy1Sprite.movementRotation[5] = false;
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
            foreach (LinkedList<Sprite> list in activeEnemies)
            {
                foreach (Enemy enemy in list)
                {
                    if (enemy.IsAlive)
                    {
                        for (int i = 1; i < enemy.movementPatterns.Length; i++)
                        {
                            if ((elapsedTime <= enemy.movementTimings[i]) && (elapsedTime >= enemy.movementTimings[i - 1]))
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
                                    UniqueEnemyMovements(enemy, i);
                            }
                        }
                        if (enemy.firingTimer == 0)
                        {
                            enemyBulletSprite = new EnemyBullet();
                            enemyBulletSprite.SetTexture(enemyBulletTexture);
                            enemyBulletSprite.SetAll(enemy);

                            enemyBullets.AddLast(enemyBulletSprite);

                            enemy.firingTimer = enemy.firingDelay + 1;
                        }

                        enemyBulletSprite.RotationAngle = enemyBulletSprite.GetDirectionAngle() + 90;
                        enemy.RotationAngle = enemy.GetDirectionAngle() + 90;

                        enemy.firingTimer -= 1;
                    }
                }
            }


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
                    playerBulletSprite = new Bullet();
                    playerBulletSprite.SetTexture(playerBulletTexture);
                    playerBulletSprite.UpperLeft = new Vector2(playerSprite.UpperLeft.X + (playerSprite.GetWidth() / 2) - (playerBulletSprite.GetWidth() / 2), playerSprite.UpperLeft.Y);
                    playerBulletSprite.SetSpeedAndDirection(10, 90);

                    friendlyBullets.AddLast(playerBulletSprite);

                    playerSprite.firingTimer = playerSprite.firingDelay - 1;
                }
            }


            foreach (LinkedList<LinkedList<Sprite>> listOfLists in activeSprites)
            {
                foreach (LinkedList<Sprite> list in listOfLists)
                {
                    foreach (Unit sprite in list)
                    {
                        sprite.MoveAndVanish(1920, 1080);

                        foreach (LinkedList<LinkedList<Sprite>> targetListOfLists in activeSprites)
                        {
                            foreach (LinkedList<Sprite> targetList in targetListOfLists)
                            {
                                foreach (Unit target in targetList)
                                {
                                    if (sprite.friendly != target.friendly)
                                    {
                                        if (sprite.IsCollided(target))
                                        {
                                            sprite.health -= target.damage;
                                            target.health -= sprite.health;

                                            if (sprite.health <= 0)
                                            {
                                                sprite.Cull();
                                            }
                                            if (target.health <= 0)
                                            {
                                                target.Cull();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
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

            foreach (LinkedList<LinkedList<Sprite>> listOfLists in activeSprites)
            {
                foreach (LinkedList<Sprite> list in listOfLists)
                {
                    foreach (Sprite sprite in list)
                    {
                        sprite.Draw(spriteBatch);
                    }
                }
            }

            spriteBatch.DrawString(Calibri, elapsedTime.ToString(), new Vector2(10, 10), Color.Black);

            foreach (Sprite enemy in activeEnemy1)
            {
                spriteBatch.DrawString(Calibri, "X: " + enemy.UpperLeft.X + ", Y: " + enemy.UpperLeft.Y, new Vector2(enemy.UpperLeft.X - 100, enemy.UpperLeft.Y - 200), Color.Black);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }


        public void UniqueEnemyMovements(Enemy enemy, int i)
        {
            int q = (int)enemy.UpperLeft.X;
            int r = (int)enemy.UpperLeft.Y;
            if (((q == 63) && (r == 484)) || (q == 1806) && (r == 484))
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
                        enemyBulletSprite = new EnemyBullet();
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
                                enemy.firingDelay = -1;
                                enemy.firingTimer = enemy.firingDelay;
                        }
                    }
                }
            }
        }
    }
}
