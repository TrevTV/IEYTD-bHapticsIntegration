using System;
using System.Collections.Generic;
using System.Linq;
using Bhaptics.Tact.Unity;

namespace BHapticsSupport
{
    public static class HapticUtils
    {
        public static HapticClip GetHapticClip(string name, bool onlyReturnWhenNoneArePlaying = false)
        {
            HapticClip clip = Globals.haptics[name];

            if (onlyReturnWhenNoneArePlaying && clip.IsPlaying()) return null;

            clip.ResetValues();
            return clip;
        }

        public static T GetHapticClip<T>(string name)
        {
            HapticClip clip = Globals.haptics[name];
            clip.ResetValues();
            return (T)Convert.ChangeType(clip, typeof(T));
        }

        public static HapticClip GetRandomClipOfMultiple(string name, bool onlyReturnWhenNoneArePlaying = false)
        {
            var enumerable = Globals.haptics.Where(c => c.Key.StartsWith(name));

            if (onlyReturnWhenNoneArePlaying && enumerable.Any(c => c.Value.IsPlaying()))
                return null;

            HapticClip clip = enumerable.ElementAt(Globals.systemRandom.Next(0, enumerable.Count())).Value;
            clip.ResetValues();
            return clip;
        }

        public static List<HapticClip> GetOrderedListOfMultiple(string name, bool onlyReturnWhenNoneArePlaying = false)
        {
            var enumerable = Globals.haptics.Where(c => c.Key.StartsWith(name));

            if (onlyReturnWhenNoneArePlaying && enumerable.All(c => !c.Value.IsPlaying()))
                return null;

            List<HapticClip> wantedClips = enumerable.ToDictionary(x => x.Key, x => x.Value).Values.ToList();
            List<HapticClip> sortedClips = wantedClips.OrderBy(x => x.name).ToList();

            return sortedClips;
        }
    }
}
