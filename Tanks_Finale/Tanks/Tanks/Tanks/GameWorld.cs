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

namespace Tanks
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>


    public class GameWorld : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        Texture2D backgroundTexture;
        int screenWidth;
        int screenHeight;

        Texture2D gridTexture;
        Texture2D foreGround;
        Texture2D foreGround2;
        Texture2D water;
        Texture2D emptyCell;
        Texture2D stone;
        Texture2D brick_100;
        Texture2D brick_75;
        Texture2D brick_50;
        Texture2D brick_25;
        Texture2D P0;
        Texture2D P1;
        Texture2D P2;
        Texture2D P3;
        Texture2D P4;

        SpriteFont font;


        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // initialization logic 
            //graphics.PreferredBackBufferWidth = 1300;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 700;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Tank Game";

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
            backgroundTexture = Content.Load<Texture2D>("tanks-background");
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;

            gridTexture = Content.Load<Texture2D>("P0");

            P0 = Content.Load<Texture2D>("P0");
            P1 = Content.Load<Texture2D>("P1");
            P2 = Content.Load<Texture2D>("P2");
            P3 = Content.Load<Texture2D>("P3");
            P4 = Content.Load<Texture2D>("P4");

            foreGround = Content.Load<Texture2D>("foreground1");
            foreGround2 = Content.Load<Texture2D>("foreground2");
            emptyCell = Content.Load<Texture2D>("empty_cell");
            water = Content.Load<Texture2D>("water");
            brick_100 = Content.Load<Texture2D>("brick_100");
            brick_75 = Content.Load<Texture2D>("brick_75");
            brick_50 = Content.Load<Texture2D>("brick_50");
            brick_25 = Content.Load<Texture2D>("brick_25");
            stone = Content.Load<Texture2D>("stone");

            font = Content.Load<SpriteFont>("SpriteFont1");
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            DrawScenery();
            DrawForeground();
            DrawForegrounf2();
            FillGrid();
            int[] shooting={0,1,1,0,1}; 
            setShooting(shooting);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        private void DrawScenery()
        {
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
        }
        private void FillGrid()//map should pass as an argument
        {
            int tileSize = 34;
            Vector2 position = Vector2.Zero;
            int x, y;

            String[,] map = new String[,]
            {
                {"P010","B1","S","E",},
                {"E","W","P121","E",},
                {"P230","S","P300","W",},
                {"E","B0","S","W",},
            };

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    String textureId = map[i, j];
                    y = (j * tileSize) + 25;
                    x = (i * tileSize) + 5;
                    Vector2 texturePosition = new Vector2(y, x);

                    if (textureId.Equals("E"))
                    {
                        spriteBatch.Draw(emptyCell, texturePosition, Color.White);
                    }
                    if (textureId.Equals("S"))
                    {
                        spriteBatch.Draw(stone, texturePosition, Color.White);
                    }
                    if (textureId.Equals("W"))
                    {
                        spriteBatch.Draw(water, texturePosition, Color.White);
                    }
                    if (textureId[0] == 'B')
                    {
                        if (textureId[1] == '0')
                        {
                            spriteBatch.Draw(brick_100, texturePosition, Color.White);
                        }
                        if (textureId[1] == '1')
                        {
                            spriteBatch.Draw(brick_75, texturePosition, Color.White);
                        }
                        if (textureId[1] == '2')
                        {
                            spriteBatch.Draw(brick_50, texturePosition, Color.White);
                        }
                        if (textureId[1] == '3')
                        {
                            spriteBatch.Draw(brick_25, texturePosition, Color.White);
                        }
                        if (textureId[1] == '4')
                        {
                            spriteBatch.Draw(emptyCell, texturePosition, Color.White);
                        }
                    }
                    if (textureId[0] == 'P')
                    {

                        Vector2 Origin = new Vector2(P0.Height / 2, P0.Width / 2);
                        Vector2 tankPosition = new Vector2(y + P0.Height / 2, x + P0.Width / 2);
                        if (textureId[1] == '0')
                        {
                            if (textureId[2] == '0' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P0, tankPosition, null, Color.White, 0, Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                            if (textureId[2] == '1' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P0, tankPosition, null, Color.White, MathHelper.ToRadians(90), Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                            if (textureId[2] == '2' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P0, tankPosition, null, Color.White, MathHelper.ToRadians(180), Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                            if (textureId[2] == '3' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P0, tankPosition, null, Color.White, MathHelper.ToRadians(-90), Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                        }
                        if (textureId[1] == '1')
                        {
                            if (textureId[2] == '0' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P1, tankPosition, null, Color.White, 0, Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                            if (textureId[2] == '1' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P1, tankPosition, null, Color.White, MathHelper.ToRadians(90), Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                            if (textureId[2] == '2' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P1, tankPosition, null, Color.White, MathHelper.ToRadians(180), Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                            if (textureId[2] == '3' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P1, tankPosition, null, Color.White, MathHelper.ToRadians(-90), Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                        }
                        if (textureId[1] == '2')
                        {
                            if (textureId[2] == '0' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P2, tankPosition, null, Color.White, 0, Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                            if (textureId[2] == '1' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P2, tankPosition, null, Color.White, MathHelper.ToRadians(90), Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                            if (textureId[2] == '2' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P2, tankPosition, null, Color.White, MathHelper.ToRadians(180), Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                            if (textureId[2] == '3' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P2, tankPosition, null, Color.White, MathHelper.ToRadians(-90), Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                        }
                        if (textureId[1] == '3')
                        {
                            if (textureId[2] == '0' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P3, tankPosition, null, Color.White, 0, Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                            if (textureId[2] == '1' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P3, tankPosition, null, Color.White, MathHelper.ToRadians(90), Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                            if (textureId[2] == '2' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P3, tankPosition, null, Color.White, MathHelper.ToRadians(180), Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                            if (textureId[2] == '3' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P3, tankPosition, null, Color.White, MathHelper.ToRadians(-90), Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                        }
                        if (textureId[1] == '4')
                        {
                            if (textureId[2] == '0' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P4, tankPosition, null, Color.White, 0, Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                            if (textureId[2] == '1' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P4, tankPosition, null, Color.White, MathHelper.ToRadians(90), Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                            if (textureId[2] == '2' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P4, tankPosition, null, Color.White, MathHelper.ToRadians(180), Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                            if (textureId[2] == '3' && textureId[3] != '4')
                            {
                                spriteBatch.Draw(P4, tankPosition, null, Color.White, MathHelper.ToRadians(-90), Origin, 1.0f, SpriteEffects.None, 0f);
                            }
                        }
                        if (textureId[3] == '4')
                        {
                            spriteBatch.Draw(emptyCell, texturePosition, Color.White);
                        }

                    }

                }
            }
        }


        private void DrawForeground()
        {
            Rectangle screenRectangle = new Rectangle(25, 5, 34 * 20, 34 * 20);
            spriteBatch.Draw(foreGround, screenRectangle, Color.White);
        }
        private void DrawForegrounf2()
        {
            Rectangle rectangle = new Rectangle(730, 20, 800, 180);
            spriteBatch.Draw(foreGround2, rectangle, Color.White);
        }

        //private void UpdateRocket(Boolean shooting, Texture2D tank)
        //{
        //    if (shooting)
        //    {
                
        //        Vector2 gravity = new Vector2(0, 0);
        //        Vector2 bulletDirection = Vector2.Zero;
        //        bulletDirection += gravity / 10.0f;

        //        Vector2 bulletPosition = Vector2.Zero;
        //        bulletPosition += bulletDirection;

        //    }

        //}
        private void setShooting(int[] shooting)
        {
            for (int i = 0; i < shooting.GetLength(0); i++)
            {
                if (shooting[i] == 1)
                {
                    DrawTextShooting("P" + i + " is shooting", i);
                }
                else
                {
                    DrawTextNotShooting("P" + i + " is not shooting", i);
                }
            }
         
        }
        private void DrawTextShooting(String text,int y)
        {
            spriteBatch.DrawString(font, text, new Vector2(750, (30*y)+30), Color.Red);
        }
        private void DrawTextNotShooting(String text, int y)
        {
            spriteBatch.DrawString(font, text, new Vector2(750, (30 * y) + 30), Color.White);
        }
    }
}
