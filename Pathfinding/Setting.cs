using System.Collections.Generic;
using System.IO;

namespace Pathfinding
{

    public class Setting //general class used to save properties between runs;
    {

        public string Name { get; set; }
        public string Value { get; set; }

        public Setting(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public override string ToString()
        {
            return (Name + "=" + Value);
        }

        public static void WriteSettings(List<Setting> settings, string file, string comments) //Writes list of settings to file in the format "NAME=VALUE" (function ToString());
        {
            using (StreamWriter writer = new StreamWriter(file, false))
            {
                writer.WriteLine(comments);
                foreach (Setting s in settings)
                {
                    writer.WriteLine(s.ToString());
                }
            }
        }

        public static List<Setting> ReadSettings(string file) //Retrieves list of settings from file;
        {
            List<Setting> myList = new List<Setting>();
            using (StreamReader reader = new StreamReader(file))
            {
                string line = "";
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (line != "")
                    {
                        if (line[0] == '*') //quits reader when reaches the map "drawing" sign
                            break;
                        if (line.Contains("=") && line[0] != '/')
                        {
                            string sName = line.Split('=')[0];
                            string sValue = line.Split('=')[1];
                            myList.Add(new Setting(sName, sValue));
                        }
                    }

                }
            }
            return (myList);
        }
    }
}

/* Example of implementing the settings list retrieved;
* 
        public void runSettings(List<Setting> myList)
        {
            foreach(
            {
                switch (myList[i].Name)
                {
                    case "top":
                        this.Top = Convert.ToInt32(myList[i].Value);
                        break;
                    case "left":
                        this.Left = Convert.ToInt32(myList[i].Value);
                        break;
                    case "isMaximized":
                        if (Convert.ToBoolean(myList[i].Value))
                            this.WindowState = FormWindowState.Maximized;
                        break;
                    default:
                        break;
                }
            }
        }
*/
