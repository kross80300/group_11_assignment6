using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace project6;
public class Particle
{
    public Vector2 position;
    public Vector2 velocity;
    Vector2 acceleration;

    public float mass;
    public float size;
    public Color particleColor;
    Texture2D _img;
    private Texture2D sprite;
    public Particle(Texture2D texture, float x, float y, float vx, float vy, Color color)
    {
        _img = texture;
        position = new Vector2(x, y);
        velocity = new Vector2(vx, vy);
        acceleration = Vector2.Zero;
        particleColor = color;
        mass = 1.5f;
        size = 2.5f;
    }
    public void ApplyForces(float fx, float fy)
    {
        acceleration.X = fx/mass;
        acceleration.Y = fy/mass;
        velocity.X += acceleration.X;
        position.X += velocity.X;
        velocity.Y += acceleration.Y;
        position.Y += velocity.Y;
    }

    public void Update()
    {
        position += velocity;
    }
    public void Display(SpriteBatch draw)
    {
        draw.Draw(_img, position, null, particleColor, 0f, Vector2.Zero, size*0.005f, SpriteEffects.None, 0f);
    }
}