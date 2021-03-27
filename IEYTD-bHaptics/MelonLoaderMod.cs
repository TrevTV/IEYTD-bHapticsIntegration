// #define EXPLORER

using MelonLoader;
using Bhaptics.Tact.Unity;
using Harmony;
using UnityEngine;
using System.IO;
using System;
using System.Reflection;
using bHapticsSDK;
using BHapticsSupport.Sections;

using SchellGames.Spectre.Assets.Scripts.Interactables.Player.States;
using SchellGames.Spectre.Assets.Scripts.Interactables.SharedStates;
using SchellGames.Spectre.Assets.Scripts.InputManagement;
using SchellGames.Spectre.Assets.Scripts.Interactables.CarLevel.Cannon;
using SchellGames.Spectre.Assets.Scripts.Interactables.CarLevel.PlaneHull;
using SchellGames.Spectre.Assets.Scripts.Interactables.SubLevel.FlareGun;
using SchellGames.Spectre.Assets.Scripts.Interactables.Player;
using SchellGames.Spectre.Assets.Scripts.Interactables.WindowLevel.VirusVat;
using SchellGames.Spectre.Assets.Scripts.Interactables.WindowLevel.BreakableWindow;
using SchellGames.Spectre.Assets.Scripts.Interactables.HunterLodgeLevel.CrossbowBear;
using SchellGames.Spectre.Assets.Scripts.Interactables.HunterLodgeLevel.DeerInteraction;
using SchellGames.Spectre.Assets.Scripts.Interactables.HunterLodgeLevel.HelicopterInteraction;
using SchellGames.Spectre.Assets.Scripts.Interactables.TrainLevel.ArmoredCarAssassin;
using SchellGames.Spectre.Assets.Scripts.Interactables.TrainLevel.PlaneAssassin;
using SchellGames.Spectre.Assets.Scripts.Interactables;
using SchellGames.Spectre.Assets.Scripts.Interactables.SpaceLevel;

#if EXPLORER
using UnityExplorer;
#endif

namespace BHapticsSupport
{
    public static class BuildInfo
    {
        public const string Name = "bHaptics Integration - Beta Testing"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "trev"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "0.1.1"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = "https://trevtv.github.io/ieytd/"; // Download Link for the Mod.  (Set as null if none)
    }

