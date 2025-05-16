using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MikuPrismMod
{
    // TODO: Move to own class
    public class LastPrismSoundOverride : GlobalItem
    {
        public static readonly SoundStyle MikuSound = new SoundStyle("MikuPrismMod/Sounds/MikuBeam")
        {
            Volume = 1.0f,
            PitchVariance = 0.1f,
            MaxInstances = 10,
            IsLooped = true
        };
    }

    // TODO: Fix repeating playback of sound. Should be played back only once per "activation"
    public class MikuBeamPlayer : ModPlayer
    {
        private SlotId? soundSlot = null;
        private ActiveSound activeSound = null;

        public override void PostUpdate()
        {
            Item heldItem = Player.HeldItem;
            bool usingPrism = heldItem.type == ItemID.LastPrism && Player.channel && Player.ItemAnimationActive;

            if (usingPrism)
            {
                if (activeSound == null || !activeSound.IsPlaying)
                {
                    if (SoundEngine.PlaySound(LastPrismSoundOverride.MikuSound with { IsLooped = true }, Player.Center) is SlotId slot)
                    {
                        soundSlot = slot;
                        SoundEngine.TryGetActiveSound(slot, out activeSound);
                    }
                }
                else
                {
                    // 🟢 Update sound position to follow player
                    activeSound.Position = Player.Center;
                }
            }
            else
            {
                if (activeSound != null && activeSound.IsPlaying)
                {
                    activeSound.Stop();
                    activeSound = null;
                    soundSlot = null;
                }
            }
        }


        public override void OnRespawn()
        {
            StopBeamSound();
        }

        public override void OnEnterWorld()
        {
            StopBeamSound();
        }

        private void StopBeamSound()
        {
            if (activeSound != null && activeSound.IsPlaying)
            {
                activeSound.Stop();
            }
            activeSound = null;
            soundSlot = null;
        }
    }
}
