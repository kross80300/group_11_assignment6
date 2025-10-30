using System;
using System.Collections.Generic;
using System.Xml.Schema;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace group_11_assignment6;

public class Galaxy
{
   private List<Particle> particles;
   private Vector2 position;
   private float mass;
   private Color galaxyColor;


   public Galaxy(Vector2 position, float mass, Color galaxyColor)
   {
      this.particles = new List<Particle>();
      this.position = position;
      this.mass = mass;
      this.galaxyColor = galaxyColor;
   }
   
   public Vector2 Position => position;
   public float Mass => mass;
   public Color GalaxyColor => galaxyColor;
   public List<Particle> Particles => particles;

   public void Update();
   {
      foreach (Particle particle in particles)
      {
         particle.Update();
      }
   }

   public void Display(SpriteBatch spriteBatch, Texture2D particleTexture)
   {
      foreach (Particle particle in particles)
      {
         particle.Display(spriteBatch, particleTexture);
      }
   }

   public void ApplyGravity()
   {
      foreach (Particle particle in particles)
      {
         Vector2 direction = position - particle.Position;
         float distance = direction.Length();

         distance = Math.Max(distance, 5f);
         
         direction.Normalize();
         
         float forceMagnitude = (mass * particle.Mass) / (distance * distance);

         forceMagnitude *= 0.1f;
         
         direction *= forceMagnitude;
         particle.ApplyForce(direction);
      }
   }

   public void AddParticle(int count, Random random)
   {
      for (int i = 0; i < count; i++)
      {
         float angle = (float)(random.NextDouble() * Math.PI * 2);
         float distance = (float)(random.NextDouble() * 200 + 50);
         
         float x = position.X + (float)Math.Cos(angle) * distance;
         float y = position.Y + (float)Math.Sin(angle) * distance;
            
         Vector2 particlePosition = new Vector2(x, y);
         
         float speed = (float)Math.Sqrt(mass / distance) * 2f;
         Vector2 velocity = new Vector2(
            -(float)Math.Sin(angle) * speed,
            (float)Math.Cos(angle) * speed
         );
         
         float particleMass = (float)(random.NextDouble() * 2 + 0.5f);
         Particle particle = new Particle(particlePosition, velocity, particleMass, galaxyColor);
            
         particles.Add(particle);
      }
   }
}