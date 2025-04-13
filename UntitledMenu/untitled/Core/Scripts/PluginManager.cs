using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Untitled.Core.Scripts
{
	// Token: 0x0200003D RID: 61
	internal class PluginManager : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001A00E
		public void Start()
		{
			this.LoadPlugins();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001A018
		public void LoadPlugins()
		{
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001A028
		public void Update()
		{
			bool initialized = this.Initialized;
			if (initialized)
			{
				bool flag = this.activePlugins.Count > 0;
				if (flag)
				{
					foreach (KeyValuePair<string, PluginButtonInfo> keyValuePair in this.activePlugins)
					{
						PluginButtonInfo value = keyValuePair.Value;
						bool flag2 = value.IsToggle && value.IsToggled;
						if (flag2)
						{
							MethodInfo method = keyValuePair.Value.ModInstance.GetType().GetMethod("Update", BindingFlags.Instance | BindingFlags.Public);
							bool flag3 = method != null;
							if (flag3)
							{
								method.Invoke(keyValuePair.Value, null);
							}
						}
					}
				}
			}
		}

		// Token: 0x04000144 RID: 324
		public Dictionary<string, PluginButtonInfo> activePlugins = new Dictionary<string, PluginButtonInfo>();

		// Token: 0x04000145 RID: 325
		public bool Initialized = false;
	}
}