    public class HapticsMain : MelonMod
    {
        public override void OnApplicationStart()
        {
            //UpdateNotices.RunUpdateCheck();

            LoadTacts();

            #region Generic Game Events

            MethodInfo switchBladeUse = GenericGameEvents.SwitchBladeState.GetMethod("Use");

            Harmony.Patch(AccessTools.Method(typeof(VRHandInput), "AttachObject"), new HarmonyMethod(typeof(GenericGameEvents).GetMethod("AttachObject")));
            Harmony.Patch(AccessTools.Method(typeof(VRHandInput), "DetachObject"), new HarmonyMethod(typeof(GenericGameEvents).GetMethod("DetachObject")));
            Harmony.Patch(AccessTools.Method(typeof(VRHandInput), "PickObject"), new HarmonyMethod(typeof(GenericGameEvents).GetMethod("PickObject")));
            Harmony.Patch(AccessTools.Method(typeof(VRHandInput), "DropObject"), new HarmonyMethod(typeof(GenericGameEvents).GetMethod("DropObject")));

            Harmony.Patch(AccessTools.Method(typeof(HandGunState), "Use"), new HarmonyMethod(typeof(GenericGameEvents).GetMethod("GunFire")));
            Harmony.Patch(AccessTools.Method(typeof(FlareGunState), "Use"), new HarmonyMethod(typeof(GenericGameEvents).GetMethod("GunFire")));

            Harmony.Patch(AccessTools.Method(typeof(PlayerAliveState), "Stun"), null, new HarmonyMethod(typeof(GenericGameEvents).GetMethod("Stun")));
            Harmony.Patch(AccessTools.Method(typeof(PlayerAliveState), "DeathByExplosion"), null, new HarmonyMethod(typeof(GenericGameEvents).GetMethod("Explode")));
            Harmony.Patch(AccessTools.Method(typeof(PlayerAliveState), "WearEntity"), null, new HarmonyMethod(typeof(GenericGameEvents).GetMethod("WearObject")));
            Harmony.Patch(AccessTools.Method(typeof(PlayerAliveState), "Eat"), null, new HarmonyMethod(typeof(GenericGameEvents).GetMethod("Eat")));
            Harmony.Patch(AccessTools.Method(typeof(PlayerAliveState), "DoDeath"), null, new HarmonyMethod(typeof(GenericGameEvents).GetMethod("DoDeath")));
            Harmony.Patch(AccessTools.Method(typeof(PlayerEntity), "ApplyDamageEffect"), null, new HarmonyMethod(typeof(GenericGameEvents).GetMethod("ApplyEffect")));

            Harmony.Patch(AccessTools.Method(typeof(InteractableEntityState), "DoExplode"), new HarmonyMethod(typeof(GenericGameEvents).GetMethod("AmbientExplosion")));
            Harmony.Patch(switchBladeUse, null, new HarmonyMethod(typeof(GenericGameEvents).GetMethod("ToggleCutterState")));

            #endregion

            #region Friendly Skies

            MethodInfo gearChangeMethod = FriendlySkies.GearShiftingState.GetMethod("OnCompoundTriggerEnterInvoked");
            MethodInfo gasPedalEnable = FriendlySkies.GasPedal.GetMethod("TurnOn");
            MethodInfo gasPedalDisable = FriendlySkies.GasPedal.GetMethod("TurnOff");
            MethodInfo carIgnitionOn = FriendlySkies.CarEngine.GetMethod("PerformOnAction");

            Harmony.Patch(gearChangeMethod, null, new HarmonyMethod(typeof(FriendlySkies), "GearChange"));
            Harmony.Patch(typeof(Cannon).GetMethod("Launch"), new HarmonyMethod(typeof(FriendlySkies).GetMethod("CannonLaunch")));
            Harmony.Patch(typeof(PlaneHullEntity).GetMethod("CargoDoorOpen"), null, new HarmonyMethod(typeof(FriendlySkies).GetMethod("CargoDoorOpen")));
            Harmony.Patch(gasPedalEnable, null, new HarmonyMethod(typeof(FriendlySkies).GetMethod("GasPedalEnabled")));
            Harmony.Patch(gasPedalDisable, null, new HarmonyMethod(typeof(FriendlySkies).GetMethod("GasPedalDisabled")));
            Harmony.Patch(carIgnitionOn, null, new HarmonyMethod(typeof(FriendlySkies).GetMethod("TurnIgnitionOn")));

            #endregion

            #region Squeaky Clean

            Harmony.Patch(AccessTools.Method(typeof(VirusRocketState), "PlayWinFX"), null, new HarmonyMethod(typeof(SqueakyClean).GetMethod("NeutralizedVirusExplosion")));
            Harmony.Patch(AccessTools.Method(typeof(BreakableWindowState), "OnBreakOrShot"), new HarmonyMethod(typeof(SqueakyClean).GetMethod("GlassBreak")));

            #endregion

            #region Deep Dive
            #endregion

            #region Winter Break

            //Harmony.Patch(AccessTools.Method(typeof(MysteryMachineActiveState), "PlayCorePullAnimation"), null, new HarmonyMethod(typeof(WinterBreak).GetMethod("MachineXCoreInit")));
            //Harmony.Patch(AccessTools.Method(typeof(MysteryMachineCore), "Update"), null, new HarmonyMethod(typeof(WinterBreak).GetMethod("CoreUpdate")));
            Harmony.Patch(AccessTools.Method(typeof(DeerActivatedState), "OnEnter"), null, new HarmonyMethod(typeof(WinterBreak).GetMethod("BeginDeerGas")));
            Harmony.Patch(AccessTools.Method(typeof(BearAttackState), "OnExplosionImpact"), null, new HarmonyMethod(typeof(WinterBreak).GetMethod("DisableBearAttack")));
            Harmony.Patch(AccessTools.Method(typeof(EscapeRopeBarState), "StartEscapeTimer"), null, new HarmonyMethod(typeof(WinterBreak).GetMethod("BeginEscapeSequence")));

            #endregion

            #region First Class

            Harmony.Patch(AccessTools.Method(typeof(ArmoredCarAssassinState), "NeutralizeArmoredCar"), null, new HarmonyMethod(typeof(FirstClass).GetMethod("NeutralizeArmoredCar")));
            Harmony.Patch(AccessTools.Method(typeof(PlaneAssassinState), "OnPlaneDestroyed"), new HarmonyMethod(typeof(FirstClass).GetMethod("OnPlaneDestroyed")));
            Harmony.Patch(AccessTools.Method(typeof(Animation), "CrossFade", new Type[] { typeof(string) }), new HarmonyMethod(typeof(FirstClass).GetMethod("AnimationPlay")));
            //Harmony.Patch(AccessTools.Method(typeof(ParticleSystem), "Play", new Type[0]), null, new HarmonyMethod(typeof(FirstClass).GetMethod("ParticleSystemPlay")));

            #endregion

            #region Seat Of Power

            Harmony.Patch(AccessTools.Method(typeof(CompoundTrigger), "OnTriggerEnter"), new HarmonyMethod(typeof(SeatOfPower).GetMethod("CompoundTriggerEnter")));
            Harmony.Patch(AccessTools.Method(typeof(GenericButtonState), "ActivateButton"), new HarmonyMethod(typeof(SeatOfPower).GetMethod("ActivateButton")));

            #endregion

            #region Death Engine

            Harmony.Patch(AccessTools.Method(typeof(LaserBlindingFlash), "Flash"), new HarmonyMethod(typeof(DeathEngine).GetMethod("DeathEngShoot")));

            #endregion
        }

        public override void OnUpdate()
        {
            GenericGameEvents.ProcessDamageHaptics();
            FriendlySkies.ProcessCarHaptics();
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            FriendlySkies.GasPedalDisabled();

            if (sceneName == "SpaceLevel")
                DeathEngine.ShittyFix_SetupEndLaserFlash();
        }

        public void LoadTacts()
        {
            foreach (string filePath in Directory.GetFiles(Environment.CurrentDirectory + @"\UserData\bHaptics", "*.tact"))
            {
                HapticClip clip = LoaderTools.LoadTactFile(Path.GetFullPath(filePath));
                clip.hideFlags = HideFlags.DontUnloadUnusedAsset;
                Globals.haptics.Add(Path.GetFileNameWithoutExtension(filePath), clip);
                Globals.Msg($"Loaded Tact {Path.GetFileNameWithoutExtension(filePath)}");
            }
        }
    }
}
