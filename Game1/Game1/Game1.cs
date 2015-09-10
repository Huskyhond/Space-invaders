using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D player_texture;
        Texture2D background_stars;
        Texture2D player_bullet_texture;
        Vector2 player_vector;
        //settings
        const int minShotDelay = 5; // frames

        int shotDelay = 0;

        List<Bullet> bullets = new List<Bullet>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            //boss_speed = 10;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player_texture = Content.Load<Texture2D>("Fighter_small.png");
            background_stars = Content.Load<Texture2D>("background_stars.png");
            player_bullet_texture = Content.Load<Texture2D>("player_bullet.png");
            // TODO: use this.Content to load your game content here
        }

        public void Shoot_Player()
        {
            if (shotDelay == 0)
            {
                Bullet newBullet = new Bullet(player_bullet_texture);
                newBullet.velocity = new Vector2(0.0f, -15.0f);
                newBullet.position = player_vector;
                newBullet.visible = true;

                //if (bullets.Count < 100)
                bullets.Add(newBullet);
                shotDelay = minShotDelay;
            }
        }

        public void UpdateBullets()
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.position += bullet.velocity;
                if (Vector2.Distance(bullet.position, player_vector) > 500)
                    bullet.visible = true;
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
            player_vector = new Vector2(Mouse.GetState().X,Mouse.GetState().Y);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                Shoot_Player();
            UpdateBullets();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background_stars, new Rectangle(0, 0, 800, 600), Color.White);
            spriteBatch.Draw(player_texture, player_vector , Color.White);
            foreach (Bullet bullet in bullets)
                bullet.Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
