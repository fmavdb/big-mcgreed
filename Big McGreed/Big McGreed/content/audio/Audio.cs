using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Big_McGreed.content.audio
{
    public class Audio
    {
        SoundEffect soundEffect;
        SoundEffectInstance instance;
        bool playingSound = false;

        public Audio(string bestand, bool loop)
        {
            soundEffect = Program.INSTANCE.Content.Load<SoundEffect>(bestand);
            instance = soundEffect.CreateInstance();
            instance.IsLooped = loop;
            playingSound = true;
        }

        public void PlaySound()
        {
            if (playingSound == true)
            {
                if (instance.IsLooped == false)
                {
                    soundEffect.Play();
                }
                else
                {
                    instance.Play();
                }
            }
            else ;
        }

        public void StopSound()
        {
            playingSound = false;
            instance.Pause();
        }
    }
}
