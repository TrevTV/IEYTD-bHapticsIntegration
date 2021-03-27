using System;
using MelonLoader;
using UnityEngine;
using Bhaptics.Tact.Unity;

using SchellGames.Spectre.Assets.Scripts.Interactables;
using SchellGames.Spectre.Assets.Scripts.InputManagement;
using SchellGames.Spectre.Assets.Scripts.Framework.StateMachine;
using SchellGames.Spectre.Assets.Scripts.Interactables.Properties;
using SchellGames.Spectre.Assets.Scripts.Interactables.SharedStates;
using SchellGames.Spectre.Assets.Scripts.Interactables.Player.States;
using SchellGames.Spectre.Assets.Scripts.Interactables.Player.States.Data;
using SchellGames.Spectre.Assets.Scripts.Interactables.Player;
using SchellGames.Spectre.Assets.Scripts.Levels;
using System.Collections;

namespace BHapticsSupport.Sections
{
    public static class GenericGameEvents
    {
        public static Type SwitchBladeState = typeof(PlayerAliveState).Assembly
            .GetType("SchellGames.Spectre.Assets.Scripts.Interactables.SharedStates.SwitchBladeState");

        public static Type PoisonousGasOnState = typeof(PlayerAliveState).Assembly
            .GetType("SchellGames.Spectre.Assets.Scripts.Interactables.CarLevel.PoisonousGasStates.PoisonousGasOnState");

        public static void GunFire(GunState __instance, VRInput vrInput)
        {
            if (__instance is GunState)
            {
                if (__instance.animationComponent.isPlaying || __instance.currentAmmo <= 0)
                    return;

                Globals.Msg("Gun fire from " + vrInput);

                HapticClip clip = vrInput.name.Contains("Left")
                    ? HapticUtils.GetHapticClip("Pistol_small_L")
                    : HapticUtils.GetHapticClip("Pistol_small_R");

                clip.Play();
            }
        }

        public static void ToggleCutterState(VRInput vrInput)
        {
            Globals.Msg("Toggling knife from " + vrInput);

            ArmsHapticClip clip = vrInput.name.Contains("Left")
                ? HapticUtils.GetHapticClip<ArmsHapticClip>("Vibrate_L")
                : HapticUtils.GetHapticClip<ArmsHapticClip>("Vibrate_R");

            clip.Play();
        }

        public static void AmbientExplosion(InteractableEntityState __instance)
        {
            if (!((LevelManager.Instance.playerEntity.transform.position - __instance.transform.position).magnitude <= __instance.GetData().explosive.radius))
            {
                Globals.Msg("ambient explosion");
                HapticClip clip = HapticUtils.GetHapticClip("AmbientExplosion");
                clip.Play();
            }
        }

        public static void OnStartUnscrewing(ScrewdriverHeldState __instance, GameObject go)
        {
            ActiveInteractableEntity targetScrew = ReflectionTools.GetValueFromObject<ActiveInteractableEntity>(__instance, "_targetScrew");
            if (ReflectionTools.GetValueFromObject<PickUpEntity>(__instance, "_interactableEntity").isPicked && targetScrew == null)
            {
                ActiveInteractableEntity component = go.GetComponent<ActiveInteractableEntity>();
                if (component != null)
                {
                    InteractableEntityState currentState = component.GetCurrentState();
                    if (currentState is ScrewState)
                    {
                        ScrewState screwState = currentState as ScrewState;
                        if (screwState.PairedScrewdriver == null)
                        {
                            string hand = Globals.leftHandObject.Contains("Screwdriver") ? " left hand" : " right hand";
                            Globals.Msg("unscrewing " + go.name + " with " + hand);
                        }
                    }
                }
            }
        }

        #region Player Events

        #region Gripping

        public static void AttachObject(VRHandInput __instance, AttachEntity entity)
        {
            if (__instance.PlayerHand == Hand.Left)
            {
                Globals.lastLeftHandObject = Globals.leftHandObject;
                Globals.leftHandObject = entity.name;
            }
            else if (__instance.PlayerHand == Hand.Right)
            {
                Globals.lastRightHandObject = Globals.rightHandObject;
                Globals.rightHandObject = entity.name;
            }
        }

