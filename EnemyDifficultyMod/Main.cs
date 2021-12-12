using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using Kingmaker.Blueprints.JsonSystem;
using EnemyDifficultyMod.Config;
using EnemyDifficultyMod.Utilities;
//using Kingmaker.Blueprints.JsonSystem;
using UnityEngine;
using UnityModManagerNet;
using Kingmaker.Localization;
using static Kingmaker.TurnBasedMode.Controllers.CombatAction;
using Kingmaker.EntitySystem.Entities;
using TurnBased.Controllers;
using static TurnBased.Controllers.CombatController;
//using static UnityModManagerNet.UnityModManager;
namespace EnemyDifficultyMod
{
    public class Main
    {
        private const string CombatSpeed = "Change Combat Speed";
        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            modEntry.OnToggle = new Func<UnityModManager.ModEntry, bool, bool>(Main.OnToggle);
            var harmony = new Harmony(modEntry.Info.Id);

            Main.settings = UnityModManager.ModSettings.Load<Settings>(modEntry);
            ModSettings.ModEntry = modEntry;
            ModSettings.LoadAllSettings();
            modEntry.OnGUI = new Action<UnityModManager.ModEntry>(Main.OnGUI);
            modEntry.OnSaveGUI = new Action<UnityModManager.ModEntry>(Main.OnSaveGUI);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            PostPatchInitializer.Initialize();
            return true;
        }
        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Main.iAmEnabled = value;
            return true;
        }

        private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Main.settings.Save(modEntry);
        }

        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (!Main.iAmEnabled)
            {
                return;
            }

            GUILayout.Space(5f);

            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.Space(5f);
            GUILayout.Label("Adjust enemy attributes", new GUILayoutOption[]
                 {
                    GUILayout.Width(200f)
                 });
            GUILayout.EndHorizontal();

            CreateStatButtons("Strength", ref Main.settings.enemyStrAmount, ref Main.settings.enemyFinalStrAmount);
            CreateStatButtons("Dexterity", ref Main.settings.enemyDexAmount, ref Main.settings.enemyFinalDexAmount);
            CreateStatButtons("Constitution", ref Main.settings.enemyConAmount, ref Main.settings.enemyFinalConAmount);
            CreateStatButtons("Intelligence", ref Main.settings.enemyIntAmount, ref Main.settings.enemyFinalIntAmount);
            CreateStatButtons("Wisdom", ref Main.settings.enemyWisAmount, ref Main.settings.enemyFinalWisAmount);
            CreateStatButtons("Charisma", ref Main.settings.enemyChaAmount, ref Main.settings.enemyFinalChaAmount);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.Label("Other adjustments", new GUILayoutOption[]
            {
                    GUILayout.Width(200f)
            });
            GUILayout.EndHorizontal();
            CreateStatButtons("Attack bonus", ref Main.settings.enemyABAmount, ref Main.settings.enemyFinalABAmount);
            CreateStatButtons("AC", ref Main.settings.enemyACAmount, ref Main.settings.enemyFinalACAmount);
            CreateStatButtons("Fortitude saving", ref Main.settings.enemyFortAmount, ref Main.settings.enemyFinalFortAmount);
            CreateStatButtons("Will saving", ref Main.settings.enemyWillAmount, ref Main.settings.enemyFinalWillAmount);
            CreateStatButtons("Reflex saving", ref Main.settings.enemyReflexAmount, ref Main.settings.enemyFinalReflexAmount);
            CreateStatButtons("Speed", ref Main.settings.enemySpeedAmount, ref Main.settings.enemyFinalSpeedAmount);
            CreateStatButtons("CMD", ref Main.settings.enemyCMDAmount, ref Main.settings.enemyFinalCMDAmount);
            CreateStatButtons("Additional DMG", ref Main.settings.enemyDMGAmount, ref Main.settings.enemyFinalDMGAmount);

            //CreateBuffInt("Buff level to use", ref Main.settings.enemyBuffString, ref Main.settings.enemyBuffInt);

            // tähä tarvitaan buff adjusteri palkki, min max value 0-10 (get integer pitäs toimii siihen)
            // combat speed tarvii tehä floatti eikä integer et saa desimaaleja. 

            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            Main.settings.usestats = GUILayout.Toggle(Main.settings.usestats, "Use adjusted stat", Array.Empty<GUILayoutOption>());
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            Main.settings.removestats = GUILayout.Toggle(Main.settings.removestats, "Remove stats from player", Array.Empty<GUILayoutOption>());
            GUILayout.EndHorizontal();


            //CreateStatButtons("Combat Speed Modifier", ref Main.settings.combatSpeedString, ref Main.settings.combatSpeedInt);
            //CreateStatButtons("Out of combat speed modifier", ref Main.settings.OutOfCombatSpeedString, ref Main.settings.OutOfCombatSpeedInt);
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            settings.combatSpeedFloat = GUILayout.HorizontalSlider(settings.combatSpeedFloat, 0.0f, 10.0f, GUILayout.Width(100f));
            GUILayout.Label("Combat Speed Modifier: " + Math.Round(settings.combatSpeedFloat, 1));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            settings.OutOfCombatSpeed = GUILayout.HorizontalSlider(settings.OutOfCombatSpeed, 0.0f, 10.0f, GUILayout.Width(100f));
            GUILayout.Label("Out Of Combat Speed modifier: " + Math.Round(settings.OutOfCombatSpeed, 1));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            settings.tbcombatSpeedFloat = GUILayout.HorizontalSlider(settings.tbcombatSpeedFloat, 0.0f, 10.0f, GUILayout.Width(100f));
            GUILayout.Label("Turn based Speed modifier: " + Math.Round(settings.tbcombatSpeedFloat, 1));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            //LogSlider(mainTimeScaleTitle, ref settings.combatSpeedFloat, 0f, 20, 1, 1, "", Width(450));
            //LogSlider(mainTimeScaleTitle, ref settings.OutOfCombatSpeed, 0f, 20, 1, 1, "", Width(450));
            Main.settings.CombatSpeed = GUILayout.Toggle(Main.settings.CombatSpeed, "Use combat speed", Array.Empty<GUILayoutOption>());
            GUILayout.EndHorizontal();

        }
        public static bool GetInteger(string stringToConvert, out int intOutValue, int minLimit, int maxLimit)
        {
            bool parsed = int.TryParse(stringToConvert, out intOutValue);
            return parsed && intOutValue >= minLimit && intOutValue <= maxLimit;
        }
        public static void CreateBuffInt(string StatName, ref string enemystat, ref int enemyfinalstat)
        {
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.Label(StatName, GUILayout.ExpandWidth(false));
            GUILayout.Space(10);
            enemystat = GUILayout.TextField(enemystat, 3, new GUILayoutOption[]
                 {
                    GUILayout.Width(85f)
                 });
            //int.TryParse(enemystat, out int border_r);
            GetInteger(enemystat, out enemyfinalstat, 0, 10);
            bool flag = GUILayout.Button("<b>+</b>", new GUILayoutOption[]
            {
                GUILayout.Width(30f)
            });
            if (flag)
            {
                if (enemyfinalstat < 10)
                {
                    enemyfinalstat = enemyfinalstat + 1;
                }
                enemystat = enemyfinalstat.ToString();
            }
            bool flag1 = GUILayout.Button("<b>-</b>", new GUILayoutOption[]
            {
                GUILayout.Width(30f)
            });

            if (flag1)
            {
                if (enemyfinalstat > 0)
                {
                    enemyfinalstat = enemyfinalstat - 1;
                }
                enemystat = enemyfinalstat.ToString();
            }
            GUILayout.EndHorizontal();
        }
            public static void CreateStatButtons(string StatName, ref string enemystat, ref int enemyfinalstat)
        {
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.Label(StatName, GUILayout.ExpandWidth(false));
            GUILayout.Space(10);
            enemystat = GUILayout.TextField(enemystat, 3, new GUILayoutOption[]
                 {
                    GUILayout.Width(85f)
                 });
            int.TryParse(enemystat, out int border_r);
            enemyfinalstat = border_r;
            bool flag = GUILayout.Button("<b>+</b>", new GUILayoutOption[]
            {
                GUILayout.Width(30f)
            });
            if (flag)
            {
                enemyfinalstat = enemyfinalstat + 1;
                enemystat = enemyfinalstat.ToString();
                //usestats = false;
            }
            bool flag1 = GUILayout.Button("<b>-</b>", new GUILayoutOption[]
            {
                GUILayout.Width(30f)
            });

            if (flag1)
            {
                enemyfinalstat = enemyfinalstat - 1;
                enemystat = enemyfinalstat.ToString();
                //usestats = false;
            }
            GUILayout.EndHorizontal();
        }

        public static bool removestats = false;
        private static bool iAmEnabled;

        public static Settings settings;

        public static void Log(string msg)
        {
            ModSettings.ModEntry.Logger.Log(msg);
        }
        [System.Diagnostics.Conditional("DEBUG")]
        public static void LogDebug(string msg)
        {
            ModSettings.ModEntry.Logger.Log(msg);
        }
        public static void LogPatch(string action, [NotNull] IScriptableObjectWithAssetId bp)
        {
            Log($"{action}: {bp.AssetGuid} - {bp.name}");
        }
        public static void LogHeader(string msg)
        {
            Log($"--{msg.ToUpper()}--");
        }
        public static Exception Error(String message)
        {
            Log(message);
            return new InvalidOperationException(message);
        }

        public static LocalizedString MakeLocalizedString(string key, string value)
        {
            LocalizationManager.CurrentPack.Strings[key] = value;
            LocalizedString localizedString = new LocalizedString();
            typeof(LocalizedString).GetField("m_Key", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(localizedString, key);
            return localizedString;
        }
    }
}