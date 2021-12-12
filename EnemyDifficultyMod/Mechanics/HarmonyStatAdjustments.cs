using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnemyDifficultyMod.Mechanics;
using Kingmaker.UnitLogic;
using Kingmaker.RuleSystem.Rules;
using TurnBased.Controllers;
using static Kingmaker.EntitySystem.Stats.ModifiableValue;
using Kingmaker.View.Spawners;

namespace EnemyDifficultyMod.Mechanics
{
    public class HarmonyStatAdjustments
    { 
    //    [HarmonyPatch(typeof(RuleInitiativeRoll), "OnTrigger")]
    //    internal static class UnitSpawner_Spawn_Patch
    //    {
    //        private static void Prefix(RuleInitiativeRoll __instance)
    //        {

    //            bool flag0 = (__instance.Initiator != null);
    //            if (flag0)
    //            {
    //                AdjustStats(__instance.Initiator);
    //            }

    //        }
    //    }
    [HarmonyPatch(typeof(UnitSpawnerBase), "Spawn")]
    internal static class UnitSpawner3_Spawn_Patch
    {
        private static void Postfix(UnitSpawnerBase __instance)
        {
                UnitEntityData entityData = __instance.Data.SpawnedUnit.Value;
                if (entityData != null)
                {
                    AdjustStats(entityData);
                }
       }
    }

    static void AdjustStats(UnitEntityData unitEntityData)
        {
                bool flag1 = (Main.settings.usestats);
                if (flag1)
                {
                    bool flag2 = unitEntityData.IsPlayersEnemy && !unitEntityData.IsPlayerFaction;
                    if (flag2)
                    {
                            if (Main.settings.enemyFinalStrAmount != 0)
                            { unitEntityData.Stats.Strength.AddModifier(Main.settings.enemyFinalStrAmount, ModifierDescriptor.None); }

                            if (Main.settings.enemyFinalDexAmount != 0!)
                            { unitEntityData.Stats.Dexterity.AddModifier(Main.settings.enemyFinalDexAmount, ModifierDescriptor.None); }

                            if (Main.settings.enemyFinalConAmount != 0)
                            { unitEntityData.Stats.Constitution.AddModifier(Main.settings.enemyFinalConAmount, ModifierDescriptor.None); }

                            if (Main.settings.enemyFinalIntAmount != 0)
                            { unitEntityData.Stats.Intelligence.AddModifier(Main.settings.enemyFinalIntAmount, ModifierDescriptor.None); }

                            if (Main.settings.enemyFinalWisAmount != 0)
                            { unitEntityData.Stats.Wisdom.AddModifier(Main.settings.enemyFinalWisAmount, ModifierDescriptor.None); }

                            if (Main.settings.enemyFinalChaAmount != 0)
                            { unitEntityData.Stats.Charisma.AddModifier(Main.settings.enemyFinalChaAmount, ModifierDescriptor.None); }

                            if (Main.settings.enemyFinalACAmount != 0) 
                            { unitEntityData.Stats.AC.AddModifier(Main.settings.enemyFinalACAmount, ModifierDescriptor.None); }

                            if (Main.settings.enemyFinalABAmount != 0)
                            { unitEntityData.Stats.AdditionalAttackBonus.AddModifier(Main.settings.enemyFinalABAmount, ModifierDescriptor.None); }

                            if (Main.settings.enemyFinalFortAmount != 0)
                            { unitEntityData.Stats.SaveFortitude.AddModifier(Main.settings.enemyFinalFortAmount, ModifierDescriptor.None); }

                            if (Main.settings.enemyFinalWillAmount != 0)
                            { unitEntityData.Stats.SaveWill.AddModifier(Main.settings.enemyFinalWillAmount, ModifierDescriptor.None); }

                            if (Main.settings.enemyFinalReflexAmount != 0)
                            { unitEntityData.Stats.SaveReflex.AddModifier(Main.settings.enemyFinalReflexAmount, ModifierDescriptor.None); }

                            if (Main.settings.enemyFinalDMGAmount != 0)
                            { unitEntityData.Stats.AdditionalDamage.AddModifier(Main.settings.enemyFinalDMGAmount, ModifierDescriptor.None); }

                            if (Main.settings.enemyFinalCMDAmount != 0)
                            { unitEntityData.Stats.AdditionalCMD.AddModifier(Main.settings.enemyFinalCMDAmount, ModifierDescriptor.None); }

                    }
                }
           
        }

