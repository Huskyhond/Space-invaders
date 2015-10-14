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
        Texture2D background_stars;
        SpriteFont font;
        Player player;
        int windowHeight, windowWidth;
        int astroiddestroyed;
        Weapon<Bullet> weapon;

        float deltaTime = 0.0f;

        Instruction astroidRain =
            new While(
                new Semicolon(new Wait(1000),
                    new Semicolon(
                        new For(0, 10,
                            new Semicolon(
                                new CreateAstroid(),
                                new Wait(500)
                            )
                        ), new Wait(500)
                    )
                )
           );

        //Lists
        List<Astroid> astroids  = new List<Astroid>();
        List<Bullet> bullets = new List<Bullet>();
        List<Powerup> powerups = new List<Powerup>();
        List<Astroid> toBeRemoved = new List<Astroid>();
        List<Bullet> bulletrain = new List<Bullet>();

        //Background_properties
        private ScrollingBackground background = new ScrollingBackground();
        private Vector2 screenpos;

        // Virtual pc
        int vpcstate = 0;
        int line1AmountOfAstroids, iLine1;
        float astroidWaitLine2, astroidWaitLine3;

        //settings
        const int minShotDelay = 5; // frames
        int shotDelay = 0;
        //float scrollspeed;

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
            Health health = new Health(Content.Load<Texture2D>("healthBar.png"));
            player = new Player(Content.Load<Texture2D>("Fighter_small.png"), health);
            player.health.position = new Vector2((windowWidth * 80 /100), (windowHeight * 95 / 100));
            player.position = new Vector2((float)windowWidth / 2, (float)(windowHeight * 0.80));
            player.controller = new CombineController(new MouseController(), new KeyboardController());
            Content.Load<SoundEffect>("player_shoot");
            Content.Load<Texture2D>("player_bullet_left");
            weapon = new SingleBlaster(Content, player.position);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background_stars = Content.Load<Texture2D>("background_stars.png");
            background.Load(GraphicsDevice, background_stars);
            font = Content.Load<SpriteFont>("GameFont");
            Content.Load<Texture2D>("asteroid.png");
            // TODO: use this.Content to load your game content here
        }

        public void Update_Background(float deltaY)
        {
            screenpos.Y += deltaY;
        }

        protected override void Update(GameTime gameTime)
        {
            deltaTime = ((float)gameTime.ElapsedGameTime.TotalMilliseconds - deltaTime);
            if (shotDelay > 0)
                shotDelay--;
            if (player.health.amount < 1)
                    Exit();
            if (player.controller.exit())
                Exit();
            if (player.controller.shooting())
                weapon.PullTrigger();

            switch (astroidRain.Execute(deltaTime))
            {
                case InstructionResult.Running:
                    break;
                case InstructionResult.Done:
                    break;
                case InstructionResult.RunningAndCreateAstroid:
                case InstructionResult.DoneAndCreateAstroid:
                    astroids.Add(new Astroid(Content.Load<Texture2D>("asteroid.png"), new Vector2((float)random.Next(windowWidth), 0.0f), new Vector2((float)random.NextDouble() * 2 - 1, (float)random.NextDouble() * 2 + 2), 5));
                    break;
                default:
                    break;
            }

            List<Astroid> rain = GenerateRain();

            // COMMIT CHANGES
 
            List<Powerup> poweruprain = UpdatePowerup();

            astroids = rain;

            powerups = poweruprain;
   
            background.Update(deltaTime / 5);

            bullets = UpdateBullets(bullets);

            player.controller.update(deltaTime, player);
            bullets.AddRange(weapon.newBullets);
            weapon.Update(deltaTime, player.position);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            spriteBatch.Draw(player.texture, player.position, Color.White);
            foreach (Bullet bullet in bullets)
                bullet.Draw(spriteBatch);
            foreach (Astroid astroid in astroids)
                astroid.Draw(spriteBatch);
            foreach (Powerup powerup in powerups)
                powerup.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Score: " + player.score, new Vector2(player.health.position.X, player.health.position.Y-20.0f), Color.White);
            player.health.Draw(spriteBatch);
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
                        player.score += 500;
                        
                    return true;
                }
            }
            return false;
        }

        public bool AstroidPlayerCollisionDetection(Astroid astroid)
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
            return false;
        }

        public bool PowerupPlayerCollisionDetection(Powerup powerup)
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
            if (player.powerupcounter <= 2)
            {
                switch(player.powerupcounter) {
                    case 1:
                        weapon = new DoubleBlaster(Content, player.position);
                    break;
                    case 2:
                        weapon = new TripleBlaster(Content, player.position);
                    break;
                }
            }
        }
    }
}
