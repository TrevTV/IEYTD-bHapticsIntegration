using MelonLoader;
using Bhaptics.Tact.Unity;

using SchellGames.Spectre.Assets.Scripts.Interactables.WindowLevel.VirusVat;

namespace BHapticsSupport.Sections
{
    public static class SqueakyClean
    {
        public static void NeutralizedVirusExplosion(VirusRocketState __instance)
        {
            bool neutralized = ReflectionTools.GetValueFromObject<bool>(__instance, "_virusNeutralized");

            if (neutralized)
            {
                Globals.DebugMsg("Neutralized explosion!");

                VestHapticClip clip = HapticUtils.GetHapticClip<VestHapticClip>("NeutralizeExplosion");
                clip.Play();
            }
        }

        public static void GlassBreak()
        {
            Globals.DebugMsg("Glass broke");

            HapticClip clip = HapticUtils.GetHapticClip("GlassBreaking");
            clip.Play();
        }
    }
}
