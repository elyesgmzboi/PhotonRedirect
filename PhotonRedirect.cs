// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.

// F:\Games\made and sourced\tabung original\BepInEx\plugins\PhotonRedirect.dll
// PhotonRedirect, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Global type: <Module>
// Architecture: AnyCPU (64-bit preferred)
// Runtime: v4.0.30319
// Hash algorithm: SHA1

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security;
using System.Security.Permissions;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Microsoft.CodeAnalysis;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace Microsoft.CodeAnalysis
{
	[CompilerGenerated]
	[Embedded]
	internal sealed class EmbeddedAttribute : Attribute
	{
	}
}
namespace PhotonRedirect
{
	[BepInPlugin("com.custom.photonredirect", "PhotonRedirect", "1.0.0")]
	public class PhotonRedirectPlugin : BaseUnityPlugin
	{
		[DataContract]
		private class JsonConfig
		{
			[DataMember]
			public bool overridePhotonSettings = true;

			[DataMember]
			public string appIdRealtime = string.Empty;

			[DataMember]
			public string appIdPun = string.Empty;

			[DataMember]
			public string appIdVoice = string.Empty;

			[DataMember]
			public string appIdChat = string.Empty;

			[DataMember]
			public string fixedRegion = string.Empty;

			[DataMember]
			public bool useNameServer = true;

			[DataMember]
			public string server = string.Empty;

			[DataMember]
			public int port = 5055;
		}

		public const string PluginGUID = "com.custom.photonredirect";
		public const string PluginName = "PhotonRedirect";
		public const string PluginVersion = "1.0.0";

		internal static ManualLogSource Log = null!;

		internal static PhotonRedirectPlugin Instance = null!;

		internal static ConfigEntry<string> CfgAppIdRealtime = null!;

		internal static ConfigEntry<string> CfgAppIdVoice = null!;

		internal static ConfigEntry<string> CfgAppIdChat = null!;

		internal static ConfigEntry<string> CfgFixedRegion = null!;

		internal static ConfigEntry<bool> CfgUseNameServer = null!;

		internal static ConfigEntry<string> CfgServer = null!;

		internal static ConfigEntry<int> CfgPort = null!;

		private Harmony _harmony = null!;

		private void Awake()
		{
			//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ac: Expected O, but got Unknown
			Instance = this;
			Log = Logger;
			CfgAppIdRealtime = Config.Bind<string>("Photon", "AppIdRealtime", "", "Your Photon Realtime App ID. If empty the original game value is kept.");
			CfgAppIdVoice = Config.Bind<string>("Photon", "AppIdVoice", "", "Your Photon Voice App ID. If empty the original game value is kept.");
			CfgAppIdChat = Config.Bind<string>("Photon", "AppIdChat", "", "Your Photon Chat App ID. If empty the original game value is kept.");
			CfgFixedRegion = Config.Bind<string>("Photon", "FixedRegion", "", "Force a specific Photon region (e.g. us, eu, asia). Leave empty for best-region auto-select.");
			CfgUseNameServer = Config.Bind<bool>("Photon", "UseNameServer", true, "Set false only if you are connecting to a self-hosted Photon server.");
			CfgServer = Config.Bind<string>("Photon", "Server", "", "Self-hosted server address (only used when UseNameServer = false).");
			CfgPort = Config.Bind<int>("Photon", "Port", 5055, "Self-hosted server port (only used when UseNameServer = false).");
			TryLoadJsonConfig();
			Log.LogInfo((object)("PhotonRedirect loaded.  AppIdRealtime = " + Mask(CfgAppIdRealtime.Value)));
			Log.LogInfo((object)("  AppIdVoice = " + Mask(CfgAppIdVoice.Value) + ", AppIdChat = " + Mask(CfgAppIdChat.Value)));
			Log.LogInfo((object)$"  FixedRegion = {CfgFixedRegion.Value}, UseNameServer = {CfgUseNameServer.Value}");
			ApplyOverridesNow();
			_harmony = new Harmony("com.custom.photonredirect");
			_harmony.PatchAll(typeof(Patches));
			Log.LogInfo((object)"Harmony patches applied.");
		}

