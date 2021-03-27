﻿using MelonLoader;
using Bhaptics.Tact.Unity;

using SchellGames.Spectre.Assets.Scripts.Interactables.HunterLodgeLevel.MysteryMachineInteraction;
using SchellGames.Spectre.Assets.Scripts.Interactables.HunterLodgeLevel;
using SchellGames.Spectre.Assets.Scripts.Interactables.HunterLodgeLevel.HelicopterInteraction;
using System.Reflection;

namespace BHapticsSupport.Sections
{
    public static class WinterBreak
    {
        public static MysteryMachineCore activeMachineCore;

        public static void MachineXCoreInit(MysteryMachineActiveState __instance)
        {
            Globals.Msg("machine core init");
            int coreIndex = ReflectionTools.GetValueFromObject<int>(__instance, "_selectedCoreIndex");
            if (!__instance.machineCores[coreIndex].coreDestroyed)
            {
                activeMachineCore = __instance.machineCores[coreIndex];
                Globals.Msg("New core active: " + activeMachineCore.name);
            }
        }

        public static void CoreUpdate(MysteryMachineCore __instance)
        {
            if (__instance == activeMachineCore)
            {
                Globals.Msg("updating active core: " + __instance.name);
            }
        }

        public static void DisableBearAttack()
        {
            Globals.Msg("Bear was disabled via explosion");

            HapticClip clip = HapticUtils.GetHapticClip("BearExplosion");
            clip.Play();
        }

        public static void BeginDeerGas()
        {
            Globals.Msg("Deer go brrrr");
        }

        public static void BeginEscapeSequence(EscapeRopeBarState __instance)
        {
            Globals.Msg("escaping pog");

            /*HapticClip clip = HapticUtils.GetHapticClip("LodgeExit");

            FieldInfo escapingField = ReflectionTools.GetFieldInfoFromObject(__instance, "_escaping");
            while ((bool)escapingField.GetValue(__instance) && !clip.IsPlaying())
            {
                // Loop Haptics
                clip.Play();
            }*/
        }
    }
}
