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
    public class SoundFile
    {

        ///
        /// Sound file must have a:
        /// File path
        /// Hotkey
        /// Play count?
        ///
        //public string fileName { get; set; }
        public string filePath { get; set; }
        public string hotKey { get; set; }
        public int playCount { get; set; }

        public Keys pressedKey { get; set; }

        public Keys[] keys { get; set; }

        public float volume { get; set; }
        

        public SoundFile() { volume = 1.0f; }

        [Serializable]
        public class SoundFiles {
            public IDictionary<int, SoundFile> soundFiles = new Dictionary<int, SoundFile>();

            public void setDictionary(IDictionary<int,SoundFile> dictionary)
            {
                soundFiles = dictionary;
            }
            public IDictionary<int,SoundFile> getDictionary()
            {
                return soundFiles;
            }

        }

        internal static void writeJSON(IDictionary<int,SoundFile> dictionary,string filename)
        {
            //JsonSerializer serializer = new JsonSerializer();
            //string jsonString;
            var jsonSerial = JsonConvert.SerializeObject(dictionary, Formatting.Indented);
           // StreamWriter file = File.CreateText(filename);

            File.WriteAllText(filename, jsonSerial);
            
            
        }

        internal static string userGetJSONLoc()
        {
            SaveFileDialog diag = new SaveFileDialog();

            diag.Filter = "JSON file containing keys and sounds|*.json";

            var result = diag.ShowDialog();

            if (result == DialogResult.OK)
            {
                return diag.FileName;
            }
            else return "";
        }
        internal static IDictionary<int,SoundFile> readJSON(string jsonLoc)
        {

            IDictionary<int, SoundFile> dict = new Dictionary<int, SoundFile>();
            using (StreamReader r = new StreamReader(jsonLoc))
            {
                //string json = r.ReadToEnd();
                JsonSerializer serializer = new JsonSerializer();
                dict = (IDictionary<int, SoundFile>)serializer.Deserialize(r, typeof(IDictionary<int, SoundFile>));
                r.Close();
                //return json;
            }

            return dict;
        }

        internal static void jsonLoad()
        {
            var diag = new OpenFileDialog();

            diag.Filter = "json file containing keys and sounds|*.json";

            var result = diag.ShowDialog();

            if (result == DialogResult.OK)
            {
                string path = diag.FileName;

                readJSON(path);
            }
        }

        

    }

    

}
