using System;
using System.Collections.Generic;
using untitled.Cheat;

namespace untitled.Core
{
	// Token: 0x0200000B RID: 11
	public class Buttons
	{
		// Token: 0x04000025 RID: 37
		public static List<List<Button>> List = new List<List<Button>>
		{
			new List<Button>
			{
				new Button("Settings", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Settings);
				}, null, null, false),
				new Button("Players", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Players);
				}, null, null, false),
				new Button("VRRig", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Vrrig);
				}, null, null, false),
				new Button("Movement", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Movement);
				}, null, null, false),
				new Button("Visuals", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Visuals);
				}, null, null, false),
				new Button("Overpowered", false, delegate()
				{
					Menu.ChangePage(Menu.Category.OP);
				}, null, null, false),
				new Button("Masterclient", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Master);
				}, null, null, false),
				new Button("Global", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Global);
				}, null, null, false),
				new Button("Gamemode", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Mode);
				}, null, null, false),
				new Button("Plugin", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Plugin);
				}, null, null, false)
			},
			new List<Button>
			{
				new Button("Back", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Base);
				}, null, null, false),
				new Button("Menu Settings", false, delegate()
				{
					Menu.ChangePage(Menu.Category.MenuSettings);
				}, null, null, false),
				new Button("Visual Settings", false, delegate()
				{
					Menu.ChangePage(Menu.Category.VisualSettings);
				}, null, null, false),
				new Button("Movement Settings", false, delegate()
				{
					Menu.ChangePage(Menu.Category.MovementSettings);
				}, null, null, false),
				new Button("VRRig Settings", false, delegate()
				{
					Menu.ChangePage(Menu.Category.VRRigSettings);
				}, null, null, false)
			},
			new List<Button>
			{
				new Button("Back", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Base);
				}, null, null, false),
				new Button("Invis Monkey [rt]", true, delegate()
				{
					Client.InvisMonkey();
				}, null, "Makes your player invisible.", false),
				new Button("Ghost Monkey [a]", true, delegate()
				{
					Client.GhostMonkey();
				}, null, "Makes your player freeze.", false),
				new Button("Rainbow Monkey [stump]", true, delegate()
				{
					Client.RGBMonkey();
				}, null, "Makes your player rainbow", false),
				new Button("Hold Rig", true, delegate()
				{
					Client.HoldRig();
				}, null, "Holds your player.", false),
				new Button("Sex Gun", true, delegate()
				{
					Client.SexGun();
				}, null, "Intercourse, forced penetration on the target.", false),
				new Button("Spaz Rig [rg]", true, delegate()
				{
					Client.SpazHead();
				}, null, "Makes your player spaz.", false),
				new Button("Spin Head X [rg]", true, delegate()
				{
					Client.SpinHeadX();
				}, null, "Makes your head spin on the x axis.", false),
				new Button("Spin Head Y [rg]", true, delegate()
				{
					Client.SpinHeadY();
				}, null, "Makes your head spin diagonally.", false),
				new Button("Spin Head z [rg]", true, delegate()
				{
					Client.SpinHeadZ();
				}, null, "Makes your head spin horizontally.", false),
				new Button("Scare Gun", true, delegate()
				{
					Client.ScareGun();
				}, null, "Makes your player chase the target.", false),
				new Button("Helicopter Rig [rg]", true, delegate()
				{
					Client.HelicopterRig();
				}, null, "Makes your player a helicopter.", false),
				new Button("Spectate Gun", true, delegate()
				{
					Client.SpectateGun();
				}, null, "Select a player and spectate.", false)
			},
			new List<Button>
			{
				new Button("Back", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Base);
				}, null, null, false),
				new Button("Platforms", true, delegate()
				{
					Movement.Platforms();
				}, null, "Allows you to walk on air.", false),
				new Button("No Clip [rt]", true, delegate()
				{
					Movement.NoClip();
				}, null, "You dont collide with objects.", false),
				new Button("Steam Long Arms", true, delegate()
				{
					Movement.SteamLongArms();
				}, delegate()
				{
					Movement.NormalArms();
				}, "Get longer arms!.", false),
				new Button("Really Long Arms", true, delegate()
				{
					Movement.ReallyLongArms();
				}, delegate()
				{
					Movement.NormalArms();
				}, "Get really longer arms!.", false),
				new Button("Speed Boost", true, delegate()
				{
					Movement.SpeedBoost();
				}, null, "Run faster than anyone.", false),
				new Button("Rewind [rt]", true, delegate()
				{
					Movement.Rewind();
				}, null, "Rewind time with ur right grip.", false),
				new Button("Macro Recorder [rt,rg]", true, delegate()
				{
					Movement.MacroRecorder();
				}, null, "Record and replay.", false),
				new Button("Spiderwalk", true, delegate()
				{
					Movement.SpiderWalk();
				}, null, "Walk on walls like a spider.", false),
				new Button("Pull Mod", true, delegate()
				{
					Movement.PullMod();
				}, null, "Less detectable speed boost.", false),
				new Button("Checkpoint [rg]", true, delegate()
				{
					Movement.Checkpoint();
				}, null, "Place a checkpoint than teleport back.", false),
				new Button("WASD Fly", true, delegate()
				{
					Movement.WASDd();
				}, null, "Allows you to fly with WASD and ur right mouse button.", false),
				new Button("Hand Fly [a]", true, delegate()
				{
					Movement.HandFly();
				}, null, "Allows you to fly with your hand.", false),
				new Button("Body Fly [a]", true, delegate()
				{
					Movement.BodyFly();
				}, null, "Allows you to fly with your body.", false),
				new Button("Slingshot Fly [a]", true, delegate()
				{
					Movement.SlingshotFly();
				}, null, "Allows you to fly keeping velocit", false),
				new Button("Joystick Fly", true, delegate()
				{
					Movement.JoystickFly();
				}, null, "Allows you to fly with your joystick", false),
				new Button("Freecam", true, delegate()
				{
					Movement.Freecam();
				}, null, "Allows you to fly with your joystick", false),
				new Button("No Clip Fly [a]", true, delegate()
				{
					Movement.NoClipFly();
				}, null, "Allows you to fly with no collision", false),
				new Button("Iron Monkey [x]", true, delegate()
				{
					Movement.IronMonkey();
				}, null, "Allows you to fly like iron man.", false),
				new Button("Wall Walk [rg]", true, delegate()
				{
					Movement.WallWalk();
				}, null, "Drags you towards walls in proximity.", false)
			},
			new List<Button>
			{
				new Button("Back", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Base);
				}, null, null, false),
				new Button("2D Wireframe ESP", true, delegate()
				{
					Visual.BoxESP2D();
				}, null, "Sees players through walls.", false),
				new Button("3D Wireframe ESP", true, delegate()
				{
					Visual.BoxEspOn3D();
				}, delegate()
				{
					Visual.BoxEspOff3D();
				}, "Sees players through walls.", false),
				new Button("Full Box ESP", true, delegate()
				{
					Visual.FullBoxESP();
				}, null, "Sees players through walls.", false),
				new Button("Breadcrumbs", true, delegate()
				{
					Visual.BreadCrumbs();
				}, delegate()
				{
					Visual.DisableBreadCrumbs();
				}, "Creates a trail for players.", false),
				new Button("Bone ESP", true, delegate()
				{
					Visual.BoneESP();
				}, delegate()
				{
					Visual.BoneESPOff();
				}, "Sees players skeleton through walls.", false),
				new Button("Tracer ESP", true, delegate()
				{
					Visual.TracerESP();
				}, delegate()
				{
					Visual.TracerESPOff();
				}, "Creates a tracer to each player", false),
				new Button("Chams ESP", true, delegate()
				{
					Visual.ChamESPOn();
				}, delegate()
				{
					Visual.ChamESPOff();
				}, "Sees players whole body through walls.", false),
				new Button("Info Tags", true, delegate()
				{
					Visual.InfoTag();
				}, delegate()
				{
					Visual.DisableInfoTags();
				}, "Displays info on Player.", false)
			},
			new List<Button>
			{
				new Button("Back", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Base);
				}, null, null, false),
				new Button("Freeze Server", false, delegate()
				{
					Overpowered.FreezeServer();
				}, null, "Freezes the server within seconds.", false),
				new Button("Kick Stump Trigger", false, delegate()
				{
					Overpowered.KickStump();
				}, null, "Kicks anyone that hits the stump trigger.", false),
				new Button("Kick Gun", true, delegate()
				{
					Overpowered.KickGun();
				}, null, "Kicks the target within a few seconds.", false),
				new Button("test gun", true, delegate()
				{
					Overpowered.test();
				}, null, "Freezes the server within seconds.", false),
				new Button("Anti Leave Gun", true, delegate()
				{
					Overpowered.AntiLeaveGun();
				}, null, "Doesnt allow the target to leave.", false),
				new Button("Anti Leave All", false, delegate()
				{
					Overpowered.AntiLeaveAll();
				}, null, "Doesnt allow the targets to leave.", false),
				new Button("Ghost Gun", true, delegate()
				{
					Overpowered.FreezeRigGun(true);
				}, null, "Freezes the targets rig on everyones screen.", false),
				new Button("Ghost All", false, delegate()
				{
					Overpowered.FreezeAllRigs();
				}, null, "Freezes everyone.", false),
				new Button("Ghost Touch", true, delegate()
				{
					Overpowered.GhostTouch();
				}, null, "Freezes the rig your touching.", false),
				new Button("Isolate Gun", true, delegate()
				{
					Overpowered.TimeStopGun(true);
				}, null, "Freezes everyone on the targets screen.", false),
				new Button("Isolate Others Gun", true, delegate()
				{
					Overpowered.TimeStopGun(false);
				}, null, "Freezes everyone but you on the targets screen.", false),
				new Button("Crash Gun", true, delegate()
				{
					Overpowered.CrashGun(1.1f, 555);
				}, null, "Crashes the target.", false),
				new Button("Crash All", true, delegate()
				{
					Overpowered.CrashAll(1.1f, 555);
				}, null, "Crashes everyone.", false),
				new Button("Lag Gun", true, delegate()
				{
					Overpowered.LagGun();
				}, null, "Lags everyone lightly", false),
				new Button("Lag All", true, delegate()
				{
					Overpowered.LagAll();
				}, null, "Lags the target lightly.", false)
			},
			new List<Button>
			{
				new Button("Back", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Base);
				}, null, null, false),
				new Button("Untag Gun", true, delegate()
				{
					Master.UntagGun();
				}, null, "Untags the target.", false),
				new Button("Untag All", true, delegate()
				{
					Master.UntagAll();
				}, null, "Untags Everyone.", false),
				new Button("Material Gun", true, delegate()
				{
					Master.MatGun();
				}, null, "Spams toggles the material of the target.", false),
				new Button("Material All", true, delegate()
				{
					Master.MatAll();
				}, null, "Spams toggles the material of everyone.", false),
				new Button("Vibrate Gun", true, delegate()
				{
					Master.ViberateGun();
				}, null, "Vibrates the controllers of the target.", false),
				new Button("Vibrate All", true, delegate()
				{
					Master.ViberateAll();
				}, null, "Vibrates the controllers of everyone.", false),
				new Button("Slow Gun", true, delegate()
				{
					Master.SlowGun();
				}, null, "Slows the target.", false),
				new Button("Slow All", true, delegate()
				{
					Master.SlowAll();
				}, null, "Slows everyone.", false),
				new Button("Red All", true, delegate()
				{
					Master.SetAllPlayersColor(true);
				}, null, "Makes everyone the color red.", false),
				new Button("Blue All", true, delegate()
				{
					Master.SetAllPlayersColor(false);
				}, null, "Makes everyone the color blue.", false),
				new Button("Red Gun", true, delegate()
				{
					Master.RedGun();
				}, null, "Makes the target the color red.", false),
				new Button("Blue Gun", true, delegate()
				{
					Master.BlueGun();
				}, null, "Makes the target the color blue.", false),
				new Button("Lucy Gun", true, delegate()
				{
					Master.LucyGun();
				}, null, "Moves lucy to the point.", false),
				new Button("HitTarget Spam", true, delegate()
				{
					Master.TargetHitSpam();
				}, null, "Spams all the hit targets.", false)
			},
			new List<Button>
			{
				new Button("Back", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Base);
				}, null, null, false),
				new Button("Accept TOS", true, delegate()
				{
					Global.AcceptTos();
				}, null, "Accepts the TOS.", false),
				new Button("Snowball Spam", true, delegate()
				{
					Global.SnowballSpam();
				}, null, "Spams regular snowballs.", false),
				new Button("Snowball Minigun", true, delegate()
				{
					Global.SnowballMinigun();
				}, null, "Shoots regular snowballs.", false),
				new Button("Big Snowball Spam", true, delegate()
				{
					Global.BigSnowballSpam();
				}, null, "Spams big snowballs.", false),
				new Button("Big Snowball Minigun", true, delegate()
				{
					Global.BigSnowballMinigun();
				}, null, "Shoots big snowballs.", false),
				new Button("Snowball Punch Mod", true, delegate()
				{
					Global.SnowballPunchmod();
				}, null, "Allows you to punch people with snowballs.", false),
				new Button("Snowball Fling Gun", true, delegate()
				{
					Global.SnowballFlingGun();
				}, null, "Allows you to fling people with snowballs.", false),
				new Button("Effect Player Gun", true, delegate()
				{
					Global.EffectPlayerGun();
				}, null, "Spams the snowball Fx on the target.", false),
				new Button("Watersplash Hands", true, delegate()
				{
					Global.WatersplashHands();
				}, null, "Spams water splash's on your hands.", false),
				new Button("Watersplash Gun", true, delegate()
				{
					Global.WatersplashGun();
				}, null, "Spams water splash's on the point.", false),
				new Button("Max Quest Score", false, delegate()
				{
					Global.MaxQuestScore();
				}, null, null, false),
				new Button("Spawn Hoverboard", false, delegate()
				{
					Global.SpawnHoverboard();
				}, null, null, false),
				new Button("Whoopee Cusion Spam (TRYON)", true, delegate()
				{
					Global.WhoopeeCusionSpam();
				}, null, "Spams whoopee cusions.", false),
				new Button("Firecracker Spam(TRYON)", true, delegate()
				{
					Global.FirecrackerSpam();
				}, null, "Spams the firecracker holdable.", false),
				new Button("Bomb Spam (TRYON)", true, delegate()
				{
					Global.BombSpam();
				}, null, "Spams the bomb holdable.", false),
				new Button("Critter Spammer", true, delegate()
				{
					Global.CritterSpammer();
				}, null, "Spams critters.", false),
				new Button("Critter Minigun", true, delegate()
				{
					Global.CritterMinigun();
				}, null, "Shoots critters out of your hand.", false),
				new Button("Stun Bomb Minigun", true, delegate()
				{
					Global.StunBombMinigun();
				}, null, "Shoots stun bombs out of your hand", false),
				new Button("Stun Bomb Spammer", true, delegate()
				{
					Global.StunBombSpammer();
				}, null, "Spams stun bombs.", false),
				new Button("Sticky Trap Minigun", true, delegate()
				{
					Global.StickyTrapMinigun();
				}, null, "Shoots sticky traps out of your hand.", false),
				new Button("Sticky Trap Spammer", true, delegate()
				{
					Global.StickyTrapSpammer();
				}, null, "Spams sticky traps.", false),
				new Button("Sticky Trap Piss", true, delegate()
				{
					Global.StickyTrapPiss();
				}, null, "Shoots sticky traps out of your balls.", false),
				new Button("Sticky Goo Gun", true, delegate()
				{
					Global.StickyGooGun();
				}, null, "Spams goo on the point.", false),
				new Button("Fx Spammer Yellow", true, delegate()
				{
					Global.FxSpammerYellow();
				}, null, "Spams yellow fx on your hand.", false),
				new Button("Fx Spammer White", true, delegate()
				{
					Global.FxSpammerWhite();
				}, null, "Spams white fx on your hand.", false),
				new Button("Fx Spammer Orange", true, delegate()
				{
					Global.FxSpammerOrange();
				}, null, "Spams orange fx on your hand.", false),
				new Button("Fx Spammer Gray", true, delegate()
				{
					Global.FxSpammerGray();
				}, null, "Spams gray fx on your hand.", false)
			},
			new List<Button>
			{
				new Button("Back", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Base);
				}, null, null, false),
				new Button("Tag Gun", true, delegate()
				{
					Gamemode.TagGun();
				}, null, "Tags the target.", false),
				new Button("Tag All", true, delegate()
				{
					Gamemode.TagAll();
				}, null, "Tags everyone.", false),
				new Button("Flick Tag Assist", true, delegate()
				{
					Gamemode.FlickTagAssist();
				}, null, "Tags players in proximity. Y & B To cycle distance.", false),
				new Button("Always Guardian", true, delegate()
				{
					Gamemode.AlwaysGuardian();
				}, null, "Makes you stay guardian.", false),
				new Button("Guardian Grab All", true, delegate()
				{
					Gamemode.GuardianGrabAll();
				}, null, "Grabs everyone while guardian.", false),
				new Button("Guardian Fling All", true, delegate()
				{
					Gamemode.GuardianFlingAll();
				}, null, "Flings everyone while guardian.", false),
				new Button("Auto Catch Ball", true, delegate()
				{
					Gamemode.GrabBallAura();
				}, null, "Grabs the ball if in proximity", false)
			},
			new List<Button>
			{
				new Button("Back", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Settings);
				}, null, null, false),
				new Button("Save Preferences", false, delegate()
				{
					Settings.SaveConfig();
				}, null, null, false),
				new Button("Auto Save", true, delegate()
				{
					Settings.SetAutosaveButtonState(true);
				}, delegate()
				{
					Settings.SetAutosaveButtonState(false);
				}, null, false),
				new Button("Toggle Disconnect Btn", true, delegate()
				{
					Settings.SetDisconnectButtonState(true);
				}, delegate()
				{
					Settings.SetDisconnectButtonState(false);
				}, null, true),
				new Button("Toggle Rounding ", true, delegate()
				{
					Settings.SetRounding(true);
				}, delegate()
				{
					Settings.SetRounding(false);
				}, null, false),
				new Button("Toggle Outlining", true, delegate()
				{
					Settings.SetOutline(true);
				}, delegate()
				{
					Settings.SetOutline(false);
				}, null, false),
				new Button("Toggle Mini Menu", true, delegate()
				{
					Settings.SetPad(true);
				}, delegate()
				{
					Settings.SetPad(false);
				}, null, false),
				new Button("Toggle Stretchy Menu", true, delegate()
				{
					Settings.SetStretchy(true);
				}, delegate()
				{
					Settings.SetStretchy(false);
				}, null, false),
				new Button("Toggle Animations", true, delegate()
				{
					Settings.SetAnimations(true);
				}, delegate()
				{
					Settings.SetAnimations(false);
				}, null, false),
				new Button("Menu Theme : " + Settings.ThemeString, false, delegate()
				{
					Settings.CycleThemes(false);
				}, null, null, false),
				new Button("Outline Theme : " + Settings.OutlineString, false, delegate()
				{
					Settings.CycleOutlines(false);
				}, null, null, false),
				new Button("Text Theme : " + Settings.TextString, false, delegate()
				{
					Settings.CycleText(false);
				}, null, null, false),
				new Button("Click Sound : " + Settings.ClickString, false, delegate()
				{
					Settings.CycleClick(false);
				}, null, null, false),
				new Button("Page Buttons : " + Settings.PageString, false, delegate()
				{
					Settings.CyclePageButtons(false);
				}, null, null, false),
				new Button("PC Menu Background : " + Settings.BackgroundString, false, delegate()
				{
					Settings.CycleBackgrounds(false);
				}, null, null, false),
				new Button("Toggle Drop Menu", true, delegate()
				{
					Settings.SetRigidbodyState(true);
				}, delegate()
				{
					Settings.SetRigidbodyState(false);
				}, null, false),
				new Button("Freeze Player in Menu", true, delegate()
				{
					Settings.FreezePlayerInMenu();
				}, delegate()
				{
					Settings.DisableFreezePlayerInMenu();
				}, null, false),
				new Button("Toggle Tooltips", true, delegate()
				{
					Settings.SetTooltips(true);
				}, delegate()
				{
					Settings.SetTooltips(false);
				}, null, false),
				new Button("Toggle FPS Counter", true, delegate()
				{
					Settings.SetFPS(true);
				}, delegate()
				{
					Settings.SetFPS(false);
				}, null, true),
				new Button("FPS Counter Pos : " + Settings.CounterString, false, delegate()
				{
					Settings.CycleCounterPos(false);
				}, null, null, false),
				new Button("Players Tab Cam : " + Settings.CamString, false, delegate()
				{
					Settings.CycleCam(false);
				}, null, null, false),
				new Button("Toggle Home Button", true, delegate()
				{
					Settings.SetHome(true);
				}, delegate()
				{
					Settings.SetHome(false);
				}, null, true),
				new Button("Toggle Array List", true, delegate()
				{
					Settings.SetArraylist(true);
				}, delegate()
				{
					Settings.SetArraylist(false);
				}, null, true),
				new Button("Toggle Gun Line", true, delegate()
				{
					Settings.SetGunLine(true);
				}, delegate()
				{
					Settings.SetGunLine(false);
				}, null, true)
			},
			new List<Button>
			{
				new Button("Back", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Settings);
				}, null, null, false),
				new Button(Settings.Ghostview ? "Disable Ghostview" : "Enable Ghostview", true, delegate()
				{
					Settings.SetGhostview(true);
				}, delegate()
				{
					Settings.SetGhostview(false);
				}, null, false)
			},
			new List<Button>
			{
				new Button("Back", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Settings);
				}, null, null, false),
				new Button("Fly Speed : " + Settings.FlyString, false, delegate()
				{
					Settings.CycleFlySpeed(false);
				}, null, null, false),
				new Button("Pull Strength :", false, delegate()
				{
					Settings.SetGhostview(true);
				}, delegate()
				{
					Settings.SetGhostview(false);
				}, null, false)
			},
			new List<Button>
			{
				new Button("Back", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Settings);
				}, null, null, false)
			},
			new List<Button>
			{
				new Button("Back", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Base);
				}, null, null, false)
			},
			new List<Button>
			{
				new Button("Back", false, delegate()
				{
					Menu.ChangePage(Menu.Category.Base);
				}, null, null, false)
			}
		};
	}
}