        public static void DetachObject(VRHandInput __instance)
        {
            if (__instance.PlayerHand == Hand.Left)
            {
                Globals.lastLeftHandObject = Globals.leftHandObject;
                Globals.leftHandObject = string.Empty;
            }
            else if (__instance.PlayerHand == Hand.Right)
            {
                Globals.lastRightHandObject = Globals.rightHandObject;
                Globals.rightHandObject = string.Empty;
            }
        }

        public static void PickObject(VRHandInput __instance, PickUpEntity entity)
        {
            if (__instance.PlayerHand == Hand.Left)
                Globals.leftHandObject = entity.name;
            else if (__instance.PlayerHand == Hand.Right)
                Globals.rightHandObject = entity.name;
        }

        public static void DropObject(VRHandInput __instance)
        {
            if (__instance.PlayerHand == Hand.Left)
                Globals.leftHandObject = string.Empty;
            else if (__instance.PlayerHand == Hand.Right)
                Globals.rightHandObject = string.Empty;
        }

        #endregion

        public static void Stun(PlayerAliveState __instance)
        {
            Globals.Msg("stunned");

            HapticClip clip = HapticUtils.GetHapticClip("StunGrenade");
            PlayerAliveStateData playerAliveStateData = ReflectionTools.GetValueFromObject<PlayerAliveStateData>(__instance, "_data");

            float duration = playerAliveStateData.stunBlurCurve.keys[playerAliveStateData.stunBlurCurve.length - 1].time + 5;
            float intensity = 0.75f;
            clip.Play(intensity, duration);
        }

        public static void DeathByExplosion(Explosive explosiveProperty) => Explode(explosiveProperty);

        public static void Explode(Explosive explosiveProperty, bool ambient = false, bool headAndArms = true)
        {
            // essentially, if `stun` is null, it'll return false
            if (explosiveProperty?.stun ?? false) return;

            Globals.Msg("Explode!");

            HapticClip vestClip = HapticUtils.GetHapticClip(ambient ? "AmbientExplosion" : "StunGrenade");
            vestClip.Play();

            if (headAndArms)
            {
                HapticClip headClip = HapticUtils.GetHapticClip("Explosion_Head");
                HapticClip armsClip = HapticUtils.GetHapticClip("Explosion_Arms");
                headClip.Play(ambient ? 0.5f : 1f);
                armsClip.Play(ambient ? 0.5f : 1f);
            }
        }

        public static void WearObject(ActiveInteractableEntity entity, Transform joint)
        {
            Globals.Msg("Wearing " + entity.name);

            if (entity.name.Contains("Hat") || entity.name.Contains("Helmet"))
            {
                HapticClip clip = HapticUtils.GetHapticClip("WearHat");
                clip.Play();
            }
            else if (entity.name.Contains("EarPiece"))
            {
                HapticClip clip = HapticUtils.GetHapticClip(joint.name.Contains("Left") ? "Earpiece_L" : "Earpiece_R");
                clip.Play();
            }
            else if (entity.name.Contains("XRay"))
            {
                HapticClip clip = HapticUtils.GetHapticClip("WearGoggles");
                clip.Play();
            }
            else if (entity.name.Contains("Cigar")) {}
        }

        public static void Eat(ActiveInteractableEntity entity, Edible edible)
        {
            Globals.Msg("Eat " + entity.name);
        }

        public static void StartDrinking(LiquidContainerPickUpState pickUpState)
        {
            Globals.Msg("Drinking of " + pickUpState.name);
        }

