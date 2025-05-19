using Terraria.Audio;

namespace MikuPrismMod
{
    public static class LastPrismSound
    {
        public static readonly SoundStyle MikuBeamLoop = new SoundStyle("MikuPrismMod/Sounds/MikuBeam")
        {
            Volume = 1.0f,
            PitchVariance = 0.1f,
            MaxInstances = 10,
            IsLooped = false
        };
    }
}