		private void ApplyOverridesNow()
		{
			try
			{
				ServerSettings photonServerSettings = PhotonNetwork.PhotonServerSettings;
				if (photonServerSettings == null)
				{
					Log.LogError((object)"PhotonServerSettings is NULL. Cannot apply overrides early.");
					return;
				}
				AppSettings appSettings = photonServerSettings.AppSettings;
				if (appSettings == null)
				{
					Log.LogError((object)"PhotonServerSettings.AppSettings is NULL.");
					return;
				}
				Log.LogInfo((object)$"[EARLY OVERRIDE] BEFORE: AppIdRealtime={appSettings.AppIdRealtime}, AppIdVoice={appSettings.AppIdVoice}, AppIdChat={appSettings.AppIdChat}, FixedRegion={appSettings.FixedRegion}, UseNameServer={appSettings.UseNameServer}, Server={appSettings.Server}, Port={appSettings.Port}");
				if (!string.IsNullOrWhiteSpace(CfgAppIdRealtime.Value))
				{
					appSettings.AppIdRealtime = CfgAppIdRealtime.Value;
				}
				if (!string.IsNullOrWhiteSpace(CfgAppIdVoice.Value))
				{
					appSettings.AppIdVoice = CfgAppIdVoice.Value;
				}
				if (!string.IsNullOrWhiteSpace(CfgAppIdChat.Value))
				{
					appSettings.AppIdChat = CfgAppIdChat.Value;
				}
				if (!string.IsNullOrWhiteSpace(CfgFixedRegion.Value))
				{
					appSettings.FixedRegion = CfgFixedRegion.Value;
				}
				appSettings.UseNameServer = CfgUseNameServer.Value;
				if (!appSettings.UseNameServer)
				{
					if (!string.IsNullOrWhiteSpace(CfgServer.Value))
					{
						appSettings.Server = CfgServer.Value;
					}
					if (CfgPort.Value > 0 && CfgPort.Value <= 65535)
					{
						appSettings.Port = CfgPort.Value;
					}
				}
				Log.LogInfo((object)$"[EARLY OVERRIDE] AFTER:  AppIdRealtime={appSettings.AppIdRealtime}, AppIdVoice={appSettings.AppIdVoice}, AppIdChat={appSettings.AppIdChat}, FixedRegion={appSettings.FixedRegion}, UseNameServer={appSettings.UseNameServer}, Server={appSettings.Server}, Port={appSettings.Port}");
			}
			catch (Exception arg)
			{
				Log.LogError((object)$"ApplyOverridesNow failed: {arg}");
			}
		}

		private void OnDestroy()
		{
			Harmony harmony = _harmony;
			if (harmony != null)
			{
				harmony.UnpatchSelf();
			}
		}

		private static string Mask(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				return "(empty)";
			}
			if (id.Length <= 8)
			{
				return "****";
			}
			return id.Substring(0, 4) + "..." + id.Substring(id.Length - 4);
		}

