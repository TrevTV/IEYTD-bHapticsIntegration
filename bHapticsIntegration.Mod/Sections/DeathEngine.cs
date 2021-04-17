using System;
using MelonLoader;
using UnityEngine;
using System.Collections;
using Bhaptics.Tact.Unity;

using SchellGames.Spectre.Assets.Scripts.Interactables.SpaceLevel;
using SG.GlobalEvents;
using UnityEngine.Events;

namespace BHapticsSupport.Sections
{
    public static class DeathEngine
    {
        public static void ShittyFix_SetupEndLaserFlash()
        {
            GameObject endBlindingFlashObj = GameObject.Find("/OutroEvents/Space_OutroFX_P/EndBlindingFlash");
            GlobalEventListener listener = endBlindingFlashObj.GetComponent<GlobalEventListener>();
            LaserBlindingFlash flash = endBlindingFlashObj.GetComponent<LaserBlindingFlash>();

            listener.Response.AddListener(new UnityAction(flash.Flash));
        }

        public static bool DeathEngShoot(LaserBlindingFlash __instance)
        {
            Globals.Msg("Laser Flash from " + __instance.name);

            HapticClip clip = HapticUtils.GetHapticClip("DeathEngineExplode");

            bool returnBool = true;

            switch (__instance.name)
            {
                case "LaserBlindingFlash":
                    returnBool = true;       
                    clip.Play();
                    break;
                case "EndBlindingFlash":
                    returnBool = false;
                    clip.Play();
                    break;
            }

            return returnBool;
        }

        public static IEnumerator DoElectrocution()
        {
            HapticClip leftArmClip = HapticUtils.GetHapticClip("Electrocution_L");
            HapticClip rightArmClip = HapticUtils.GetHapticClip("Electrocution_R");

            leftArmClip.Play();
            rightArmClip.Play();

            while (rightArmClip.IsPlaying())
                yield return new WaitForSeconds(0.01f);

            HapticUtils.GetHapticClip("Electrocution_Vest").Play(0.75f);
        }
    }
}
