using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Big_McGreed;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Big_McGreed.content.video
{
    public class Intro
    {
        Video intro;
        VideoPlayer videoPlayer;

        public Intro()
        {
            videoPlayer = new VideoPlayer();
            intro = Program.INSTANCE.Content.Load<Video>("Big McGreed Intro");
        }

        public void StartVideo()
        {
            videoPlayer.Play(intro);
        }

        public void DrawVideo(SpriteBatch batch)
        {
            batch.Draw(videoPlayer.GetTexture(), new Rectangle(0, 0, gameframe.GameFrame.Width, gameframe.GameFrame.Height), Color.White);
        }

        public void StopVideo()
        {
            if (videoPlayer.State != MediaState.Playing)
            {
                videoPlayer.Stop();
                Program.INSTANCE.CurrentGameState = GameWorld.GameState.InGame;
                Program.INSTANCE.newGame();
            }
        }

        public void SkipVideo()
        {
            videoPlayer.Stop();
            Program.INSTANCE.CurrentGameState = GameWorld.GameState.InGame;
            Program.INSTANCE.newGame();
        }
    }
}
