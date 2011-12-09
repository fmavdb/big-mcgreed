using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.logic.player
{
    public class PlayerDefinition 
    {
        public static PlayerDefinition loadDefinition() {
            PlayerDefinition def = new PlayerDefinition();
            //Laad gegevens.
            def.mainTexture = Program.INSTANCE.Content.Load<Texture2D>("compas");
            def.personTexture = Program.INSTANCE.Content.Load<Texture2D>("poppetje");
            return def;
        }

        public Texture2D mainTexture { get; private set; }

        public Texture2D personTexture { get; private set; }

        public PlayerDefinition()
        {
            mainTexture = null;
        }
    }
}
