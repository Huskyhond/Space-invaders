using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        Vector2 player_vector;
        Texture2D kappa_texture;
        int boss_speed;
        float ypos;
        float xpos;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            boss_speed = 10;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player_texture = Content.Load<Texture2D>("Boris_Hoofd.jpg");
            kappa_texture = Content.Load<Texture2D>("Kappa.png");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                ypos += -1;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                xpos += -1;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                ypos += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                xpos += 1;
            // TODO: Add your update logic here

            player_vector = new Vector2(xpos * boss_speed, ypos * boss_speed);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(player_texture, player_vector, new Rectangle(0,0, 100, 100) , Color.White);

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
