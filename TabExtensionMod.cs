using System.Collections;
using MelonLoader;
using TabExtension.Config;
using TabExtension.UI;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace TabExtension
{
	public class TabExtensionMod : MelonMod
	{
		public static readonly string Version = "1.5.0";

		public static TabExtensionMod Instance { get; private set; }
		public static GameObject UserInterface { get; private set; }

		public override void OnInitializeMelon()
		{
			Instance = this;
			MelonLogger.Msg("Initializing TabExtension " + Version + "...");
			Configuration.Init();
			ClassInjector.RegisterTypeInIl2Cpp<LayoutListener>();
			ClassInjector.RegisterTypeInIl2Cpp<TabLayout>();
			MelonCoroutines.Start(Init());
		}

		private IEnumerator Init()
		{
			while (VRCUiManager.field_Private_Static_VRCUiManager_0 == null) yield return null;
			foreach (var GameObjects in Resources.FindObjectsOfTypeAll<GameObject>())
			{
				if (GameObjects.name.Contains("UserInterface")) UserInterface = GameObjects;
			}
			while (UserInterface.transform.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent") == null) yield return null;
			GameObject quickMenu = UserInterface.transform.Find("Canvas_QuickMenu(Clone)").gameObject;
			GameObject layout = quickMenu.transform.Find("CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup").gameObject;
			layout.AddComponent<TabLayout>();
			MelonLogger.Msg("Running version " + Version + " of TabExtension.");
		}
	}
}
