﻿using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

using Settings = Ashe_Beta_Fixed.MenusSetting.Modes.JungleClear;

namespace Ashe_Beta_Fixed.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ExecuteOnComands()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            if (Settings.mana >= Player.Instance.ManaPercent)
                return;

            if (Settings.UseQ && Q.IsReady() && EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, 610).Count() > 0)
            {
                foreach (var b in Player.Instance.Buffs)
                    if (b.Name == "asheqcastready")
                    {
                        Q.Cast();
                    }
            }
            if (Settings.UseW && W.IsReady())
            {
                var minion = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, W.Range).Where(t => !t.IsDead && t.IsValid && !t.IsInvulnerable).OrderBy(t => t.MaxHealth).FirstOrDefault();
                if (minion != null)
                {
                    W.Cast(minion);
                }
            }
        }
    }
}