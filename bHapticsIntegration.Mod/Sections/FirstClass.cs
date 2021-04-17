using System;
using MelonLoader;
using UnityEngine;
using Bhaptics.Tact.Unity;

namespace BHapticsSupport.Sections
{
    public static class FirstClass
    {
        public static void NeutralizeArmoredCar()
        {
            Globals.Msg("neutralized car");
            HapticClip clip = HapticUtils.GetHapticClip("TankExplosion");
            clip.Play();
        }

        public static void OnPlaneDestroyed()
        {
            Globals.Msg("plane destroyed");
            HapticClip clip = HapticUtils.GetHapticClip("TankExplosion");
            clip.Play();
        }

        public static void AnimationPlay(Animation __instance, string animation)
        {
            if (__instance.gameObject.name == "EnvironmentRotationRoot" && animation == "TrainStopAnimation")
            {
                Globals.Msg("stopping train");
                HapticClip clip = HapticUtils.GetHapticClip("TrainStop");
                clip.Play();
            }
        }

        public static void ParticleSystemPlay(ParticleSystem __instance)
        {
            //todo: does this even work
            if (__instance.name.Contains("fx_Train_ArmoredCar_01_Fire"))
                Globals.Msg("Armored Car Fire");
            else if (__instance.name.Contains("PlaneGunTracer"))
                Globals.Msg("Plane Fire");
        }
    }
}
