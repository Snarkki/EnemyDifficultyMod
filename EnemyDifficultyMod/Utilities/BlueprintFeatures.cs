using EnemyDifficultyMod;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemyDifficultyMod.Utilities
{
	public static class BlueprintFeatures
	{


		public static BlueprintCharacterClass ArcanistClass
		{
			get
			{
				return Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
			}
		}
		public static BlueprintCharacterClass SorcererClass
		{
			get
			{
				return Resources.GetBlueprint<BlueprintCharacterClass>("b3a505fb61437dc4097f43c3f8f9a4cf");
			}
		}
		public static BlueprintCharacterClass WizardClass
		{
			get
			{
				return Resources.GetBlueprint<BlueprintCharacterClass>("ba34257984f4c41408ce1dc2004e342e");
			}
		}
		public static BlueprintCharacterClass WitchClass
		{
			get
			{
				return Resources.GetBlueprint<BlueprintCharacterClass>("b9873f1e7bfe5449bc84d03e9c8e3cc");
			}
		}

		public static BlueprintFeatureSelection BasicFeatSelection
		{
			get
			{
				return Resources.GetBlueprint<BlueprintFeatureSelection>("247a4068296e8be42890143f451b4b45");
			}
		}

		public static BlueprintFeatureSelection MythicAbilitySelection
		{
			get
			{
				return Resources.GetBlueprint<BlueprintFeatureSelection>("ba0e5a900b775be4a99702f1ed08914d");
			}
		}

		public static BlueprintFeatureSelection MythicFeatSelection
		{
			get
			{
				return Resources.GetBlueprint<BlueprintFeatureSelection>("9ee0f6745f555484299b0a1563b99d81");
			}
		}
		public static BlueprintProgression BasicFeatsProgression
		{
			get
			{
				return Resources.GetBlueprint<BlueprintProgression>("5b72dd2ca2cb73b49903806ee8986325");
			}
		}

		public static BlueprintProgression MythicCompanionProgression
		{
			get
			{
				return Resources.GetBlueprint<BlueprintProgression>("21e74c19da02acb478e32da25abd9d28");
			}
		}


		public static BlueprintProgression MythicStartingProgression
		{
			get
			{
				return Resources.GetBlueprint<BlueprintProgression>("af4ee0acb9114e544bf02f39027966b0");
			}
		}


		public static BlueprintProgression AeonProgression
		{
			get
			{
				return Resources.GetBlueprint<BlueprintProgression>("34b9484b0d5ce9340ae51d2bf9518bbe");
			}
		}

		public static BlueprintProgression AngelProgression
		{
			get
			{
				return Resources.GetBlueprint<BlueprintProgression>("2f6fe889e91b6a645b055696c01e2f74");
			}
		}


		public static BlueprintProgression AzataProgression
		{
			get
			{
				return Resources.GetBlueprint<BlueprintProgression>("9db53de4bf21b564ca1a90ff5bd16586");
			}
		}


		public static BlueprintProgression DemonProgression
		{
			get
			{
				return Resources.GetBlueprint<BlueprintProgression>("285fe49f7df8587468f676aa49362213");
			}
		}


		public static BlueprintProgression DevilProgression
		{
			get
			{
				return Resources.GetBlueprint<BlueprintProgression>("87bc9abf00b240a44bb344fea50ec9bc");
			}
		}

		public static BlueprintProgression GoldenDragonProgression
		{
			get
			{
				return Resources.GetBlueprint<BlueprintProgression>("a6fbca43902c6194c947546e89af64bd");
			}
		}


		public static BlueprintProgression LegendProgression
		{
			get
			{
				return Resources.GetBlueprint<BlueprintProgression>("905383229aaf79e4b8d7e2d316b68715");
			}
		}


		public static BlueprintProgression LichProgression
		{
			get
			{
				return Resources.GetBlueprint<BlueprintProgression>("ccec4e01b85bf5d46a3c3717471ba639");
			}
		}


		public static BlueprintProgression SwarmThatWalksProgression
		{
			get
			{
				return Resources.GetBlueprint<BlueprintProgression>("bf5f103ccdf69254abbad84fd371d5c9");
			}
		}

		public static BlueprintProgression TricksterProgression
		{
			get
			{
				return Resources.GetBlueprint<BlueprintProgression>("cc64789b0cc5df14b90da1ffee7bbeea");
			}
		}
	}
}
