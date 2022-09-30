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
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Blueprints.Classes;

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
        //[HarmonyPatch(typeof(InteractionHighlightController), nameof(InteractionHighlightController.HighlightOn))]
        [HarmonyPatch(typeof(UnitSpawnerBase), nameof(UnitSpawner.Spawn))]
    internal static class UnitSpawner3_Spawn_Patch
    {
        private static void Postfix(UnitSpawnerBase __instance)
        {
                UnitEntityData entityData = __instance.Data.SpawnedUnit.Value;
                if (entityData != null)
                {
                    AdjustStats(entityData);
                    CreateRandomStats(entityData);
                }
       }
    }

    static void CreateRandomStats(UnitEntityData unitEntityData)
        {
            bool flag1 = (Main.settings.userandomstats);
            if (flag1)
            {
                bool flag2 = unitEntityData.IsPlayersEnemy && !unitEntityData.IsPlayerFaction;
                if (flag2)
                {

                    int StatModifier = CreateRandomStat(unitEntityData.Stats.Strength.m_BaseValue);
                    unitEntityData.Stats.Strength.AddModifier(StatModifier, ModifierDescriptor.Cooking);

                    StatModifier = CreateRandomStat(unitEntityData.Stats.Dexterity.m_BaseValue);
                    unitEntityData.Stats.Dexterity.AddModifier(StatModifier, ModifierDescriptor.Cooking);

                    StatModifier = CreateRandomStat(unitEntityData.Stats.Constitution.m_BaseValue);
                    unitEntityData.Stats.Constitution.AddModifier(StatModifier, ModifierDescriptor.Cooking);

                    StatModifier = CreateRandomStat(unitEntityData.Stats.Intelligence.m_BaseValue);
                    unitEntityData.Stats.Intelligence.AddModifier(StatModifier, ModifierDescriptor.Cooking);

                    StatModifier = CreateRandomStat(unitEntityData.Stats.Wisdom.m_BaseValue);
                    unitEntityData.Stats.Wisdom.AddModifier(StatModifier, ModifierDescriptor.Cooking);

                    StatModifier = CreateRandomStat(unitEntityData.Stats.Charisma.m_BaseValue);
                    unitEntityData.Stats.Charisma.AddModifier(StatModifier, ModifierDescriptor.Cooking);

                    StatModifier = CreateRandomStat(unitEntityData.Stats.BaseAttackBonus.m_BaseValue);
                    unitEntityData.Stats.AdditionalAttackBonus.AddModifier(StatModifier, ModifierDescriptor.Cooking);

                    StatModifier = CreateRandomStat(unitEntityData.Stats.AC.m_BaseValue);
                    unitEntityData.Stats.AC.AddModifier(StatModifier, ModifierDescriptor.Cooking);

                    StatModifier = CreateRandomStat(unitEntityData.Stats.HitPoints.m_BaseValue);
                    unitEntityData.Stats.HitPoints.AddModifier(StatModifier, ModifierDescriptor.Cooking);

                }

            }

        }

        private static int CreateRandomStat(int statIn)
        {
            float randomRoll = 0f;
            do
            {
                randomRoll = UnityEngine.Random.Range(-0.3f, 0.3f);
            } while (randomRoll == 0f);

           int result = (int)Math.Ceiling(statIn * randomRoll);
           //Main.LogDebug("Roll" + randomRoll + " statIn " + statIn + " result " + result);

            return result;
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
                    {
                        unitEntityData.Stats.Strength.RemoveModifiers(ModifierDescriptor.Cooking);
                        unitEntityData.Stats.Strength.AddModifier(Main.settings.enemyFinalStrAmount, ModifierDescriptor.Cooking);
                    }

                    if (Main.settings.enemyFinalDexAmount != 0!)
                            { unitEntityData.Stats.Dexterity.RemoveModifiers(ModifierDescriptor.Cooking); 
                        unitEntityData.Stats.Dexterity.AddModifier(Main.settings.enemyFinalDexAmount, ModifierDescriptor.Cooking); }

                            if (Main.settings.enemyFinalConAmount != 0)
                            { unitEntityData.Stats.Constitution.RemoveModifiers(ModifierDescriptor.Cooking); 
                        unitEntityData.Stats.Constitution.AddModifier(Main.settings.enemyFinalConAmount, ModifierDescriptor.Cooking); }

                            if (Main.settings.enemyFinalIntAmount != 0)
                            {
                        unitEntityData.Stats.Intelligence.RemoveModifiers(ModifierDescriptor.Cooking);
                        unitEntityData.Stats.Intelligence.AddModifier(Main.settings.enemyFinalIntAmount, ModifierDescriptor.Cooking); }

                            if (Main.settings.enemyFinalWisAmount != 0)
                            {
                        unitEntityData.Stats.Wisdom.RemoveModifiers(ModifierDescriptor.Cooking);
                        unitEntityData.Stats.Wisdom.AddModifier(Main.settings.enemyFinalWisAmount, ModifierDescriptor.Cooking); }

                            if (Main.settings.enemyFinalChaAmount != 0)
                            { unitEntityData.Stats.Charisma.RemoveModifiers(ModifierDescriptor.Cooking); 
                        unitEntityData.Stats.Charisma.AddModifier(Main.settings.enemyFinalChaAmount, ModifierDescriptor.Cooking); }

                            if (Main.settings.enemyFinalACAmount != 0) 
                            { unitEntityData.Stats.AC.RemoveModifiers(ModifierDescriptor.Cooking); 
                        unitEntityData.Stats.AC.AddModifier(Main.settings.enemyFinalACAmount, ModifierDescriptor.Cooking); }

                            if (Main.settings.enemyFinalABAmount != 0)
                            {
                        unitEntityData.Stats.AdditionalAttackBonus.RemoveModifiers(ModifierDescriptor.Cooking);
                        unitEntityData.Stats.AdditionalAttackBonus.AddModifier(Main.settings.enemyFinalABAmount, ModifierDescriptor.Cooking); }

                            if (Main.settings.enemyFinalFortAmount != 0)
                            { unitEntityData.Stats.SaveFortitude.RemoveModifiers(ModifierDescriptor.Cooking);
                        unitEntityData.Stats.SaveFortitude.AddModifier(Main.settings.enemyFinalFortAmount, ModifierDescriptor.Cooking); }

                            if (Main.settings.enemyFinalWillAmount != 0)
                            {
                        unitEntityData.Stats.SaveWill.RemoveModifiers(ModifierDescriptor.Cooking);
                        unitEntityData.Stats.SaveWill.AddModifier(Main.settings.enemyFinalWillAmount, ModifierDescriptor.Cooking); }

                            if (Main.settings.enemyFinalReflexAmount != 0)
                            {
                        unitEntityData.Stats.SaveReflex.RemoveModifiers(ModifierDescriptor.Cooking);
                        unitEntityData.Stats.SaveReflex.AddModifier(Main.settings.enemyFinalReflexAmount, ModifierDescriptor.Cooking); }

                            if (Main.settings.enemyFinalDMGAmount != 0)
                            {
                        unitEntityData.Stats.AdditionalDamage.RemoveModifiers(ModifierDescriptor.Cooking);
                        unitEntityData.Stats.AdditionalDamage.AddModifier(Main.settings.enemyFinalDMGAmount, ModifierDescriptor.Cooking); }

                            if (Main.settings.enemyFinalCMDAmount != 0)
                            {
                        unitEntityData.Stats.AdditionalCMD.RemoveModifiers(ModifierDescriptor.Cooking);
                        unitEntityData.Stats.AdditionalCMD.AddModifier(Main.settings.enemyFinalCMDAmount, ModifierDescriptor.Cooking); }

                    }
                }
           
        }

        [HarmonyPatch(typeof(Player), "OnAreaLoaded")]
        internal static class UnitSpawner_Spawn_Patch
        {
            private static void Postfix()
            {

                foreach (UnitEntityData unitEntityData in Game.Instance.State.Units)
                {
                    bool flag = (unitEntityData != null);
                    if (flag)
                    {
                        AdjustStats(unitEntityData);
                    }

                }
            }
        }



        [HarmonyPatch(typeof(GameHistoryLog), nameof(GameHistoryLog.HandlePartyCombatStateChanged))]
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
        //[HarmonyPatch(typeof(CombatController), nameof(CombatController.Activate))]
        //private static class TurnBasedActivate_Patch
        //{
        //    private static void Postfix()
        //    {
        //        bool flag1 = Game.Instance.Player.IsInCombat && Main.settings.CombatSpeed && Game.Instance.TurnBasedCombatController.m_Enabled;
        //        if (flag1)
        //        {
        //            if (Game.Instance.TimeController.DebugTimeScale != Main.settings.tbcombatSpeedFloat)
        //            {
        //                Game.Instance.TimeController.DebugTimeScale = Main.settings.tbcombatSpeedFloat;
        //            }
        //        }

        //        bool flag2 = !Game.Instance.Player.IsInCombat && Main.settings.CombatSpeed;
        //        if (flag2)
        //        {
        //            Game.Instance.TimeController.DebugTimeScale = Main.settings.OutOfCombatSpeed;
        //        }
        //    }
        //}
        //[HarmonyPatch(typeof(CombatController), nameof(CombatController.Deactivate))]
        //private static class TurnBasedDeActivate_Patch
        //{
        //    private static void Postfix()
        //    {
        //        bool flag = Game.Instance.Player.IsInCombat && Main.settings.CombatSpeed && !Game.Instance.TurnBasedCombatController.m_Enabled;
        //        if (flag)
        //        {
        //            if (Game.Instance.TimeController.DebugTimeScale != Main.settings.combatSpeedFloat)
        //            {
        //                Game.Instance.TimeController.DebugTimeScale = Main.settings.combatSpeedFloat;
        //            }
        //        }
        //        bool flag2 = !Game.Instance.Player.IsInCombat && Main.settings.CombatSpeed;
        //        if (flag2)
        //        {
        //            Game.Instance.TimeController.DebugTimeScale = Main.settings.OutOfCombatSpeed;
        //        }
        //    }
        //}

    }
}

