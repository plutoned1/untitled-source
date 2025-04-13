using System;
using System.Reflection;
using BepInEx;
using HarmonyLib;

namespace untitled
{
	// Token: 0x02000006 RID: 6
	[BepInPlugin("cheat.panel.untitled", "untitled's Cheat Panel", "1.0.0")]
	public class Plugin : BaseUnityPlugin
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002C25 File Offset: 0x00000E25
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002C2C File Offset: 0x00000E2C
		public static Plugin instance { get; private set; }

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002C34
		private void Start()
		{
			Plugin.instance = this;
			Harmony harmony = new Harmony("cheat.panel.untitled");
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
	}
}
