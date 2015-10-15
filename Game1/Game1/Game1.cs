using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Game1.GameControllers;
using Game1.Scripts;

namespace Game1
{
    /// <summary>
    /// Simple Space shooter
    /// </summary>
    public class Game1 : Game
    {
        Random random = new Random();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        //Player player;
        public int amountOfPlayers = 0;
        public List<ControllerOptions> PlayerControllerChoices;
        int windowHeight, windowWidth;
        int astroiddestroyed;

        List<Player> DefaultPlayerSettings = new List<Player>();

        float deltaTime = 0.0f;

        //Lists
        List<Player> Players = new List<Player>();
        List<Astroid> astroids  = new List<Astroid>();
        List<Bullet> bullets = new List<Bullet>();
        List<Powerup> powerups = new List<Powerup>();
        List<Astroid> toBeRemoved = new List<Astroid>();
        List<Bullet> bulletrain = new List<Bullet>();

        //Background_properties
        private ScrollingBackground background = new ScrollingBackground();

        //float scrollspeed;

        Instruction astroidRain;

        public GameController getController(ControllerOptions controller)
        {
            switch (controller)
            {
                case ControllerOptions.Mouse:
                    return new MouseController();
                case ControllerOptions.Keyboard:
                    return new KeyboardController();
                case ControllerOptions.GamePad:
                    return new GamePadController();
                default:
                    return new MouseController();
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            Rectangle b = GraphicsDevice.Viewport.Bounds;
            windowWidth = b.Width;
            windowHeight = b.Height;

            // Preset players:
            Player p1 = new Player(Content.Load<Texture2D>("Fighter_small.png"), new Health(Content.Load<Texture2D>("healthBar.png")), "Player 1");
            p1.health.position = new Vector2(10.0f, (windowHeight * 95 / 100));
            p1.position = new Vector2((windowWidth/2)-80, windowHeight/2);

            Player p2 = new Player(Content.Load<Texture2D>("Fighter_small.png"), new Health(Content.Load<Texture2D>("healthBar.png")), "Player 2");
            p2.health.position = new Vector2((windowWidth * 80 / 100), (windowHeight * 95 / 100));
            p2.position = new Vector2((windowWidth / 2) - 50, windowHeight / 2);
            Player p3 = new Player(Content.Load<Texture2D>("Fighter_small.png"), new Health(Content.Load<Texture2D>("healthBar.png")), "Player 3");
            p3.health.position = new Vector2(10.0f, (windowHeight * 5 / 100));
            p3.position = new Vector2((windowWidth / 2) - 20, windowHeight / 2);

            Player p4 = new Player(Content.Load<Texture2D>("Fighter_small.png"), new Health(Content.Load<Texture2D>("healthBar.png")), "Player 4");
            p4.health.position = new Vector2((windowWidth * 80 / 100), (windowHeight * 5 / 100));
            p4.position = new Vector2((windowWidth / 2) + 10, windowHeight / 2);

            DefaultPlayerSettings.Add(p1);
            DefaultPlayerSettings.Add(p2);
            DefaultPlayerSettings.Add(p3);
            DefaultPlayerSettings.Add(p4);

            for (int i = 0; i < amountOfPlayers; i++)
            {
                Player ptoAdd = DefaultPlayerSettings[i];
                ptoAdd.controller = getController(PlayerControllerChoices[i]);
                ptoAdd.weapon = new SingleBlaster(Content, ptoAdd);
                Players.Add(ptoAdd);

            }

            astroidRain =
                new While(
                    new Semicolon(new Wait(100),
                        new Semicolon(
                            new For(0, (random.Next(50, 300)),
                                new Semicolon(
                                    new CreateAstroid(),
                                    new Wait(100)
                                )
                            ), new Wait(5000)
                        )
                    )
               );
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background.Load(GraphicsDevice, Content.Load<Texture2D>("background_stars.png"));
            font = Content.Load<SpriteFont>("GameFont");
            Content.Load<Texture2D>("asteroid.png");
            Content.Load<SoundEffect>("player_shoot");
            Content.Load<Texture2D>("player_bullet_left");
            Content.Load<Texture2D>("Fighter_small.png");
            Content.Load<Texture2D>("healthBar.png");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            deltaTime = ((float)gameTime.ElapsedGameTime.TotalMilliseconds - deltaTime);
            List<Player> survivingPlayers = new List<Player>();
            foreach (Player player in Players)
            {
                /* Controller logic */
                if (player.controller.shooting())
                    player.weapon.PullTrigger();
                if (player.controller.exit() || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                /* Player logic */
                if (player.health.amount > 0)
                    survivingPlayers.Add(player);
            }

            switch (astroidRain.Execute(deltaTime))
            {
                case InstructionResult.RunningAndCreateAstroid:
                case InstructionResult.DoneAndCreateAstroid:
                    astroids.Add(new Astroid(Content.Load<Texture2D>("asteroid.png"), new Vector2((float)random.Next(windowWidth), 0.0f), new Vector2((float)random.NextDouble() * 2 - 1, (float)random.NextDouble() * 2 + 2), 5));
                    break;
                default:
                    break;
            }

            List<Astroid> currentAstroidStream = GenerateRain();
            List<Powerup> poweruprain = UpdatePowerup();

            // COMMIT CHANGES

            astroids = currentAstroidStream;
            powerups = poweruprain;
            Players = survivingPlayers;

            if (survivingPlayers.Count < 1)
                Exit();

            foreach (Player player in Players)
            {
                bullets.AddRange(player.weapon.newBullets);
                player.controller.update(deltaTime, player);
                player.weapon.Update(deltaTime, player);
            }
            bullets = UpdateBullets(bullets);
            background.Update(deltaTime / 5);         
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            foreach (Player player in Players)
            {
                spriteBatch.Draw(player.texture, player.position, Color.White);
                player.health.Draw(spriteBatch);
            }
            foreach (Bullet bullet in bullets)
                bullet.Draw(spriteBatch);
            foreach (Astroid astroid in astroids)
                astroid.Draw(spriteBatch);
            foreach (Powerup powerup in powerups)
                powerup.Draw(spriteBatch);
                
            switch (Players.Count())
            {
                case 1:
                    spriteBatch.DrawString(font, "Score: " + Players[0].score, new Vector2(Players[0].health.position.X, Players[0].health.position.Y - 20.0f), Color.White);
                    spriteBatch.DrawString(font, Players[0].player_name, new Vector2(Players[0].health.position.X, Players[0].health.position.Y), Color.White);
                    break;
                case 2:
                    spriteBatch.DrawString(font, "Score: " + Players[0].score, new Vector2(Players[0].health.position.X, Players[0].health.position.Y - 20.0f), Color.White);
                    spriteBatch.DrawString(font, "Score: " + Players[1].score, new Vector2(Players[1].health.position.X, Players[1].health.position.Y - 20.0f), Color.White);
                    spriteBatch.DrawString(font, Players[0].player_name, new Vector2(Players[0].health.position.X, Players[0].health.position.Y), Color.White);
                    spriteBatch.DrawString(font, Players[1].player_name, new Vector2(Players[1].health.position.X, Players[1].health.position.Y), Color.White);
                    break;
                case 3:
                    spriteBatch.DrawString(font, "Score: " + Players[0].score, new Vector2(Players[0].health.position.X, Players[0].health.position.Y - 20.0f), Color.White);
                    spriteBatch.DrawString(font, "Score: " + Players[1].score, new Vector2(Players[1].health.position.X, Players[1].health.position.Y - 20.0f), Color.White);
                    spriteBatch.DrawString(font, "Score: " + Players[2].score, new Vector2(Players[2].health.position.X, Players[2].health.position.Y + 20.0f), Color.White);
                    spriteBatch.DrawString(font, Players[0].player_name, new Vector2(Players[0].health.position.X, Players[0].health.position.Y), Color.White);
                    spriteBatch.DrawString(font, Players[1].player_name, new Vector2(Players[1].health.position.X, Players[1].health.position.Y), Color.White);
                    spriteBatch.DrawString(font, Players[2].player_name, new Vector2(Players[2].health.position.X, Players[2].health.position.Y), Color.White);
                    break;
                case 4:
                    spriteBatch.DrawString(font, "Score: " + Players[0].score, new Vector2(Players[0].health.position.X, Players[0].health.position.Y - 20.0f), Color.White);
                    spriteBatch.DrawString(font, "Score: " + Players[1].score, new Vector2(Players[1].health.position.X, Players[1].health.position.Y - 20.0f), Color.White);
                    spriteBatch.DrawString(font, "Score: " + Players[2].score, new Vector2(Players[2].health.position.X, Players[2].health.position.Y + 20.0f), Color.White);
                    spriteBatch.DrawString(font, "Score: " + Players[3].score, new Vector2(Players[3].health.position.X, Players[3].health.position.Y + 20.0f), Color.White);
                    spriteBatch.DrawString(font, Players[0].player_name, new Vector2(Players[0].health.position.X, Players[0].health.position.Y), Color.White);
                    spriteBatch.DrawString(font, Players[1].player_name, new Vector2(Players[1].health.position.X, Players[1].health.position.Y), Color.White);
                    spriteBatch.DrawString(font, Players[2].player_name, new Vector2(Players[2].health.position.X, Players[2].health.position.Y), Color.White);
                    spriteBatch.DrawString(font, Players[3].player_name, new Vector2(Players[3].health.position.X, Players[3].health.position.Y), Color.White);
                    break;
                default:
                    break;
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public bool AstroidBulletCollisionDetection(Bullet bullet, Astroid astroid)
        {
            float bulletMinY = bullet.position.Y;
            float bulletMaxY = (bullet.position.Y + bullet.texture.Bounds.Height);

            float bulletMinX = bullet.position.X;
            float bulletMaxX = (bullet.position.X + bullet.texture.Bounds.Width);
        
            float astroidMinX = astroid.position.X;
            float astroidMaxX = (astroid.position.X + astroid.texture.Bounds.Width);

            float astroidMinY = astroid.position.Y;
            float astroidMaxY = (astroid.position.Y + astroid.texture.Bounds.Height);

            if (astroidMinX < bulletMinX && astroidMaxX > bulletMinX && astroidMinX < bulletMaxX && astroidMaxX > bulletMaxX)
            {
                if (astroidMinY < bulletMinY && astroidMaxY > bulletMinY && astroidMinY < bulletMaxY && astroidMaxY > bulletMaxY)
                {
                    astroid.health--;
                    if (astroid.health < 1)  
                        bullet.shotBy.score += 500;
                        
                    return true;
                }
            }
            return false;
        }

        public bool AstroidPlayerCollisionDetection(Astroid astroid)
        {
            foreach (Player player in Players)
            {
                float playerMinX = player.position.X;
                float playerMaxX = (player.position.X + player.texture.Bounds.Width);

                float playerMinY = player.position.Y;
                float playerMaxY = (player.position.Y + player.texture.Bounds.Height);


                float astroidMinX = astroid.position.X;
                float astroidMaxX = (astroid.position.X + astroid.texture.Bounds.Width);

                float astroidMinY = astroid.position.Y;
                float astroidMaxY = (astroid.position.Y + astroid.texture.Bounds.Height);

                float middroidX = (astroidMinX + ((astroidMaxX - astroidMinX) / 2));
                float middroidY = (astroidMinY + ((astroidMaxY - astroidMinY) / 2));

                if (middroidX > playerMinX && middroidX < playerMaxX)
                {
                    if (middroidY > playerMinY && middroidY < playerMaxY)
                    {
                        player.health.amount--;
                        astroid.health = 0;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PowerupPlayerCollisionDetection(Powerup powerup)
        {
            foreach (Player player in Players)
            {
                float playerMinX = player.position.X;
                float playerMaxX = (player.position.X + player.texture.Bounds.Width);

                float playerMinY = player.position.Y;
                float playerMaxY = (player.position.Y + player.texture.Bounds.Height);

                float powerupMinY = powerup.position.Y;
                float powerupMaxY = (powerup.position.Y + powerup.texture.Bounds.Height);

                float powerupMinX = powerup.position.X;
                float powerupMaxX = (powerup.position.X + powerup.texture.Bounds.Width);

                float midpowerX = (powerupMinX + ((powerupMaxX - powerupMinX) / 2));
                float midpowerY = (powerupMinY + ((powerupMaxY - powerupMinY) / 2));

                if (midpowerX > playerMinX && midpowerX < playerMaxX)
                {
                    if (midpowerY > playerMinY && midpowerY < playerMaxY)
                    {
                        if (player.powerupcounter == 0)
                            player.powerupcounter++;
                        else if (player.powerupcounter == 1)
                            player.powerupcounter++;
                        else if (player.powerupcounter == 2)
                            player.powerupcounter++;
                        else if (player.powerupcounter == 3)
                            player.powerupcounter++;
                        else if (player.powerupcounter == 4)
                            player.powerupcounter++;
                        PowerupChecker();
                        return true;
                    }
                }
            }
            return false;
        }

        public List<Astroid> GenerateRain()
        {
            //AstroidStateChanger();
            var currentRain = (from astroid in astroids
                               let colliders = 
                               from bullet in bullets
                               where AstroidBulletCollisionDetection(bullet, astroid)
                               select bullet
                               where astroid.position.Y <= windowHeight &&
                                     astroid.position.X <= windowWidth &&
                                     astroid.position.X > -astroid.texture.Width &&
                                     astroid.position.Y > -astroid.texture.Height &&
                                     astroid.health > 0 &&
                                     !AstroidPlayerCollisionDetection(astroid)
                               select astroid).ToList();
            foreach (Astroid astroid in astroids.Except(currentRain))
            {
                astroiddestroyed++;
                if (random.NextDouble() > 0.9)
                {
                    Powerup newPowerup = Generate_Powerup(astroid.position);
                    powerups.Add(newPowerup);
                }
            }
            foreach (var astroid in currentRain)
                astroid.position += astroid.velocity;
            return currentRain;
        }

        public List<Bullet> UpdateBullets(List<Bullet> bullets)
        {
            var currentBullet = (from bullet in bullets
                                 let colliders =
                                 from astroid in astroids
                                 where AstroidBulletCollisionDetection(bullet, astroid)
                                 select astroid
                                 where bullet.position.X <= windowWidth &&
                                       bullet.position.Y <= windowHeight &&
                                       bullet.position.X > -bullet.texture.Width &&
                                       bullet.position.Y > -bullet.texture.Height &&
                                       colliders.Count() == 0
                                 select bullet).ToList<Bullet>();
            foreach (var bullet in currentBullet)
                bullet.position += bullet.velocity;
            return currentBullet;
        }

        public Powerup Generate_Powerup(Vector2 position)
        {
            Powerup newPowerup = new Powerup(position, new Vector2(0, 3), Content.Load<Texture2D>("powerup1.png"));
            return newPowerup;
        }

        public List<Powerup> UpdatePowerup()
        {
            var currentPowerups = (from powerup in powerups
                                   where powerup.position.Y <= windowHeight &&
                                         powerup.position.X <= windowWidth &&
                                         powerup.position.X > -powerup.texture.Width &&
                                         powerup.position.Y > -powerup.texture.Height &&
                                         !PowerupPlayerCollisionDetection(powerup)
                                   select powerup).ToList();
            foreach (var powerup in currentPowerups)
                powerup.position += powerup.velocity;
            return currentPowerups;
        }

        public void PowerupChecker()
        {
            foreach (Player player in Players)
            {
                if (player.powerupcounter <= 2)
                {
                    switch (player.powerupcounter)
                    {
                        case 1:
                            player.weapon = new DoubleBlaster(Content, player);
                            break;
                        case 2:
                            player.weapon = new TripleBlaster(Content, player);
                            break;
                    }
                }
            }
        }
    }
}