        public static void ProcessDamageHaptics()
        {
            try
            {
                PlayerDamageData data = LevelManager.Instance.playerEntity.DamageEffects[0];
                if (data)
                {
                    Globals.Msg($"applying damage {data.deathText} with rate of {data.damageRate}");

                    if (Globals.applyDamageRepeats.ContainsKey(data.deathText))
                        Globals.applyDamageRepeats[data.deathText]++;
                    else
                        Globals.applyDamageRepeats.Add(data.deathText, 1);

                    int currentRepeatAmount = Globals.applyDamageRepeats[data.deathText];

                    switch (data.deathText)
                    {
                        case "Hostile Guard":
                        case "Laser Blast":
                        case "Impaled":
                            HapticUtils.GetHapticClip("HeadVibrate").Play();
                            break;
                        case "Neurotoxin":
                        case "Virus Outbreak":
                        case "Poison Gas":
                            if (!(currentRepeatAmount >= 5)) break;
                            HapticClip coughClip = HapticUtils.GetRandomClipOfMultiple("Cough", true);
                            coughClip?.Play();
                            DoHeartbeat();
                            break;
                        case "Armored Car":
                        case "Shot by Plane":
                            HapticUtils.GetRandomClipOfMultiple("HeadShot").Play();
                            break;
                        case "Bees":
                            if (currentRepeatAmount == 1)
                                MelonCoroutines.Start(SeatOfPower.BeeStingDeath());
                            break;
                        case "Self Destruct":
                        case "Void of Space":
                        case "Explosion":
                            if (currentRepeatAmount == 1)
                                Explode(null);
                            break;
                        case "Electrocution":
                            if (currentRepeatAmount == 1)
                                MelonCoroutines.Start(DeathEngine.DoElectrocution());
                            break;
                        case "Radioactivity":
                            HapticClip radioactivity = HapticUtils.GetRandomClipOfMultiple("Radioactivity", true);
                            radioactivity?.Play();
                            DoHeartbeat();
                            break;
                        case "Drowned":
                        case "Suffocation":
                            if (currentRepeatAmount == 1)
                                MelonCoroutines.Start(DoSuffocation(data.damageRate));
                            break;
                        case "Fire":
                            if (currentRepeatAmount == 1)
                                Explode(null);

                            HapticClip fireClip = HapticUtils.GetRandomClipOfMultiple("Fire", true);
                            fireClip?.Play();
                            DoHeartbeat();
                            break;
                    }
                }
            }
            catch { }
        }

        public static void DoHeartbeat()
        {
            HapticClip heartClip = HapticUtils.GetHapticClip("HeartbeatFast", true);
            heartClip?.Play();
        }

        public static IEnumerator DoSuffocation(float deathRate)
        {
            HapticClip sternumClip = HapticUtils.GetHapticClip("Suffocation_Sternum");
            HapticClip spineClip = HapticUtils.GetHapticClip("Suffocation");

            int deathSeconds = (int)Math.Ceiling(1f / deathRate);

            for (int i = 0; i < deathSeconds; i++)
            {
                sternumClip.Play(0.5f);
                spineClip.Play();
                yield return new WaitForSeconds(1);
            }
        }

        public static void DoDeath(PlayerDamageData killingBlow)
        {
            Globals.Msg($"killed by {killingBlow.deathText} with a value of {killingBlow.damageRate}");
            Globals.applyDamageRepeats[killingBlow.deathText] = 0;
        }

        #endregion

        #region Unused Telekinesis Code
        private static HapticSource source;
        public static void Telekinesis(VRHandInput __instance)
        {
            if (source == null)
                source = new GameObject("[bHapticsSource]").AddComponent<HapticSource>();

            if (__instance.gameObject.name.Contains("Left"))
            {
                ArmsHapticClip clip = (ArmsHapticClip)Globals.haptics["Vibrate_L"];
                clip.Intensity = 0.40f;
                source.clip = clip;
                source.PlayLoop();
            }
            else
            {
                ArmsHapticClip clip = (ArmsHapticClip)Globals.haptics["Vibrate_R"];
                clip.Intensity = 0.40f;
                source.clip = clip;
                source.PlayLoop();
            }
        }

        public static void SetStateMachine(IState state)
        {
            if (state.ToString().Contains("VRHandLocalState") && source.IsPlaying())
                source.Stop();
        }
        #endregion
    }
}
