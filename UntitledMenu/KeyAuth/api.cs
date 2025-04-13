using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace KeyAuth
{
	// Token: 0x02000037 RID: 55
	public class api : MonoBehaviour
	{
		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00017E8D
		private IEnumerator Error_ApplicationNotSetupCorrectly()
		{
			Debug.LogError("Application is not setup correctly. Please make sure you entered the correct application name, secret, ownerID and version and try again.");
			yield return new WaitForSeconds(3f);
			Application.Quit();
			yield break;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00017E9C
		public api(string name, string ownerid, string secret, string version)
		{
			bool flag = string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(ownerid) || string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(version);
			if (flag)
			{
				base.StartCoroutine(this.Error_ApplicationNotSetupCorrectly());
			}
			this.name = name;
			this.ownerid = ownerid;
			this.secret = secret;
			this.version = version;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00017F34
		private IEnumerator Error_ApplicatonNotFound()
		{
			Debug.LogError("Application was not found. Please check your application information.");
			yield return new WaitForSeconds(3f);
			Application.Quit();
			yield break;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00017F44
		public void init()
		{
			this.enckey = encryption.sha256(encryption.iv_key());
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("init"));
			nameValueCollection["ver"] = encryption.encrypt(this.version, this.secret, text);
			nameValueCollection["hash"] = null;
			nameValueCollection["enckey"] = encryption.encrypt(this.enckey, this.secret, text);
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			bool flag = text2 == "KeyAuth_Invalid";
			if (flag)
			{
				base.StartCoroutine(this.Error_ApplicatonNotFound());
			}
			text2 = encryption.decrypt(text2, this.secret, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			if (success)
			{
				this.load_app_data(response_structure.appinfo);
				this.sessionid = response_structure.sessionid;
				this.initialized = true;
			}
			else
			{
				bool flag2 = response_structure.message == "invalidver";
				if (flag2)
				{
					this.app_data.downloadLink = response_structure.download;
				}
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000180CF
		private IEnumerator Error_PleaseInitializeFirst()
		{
			Debug.LogError("Please Initialize First. Put KeyAuthApp.Init(); on the start function of your login scene.");
			yield return new WaitForSeconds(3f);
			Application.Quit();
			yield break;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000180E0
		public void register(string username, string pass, string key)
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("register"));
			nameValueCollection["username"] = encryption.encrypt(username, this.enckey, text);
			nameValueCollection["pass"] = encryption.encrypt(pass, this.enckey, text);
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(deviceUniqueIdentifier, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			if (success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00018260
		public void login(string username, string pass)
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("login"));
			nameValueCollection["username"] = encryption.encrypt(username, this.enckey, text);
			nameValueCollection["pass"] = encryption.encrypt(pass, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(deviceUniqueIdentifier, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			if (success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000183C8
		public void upgrade(string username, string key)
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("upgrade"));
			nameValueCollection["username"] = encryption.encrypt(username, this.enckey, text);
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			response_structure.success = false;
			this.load_response_struct(response_structure);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00018504
		public void license(string key)
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string windowsSID = this.GetWindowsSID();
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("license"));
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(windowsSID, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			if (success)
			{
				this.load_user_data(response_structure.info);
			}
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00018654
		private string GetWindowsSID()
		{
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				uint capacity = (uint)stringBuilder.Capacity;
				bool flag = !api.GetUserNameEx(2, stringBuilder, ref capacity);
				if (flag)
				{
					Debug.LogError("Failed get user name.");
					result = string.Empty;
				}
				else
				{
					string lpAccountName = stringBuilder.ToString();
					IntPtr intPtr = IntPtr.Zero;
					IntPtr intPtr2 = IntPtr.Zero;
					int cb = 0;
					int cb2 = 0;
					int num;
					api.LookupAccountName(null, lpAccountName, IntPtr.Zero, ref cb, IntPtr.Zero, ref cb2, out num);
					intPtr = Marshal.AllocHGlobal(cb);
					intPtr2 = Marshal.AllocHGlobal(cb2);
					bool flag2 = !api.LookupAccountName(null, lpAccountName, intPtr, ref cb, intPtr2, ref cb2, out num);
					if (flag2)
					{
						Debug.LogError("Failed to look account name.");
						Marshal.FreeHGlobal(intPtr);
						Marshal.FreeHGlobal(intPtr2);
						result = string.Empty;
					}
					else
					{
						StringBuilder stringBuilder2 = new StringBuilder(256);
						bool flag3 = !api.ConvertSidToStringSid(intPtr, out stringBuilder2);
						if (flag3)
						{
							Debug.LogError("Failed to convert SID to string.");
							Marshal.FreeHGlobal(intPtr);
							Marshal.FreeHGlobal(intPtr2);
							result = string.Empty;
						}
						else
						{
							Marshal.FreeHGlobal(intPtr);
							Marshal.FreeHGlobal(intPtr2);
							result = stringBuilder2.ToString();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Error fetching HWID: " + ex.Message);
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060001BD RID: 445
		[DllImport("secur32.dll", SetLastError = true)]
		private static extern bool GetUserNameEx(int nameFormat, StringBuilder userName, ref uint userNameSize);

		// Token: 0x060001BE RID: 446
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool LookupAccountName(string lpSystemName, string lpAccountName, IntPtr Sid, ref int cbSid, IntPtr ReferencedDomainName, ref int cchReferencedDomainName, out int peUse);

		// Token: 0x060001BF RID: 447
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool ConvertSidToStringSid(IntPtr Sid, out StringBuilder StringSid);

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000187BC
		public void check()
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("check"));
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure data = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(data);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000188B4
		public void setvar(string var, string data)
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("setvar"));
			nameValueCollection["var"] = encryption.encrypt(var, this.enckey, text);
			nameValueCollection["data"] = encryption.encrypt(data, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure data2 = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(data2);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000189E0
		public string getvar(string var)
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("getvar"));
			nameValueCollection["var"] = encryption.encrypt(var, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			string result;
			if (success)
			{
				result = response_structure.response;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00018B10
		public void ban()
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("ban"));
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure data = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(data);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00018C08
		public string var(string varid)
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("var"));
			nameValueCollection["varid"] = encryption.encrypt(varid, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			string result;
			if (success)
			{
				result = response_structure.message;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00018D40
		public List<api.users> fetchOnline()
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("fetchOnline"));
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			List<api.users> result;
			if (success)
			{
				result = response_structure.users;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00018E54
		public List<api.msg> chatget(string channelname)
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("chatget"));
			nameValueCollection["channel"] = encryption.encrypt(channelname, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			List<api.msg> result;
			if (success)
			{
				result = response_structure.messages;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00018F84
		public bool chatsend(string msg, string channelname)
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("chatsend"));
			nameValueCollection["message"] = encryption.encrypt(msg, this.enckey, text);
			nameValueCollection["channel"] = encryption.encrypt(channelname, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			return response_structure.success;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000190C8
		public bool checkblack()
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string deviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("checkblacklist"));
			nameValueCollection["hwid"] = encryption.encrypt(deviceUniqueIdentifier, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			return response_structure.success;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000191FC
		public string webhook(string webid, string param, string body = "", string conttype = "")
		{
			bool flag = !this.initialized;
			string result;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
				result = null;
			}
			else
			{
				string text = encryption.sha256(encryption.iv_key());
				NameValueCollection nameValueCollection = new NameValueCollection();
				nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("webhook"));
				nameValueCollection["webid"] = encryption.encrypt(webid, this.enckey, text);
				nameValueCollection["params"] = encryption.encrypt(param, this.enckey, text);
				nameValueCollection["body"] = encryption.encrypt(body, this.enckey, text);
				nameValueCollection["conttype"] = encryption.encrypt(conttype, this.enckey, text);
				nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
				nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
				nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
				nameValueCollection["init_iv"] = text;
				NameValueCollection post_data = nameValueCollection;
				string text2 = api.req(post_data);
				text2 = encryption.decrypt(text2, this.enckey, text);
				api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
				this.load_response_struct(response_structure);
				bool success = response_structure.success;
				if (success)
				{
					result = response_structure.response;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001937C
		public byte[] download(string fileid)
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("file"));
			nameValueCollection["fileid"] = encryption.encrypt(fileid, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			string text2 = api.req(post_data);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			this.load_response_struct(response_structure);
			bool success = response_structure.success;
			byte[] result;
			if (success)
			{
				result = encryption.str_to_byte_arr(response_structure.contents);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000194B0
		public void log(string message)
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("log"));
			nameValueCollection["pcuser"] = encryption.encrypt(Environment.UserName, this.enckey, text);
			nameValueCollection["message"] = encryption.encrypt(message, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			NameValueCollection post_data = nameValueCollection;
			api.req(post_data);
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000195B8
		public static string checksum(string filename)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				using (FileStream fileStream = File.OpenRead(filename))
				{
					byte[] value = md.ComputeHash(fileStream);
					result = BitConverter.ToString(value).Replace("-", "").ToLowerInvariant();
				}
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00019638
		private static string req(NameValueCollection post_data)
		{
			string result;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					byte[] bytes = webClient.UploadValues("https://prod.keyauth.com/api/1.0/", post_data);
					result = Encoding.Default.GetString(bytes);
				}
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
				HttpStatusCode statusCode = httpWebResponse.StatusCode;
				HttpStatusCode httpStatusCode = statusCode;
				if (httpStatusCode != (HttpStatusCode)429)
				{
					Debug.LogError("Connection failed. Please try again");
					Application.Quit();
					result = "";
				}
				else
				{
					Debug.LogError("You're connecting too fast. Please slow down your requests and try again");
					Application.Quit();
					result = "";
				}
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x000196EC
		private static string req_unenc(NameValueCollection post_data)
		{
			string result;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					byte[] bytes = webClient.UploadValues("https://prod.keyauth.com/api/1.1/", post_data);
					result = Encoding.Default.GetString(bytes);
				}
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
				HttpStatusCode statusCode = httpWebResponse.StatusCode;
				HttpStatusCode httpStatusCode = statusCode;
				if (httpStatusCode != (HttpStatusCode)429)
				{
					Debug.LogError("Connection failed. Please try again");
					Application.Quit();
					result = "";
				}
				else
				{
					result = new WaitForSeconds(3f).ToString();
				}
			}
			return result;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00019798
		private void load_app_data(api.app_data_structure data)
		{
			this.app_data.numUsers = data.numUsers;
			this.app_data.numOnlineUsers = data.numOnlineUsers;
			this.app_data.numKeys = data.numKeys;
			this.app_data.version = data.version;
			this.app_data.customerPanelLink = data.customerPanelLink;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x00019800
		private void load_user_data(api.user_data_structure data)
		{
			this.user_data.username = data.username;
			this.user_data.ip = data.ip;
			this.user_data.hwid = data.hwid;
			this.user_data.createdate = data.createdate;
			this.user_data.lastlogin = data.lastlogin;
			this.user_data.subscriptions = data.subscriptions;
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001987C
		public string expirydaysleft()
		{
			bool flag = !this.initialized;
			if (flag)
			{
				base.StartCoroutine(this.Error_PleaseInitializeFirst());
			}
			DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
			d = d.AddSeconds((double)long.Parse(this.user_data.subscriptions[0].expiry)).ToLocalTime();
			TimeSpan timeSpan = d - DateTime.Now;
			return Convert.ToString(timeSpan.Days.ToString() + " Days " + timeSpan.Hours.ToString() + " Hours Left");
		}

		// ProcessedBy_MiDeobf_Engine_b3.2.r3 RVA: 0x0001992B
		private void load_response_struct(api.response_structure data)
		{
			this.response.success = data.success;
			this.response.message = data.message;
		}

		// Token: 0x04000129 RID: 297
		public string name;

		// Token: 0x0400012A RID: 298
		public string ownerid;

		// Token: 0x0400012B RID: 299
		public string secret;

		// Token: 0x0400012C RID: 300
		public string version;

		// Token: 0x0400012D RID: 301
		private string sessionid;

		// Token: 0x0400012E RID: 302
		private string enckey;

		// Token: 0x0400012F RID: 303
		private bool initialized;

		// Token: 0x04000130 RID: 304
		public api.app_data_class app_data = new api.app_data_class();

		// Token: 0x04000131 RID: 305
		public api.user_data_class user_data = new api.user_data_class();

		// Token: 0x04000132 RID: 306
		public api.response_class response = new api.response_class();

		// Token: 0x04000133 RID: 307
		private json_wrapper response_decoder = new json_wrapper(new api.response_structure());

		// Token: 0x02000057 RID: 87
		[DataContract]
		private class response_structure
		{
			// Token: 0x1700003A RID: 58
			// (get) Token: 0x06000336 RID: 822 RVA: 0x0001BDC0 File Offset: 0x00019FC0
			// (set) Token: 0x06000337 RID: 823 RVA: 0x0001BDC8 File Offset: 0x00019FC8
			[DataMember]
			public bool success { get; set; }

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x06000338 RID: 824 RVA: 0x0001BDD1 File Offset: 0x00019FD1
			// (set) Token: 0x06000339 RID: 825 RVA: 0x0001BDD9 File Offset: 0x00019FD9
			[DataMember]
			public string sessionid { get; set; }

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x0600033A RID: 826 RVA: 0x0001BDE2 File Offset: 0x00019FE2
			// (set) Token: 0x0600033B RID: 827 RVA: 0x0001BDEA File Offset: 0x00019FEA
			[DataMember]
			public string contents { get; set; }

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x0600033C RID: 828 RVA: 0x0001BDF3 File Offset: 0x00019FF3
			// (set) Token: 0x0600033D RID: 829 RVA: 0x0001BDFB File Offset: 0x00019FFB
			[DataMember]
			public string response { get; set; }

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x0600033E RID: 830 RVA: 0x0001BE04 File Offset: 0x0001A004
			// (set) Token: 0x0600033F RID: 831 RVA: 0x0001BE0C File Offset: 0x0001A00C
			[DataMember]
			public string message { get; set; }

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x06000340 RID: 832 RVA: 0x0001BE15 File Offset: 0x0001A015
			// (set) Token: 0x06000341 RID: 833 RVA: 0x0001BE1D File Offset: 0x0001A01D
			[DataMember]
			public string download { get; set; }

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x06000342 RID: 834 RVA: 0x0001BE26 File Offset: 0x0001A026
			// (set) Token: 0x06000343 RID: 835 RVA: 0x0001BE2E File Offset: 0x0001A02E
			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public api.user_data_structure info { get; set; }

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x06000344 RID: 836 RVA: 0x0001BE37 File Offset: 0x0001A037
			// (set) Token: 0x06000345 RID: 837 RVA: 0x0001BE3F File Offset: 0x0001A03F
			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public api.app_data_structure appinfo { get; set; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x06000346 RID: 838 RVA: 0x0001BE48 File Offset: 0x0001A048
			// (set) Token: 0x06000347 RID: 839 RVA: 0x0001BE50 File Offset: 0x0001A050
			[DataMember]
			public List<api.msg> messages { get; set; }

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x06000348 RID: 840 RVA: 0x0001BE59 File Offset: 0x0001A059
			// (set) Token: 0x06000349 RID: 841 RVA: 0x0001BE61 File Offset: 0x0001A061
			[DataMember]
			public List<api.users> users { get; set; }
		}

		// Token: 0x02000058 RID: 88
		public class msg
		{
			// Token: 0x17000044 RID: 68
			// (get) Token: 0x0600034B RID: 843 RVA: 0x0001BE73 File Offset: 0x0001A073
			// (set) Token: 0x0600034C RID: 844 RVA: 0x0001BE7B File Offset: 0x0001A07B
			public string message { get; set; }

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x0600034D RID: 845 RVA: 0x0001BE84 File Offset: 0x0001A084
			// (set) Token: 0x0600034E RID: 846 RVA: 0x0001BE8C File Offset: 0x0001A08C
			public string author { get; set; }

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x0600034F RID: 847 RVA: 0x0001BE95 File Offset: 0x0001A095
			// (set) Token: 0x06000350 RID: 848 RVA: 0x0001BE9D File Offset: 0x0001A09D
			public string timestamp { get; set; }
		}

		// Token: 0x02000059 RID: 89
		public class users
		{
			// Token: 0x17000047 RID: 71
			// (get) Token: 0x06000352 RID: 850 RVA: 0x0001BEAF File Offset: 0x0001A0AF
			// (set) Token: 0x06000353 RID: 851 RVA: 0x0001BEB7 File Offset: 0x0001A0B7
			public string credential { get; set; }
		}

		// Token: 0x0200005A RID: 90
		[DataContract]
		private class user_data_structure
		{
			// Token: 0x17000048 RID: 72
			// (get) Token: 0x06000355 RID: 853 RVA: 0x0001BEC9 File Offset: 0x0001A0C9
			// (set) Token: 0x06000356 RID: 854 RVA: 0x0001BED1 File Offset: 0x0001A0D1
			[DataMember]
			public string username { get; set; }

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x06000357 RID: 855 RVA: 0x0001BEDA File Offset: 0x0001A0DA
			// (set) Token: 0x06000358 RID: 856 RVA: 0x0001BEE2 File Offset: 0x0001A0E2
			[DataMember]
			public string ip { get; set; }

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x06000359 RID: 857 RVA: 0x0001BEEB File Offset: 0x0001A0EB
			// (set) Token: 0x0600035A RID: 858 RVA: 0x0001BEF3 File Offset: 0x0001A0F3
			[DataMember]
			public string hwid { get; set; }

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x0600035B RID: 859 RVA: 0x0001BEFC File Offset: 0x0001A0FC
			// (set) Token: 0x0600035C RID: 860 RVA: 0x0001BF04 File Offset: 0x0001A104
			[DataMember]
			public string createdate { get; set; }

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x0600035D RID: 861 RVA: 0x0001BF0D File Offset: 0x0001A10D
			// (set) Token: 0x0600035E RID: 862 RVA: 0x0001BF15 File Offset: 0x0001A115
			[DataMember]
			public string lastlogin { get; set; }

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x0600035F RID: 863 RVA: 0x0001BF1E File Offset: 0x0001A11E
			// (set) Token: 0x06000360 RID: 864 RVA: 0x0001BF26 File Offset: 0x0001A126
			[DataMember]
			public List<api.Data> subscriptions { get; set; }
		}

		// Token: 0x0200005B RID: 91
		[DataContract]
		private class app_data_structure
		{
			// Token: 0x1700004E RID: 78
			// (get) Token: 0x06000362 RID: 866 RVA: 0x0001BF38 File Offset: 0x0001A138
			// (set) Token: 0x06000363 RID: 867 RVA: 0x0001BF40 File Offset: 0x0001A140
			[DataMember]
			public string numUsers { get; set; }

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x06000364 RID: 868 RVA: 0x0001BF49 File Offset: 0x0001A149
			// (set) Token: 0x06000365 RID: 869 RVA: 0x0001BF51 File Offset: 0x0001A151
			[DataMember]
			public string numOnlineUsers { get; set; }

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x06000366 RID: 870 RVA: 0x0001BF5A File Offset: 0x0001A15A
			// (set) Token: 0x06000367 RID: 871 RVA: 0x0001BF62 File Offset: 0x0001A162
			[DataMember]
			public string numKeys { get; set; }

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000368 RID: 872 RVA: 0x0001BF6B File Offset: 0x0001A16B
			// (set) Token: 0x06000369 RID: 873 RVA: 0x0001BF73 File Offset: 0x0001A173
			[DataMember]
			public string version { get; set; }

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x0600036A RID: 874 RVA: 0x0001BF7C File Offset: 0x0001A17C
			// (set) Token: 0x0600036B RID: 875 RVA: 0x0001BF84 File Offset: 0x0001A184
			[DataMember]
			public string customerPanelLink { get; set; }

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x0600036C RID: 876 RVA: 0x0001BF8D File Offset: 0x0001A18D
			// (set) Token: 0x0600036D RID: 877 RVA: 0x0001BF95 File Offset: 0x0001A195
			[DataMember]
			public string downloadLink { get; set; }
		}

		// Token: 0x0200005C RID: 92
		private enum NameFormat
		{
			// Token: 0x040001E5 RID: 485
			NameSamCompatible = 2
		}

		// Token: 0x0200005D RID: 93
		public class app_data_class
		{
			// Token: 0x17000054 RID: 84
			// (get) Token: 0x0600036F RID: 879 RVA: 0x0001BFA7 File Offset: 0x0001A1A7
			// (set) Token: 0x06000370 RID: 880 RVA: 0x0001BFAF File Offset: 0x0001A1AF
			public string numUsers { get; set; }

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000371 RID: 881 RVA: 0x0001BFB8 File Offset: 0x0001A1B8
			// (set) Token: 0x06000372 RID: 882 RVA: 0x0001BFC0 File Offset: 0x0001A1C0
			public string numOnlineUsers { get; set; }

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000373 RID: 883 RVA: 0x0001BFC9 File Offset: 0x0001A1C9
			// (set) Token: 0x06000374 RID: 884 RVA: 0x0001BFD1 File Offset: 0x0001A1D1
			public string numKeys { get; set; }

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000375 RID: 885 RVA: 0x0001BFDA File Offset: 0x0001A1DA
			// (set) Token: 0x06000376 RID: 886 RVA: 0x0001BFE2 File Offset: 0x0001A1E2
			public string version { get; set; }

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000377 RID: 887 RVA: 0x0001BFEB File Offset: 0x0001A1EB
			// (set) Token: 0x06000378 RID: 888 RVA: 0x0001BFF3 File Offset: 0x0001A1F3
			public string customerPanelLink { get; set; }

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000379 RID: 889 RVA: 0x0001BFFC File Offset: 0x0001A1FC
			// (set) Token: 0x0600037A RID: 890 RVA: 0x0001C004 File Offset: 0x0001A204
			public string downloadLink { get; set; }
		}

		// Token: 0x0200005E RID: 94
		public class user_data_class
		{
			// Token: 0x1700005A RID: 90
			// (get) Token: 0x0600037C RID: 892 RVA: 0x0001C016 File Offset: 0x0001A216
			// (set) Token: 0x0600037D RID: 893 RVA: 0x0001C01E File Offset: 0x0001A21E
			public string username { get; set; }

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x0600037E RID: 894 RVA: 0x0001C027 File Offset: 0x0001A227
			// (set) Token: 0x0600037F RID: 895 RVA: 0x0001C02F File Offset: 0x0001A22F
			public string ip { get; set; }

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000380 RID: 896 RVA: 0x0001C038 File Offset: 0x0001A238
			// (set) Token: 0x06000381 RID: 897 RVA: 0x0001C040 File Offset: 0x0001A240
			public string hwid { get; set; }

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000382 RID: 898 RVA: 0x0001C049 File Offset: 0x0001A249
			// (set) Token: 0x06000383 RID: 899 RVA: 0x0001C051 File Offset: 0x0001A251
			public string createdate { get; set; }

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x06000384 RID: 900 RVA: 0x0001C05A File Offset: 0x0001A25A
			// (set) Token: 0x06000385 RID: 901 RVA: 0x0001C062 File Offset: 0x0001A262
			public string lastlogin { get; set; }

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x06000386 RID: 902 RVA: 0x0001C06B File Offset: 0x0001A26B
			// (set) Token: 0x06000387 RID: 903 RVA: 0x0001C073 File Offset: 0x0001A273
			public List<api.Data> subscriptions { get; set; }
		}

		// Token: 0x0200005F RID: 95
		public class Data
		{
			// Token: 0x17000060 RID: 96
			// (get) Token: 0x06000389 RID: 905 RVA: 0x0001C085 File Offset: 0x0001A285
			// (set) Token: 0x0600038A RID: 906 RVA: 0x0001C08D File Offset: 0x0001A28D
			public string subscription { get; set; }

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x0600038B RID: 907 RVA: 0x0001C096 File Offset: 0x0001A296
			// (set) Token: 0x0600038C RID: 908 RVA: 0x0001C09E File Offset: 0x0001A29E
			public string expiry { get; set; }

			// Token: 0x17000062 RID: 98
			// (get) Token: 0x0600038D RID: 909 RVA: 0x0001C0A7 File Offset: 0x0001A2A7
			// (set) Token: 0x0600038E RID: 910 RVA: 0x0001C0AF File Offset: 0x0001A2AF
			public string timeleft { get; set; }
		}

		// Token: 0x02000060 RID: 96
		public class response_class
		{
			// Token: 0x17000063 RID: 99
			// (get) Token: 0x06000390 RID: 912 RVA: 0x0001C0C1 File Offset: 0x0001A2C1
			// (set) Token: 0x06000391 RID: 913 RVA: 0x0001C0C9 File Offset: 0x0001A2C9
			public bool success { get; set; }

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x06000392 RID: 914 RVA: 0x0001C0D2 File Offset: 0x0001A2D2
			// (set) Token: 0x06000393 RID: 915 RVA: 0x0001C0DA File Offset: 0x0001A2DA
			public string message { get; set; }
		}
	}
}
