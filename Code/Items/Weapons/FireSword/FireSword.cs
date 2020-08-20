using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.Windows.Forms;

namespace UnbiddenMod.Code.Items.Weapons.FireSword
{
    public class FireSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Sword");
            Tooltip.SetDefault("\"A sword of pure flame\"");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.PlatinumBroadsword);
            item.GetGlobalItem<UnbiddenItem>().element = 0; // Fire
            // item.shoot = true; // Commenting this until we have a projectile to shoot
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (Main.rand.Next(10) == 0) // 10% chance
            {
                target.AddBuff(BuffID.OnFire, 300, true); // Burn for 5 seconds
            }
        }

        public override void AddRecipes()
        {
            // Recipes here. See Basic Recipe Guide2
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.PlatinumBroadsword, 1);
            recipe.AddIngredient(ItemID.Torch, 25);
            recipe.AddIngredient(ItemID.Gel, 99);
            recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
            recipe.SetResult(this); //Sets the result of this recipe to this item
            recipe.AddRecipe(); //Adds the recipe to the mod

            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(ItemID.GoldBroadsword, 1);
            recipe.AddIngredient(ItemID.Torch, 25);
            recipe.AddIngredient(ItemID.Gel, 99);
            recipe.AddTile(TileID.Anvils); //The tile you craft this sword at
            recipe.SetResult(this); //Sets the result of this recipe to this item
            recipe.AddRecipe(); //Adds the recipe to the mod
        }
    }
}