using System;
using MelonLoader;
using UnityEngine;
using Bhaptics.Tact.Unity;

using SchellGames.Spectre.Assets.Scripts.Interactables.Player.States;
using SchellGames.Spectre.Assets.Scripts.Interactables;
using SchellGames.Spectre.Assets.Scripts.Interactables.CarLevel.Cannon;

namespace BHapticsSupport.Sections
{
    public static class FriendlySkies
    {
        public static Type GearShiftingState = typeof(PlayerAliveState).Assembly
            .GetType("SchellGames.Spectre.Assets.Scripts.Interactables.CarLevel.GearShifterStates.GearShiftingState");
        public static Type RiggedBombPickUpState = typeof(PlayerAliveState).Assembly
            .GetType("SchellGames.Spectre.Assets.Scripts.Interactables.CarLevel.Bomb.States.RiggedBombPickUpState");
        public static Type GasPedal = typeof(PlayerAliveState).Assembly
            .GetType("SchellGames.Spectre.Assets.Scripts.Interactables.CarLevel.GasPedal.GasPedal");
        public static Type CarEngine = typeof(PlayerAliveState).Assembly
            .GetType("SchellGames.Spectre.Assets.Scripts.Interactables.CarLevel.CarEngine.CarEngine");

        public static void GearChange(Collider collider)
        {
            if (collider.name == "Collider_GS")
            {
                Globals.DebugMsg("Gear changed!");

                ArmsHapticClip clip = Globals.leftHandObject == "GearShifterCollider"
                    ? HapticUtils.GetHapticClip<ArmsHapticClip>("Vibrate_L")
                    : HapticUtils.GetHapticClip<ArmsHapticClip>("Vibrate_R");

                clip.Play();
            }
        }

        public static void CannonLaunch(Cannon __instance)
        {
            ActiveInteractableEntity entity =
                ReflectionTools.GetValueFromObject<ActiveInteractableEntity>(__instance, "_entityToBeLaunched");

            bool firing = ReflectionTools.GetValueFromObject<bool>(__instance, "_firing");

            if (entity != null && !firing)
            {
                Globals.DebugMsg("Cannon launched");

                bool isRightHand = Globals.lastRightHandObject == "Shared_Interactives_SpyCar_Cannon_FireLever";
                ArmsHapticClip clip = isRightHand
                    ? HapticUtils.GetHapticClip<ArmsHapticClip>("Vibrate_R")
                    : HapticUtils.GetHapticClip<ArmsHapticClip>("Vibrate_L");

                clip.Play();
            }
        }

        public static void CargoDoorOpen()
        {
            Globals.DebugMsg("Cargo door open");
            GenericGameEvents.Explode(null);
        }

        public static void TurnIgnitionOn()
        {
            Globals.DebugMsg("Car ignition");
            HapticClip clip = HapticUtils.GetHapticClip("CarIgnition");
            clip.Play();
        }

        #region Gas Pedal

        private static bool carMoving;

        public static void ProcessCarHaptics()
        {
            if (carMoving)
            {
                Globals.DebugMsg("Car moving");
                HapticClip clip = HapticUtils.GetHapticClip("Driving");
                clip.Play();
            }
        }

        public static void GasPedalEnabled()
            => carMoving = true;

        public static void GasPedalDisabled()
            => carMoving = false;

        #endregion
    }
}
