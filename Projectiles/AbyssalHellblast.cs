using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using UnbiddenMod.NPCs.FireAncient;

namespace UnbiddenMod.Projectiles
{
    public class AbyssalHellblast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Hellblast");
            Main.projFrames[projectile.type] = 9;
        }

        public override void SetDefaults()
        {
            projectile.width = 45;
            projectile.height = 22;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 180;
            projectile.penetrate = 1;
            projectile.scale = 1f;
            projectile.damage = 100;
            projectile.hostile = true;
            projectile.GetGlobalProjectile<UnbiddenGlobalProjectile>().element = 0; // Fire
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            NPC npc = Main.npc[(int)projectile.ai[0]];
            projectile.ai[1] += 1f;
            projectile.localAI[0] += 1f;
            IList<int> targets = ((FireAncient)npc.modNPC).targets;
            int player2 = targets[0];
            Player player = Main.player[player2];
            if (++projectile.frameCounter >= 3) // Frame time
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 9) //Frame number
                {
                    projectile.frame = 0;
                }
            }
            Vector2 offset = Main.player[player2].position - projectile.position;
            float speedCap = 8f;
            float gainStrength = 0.2f;
            float slowStrength = 1.1f;
            UnbiddenGlobalProjectile.IsHomingPlayer(projectile, offset, player, speedCap, gainStrength, slowStrength);
        }

        private void SetDirection(NPC npc)
        {
            // int npcType = ModContent.NPCType<NPCs.FireAncient.FireAncient>();
            // FireAncient npc2 = (FireAncient)ModContent.GetModNPC(npcType);
            // IList<int> targets = npc2.targets;
            IList<int> targets = ((FireAncient)npc.modNPC).targets;
            bool needsRotation = true;
            if (targets.Count > 0)
            {
                int player = targets[0];
                Vector2 offset = Main.player[player].Center - projectile.Center;
                if (offset != Vector2.Zero)
                {
                    projectile.rotation = (float)Math.Atan2(offset.Y, offset.X);
                    needsRotation = false;
                }
            }
            if (needsRotation)
            {
                projectile.rotation = -(float)Math.PI / 2f;
            }
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff((BuffID.OnFire), 10);
            UnbiddenPlayer unbiddenPlayer = target.Unbidden();
        }

        public override Color? GetAlpha(Color lightColor)
        {
            Color color = Color.White;
            return (color);
        }
    }
}