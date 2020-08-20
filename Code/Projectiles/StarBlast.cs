using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UnbiddenMod.Code.Dusts;

namespace UnbiddenMod.Code.Projectiles
{
  public class StarBlast : ModProjectile
  {
    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Star Blast");
      Main.projFrames[projectile.type] = 30;
    }

    public override void SetDefaults()
    {
      projectile.arrow = true;
      projectile.width = 16;
      projectile.height = 16;
      projectile.aiStyle = 1;
      projectile.friendly = true;
      projectile.melee = true;
      projectile.tileCollide = true;
      projectile.ignoreWater = true;
      aiType = 0;
      projectile.timeLeft = 300;
      projectile.penetrate = 3;
      projectile.scale = 1.5f;

    }

    public override void AI()
    {
      Player player = Main.player[projectile.owner];
      Lighting.AddLight(projectile.Center, 0.25f, 0f, 0.75f);
      projectile.ai[0] += 1f;
      Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("StarBlastDust"));
      if (projectile.soundDelay == 0)
      {
        projectile.soundDelay = 640;
        Main.PlaySound(SoundID.Item9, projectile.position);
      }
      projectile.rotation += 3f * (float)projectile.direction;
      /*if ((float) ((Vector2) ((Entity) projectile).velocity).X > 0.0)
      {
        projectile.rotation += 0.8f;
      }
      else
      {
        projectile.rotation -= 0.8f;
      }*/
      if (++projectile.frameCounter >= 2) 
      {
				projectile.frameCounter = 0;
				if (++projectile.frame >= 30) 
        {
					projectile.frame = 0;
				}
			}
      for (int i = 0; i < 200; i++)
      {
        NPC target = Main.npc[i];
        //This will allow the projectile to only target hostile NPC's by referencing the variable, "target", above
        if (target.active && !target.dontTakeDamage && !target.friendly)
        {
          //Finding the horizontal position of the target and adjusting trajectory accordingly
          float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
          //Finding the vertical position of the target and adjusting trajectory accordingly
          float shootToY = target.position.Y - projectile.Center.Y;
          //  √ shootToX² + shootToY², using the Pythagorean Theorem to calculate the distance from the target
          float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

          //f, in this scenario, is a measurement of Pixel Distance
          if (distance < 80f && !target.friendly && target.active)
          {
            distance = 3f / distance;
            shootToY *= distance * 5;
            shootToX *= distance * 5;

            projectile.velocity.Y = shootToY;
            projectile.velocity.X = shootToX;
          }
        }
      }
    }
    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
      Player player = Main.player[projectile.owner];
      int healingAmount = damage / 60; //decrease the value 30 to increase heal, increase value to decrease. Or you can just replace damage/x with a set value to heal, instead of making it based on damage.
      player.statLife += healingAmount;
      player.HealEffect(healingAmount, true);
      projectile.penetrate--;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
      projectile.penetrate--;
      if (projectile.penetrate <= 0)
      {
        projectile.Kill();
      }
      else
      {
        Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
        Main.PlaySound(SoundID.Item10, projectile.position);
        if (projectile.velocity.X != oldVelocity.X)
        {
          projectile.velocity.X = -oldVelocity.X;
        }
        if (projectile.velocity.Y != oldVelocity.Y)
        {
          projectile.velocity.Y = -oldVelocity.Y;
        }
      }
      return false;
    }
    public override void Kill(int timeLeft)
    {

    }
  }
}