using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace group_11_assignment6;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private int _height = 800;
    private int _width = 1200;
    Random random = new Random();
    private List<Galaxy> galaxies;
    private Texture2D particleTexture;
    private MouseState last;
    

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferHeight = _height;
        _graphics.PreferredBackBufferWidth = _width;
        _graphics.ApplyChanges();

        galaxies = new List<Galaxy>();
        random = new Random();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        particleTexture = Content.Load<Texture2D>("white_spot");
        
        Galaxy ga = new Galaxy(new Vector2(random.Next(100, _width - 100), random.Next(50, _height - 50)), (float)random.NextDouble() * 50 + 10, GetRandomGalaxyColor());
        ga.AddParticle(random.Next(200, 600), random, particleTexture);
        galaxies.Add(ga);
        
        Galaxy gb = new Galaxy(new Vector2(random.Next(100, _width - 100), random.Next(50, _height - 50)), (float)random.NextDouble() * 50 + 10, GetRandomGalaxyColor());
        gb.AddParticle(random.Next(200, 600), random, particleTexture);
        galaxies.Add(gb);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        MouseState m = Mouse.GetState();
        if (m.X >= 0 && m.X < _width && m.Y >= 0 && m.Y < _height && m.LeftButton == ButtonState.Pressed
         && last.LeftButton == ButtonState.Released)
        {
            Galaxy ng = new Galaxy(new Vector2(m.X, m.Y), (float)random.NextDouble() * 50 + 10, GetRandomGalaxyColor());
            ng.AddParticle(random.Next(200, 600), random, particleTexture);
            galaxies.Add(ng);
        }
        last = m;
        
        foreach (Galaxy g in galaxies)
        {
            g.Update();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        foreach (Galaxy g in galaxies)
        {
            g.Display(_spriteBatch, particleTexture);
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private Color GetRandomGalaxyColor()
    {
        Color[] galaxyColors = new Color[]
        {
            new Color(100, 150, 255),   
            new Color(255, 100, 150),   
            new Color(150, 255, 150),   
            new Color(255, 200, 100),   
            new Color(200, 100, 255),   
            new Color(100, 255, 255),  
            new Color(255, 255, 100), 
            new Color(255, 150, 200) 
        };
        
        return galaxyColors[random.Next(galaxyColors.Length)];
    }
}