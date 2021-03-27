// #define LOG

using MelonLoader;
using Bhaptics.Tact.Unity;
using System.Collections.Generic;
using SchellGames.Spectre.Assets.Scripts.Interactables.Player;

namespace BHapticsSupport
{
    public static class Globals
    {
        public static Dictionary<string, HapticClip> haptics = new Dictionary<string, HapticClip>();
        public static Dictionary<string, int> applyDamageRepeats = new Dictionary<string, int>();

        public static string leftHandObject;
        public static string rightHandObject;

        public static string lastLeftHandObject;
        public static string lastRightHandObject;

        public static System.Random systemRandom = new System.Random();

        public static void Msg(string str)
        {
            #if LOG
            MelonLogger.Msg(str);
            #endif
        }

        public static void Msg(object obj)
        {
            #if LOG
            MelonLogger.Msg(obj.ToString());
            #endif
        }
    }
}