		private void TryLoadJsonConfig()
		{
			try
			{
				string text = Path.Combine(Path.GetDirectoryName(((BaseUnityPlugin)this).Info.Location), "photon-config.json");
				string text2 = Path.Combine(Application.streamingAssetsPath, "photon-config.json");
				string text3 = Path.Combine(Application.persistentDataPath, "photon-config.json");
				string? text4 = (File.Exists(text) ? text : (File.Exists(text2) ? text2 : (File.Exists(text3) ? text3 : null)));
				if (text4 == null)
				{
					return;
				}
				Log.LogInfo((object)("Loading photon-config.json from: " + text4));
				JsonConfig? jsonConfig;
				using (var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(text4))))
				{
					var serializer = new DataContractJsonSerializer(typeof(JsonConfig));
					jsonConfig = (JsonConfig?)serializer.ReadObject(ms);
				}
				if (jsonConfig != null)
				{
					if (!string.IsNullOrWhiteSpace(jsonConfig.appIdRealtime))
					{
						CfgAppIdRealtime.Value = jsonConfig.appIdRealtime.Trim();
					}
					else if (!string.IsNullOrWhiteSpace(jsonConfig.appIdPun))
					{
						CfgAppIdRealtime.Value = jsonConfig.appIdPun.Trim();
					}
					if (!string.IsNullOrWhiteSpace(jsonConfig.appIdVoice))
					{
						CfgAppIdVoice.Value = jsonConfig.appIdVoice.Trim();
					}
					if (!string.IsNullOrWhiteSpace(jsonConfig.appIdChat))
					{
						CfgAppIdChat.Value = jsonConfig.appIdChat.Trim();
					}
					if (!string.IsNullOrWhiteSpace(jsonConfig.fixedRegion))
					{
						CfgFixedRegion.Value = jsonConfig.fixedRegion.Trim();
					}
					if (!string.IsNullOrWhiteSpace(jsonConfig.server))
					{
						CfgServer.Value = jsonConfig.server.Trim();
					}
					CfgUseNameServer.Value = jsonConfig.useNameServer;
					if (jsonConfig.port > 0 && jsonConfig.port <= 65535)
					{
						CfgPort.Value = jsonConfig.port;
					}
				}
			}
			catch (Exception ex)
			{
				Log.LogWarning((object)("Failed to read photon-config.json: " + ex.Message));
			}
		}
	}
	internal static class Patches
	{
		[HarmonyPatch(typeof(PhotonNetwork), "ConnectUsingSettings", new Type[] { })]
		[HarmonyPrefix]
		private static void BeforeConnectUsingSettings()
		{
			ManualLogSource log = PhotonRedirectPlugin.Log;
			try
			{
				if (PhotonNetwork.PhotonServerSettings == null)
				{
					log.LogError((object)"PhotonServerSettings is null — cannot override.");
					return;
				}
				AppSettings appSettings = PhotonNetwork.PhotonServerSettings.AppSettings;
				log.LogInfo((object)("[BEFORE] AppIdRealtime=" + appSettings.AppIdRealtime + ", Region=" + appSettings.FixedRegion + ", Server=" + appSettings.Server));
				if (!string.IsNullOrWhiteSpace(PhotonRedirectPlugin.CfgAppIdRealtime.Value))
				{
					appSettings.AppIdRealtime = PhotonRedirectPlugin.CfgAppIdRealtime.Value;
				}
				if (!string.IsNullOrWhiteSpace(PhotonRedirectPlugin.CfgAppIdVoice.Value))
				{
					appSettings.AppIdVoice = PhotonRedirectPlugin.CfgAppIdVoice.Value;
				}
				if (!string.IsNullOrWhiteSpace(PhotonRedirectPlugin.CfgAppIdChat.Value))
				{
					appSettings.AppIdChat = PhotonRedirectPlugin.CfgAppIdChat.Value;
				}
				if (!string.IsNullOrWhiteSpace(PhotonRedirectPlugin.CfgFixedRegion.Value))
				{
					appSettings.FixedRegion = PhotonRedirectPlugin.CfgFixedRegion.Value;
				}
				appSettings.UseNameServer = PhotonRedirectPlugin.CfgUseNameServer.Value;
				if (!appSettings.UseNameServer)
				{
					if (!string.IsNullOrWhiteSpace(PhotonRedirectPlugin.CfgServer.Value))
					{
						appSettings.Server = PhotonRedirectPlugin.CfgServer.Value;
					}
					if (PhotonRedirectPlugin.CfgPort.Value > 0 && PhotonRedirectPlugin.CfgPort.Value <= 65535)
					{
						appSettings.Port = PhotonRedirectPlugin.CfgPort.Value;
					}
				}
				log.LogInfo((object)("[AFTER]  AppIdRealtime=" + appSettings.AppIdRealtime + ", AppIdVoice=" + appSettings.AppIdVoice + ", AppIdChat=" + appSettings.AppIdChat));
				log.LogInfo((object)$"         FixedRegion={appSettings.FixedRegion}, UseNameServer={appSettings.UseNameServer}, Server={appSettings.Server}, Port={appSettings.Port}");
			}
			catch (Exception arg)
			{
				log.LogError((object)$"Exception in BeforeConnectUsingSettings: {arg}");
			}
		}

		[HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnConnectedToMaster")]
		[HarmonyPostfix]
		private static void AfterOnConnectedToMaster(MonoBehaviourPunCallbacks __instance)
		{
			PhotonRedirectPlugin.Log.LogInfo((object)("✓ Connected to master! Region=" + PhotonNetwork.CloudRegion + ", Server=" + PhotonNetwork.ServerAddress));
		}

		[HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnDisconnected")]
		[HarmonyPostfix]
		private static void AfterOnDisconnected(MonoBehaviourPunCallbacks __instance, DisconnectCause cause)
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			PhotonRedirectPlugin.Log.LogWarning((object)$"✗ Disconnected. Cause: {cause}");
		}
	}
}