        //[HarmonyPatch(typeof(Game), "OnAreaLoaded")]
        //internal static class UnitSpawner1_Spawn_Patch
        //{
        //    private static void Postfix()
        //    {

        //        foreach (UnitEntityData unitEntityData in Game.Instance.State.Units)
        //        {
        //            bool flag = (unitEntityData != null);
        //            if (flag)
        //            {
        //                AdjustStats(unitEntityData);
        //            }
                        
        //        }
        //    }
        //}



        [HarmonyPatch(typeof(GameHistoryLog), "HandlePartyCombatStateChanged")]
        private static class GameHistoryLog_HandlePartyCombatStateChanged_Patch
        {
            private static void Postfix(ref bool inCombat)
            {
                bool flag = inCombat && Main.settings.CombatSpeed && !Game.Instance.TurnBasedCombatController.m_Enabled;
                if (flag)
                {
                    if (Game.Instance.TimeController.DebugTimeScale != Main.settings.combatSpeedFloat)
                    {
                        Game.Instance.TimeController.DebugTimeScale = Main.settings.combatSpeedFloat;
                    }
                }
                bool flag1 = inCombat && Main.settings.CombatSpeed && Game.Instance.TurnBasedCombatController.m_Enabled;
                if (flag1)
                {
                    if (Game.Instance.TimeController.DebugTimeScale != Main.settings.tbcombatSpeedFloat)
                    {
                        Game.Instance.TimeController.DebugTimeScale = Main.settings.tbcombatSpeedFloat;
                    }
                }
                bool flag2 = !inCombat && Main.settings.CombatSpeed;
                if (flag2)
                {
                    Game.Instance.TimeController.DebugTimeScale = Main.settings.OutOfCombatSpeed;
                }
            }
        }
        [HarmonyPatch(typeof(CombatController), "Activate")]
        private static class TurnBasedActivate_Patch
        {
            private static void Postfix()
            {
                bool flag1 = Game.Instance.Player.IsInCombat && Main.settings.CombatSpeed && Game.Instance.TurnBasedCombatController.m_Enabled;
                if (flag1)
                {
                    if (Game.Instance.TimeController.DebugTimeScale != Main.settings.tbcombatSpeedFloat)
                    {
                        Game.Instance.TimeController.DebugTimeScale = Main.settings.tbcombatSpeedFloat;
                    }
                }

                bool flag2 = !Game.Instance.Player.IsInCombat && Main.settings.CombatSpeed;
                if (flag2)
                {
                    Game.Instance.TimeController.DebugTimeScale = Main.settings.OutOfCombatSpeed;
                }
            }
        }
        [HarmonyPatch(typeof(CombatController), "Deactivate")]
        private static class TurnBasedDeActivate_Patch
        {
            private static void Postfix()
            {
                bool flag = Game.Instance.Player.IsInCombat && Main.settings.CombatSpeed && !Game.Instance.TurnBasedCombatController.m_Enabled;
                if (flag)
                {
                    if (Game.Instance.TimeController.DebugTimeScale != Main.settings.combatSpeedFloat)
                    {
                        Game.Instance.TimeController.DebugTimeScale = Main.settings.combatSpeedFloat;
                    }
                }
                bool flag2 = !Game.Instance.Player.IsInCombat && Main.settings.CombatSpeed;
                if (flag2)
                {
                    Game.Instance.TimeController.DebugTimeScale = Main.settings.OutOfCombatSpeed;
                }
            }
        }

    }
}

