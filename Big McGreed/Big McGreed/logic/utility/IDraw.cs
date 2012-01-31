using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Big_McGreed.utility
{
    public interface IDraw
    {
        /// <summary>
        /// Draws the specified batch.
        /// </summary>
        /// <param name="batch">The batch.</param>
        void Draw(SpriteBatch batch);

        /// <summary>
        /// Loads the content.
        /// </summary>
        void LoadContent();
    }
}
