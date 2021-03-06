using Terraria;
using Terraria.ModLoader;

namespace UnbiddenMod.Buffs.StatDebuffs
{
  public class Intimidated : ModBuff
  {
    public override void SetDefaults()
    {
      DisplayName.SetDefault("Intimidated");
      Description.SetDefault("Enemies are scared!");
      Main.debuff[Type] = true;
      Main.pvpBuff[Type] = true;
      Main.buffNoSave[Type] = true;
    }
    public override void Update(Player player, ref int buffIndex)
    {
      UnbiddenPlayer unPlayer = player.Unbidden();
      unPlayer.intimidated = true;
    }
  }
}