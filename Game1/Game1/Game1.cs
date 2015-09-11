using Microsoft.Xna.Framework;
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
        Player player;
        Health health;
        int windowHeight;
        int windowWidth;
        List<Bullet> bullets = new List<Bullet>();
        List<Astroid> astroids = new List<Astroid>();


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
            player = new Player(player_texture, health);
            player.health.position = new Vector2(600, 450);
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
            // TODO: use this.Content to load your game content here
        }

        public void GenerateRain()
        {
            if (rainDelay < 1)
            {
                float rdm = random.Next(0, (windowWidth-30));
                Astroid astroid = new Astroid(enemy_astroid);
                astroid.velocity = new Vector2((float)random.NextDouble()*2-1, 1.0f);
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
                Bullet newBullet = new Bullet(player_bullet_texture);
                newBullet.velocity = new Vector2(0.0f, -15.0f);
                newBullet.position = player.position;
                newBullet.visible = true;

                bullets.Add(newBullet);
                shotDelay = minShotDelay;
            }
        }

        public void UpdateRain()
        {
            List<Astroid> toBeRemoved = new List<Astroid>();
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
                    else if(AstroidPlayerCollisionDetection(astroid))
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
                    toBeRemoved.Add(astroids[i]);
                foreach (Astroid remAstroid in toBeRemoved)
                    astroids.Remove(remAstroid);
            }
        }

        public void UpdateBullets()
        {
            if (bullets.Count > 0)
            {
                List<Bullet> toBeRemoved = new List<Bullet>();
                foreach (Bullet bullet in bullets)
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
                    bullets.Remove(remBullet);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if(shotDelay > 0) { shotDelay--; }
            player.position = new Vector2(Mouse.GetState().X,Mouse.GetState().Y);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                Shoot_Player();
            UpdateBullets();
            GenerateRain();

            if (player.health.amount < 1)
            {
                Exit();
            }

            base.Update(gameTime);
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

               //Depricated Collision detection values
               // float middroidX = (astroidMinX + ((astroidMaxX - astroidMinX) / 2));
               // float middroidY = (astroidMinY + ((astroidMaxY - astroidMinY) / 2));

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

            float middroidX = (astroidMinX + ((astroidMaxX - astroidMinX)/2));
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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background_stars, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            spriteBatch.Draw(player_texture, player.position, Color.White);
            player.health.Draw(spriteBatch);
            foreach (Bullet bullet in bullets)
                bullet.Draw(spriteBatch);
            foreach (Astroid astroid in astroids)
                astroid.Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
