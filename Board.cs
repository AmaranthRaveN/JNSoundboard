using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundBoard
{
    public class Board
    {
        public string boardName { get; set; }
        public string rndHotkey { get; set; }
        public Keys rndPressedKey { get; set; }
        public Keys[] rndKeys { get; set; }

        public Dictionary<int, SoundFile> SoundBoard { get; set; }


        public Board()
        {
            SoundBoard = new Dictionary<int, SoundFile>();
        }

        public SoundFile randomFile()
        {
            Random rnd = new Random();
            SoundFile randFile = new SoundFile();
            SoundBoard.TryGetValue(rnd.Next(0, SoundBoard.Count), out randFile);

            
            return randFile;
        }
    }
}
