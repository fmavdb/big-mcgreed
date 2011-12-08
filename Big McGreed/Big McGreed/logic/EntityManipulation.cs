using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed.logic.npc;
using Big_McGreed.logic.player;
using Big_McGreed.logic.map.objects;

namespace Big_McGreed.logic
{
    public class EntityManipulation
    {
        public static void NPCRun(NPC npc)
        {
            if (npc.definition.mainTexture != null)
            {
                npc.run();
            }
        }

        public static void NPCDraw(NPC npc)
        {
            if (npc.visible && npc.definition.mainTexture != null)
            {
                npc.Draw();
            }
        }

        public static void ObjectDraw(GameObject gameObject)
        {
            if (gameObject.definition.mainTexture != null && gameObject.visible)
            {
                gameObject.Draw();
            }
        }

        public static void PlayerRun(Player player)
        {
            if (player.definition.mainTexture != null)
            {
                player.run();
            }
        }
    }
}
