using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Big_McGreed.logic.sound
{
    public class Sound
    {
        public string name { get; 
            set {
                this.name = value;
                soundEffect = Program.INSTANCE.Content.Load<SoundEffect>(value);
            }
        }

        public SoundEffect soundEffect { get; set; }

        public Sound(string name)
        {
            this.name = name;
            soundEffect = Program.INSTANCE.Content.Load<SoundEffect>(name);
        }
    }
}
