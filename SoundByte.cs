using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundBoard
{
    class SoundByte
    {
        private string audioPath { get; set; }
        private int modifier { get; set; }
        private int key { get; set; }

        public SoundByte(int modifier, Keys key, Form form)
        {
            this.modifier = modifier;
            this.key = (int)key;

        }
    }
}
