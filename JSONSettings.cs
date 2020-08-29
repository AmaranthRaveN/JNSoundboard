using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace SoundBoard
{
    public class JSONSettings
    {
        readonly static SoundboardSettings DEFAULT_SOUNDBOARD_SETTINGS = new SoundboardSettings(new Keys[] { }, new Keys[] { }, new Keys[] { }, "", new LoadJSONFile[] { new LoadJSONFile(new Keys[] { }, "") }, true, false, "", "", 0);

        internal static SoundboardSettings soundboardSettings = new SoundboardSettings();

        //saving XML files like this makes the XML messy, but it works. if you can un-messy it, please do it and make a pull request :)
        #region Keys and sounds settings
        public class KeysSounds
        {
            public Keys[] Keys;
            public string[] SoundLocations;

            public KeysSounds() { }

            public KeysSounds(Keys[] keys, string[] soundLocs)
            {
                Keys = keys;
                SoundLocations = soundLocs;
            }
        }

        [Serializable]
        public class Settings
        {
            public KeysSounds[] KeysSounds;

            public Settings() { }

            public Settings(KeysSounds[] ks)
            {
                KeysSounds = ks;
            }
        }
        #endregion

        #region Soundboard settings
        public class LoadJSONFile
        {
            public Keys[] Keys;
            public string JSONLocation;

            public LoadJSONFile() { }

            public LoadJSONFile(Keys[] keys, string jsonLocation)
            {
                Keys = keys;
                JSONLocation = jsonLocation;
            }
        }

        [Serializable]
        public class SoundboardSettings
        {
            public Keys[] StopSoundKeys;
            public Keys[] RecSoundKeys;
            public Keys[] StopRecSoundKeys;
            public LoadJSONFile[] LoadJSONFiles;
            public bool MinimizeToTray, PlaySoundsOverEachOther;
            public string LastPlaybackDevice, LastLoopbackDevice, recordDirectory;
            public int recordedNum;
            public SoundboardSettings() { }

            public SoundboardSettings(Keys[] stopSoundKeys,Keys[] recSoundKeys, Keys[] stopRecSoundKeys, string recDirect, LoadJSONFile[] loadJSONFiles, bool minimizeToTray, bool playSoundsOverEachOther, string lastPlaybackDevice, string lastLoopbackDevice, int recordNum)
            {
                StopSoundKeys = stopSoundKeys;
                RecSoundKeys = recSoundKeys;
                StopRecSoundKeys = stopRecSoundKeys;
                recordDirectory = recDirect;

                LoadJSONFiles = loadJSONFiles;
                MinimizeToTray = minimizeToTray;
                PlaySoundsOverEachOther = playSoundsOverEachOther;
                LastPlaybackDevice = lastPlaybackDevice;
                LastLoopbackDevice = lastLoopbackDevice;
                recordedNum = recordNum;
            }
        }
        #endregion

        internal static void WriteJSON(object kl, string jsonloc)
        {
            var jsonSerial = JsonConvert.SerializeObject(kl, Newtonsoft.Json.Formatting.Indented);
            // StreamWriter file = File.CreateText(filename);

            File.WriteAllText(jsonloc, jsonSerial);

            /*
            XmlSerializer serializer = new XmlSerializer(kl.GetType());

            using (MemoryStream memStream = new MemoryStream())
            {
                using (StreamWriter stream = new StreamWriter(memStream, Encoding.Unicode))
                {
                    var settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.OmitXmlDeclaration = true;

                    using (var writer = XmlWriter.Create(stream, settings))
                    {
                        var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                        serializer.Serialize(writer, kl, emptyNamepsaces);

                        int count = (int)memStream.Length;

                        byte[] arr = new byte[count];
                        memStream.Seek(0, SeekOrigin.Begin);

                        memStream.Read(arr, 0, count);

                        using (BinaryWriter binWriter = new BinaryWriter(File.Open(xmlLoc, FileMode.Create)))
                        {
                            binWriter.Write(arr);
                        }
                    }
                }
            }
            */
        }

        internal static object ReadJSON(Type type, string jsonloc)
        {
            //IDictionary<int, SoundboardSettings> dict = new Dictionary<int, SoundboardSettings>();
            SoundboardSettings settings = new SoundboardSettings();
            using (StreamReader r = new StreamReader(jsonloc))
            {
                JsonSerializer serializer = new JsonSerializer();
                settings = (SoundboardSettings)serializer.Deserialize(r, typeof(SoundboardSettings));
                r.Close();
            }
            return settings;


            /*
             * internal static IDictionary<int,SoundFile> readJSON(string jsonLoc)
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


            /*
            var serializer = new XmlSerializer(type);

            using (var reader = XmlReader.Create(xmlLoc))
            {
                if (serializer.CanDeserialize(reader))
                {
                    return serializer.Deserialize(reader);
                }
                else return null;
            }
            */
        }

        internal static void SaveSoundboardSettingsJSON()
        {
            WriteJSON(soundboardSettings, Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.json");
        }

        internal static void LoadSoundboardSettingsJSON()
        {
            string filePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.json";

            if (File.Exists(filePath))
            {
                SoundboardSettings settings;

                try
                {
                    settings = (SoundboardSettings)ReadJSON(typeof(SoundboardSettings), filePath);
                }
                catch
                {
                    soundboardSettings = DEFAULT_SOUNDBOARD_SETTINGS;
                    return;
                }

                if (settings == null)
                {
                    soundboardSettings = DEFAULT_SOUNDBOARD_SETTINGS;
                    return;
                }

                if (settings.StopSoundKeys == null) settings.StopSoundKeys = new Keys[] { };

                if (settings.StopRecSoundKeys == null) settings.StopRecSoundKeys = new Keys[] { };

                if (settings.RecSoundKeys == null) settings.RecSoundKeys = new Keys[] { };

                if (settings.LoadJSONFiles == null) settings.LoadJSONFiles = new LoadJSONFile[] { };

                if (settings.LastPlaybackDevice == null) settings.LastPlaybackDevice = "";

                if (settings.LastLoopbackDevice == null) settings.LastLoopbackDevice = "";

                soundboardSettings = settings;
            }
            else
            {
                WriteJSON(DEFAULT_SOUNDBOARD_SETTINGS, filePath);
            }
        }
    }
}
