using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.Windows.Forms;

namespace UnbiddenMod
{
  public class UnbiddenItem : GlobalItem
  {
    public override bool InstancePerEntity => true;

    public int element = -1; // -1 means Typeless, meaning we don't worry about this in the first place

    public UnbiddenItem()
    {
      element = -1;
    }
    public override GlobalItem Clone(Item item, Item itemClone)
    {
      UnbiddenItem myClone = (UnbiddenItem)base.Clone(item, itemClone);
      myClone.element = element;
      return myClone;
    }
  }
}