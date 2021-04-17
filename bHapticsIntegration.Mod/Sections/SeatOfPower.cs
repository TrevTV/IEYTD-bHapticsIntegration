using System;
using MelonLoader;
using UnityEngine;
using Bhaptics.Tact.Unity;
using System.Collections;

using SchellGames.Spectre.Assets.Scripts.Interactables;
using SchellGames.Spectre.Assets.Scripts.Interactables.SharedStates;

namespace BHapticsSupport.Sections
{
    public static class SeatOfPower
    {
        public static void CompoundTriggerEnter(CompoundTrigger __instance, Collider other)
        {
            if (__instance.name == "HarnessLockedTrigger")
            {
                if (__instance.targetCollider == other)
                {
                    Globals.DebugMsg("Chair seatbelt down");
                    HapticClip clip = HapticUtils.GetHapticClip("Seatbelt");
                    clip.Play();
                }
            }
        }

        public static void ActivateButton(GenericButtonState __instance)
        {
            if (__instance.name == "Vault_Interactives_LaunchZorsChairButton_P")
            {
                Globals.DebugMsg("Start chair launch");
            }
        }

        public static IEnumerator BeeStingDeath()
        {
            yield return new WaitForSeconds(3);
            for (int i = 0; i < 175; i++)
            {
                HapticClip beeStingClip = HapticUtils.GetRandomClipOfMultiple("BeeSting");
                beeStingClip?.Play();
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
