using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace group_11_assignment6;

public class Galaxy
{
   private List<project6.Particle> particles;
   private Vector2 position;
   private float mass;
   private Color galaxyColor;


   public Galaxy(Vector2 position, float mass, Color galaxyColor)
   {
      this.particles = new List<project6.Particle>();
      this.position = position;
      this.mass = mass;
      this.galaxyColor = galaxyColor;
   }
   
   public Vector2 Position => position;
   public float Mass => mass;
   public Color GalaxyColor => galaxyColor;
   public List<project6.Particle> Particles => particles;

   public void Update()
   {
      foreach (project6.Particle particle in particles)
      {
         particle.Update();
      }
   }

   public void Display(SpriteBatch spriteBatch, Texture2D particleTexture)
   {
      foreach (project6.Particle particle in particles)
      {
         particle.Display(spriteBatch);
      }
   }

   public void ApplyGravity()
   {
      foreach (project6.Particle particle in particles)
      {
         Vector2 direction = position - particle.position;
         float distance = direction.Length();

         distance = Math.Max(distance, 5f);
         
         direction.Normalize();
         
         float forceMagnitude = (mass * particle.mass) / (distance * distance);

         forceMagnitude *= 0.1f;
         
         Vector2 force = direction * forceMagnitude;
         particle.ApplyForces(force.X, force.Y);
      }
   }

   public void AddParticle(int count, Random random, Texture2D particleTexture)
   {
      for (int i = 0; i < count; i++)
      {
         float angle = (float)(random.NextDouble() * Math.PI * 2);
         float distance = (float)(random.NextDouble() * 200 + 50);
         
         float x = position.X + (float)Math.Cos(angle) * distance;
         float y = position.Y + (float)Math.Sin(angle) * distance;
         
         float speed = (float)Math.Sqrt(mass / distance) * 2f;
         float vx = -(float)Math.Sin(angle) * speed;
         float vy = (float)Math.Cos(angle) * speed;
         
         project6.Particle particle = new project6.Particle(particleTexture, x, y, vx, vy, galaxyColor);
            
         particles.Add(particle);
      }
   }
}