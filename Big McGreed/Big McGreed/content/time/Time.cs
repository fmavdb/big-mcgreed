using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Big_McGreed.content.gameframe;

namespace Big_McGreed.content.time
{
    public class Time
    {

        public Stopwatch watch { get; private set; }

        private Vector2 position;

        public Time()
        {
            watch = new Stopwatch();
            position = new Vector2(GameFrame.Width / 2, 0);
        }

        private int seconds;

        private int milliseconds;

        private int minutes;

        public void Pause()
        {
            watch.Stop();
        }

        public void Start()
        {
            watch.Start();
        }

        public void Reset()
        {
            watch.Reset();
        }

        public void Stop()
        {
            watch.Stop();
            watch.Reset();
        }

        public int getMilliSeconds()
        {     
            return (int)watch.ElapsedMilliseconds;
        }

        public int getSeconds()
        {
            return (int)(watch.ElapsedMilliseconds / 1000);
        }

        public int getMinutes()
        {
            return (int)(watch.ElapsedMilliseconds / 1000) / 60;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.DrawString(Program.INSTANCE.IManager.font, getMinutes() + ":" + getSeconds() + ":" + getMilliSeconds(), position, Color.White);
        }
    }
}
