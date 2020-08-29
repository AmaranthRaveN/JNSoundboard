using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundBoard
{
    [Serializable]
    public class Boards
    {
        //public string BoardName { get; set; }
        public string JsonPath { get; set; }

        //public int SoundCount { get; set; }

        public Dictionary<int, SoundFile> SoundBoard { get; set; }

        public Dictionary<string, IDictionary<int, SoundFile>> SoundBoards { get; set; }


        public Boards()
        {
            SoundBoard = new Dictionary<int, SoundFile>();
            SoundBoards = new Dictionary<string, IDictionary<int, SoundFile>>();
        }

        
        
        internal static void writeJSON(Boards boards, string jsonloc)
        {
            //MessageBox.Show(boards.SoundBoards.Count.ToString());

            var jsonSerial = JsonConvert.SerializeObject(boards, Formatting.Indented);
            File.WriteAllText(jsonloc, jsonSerial);
            //var jsonSerial = JsonConvert.SerializeObject(boards, Formatting.Indented);
            //File.WriteAllText(jsonloc, jsonSerial);
        }

        
        
        internal static string userGetJSONLoc()
        {
            SaveFileDialog diag = new SaveFileDialog();

            diag.Filter = "JSON file containing soundboard|*.json";

            var result = diag.ShowDialog();

            if (result == DialogResult.OK)
            {
                return diag.FileName;
            }
            else return "";
        }

        internal static Boards readJSON(string jsonLoc)
        {
            //IDictionary<string, Dictionary<int, SoundFile>> dict = new Dictionary<string, Dictionary<int, SoundFile>>();
            Boards boards;
            using (StreamReader r = new StreamReader(jsonLoc))
            {
                //string json = r.ReadToEnd();
                JsonSerializer serializer = new JsonSerializer();
                boards = (Boards)serializer.Deserialize(r, typeof(Boards));
                r.Close();
            }

            return boards;
        }
        internal static void jsonLoad()
        {
            var diag = new OpenFileDialog();

            diag.Filter = "json file of soundboard|*.json";

            var result = diag.ShowDialog();

            if (result == DialogResult.OK)
            {
                string path = diag.FileName;

                readJSON(path);
            }

        }
    }

   
}
