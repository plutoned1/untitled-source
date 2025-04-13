using System;
using UnityEngine;
using untitled.Assets;
using untitled.Cheat;

namespace untitled.Core
{
	// Token: 0x02000010 RID: 16
	public class ButtonCollider : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000BE90
		public void OnTriggerEnter(Collider collider)
		{
			bool flag = collider != null && Menu.PointerObject != null;
			if (flag)
			{
				bool flag2 = collider == Menu.PointerObject.GetComponent<Collider>() && collider != null;
				if (flag2)
				{
					bool flag3 = Time.time > Menu.btnDelay + 0.2f;
					if (flag3)
					{
						Menu.btnDelay = Time.time;
						Menu.ToggleButton(this.ButtonIdentifier);
						Menu.ButtonCooldown = Time.frameCount;
						GorillaTagger.Instance.StartVibration(false, 1f, 0.1f);
						bool audio = Settings.Audio;
						if (audio)
						{
							AssetLoader.PlayClick(Settings.AudioName);
						}
						else
						{
							GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(Settings.AudioID, false, 0.8f);
						}
					}
				}
			}
		}

		// Token: 0x0400007B RID: 123
		public Button ButtonIdentifier;
	}
}
