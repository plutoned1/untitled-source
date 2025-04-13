using System;
using System.Collections.Generic;
using System.IO;
using KeyAuth;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000002 RID: 2
public class KeyAuthManager : MonoBehaviour
{
	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002050
	private void Start()
	{
		bool flag = PlayerPrefs.GetString("login_username") != null && PlayerPrefs.GetString("login_password") != null;
		if (flag)
		{
			this.loginUsernameBox.text = PlayerPrefs.GetString("login_username");
			this.loginPasswordBox.text = PlayerPrefs.GetString("login_password");
			this.loginRememberMeToggle.isOn = true;
		}
		else
		{
			this.loginRememberMeToggle.isOn = false;
		}
		KeyAuthManager.KeyAuthApp.init();
		bool success = KeyAuthManager.KeyAuthApp.response.success;
		if (success)
		{
			this.logsLbl.text = this.logsLbl.text + "\n <color=green> ! <color=white>Successfully Initialized";
		}
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000210C
	private void Update()
	{
		this.statusLbl.text = "Status: " + KeyAuthManager.KeyAuthApp.response.success.ToString();
		bool flag = KeyAuthManager.loggedIn;
		if (flag)
		{
			bool keyDown = Input.GetKeyDown(this.SendKeyAuthMessage);
			if (keyDown)
			{
				this.SendMessage();
			}
			this.RetrieveMessages();
		}
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002174
	public void Login()
	{
		KeyAuthManager.KeyAuthApp.login(this.loginUsernameBox.text, this.loginPasswordBox.text);
		bool success = KeyAuthManager.KeyAuthApp.response.success;
		if (success)
		{
			this.logsLbl.text = this.logsLbl.text + "\n <color=green> ! <color=white>Successfully logged in.";
			this.RememberMe();
			this.UserData();
			this.loginUsernameBox.text = null;
			this.loginPasswordBox.text = null;
			this.welcomeLbl.enabled = false;
			this.statusLbl.enabled = false;
			this.sectionPanel.SetActive(true);
			this.loginPanel.SetActive(false);
			this.logsLbl.enabled = false;
			KeyAuthManager.loggedIn = true;
		}
		else
		{
			this.logsLbl.text = this.logsLbl.text + "\n Failed to log in";
			this.loginUsernameBox.text = null;
			this.loginPasswordBox.text = null;
		}
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0000228C
	public void UserData()
	{
		this.userDataBox.text = string.Concat(new string[]
		{
			"Username: ",
			KeyAuthManager.KeyAuthApp.user_data.username,
			"\nExpiry: ",
			this.UnixTimeToDateTime(long.Parse(KeyAuthManager.KeyAuthApp.user_data.subscriptions[0].expiry)).ToString(),
			"\nSubscription: ",
			KeyAuthManager.KeyAuthApp.user_data.subscriptions[0].subscription,
			"\nIP: ",
			KeyAuthManager.KeyAuthApp.user_data.ip,
			"\nHWID: ",
			KeyAuthManager.KeyAuthApp.user_data.hwid,
			"\nCreation Date: ",
			this.UnixTimeToDateTime(long.Parse(KeyAuthManager.KeyAuthApp.user_data.createdate)).ToString(),
			"\nLast Login: ",
			this.UnixTimeToDateTime(long.Parse(KeyAuthManager.KeyAuthApp.user_data.lastlogin)).ToString(),
			"\nTime Left: ",
			KeyAuthManager.KeyAuthApp.expirydaysleft(),
			"\nTotal Users: ",
			KeyAuthManager.KeyAuthApp.app_data.numUsers,
			"\nOnline Users: ",
			KeyAuthManager.KeyAuthApp.app_data.numOnlineUsers,
			"\nLicenses: ",
			KeyAuthManager.KeyAuthApp.app_data.numKeys,
			"\nVersion: ",
			KeyAuthManager.KeyAuthApp.app_data.version,
			"\nCustomer Panel: ",
			KeyAuthManager.KeyAuthApp.app_data.customerPanelLink
		});
		List<api.users> list = KeyAuthManager.KeyAuthApp.fetchOnline();
		foreach (api.users users in list)
		{
			TextMeshProUGUI textMeshProUGUI = this.onlineUsersDisplay;
			textMeshProUGUI.text = textMeshProUGUI.text + "\n " + users.credential;
		}
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000024CC
	public void SendMessage()
	{
		bool flag = KeyAuthManager.KeyAuthApp.chatsend(this.chatroomInputField.text, this.chatroomChannel);
		if (flag)
		{
			TextMeshProUGUI textMeshProUGUI = this.chatroomMessageDisplay;
			textMeshProUGUI.text = string.Concat(new string[]
			{
				textMeshProUGUI.text,
				KeyAuthManager.KeyAuthApp.user_data.username,
				"     > ",
				this.chatroomInputField.text,
				"          ",
				this.UnixTimeToDateTime(DateTimeOffset.Now.ToUnixTimeSeconds()).ToString()
			});
			this.chatroomInputField.text = null;
		}
		else
		{
			this.logsLbl.text = this.logsLbl.text + "\n" + KeyAuthManager.KeyAuthApp.response.message;
			Debug.LogError(KeyAuthManager.KeyAuthApp.response.message);
		}
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000025C4
	private void RetrieveMessages()
	{
		this.timeToGatherMessages -= 1f;
		bool flag = this.timeToGatherMessages <= 0f;
		if (flag)
		{
			this.chatroomMessageDisplay.text = null;
			bool flag2 = !string.IsNullOrEmpty(this.chatroomChannel);
			if (flag2)
			{
				List<api.msg> list = KeyAuthManager.KeyAuthApp.chatget(this.chatroomChannel);
				bool flag3 = list == null || list[0].message == "not_found";
				if (flag3)
				{
					Debug.Log("No messages found");
				}
				else
				{
					foreach (api.msg msg in list)
					{
						TextMeshProUGUI textMeshProUGUI = this.chatroomMessageDisplay;
						textMeshProUGUI.text = string.Concat(new string[]
						{
							textMeshProUGUI.text,
							"\n",
							msg.author,
							"     > ",
							msg.message,
							"          ",
							this.UnixTimeToDateTime(long.Parse(msg.timestamp)).ToString()
						});
						this.timeToGatherMessages = 10f;
					}
				}
			}
			else
			{
				Debug.Log("No messages");
			}
		}
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002738
	public DateTime UnixTimeToDateTime(long unixtime)
	{
		DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
		result = result.AddSeconds((double)unixtime).ToLocalTime();
		return result;
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002770
	public void Register()
	{
		bool flag = this.registerPasswordBox.text != this.registerConfirmPasswordBox.text;
		if (flag)
		{
			this.logsLbl.text = this.logsLbl.text + "\n <color=red> ! <color=white> Passwords do not match!";
		}
		else
		{
			KeyAuthManager.KeyAuthApp.register(this.registerUsernameBox.text, this.registerPasswordBox.text, this.registerLicenseBox.text);
			bool success = KeyAuthManager.KeyAuthApp.response.success;
			if (success)
			{
				this.logsLbl.text = this.logsLbl.text + "\n Successfully registered.";
				this.registerUsernameBox.text = null;
				this.registerPasswordBox.text = null;
				this.registerConfirmPasswordBox.text = null;
				this.registerLicenseBox.text = null;
				this.registerPanel.SetActive(false);
				this.loginPanel.SetActive(true);
			}
			else
			{
				this.logsLbl.text = this.logsLbl.text + "\n Failed To register";
				this.registerUsernameBox.text = null;
				this.registerPasswordBox.text = null;
				this.registerConfirmPasswordBox.text = null;
				this.registerLicenseBox.text = null;
			}
		}
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000028D0
	public void RememberMe()
	{
		bool isOn = this.loginRememberMeToggle.isOn;
		if (isOn)
		{
			PlayerPrefs.SetString("login_username", this.loginUsernameBox.text);
			PlayerPrefs.SetString("login_password", this.loginPasswordBox.text);
			PlayerPrefs.Save();
		}
		else
		{
			PlayerPrefs.SetString("login_username", null);
			PlayerPrefs.SetString("login_password", null);
			PlayerPrefs.Save();
		}
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002943
	private void SendWebhook()
	{
		KeyAuthManager.KeyAuthApp.webhook("WebhookID", "param", "", "");
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002968
	private void Download()
	{
		byte[] bytes = KeyAuthManager.KeyAuthApp.download("fileID");
		File.WriteAllBytes("PathOfYourChoosing", bytes);
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002992
	private void ShowVariable()
	{
		Debug.Log(KeyAuthManager.KeyAuthApp.var("VarID"));
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000029AC
	private void CheckBlacklist()
	{
		bool flag = KeyAuthManager.KeyAuthApp.checkblack();
		if (flag)
		{
			Debug.Log("User is blacklisted");
			Application.Quit();
		}
		else
		{
			Debug.Log("User is not blacklisted");
		}
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000029EC
	private void CheckSession()
	{
		KeyAuthManager.KeyAuthApp.check();
		bool success = KeyAuthManager.KeyAuthApp.response.success;
		if (success)
		{
			Debug.Log("Session is valid");
		}
		else
		{
			Debug.Log("Session is not valid");
		}
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002A34
	private void Upgrade()
	{
		KeyAuthManager.KeyAuthApp.upgrade("KeyAuthUsernameThatYouWantToUpgrade", "LicenseWithTheSameLevelAsTheSubYouWantToGiveTheUser");
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002A4C
	private void Log()
	{
		KeyAuthManager.KeyAuthApp.log("LogYouWantToSend");
	}

	// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00002A60
	private void LoginRegisterWithLicenseOnly()
	{
		KeyAuthManager.KeyAuthApp.license("license");
		bool success = KeyAuthManager.KeyAuthApp.response.success;
		if (success)
		{
			Debug.Log("Success!");
		}
		else
		{
			Debug.Log("Failed!");
		}
	}

	// Token: 0x04000001 RID: 1
	[Header("Login Section")]
	public TMP_InputField loginUsernameBox;

	// Token: 0x04000002 RID: 2
	public TMP_InputField loginPasswordBox;

	// Token: 0x04000003 RID: 3
	public Toggle loginRememberMeToggle;

	// Token: 0x04000004 RID: 4
	public TextMeshProUGUI welcomeLbl;

	// Token: 0x04000005 RID: 5
	public static bool loggedIn;

	// Token: 0x04000006 RID: 6
	[Header("Register Section")]
	public TMP_InputField registerUsernameBox;

	// Token: 0x04000007 RID: 7
	public TMP_InputField registerPasswordBox;

	// Token: 0x04000008 RID: 8
	public TMP_InputField registerConfirmPasswordBox;

	// Token: 0x04000009 RID: 9
	public TMP_InputField registerLicenseBox;

	// Token: 0x0400000A RID: 10
	[Header("Logs")]
	public TextMeshProUGUI logsLbl;

	// Token: 0x0400000B RID: 11
	public TextMeshProUGUI statusLbl;

	// Token: 0x0400000C RID: 12
	[Header("Panels")]
	public GameObject loginPanel;

	// Token: 0x0400000D RID: 13
	public GameObject registerPanel;

	// Token: 0x0400000E RID: 14
	public GameObject sectionPanel;

	// Token: 0x0400000F RID: 15
	[Header("User Data")]
	public TextMeshProUGUI userDataBox;

	// Token: 0x04000010 RID: 16
	public TextMeshProUGUI onlineUsersDisplay;

	// Token: 0x04000011 RID: 17
	[Header("Chatroom")]
	public TMP_InputField chatroomInputField;

	// Token: 0x04000012 RID: 18
	public TextMeshProUGUI chatroomMessageDisplay;

	// Token: 0x04000013 RID: 19
	public float timeToGatherMessages = 10f;

	// Token: 0x04000014 RID: 20
	public string chatroomChannel = "testing";

	// Token: 0x04000015 RID: 21
	public KeyCode SendKeyAuthMessage = 13;

	// Token: 0x04000016 RID: 22
	public static api KeyAuthApp = new api("Untitled", "vpOGNhunWP", "ed9f45b8611b7f2a469fd9d3386c28346cabf869af68535f97b6db5b24115483", "1.0");
}
