using Kingmaker.Blueprints.Classes;

using UnityModManagerNet;

namespace EnemyDifficultyMod
{
    public class Settings : UnityModManager.ModSettings
    {

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            UnityModManager.ModSettings.Save<Settings>(this, modEntry);
        }

        public string searchText = "";
        public int searchLimit = 100;
        public int selectedTab = 0;

        public string enemyStrAmount = "0";
        public int enemyFinalStrAmount = 0;
        public bool usestats_str = false;

        public string enemyDexAmount = "0";
        public int enemyFinalDexAmount = 0;
        public bool usestats_dex = false;

        public string enemyConAmount = "0";
        public int enemyFinalConAmount = 0;
        public bool usestats_con = false;

        public string enemyIntAmount = "0";
        public int enemyFinalIntAmount = 0;
        public bool usestats_int = false;

        public string enemyWisAmount = "0";
        public int enemyFinalWisAmount = 0;
        public bool usestats_wis = false;

        public string enemyChaAmount = "0";
        public int enemyFinalChaAmount = 0;
        public bool usestats_cha = false;

        public string enemyABAmount = "0";
        public int enemyFinalABAmount = 0;
        public bool usestats_ab = false;

        public string enemyACAmount = "0";
        public int enemyFinalACAmount = 0;
        public bool usestats_ac = false;

        public string enemyFortAmount = "0";
        public int enemyFinalFortAmount = 0;
        public bool usestats_fort = false;

        public string enemyWillAmount = "0";
        public int enemyFinalWillAmount = 0;
        public bool usestats_will = false;

        public string enemyReflexAmount = "0";
        public int enemyFinalReflexAmount = 0;
        public bool usestats_reflex = false;

        public string enemySpeedAmount = "0";
        public int enemyFinalSpeedAmount = 0;
        public bool usestats_speed = false;

        public string enemyCMDAmount = "0";
        public int enemyFinalCMDAmount = 0;
        public bool usestats_cmd = false;

        public string enemyDMGAmount = "0";
        public int enemyFinalDMGAmount = 0;
        public bool usestats_dmg = false;

        public float enemyHPmultiplier = 1f;
        public bool usestats_hp = false;

        public float combatSpeedFloat = 1;
        public float tbcombatSpeedFloat = 1;
        public float OutOfCombatSpeed = 1;

        public bool removestats = false;
        public bool usestats = true;
        public bool slowcombat = false;
        public int game_difficulty = 0;



        public bool CombatSpeed = false;
        public bool enemyBuffsActivated = false;
        public int enemyBuffInt = 0;
        public string enemyBuffString = "0";
    }
}
