using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Big_McGreed.content.highscore
{
    public class HighScore
    {
        public Dictionary<string, int> highScores;

        public HighScore() {
            highScores = new Dictionary<string, int>();
            //Laad gegevens...
            //Dit is een voorbeeld:
            highScores.Add("Frank", 1000);
            highScores.Add("Geert", 5);
            highScores.Add("Jelle", -1);
            highScores.Add("Kevin", 100);
            highScores.Add("Rick", 500);
            highScores.Add("Wouter", 0);
        }
    }
}
