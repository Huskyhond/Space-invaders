using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        Random random = new Random();

        int windowWidth = 800;
        int windowHeight = 600;
        int rainSpeed = 20; // frames per astroid

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D player_texture;
        Texture2D background_stars;
        Texture2D player_bullet_texture;
        Texture2D enemy_astroid;
        Player player;

        //settings
        const int minShotDelay = 5; // frames

        int shotDelay = 0;
        
        int rainDelay = 0;
        List<Bullet> bullets = new List<Bullet>();
        List<Astroid> astroids = new List<Astroid>();

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
            player = new Player(player_texture);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player_texture = Content.Load<Texture2D>("Fighter_small.png");
            background_stars = Content.Load<Texture2D>("background_stars.png");
            player_bullet_texture = Content.Load<Texture2D>("player_bullet.png");
            enemy_astroid = Content.Load<Texture2D>("astroid.png");
            // TODO: use this.Content to load your game content here
        }

        public void GenerateRain()
        {
            if (rainDelay < 1)
            {
                float rdm = random.Next(0, (windowWidth-30));
                Astroid astroid = new Astroid(enemy_astroid);
                astroid.velocity = new Vector2(0.0f, 1.0f);
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

                //if (bullets.Count < 100)
                bullets.Add(newBullet);
                shotDelay = minShotDelay;
            }
        }

        public void UpdateRain()
        {
            if (astroids.Count > 0)
            {
                List<Astroid> toBeRemoved = new List<Astroid>();
                foreach (Astroid astroid in astroids)
                {
                    astroid.position += astroid.velocity;
                    Vector2 pos = astroid.position;
                    if (pos.Y > windowHeight)
                    {
                        toBeRemoved.Add(astroid);
                    }
                    else if(CollisionDetection(astroid))
                    {
                        player.health += -1;
                        toBeRemoved.Add(astroid);
                    }
                }
                foreach (Astroid remAstroid in toBeRemoved)
                {
                    astroids.Remove(remAstroid);
                }
            }
        }

        public void UpdateBullets()
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.position += bullet.velocity;
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].visible)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
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

            if (player.health < 1)
            {
                throw new Exception();
            }

            base.Update(gameTime);
        }

        public bool CollisionDetection(Astroid astroid)
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
