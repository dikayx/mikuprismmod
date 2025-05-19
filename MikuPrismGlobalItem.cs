using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MikuPrismMod
{
    public class MikuBeamPlayer : ModPlayer
    {
        private ActiveSound activeSound = null;
        private bool isSoundActive = false;

        public override void PostUpdate()
        {
            Item heldItem = Player.HeldItem;
            bool usingPrism = heldItem.type == ItemID.LastPrism && Player.channel && Player.ItemAnimationActive;

            if (usingPrism)
            {
                // Start sound once per activation
                if (!isSoundActive)
                {
                    if (SoundEngine.PlaySound(LastPrismSound.MikuBeamLoop, Player.Center) is SlotId slot &&
                        SoundEngine.TryGetActiveSound(slot, out activeSound))
                    {
                        isSoundActive = true;
                    }
                }

                // Keep sound positioned at the player
                if (activeSound?.IsPlaying == true)
                {
                    activeSound.Position = Player.Center;
                }
            }
            else
            {
                StopBeamSound();
            }
        }

        public override void OnRespawn() => StopBeamSound();
        public override void OnEnterWorld() => StopBeamSound();

        private void StopBeamSound()
        {
            if (activeSound?.IsPlaying == true)
            {
                activeSound.Stop();
            }

            activeSound = null;
            isSoundActive = false;
        }
    }
}
