using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace untitled.Core.Scripts
{
	// Token: 0x0200002C RID: 44
	public class Notifications : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000CC61
		private void Awake()
		{
			Notifications.instance = this;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000CC6C
		private void Init()
		{
			bool flag = Notifications.MainCamera != null;
			if (!flag)
			{
				Notifications.MainCamera = GameObject.Find("Main Camera");
				Notifications.HUDObj2 = new GameObject("NotificationsParent");
				Notifications.HUDObj = new GameObject("NotificationsObject");
				Canvas canvas = Notifications.HUDObj.AddComponent<Canvas>();
				Notifications.HUDObj.AddComponent<CanvasScaler>();
				Notifications.HUDObj.GetComponent<CanvasScaler>().dynamicPixelsPerUnit = 10f;
				Notifications.HUDObj.AddComponent<GraphicRaycaster>();
				canvas.renderMode = 2;
				canvas.worldCamera = Notifications.MainCamera.GetComponent<Camera>();
				RectTransform component = Notifications.HUDObj.GetComponent<RectTransform>();
				component.sizeDelta = new Vector2(5f, 5f);
				component.localScale = Vector3.one;
				component.localPosition = new Vector3(0f, 0f, 1.6f);
				component.rotation = Quaternion.Euler(0f, -270f, 0f);
				Notifications.HUDObj.transform.parent = Notifications.HUDObj2.transform;
				Notifications.HUDObj2.transform.position = Notifications.MainCamera.transform.position + new Vector3(-1.5f, 0f, -4.5f);
				Notifications.notificationText = this.CreateTextElement("NotificationText", Notifications.HUDObj, new Vector3(-1.2f, -0.9f, -0.22f), new Vector2(260f, 70f), 7);
				Notifications.notificationText.font = (Notifications.notificationText.font = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/debugtext").GetComponent<Text>().font);
				Notifications.notificationText.material = new Material(Shader.Find("GUI/Text Shader"));
				this.hasInitialized = true;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000CE48
		private Text CreateTextElement(string name, GameObject parent, Vector3 position, Vector2 size, int fontSize)
		{
			Text text = new GameObject(name)
			{
				transform = 
				{
					parent = parent.transform
				}
			}.AddComponent<Text>();
			text.fontSize = fontSize;
			text.alignment = 4;
			text.rectTransform.sizeDelta = size;
			text.rectTransform.localScale = new Vector3(0.01f, 0.01f, 1f);
			text.rectTransform.localPosition = position;
			return text;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000CEC8
		private void FixedUpdate()
		{
			bool flag = !this.hasInitialized && GameObject.Find("Main Camera") != null;
			if (flag)
			{
				this.Init();
			}
			bool flag2 = Notifications.HUDObj2 != null && Notifications.MainCamera != null;
			if (flag2)
			{
				Notifications.HUDObj2.transform.SetPositionAndRotation(Notifications.MainCamera.transform.position, Notifications.MainCamera.transform.rotation);
			}
			float now = Time.time;
			List<string> notificationsToRemove = (from kvp in Notifications.notificationTimestamps
			where now - kvp.Value > Notifications.notificationDelay
			select kvp.Key).ToList<string>();
			bool flag3 = notificationsToRemove.Any<string>();
			if (flag3)
			{
				Notifications.notificationText.text = string.Join(Environment.NewLine, from line in Notifications.notificationText.text.Split(new string[]
				{
					Environment.NewLine
				}, StringSplitOptions.None)
				where !notificationsToRemove.Contains(line)
				select line);
				notificationsToRemove.ForEach(delegate(string notification)
				{
					Notifications.notificationTimestamps.Remove(notification);
				});
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000D024
		public static void SendNotification(string content)
		{
			bool flag = !Notifications.IsEnabled || content == Notifications.PreviousNotification;
			if (!flag)
			{
				bool flag2 = !content.Contains(Environment.NewLine);
				if (flag2)
				{
					content += Environment.NewLine;
				}
				Text text = Notifications.notificationText;
				text.text += content;
				Notifications.notificationText.color = Color.white;
				Notifications.PreviousNotification = content;
				Notifications.notificationTimestamps[content.Trim()] = Time.time;
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000D0B1
		public static void ClearAllNotifications()
		{
			Notifications.notificationText.text = string.Empty;
			Notifications.notificationTimestamps.Clear();
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000D0D0
		public static void ClearPastNotifications(int amount)
		{
			string[] value = (from line in Notifications.notificationText.text.Split(new string[]
			{
				Environment.NewLine
			}, StringSplitOptions.None).Skip(amount)
			where !string.IsNullOrEmpty(line)
			select line).ToArray<string>();
			Notifications.notificationText.text = string.Join(Environment.NewLine, value);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000D144
		private void OnDestroy()
		{
			bool flag = Notifications.notificationText.material != null;
			if (flag)
			{
				Object.Destroy(Notifications.notificationText.material);
			}
			Object.Destroy(Notifications.HUDObj);
			Object.Destroy(Notifications.HUDObj2);
		}

		// Token: 0x04000099 RID: 153
		public static Notifications instance;

		// Token: 0x0400009A RID: 154
		private static GameObject HUDObj;

		// Token: 0x0400009B RID: 155
		private static GameObject HUDObj2;

		// Token: 0x0400009C RID: 156
		private static GameObject MainCamera;

		// Token: 0x0400009D RID: 157
		private static Text notificationText;

		// Token: 0x0400009E RID: 158
		private static readonly float notificationDelay = 1f;

		// Token: 0x0400009F RID: 159
		private static Dictionary<string, float> notificationTimestamps = new Dictionary<string, float>();

		// Token: 0x040000A0 RID: 160
		public static string PreviousNotification;

		// Token: 0x040000A1 RID: 161
		public static bool IsEnabled = true;

		// Token: 0x040000A2 RID: 162
		private bool hasInitialized;
	}
}
