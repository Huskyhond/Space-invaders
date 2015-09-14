﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        Texture2D player_texture;
        Texture2D background_stars;
        Texture2D player_bullet_texture;
        Texture2D enemy_astroid;
        Texture2D healthbar;
        Texture2D powerup_texture;
        Player player;
        Health health;
        int windowHeight;
        int windowWidth;
        SoundEffect player_shoot;
        List<Astroid> astroids = new List<Astroid>();
        List<Bullet_Path> bullet_paths = new List<Bullet_Path>();
        List<Powerup> powerups = new List<Powerup>();
        List<Astroid> toBeRemoved = new List<Astroid>();


        //settings
        const int minShotDelay = 5; // frames
        int shotDelay = 0;
        int rainDelay = 0;
        int rainSpeed = 20; // frames per astroid

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
            //player_shoot = Content.Load<SoundEffect>("player_shoot.wav");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (shotDelay > 0) { shotDelay--; }
            player.position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                Shoot_Player();
                //player_shoot.Play();
            }
            UpdateBullets();
            UpdateBullet_Paths();
            GenerateRain();
            UpdatePowerup();

            if (player.health.amount < 1)
            {
                Exit();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background_stars, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            spriteBatch.Draw(player_texture, player.position, Color.White);
            player.health.Draw(spriteBatch);
            for (int i = 0; i < bullet_paths.Count; i++)
            {
                foreach (Bullet bullet in bullet_paths[i].bullets)
                    bullet.Draw(spriteBatch);
            }
            foreach (Astroid astroid in astroids)
                astroid.Draw(spriteBatch);
            foreach (Powerup powerup in powerups)
                powerup.Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public bool AstroidBulletCollisionDetection(Bullet bullet)
        {
            float bulletMinY = bullet.position.Y;
            float bulletMaxY = (bullet.position.Y + bullet.texture.Bounds.Height);

            float bulletMinX = bullet.position.X;
            float bulletMaxX = (bullet.position.X + bullet.texture.Bounds.Width);
            for (int i = 0; i < astroids.Count; i++)
            {
                float astroidMinX = astroids[i].position.X;
                float astroidMaxX = (astroids[i].position.X + astroids[i].texture.Bounds.Width);

                float astroidMinY = astroids[i].position.Y;
                float astroidMaxY = (astroids[i].position.Y + astroids[i].texture.Bounds.Height);

                if (astroidMinX < bulletMinX && astroidMaxX > bulletMinX && astroidMinX < bulletMaxX && astroidMaxX > bulletMaxX)
                {
                    if (astroidMinY < bulletMinY && astroidMaxY > bulletMinY && astroidMinY < bulletMaxY && astroidMaxY > bulletMaxY)
                    {
                        astroids[i].health--;
                        return true;
                    }
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

        public void GenerateRain()
        {
            if (rainDelay < 1)
            {
                float rdm = random.Next(0, (windowWidth - 30));
                Astroid astroid = new Astroid(enemy_astroid);
                astroid.velocity = new Vector2((float)random.NextDouble() * 2 - 1, 1.0f);
                astroid.position = new Vector2(rdm, -30.0f);
                astroids.Add(astroid);
                rainDelay = rainSpeed;
            }
            else
            {
                rainDelay--;
            }
            UpdateRain();
        }

        public void Shoot_Player()
        {
            if (shotDelay == 0)
            {
                for (int i = 0; i < bullet_paths.Count; i++)
                {
                    bullet_paths[i].Shoot(player_bullet_texture);
                }
                shotDelay = minShotDelay;
            }
        }

        public void Shoot_Powerup(Vector2 position)
        {
            Powerup newPowerup = new Powerup(position, new Vector2(0, 3), powerup_texture);
            powerups.Add(newPowerup);
        }

        public void UpdateRain()
        {
            if (astroids.Count > 0)
            {
                foreach (Astroid astroid in astroids)
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

        public void HealthHUD()
        {

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
                        if (AstroidBulletCollisionDetection(bullet))
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
    }
}
