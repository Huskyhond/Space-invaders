using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq; 
using System.Collections.Generic;

namespace Game1
{
    // Joey heeft ook een commit gedaan wajow
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        Random random = new Random();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D player_texture, background_stars, player_bullet_texture, enemy_astroid, healthbar, powerup_texture;
        Player player;
        Health health;
        int windowHeight, windowWidth;
        List<Astroid> astroids = new List<Astroid>();
        List<Bullet_Path> bullet_paths = new List<Bullet_Path>();
        List<Bullet> bullets = new List<Bullet>();
        List<Powerup> powerups = new List<Powerup>();
        List<Astroid> toBeRemoved = new List<Astroid>();

        float deltaTime = 0.0f;

        //Background_properties
        private ScrollingBackground background = new ScrollingBackground();
        private Vector2 screenpos;
        private Texture2D mytexture;

        // Virtual pc
        int vpcstate = 0;
        int line1AmountOfAstroids, iLine1;
        float astroidWaitLine2, astroidWaitLine3;

        //settings
        const int minShotDelay = 5; // frames
        int shotDelay = 0;

        public void AstroidStateChanger()
        {
            switch (vpcstate)
            {
                case 0:
                    vpcstate = 1;
                    line1AmountOfAstroids = random.Next(10, 100);
                    iLine1 = 0;
                    astroidWaitLine3 = (float)random.Next(5000, 7000);
                    astroidWaitLine2 = 0;
                    break;
                case 1:
                    if(line1AmountOfAstroids > iLine1)
                        vpcstate = 2;
                    else
                        vpcstate = 3;
                    break;
                case 2:
                    astroidWaitLine2 -= deltaTime;
                    if (astroidWaitLine2 <= 0)
                    {
                        Astroid astroid = new Astroid(enemy_astroid);
                        astroids.Add(new Astroid(enemy_astroid, new Vector2((float)random.Next(windowWidth), 0.0f), new Vector2(0.0f, 1.0f)));
                        iLine1++;
                        vpcstate = 1;
                        astroidWaitLine2 = (float)(random.NextDouble() * 0.2 + 50);
                    }
                    break;
                case 3:
                    astroidWaitLine3 -= deltaTime;
                    if (astroidWaitLine3 <= 0)
                        vpcstate = 0;
                    break;

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
            health = new Health(healthbar);
            player = new Player(player_texture, health, bullet_paths);
            player.health.position = new Vector2(600, 450);
            bullet_paths.Add(new Bullet_Path(player.position, new Vector2(0, -10.0f), new List<Bullet>(), new Vector2(0, 0)));
            bullet_paths.Add(new Bullet_Path(player.position, new Vector2(0, -10.0f), new List<Bullet>(), new Vector2(25, 0)));
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player_texture = Content.Load<Texture2D>("Fighter_small.png");
            background_stars = Content.Load<Texture2D>("background_stars.png");
            player_bullet_texture = Content.Load<Texture2D>("player_bullet_left.png");
            enemy_astroid = Content.Load<Texture2D>("asteroid.png");
            healthbar = Content.Load<Texture2D>("healthBar.png");
            powerup_texture = Content.Load<Texture2D>("powerup1.png");
            background.Load(GraphicsDevice, background_stars);
            //player_shoot = Content.Load<SoundEffect>("player_shoot.wav");

            // TODO: use this.Content to load your game content here
        }

        public void Update_Background(float deltaY)
        {
            screenpos.Y += deltaY;
            screenpos.Y = screenpos.Y % mytexture.Height;
        }

        protected override void Update(GameTime gameTime)
        {
            deltaTime = ((float)gameTime.ElapsedGameTime.TotalMilliseconds - deltaTime);

            if (shotDelay > 0)
                shotDelay--;
            player.position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                if (shotDelay == 0)
                {
                    bullets.Add(new Bullet(player_bullet_texture, player.position, new Vector2(0.0f, -10.0f)));
                    shotDelay = minShotDelay;
                }
            }

            List<Astroid> rain = GenerateRain();
            List<Bullet> bulletrain = UpdateBullet();

            if (player.health.amount < 1)
                Exit();

            // COMMIT CHANGES
            astroids = rain;
            bullets = bulletrain;
            background.Update(deltaTime / 5);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            spriteBatch.Draw(player_texture, player.position, Color.White);
            player.health.Draw(spriteBatch);
            for (int i = 0; i < bullets.Count; i++)
            {
                foreach (Bullet bullet in bullets)
                    bullet.Draw(spriteBatch);
            }
            foreach (Astroid astroid in astroids)
                astroid.Draw(spriteBatch);
            foreach (Powerup powerup in powerups)
                powerup.Draw(spriteBatch);
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
                    return true;
                }
            }
            return false;
        }

        public bool AstroidPlayerCollisionDetection(Astroid astroid)
        {
            float playerMinX = player.position.X;
            float playerMaxX = (player.position.X + player_texture.Bounds.Width);

            float playerMinY = player.position.Y;
            float playerMaxY = (player.position.Y + player_texture.Bounds.Height);

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
                    return true;
                }
            }
            return false;
        }

        public bool PowerupPlayerCollisionDetection(Powerup powerup)
        {
            float playerMinX = player.position.X;
            float playerMaxX = (player.position.X + player_texture.Bounds.Width);

            float playerMinY = player.position.Y;
            float playerMaxY = (player.position.Y + player_texture.Bounds.Height);

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
                    bullet_paths.Add(new Bullet_Path(player.position, new Vector2(0, -10.0f), new List<Bullet>(), new Vector2(12, 0)));
                    return true;
                }
            }
            return false;
        }

        public List<Astroid> GenerateRain()
        {
            AstroidStateChanger();
            var currentRain = (from astroid in astroids
                               let colliders = 
                               from bullet in bullets
                               where AstroidBulletCollisionDetection(bullet, astroid)
                               select bullet
                               where astroid.position.Y <= windowHeight &&
                                     astroid.position.X <= windowWidth &&
                                     astroid.position.X > -astroid.texture.Width &&
                                     astroid.position.Y > -astroid.texture.Height &&
                                     astroid.health > 0
                               select astroid).ToList<Astroid>();
            foreach (var astroid in currentRain)
                astroid.position += astroid.velocity;
            return currentRain;
        }

        public List<Bullet> UpdateBullet()
        {
            var currentBullet = (from bullet in bullets
                                 let colliders =
                                 from astroid in astroids
                                 where AstroidBulletCollisionDetection(bullet, astroid)
                                 select astroid
                                 where bullet.position.X <= windowHeight &&
                                       bullet.position.X <= windowWidth &&
                                       bullet.position.X > -bullet.texture.Width &&
                                       bullet.position.Y > -bullet.texture.Height &&
                                       colliders.Count() == 0
                                 select bullet).ToList<Bullet>();
            foreach (var bullet in currentBullet)
                bullet.position += bullet.velocity;
            return currentBullet;
        }

        [Obsolete("Shoot_Powerup() is deprecated, please use generatePowerup() instead", true)]
        public void Shoot_Powerup(Vector2 position)
        {
            Powerup newPowerup = new Powerup(position, new Vector2(0, 3), powerup_texture);
            powerups.Add(newPowerup);
        }

        [Obsolete("UpdateRain() is deprecated, please use GenerateRain() instead.", true)]
        public void UpdateRain(List<Astroid> newRain)
        {
            if (newRain.Count > 0)
            {
                foreach (Astroid astroid in newRain)
                {
                    astroid.position += astroid.velocity;
                    Vector2 pos = astroid.position;
                    if (pos.Y > windowHeight)
                    {
                        toBeRemoved.Add(astroid);
                    }
                    else if (AstroidPlayerCollisionDetection(astroid))
                    {
                        player.health.amount += -1;
                        toBeRemoved.Add(astroid);
                    }
                }
                foreach (Astroid remAstroid in toBeRemoved)
                    astroids.Remove(remAstroid);
            }
            for (int i = 0; i < astroids.Count; i++)
            {
                if (astroids[i].health < 1)
                {
                    toBeRemoved.Add(astroids[i]);
                    if (random.NextDouble() > 0.9)
                    {
                        Shoot_Powerup(astroids[i].position);
                    }
                }
                foreach (Astroid remAstroid in toBeRemoved)
                    astroids.Remove(remAstroid);
            }
        }

        public void UpdateBullets()
        {
            for (int i = 0; i < bullet_paths.Count; i++)
            {
                if (bullet_paths[i].bullets.Count > 0)
                {
                    List<Bullet> toBeRemoved = new List<Bullet>();
                    foreach (Bullet bullet in bullet_paths[i].bullets)
                    {
                        bullet.position += bullet.velocity;
                        Vector2 pos = bullet.position;
                        if (pos.Y > windowHeight)
                            toBeRemoved.Add(bullet);
                        if (AstroidBulletCollisionDetection(bullet, astroids[0]))
                        {
                            toBeRemoved.Add(bullet);
                        }
                    }
                    foreach (Bullet remBullet in toBeRemoved)
                        bullet_paths[i].bullets.Remove(remBullet);
                }
            }
        }

        public void UpdateBullet_Paths()
        {
            for (int i = 0; i < bullet_paths.Count; i++)
            {
                bullet_paths[i].position = player.position;
            }
        }

        public void UpdatePowerup()
        {
            if (powerups.Count > 0)
            {
                List<Powerup> toBeRemoved = new List<Powerup>();
                foreach (Powerup powerup in powerups)
                {
                    powerup.position += powerup.velocity;
                    Vector2 pos = powerup.position;
                    if (pos.Y > windowHeight)
                    {
                        toBeRemoved.Add(powerup);
                    }
                    else if (!player.powerup1 && PowerupPlayerCollisionDetection(powerup))
                    {
                        toBeRemoved.Add(powerup);
                        player.powerup1 = true;
                    }
                }
                foreach (Powerup remPowerup in toBeRemoved)
                    powerups.Remove(remPowerup);
            }
        }

        public List<Powerup> generatePowerup()
        {
            var currentPowerups = (from powerup in powerups
                               where powerup.position.Y <= windowHeight &&
                                     powerup.position.X <= windowWidth &&
                                     powerup.position.X > -powerup.texture.Width &&
                                     powerup.position.Y > -powerup.texture.Height
                               select powerup).ToList<Powerup>();

            foreach (var powerup in currentPowerups)
                powerup.position += powerup.velocity;
            return currentPowerups;
        }
    }
}
