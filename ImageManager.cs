using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SFML.Graphics;

namespace ECSIsaac
{
    static class ImageManager
    {
        static Dictionary<string, Image> images;

        static ImageManager()
        {
            images = new Dictionary<string, Image>();
        }

        public static Image Get(string key)
        {
            return images[key];
        }

        public static void Load(string pathToFrames)
        {
            string[] fileNames = Directory.GetFiles(pathToFrames, "*", SearchOption.AllDirectories);
            for (int i = 0; i < fileNames.Length; i++)
            {
                if (IsImage(Path.GetExtension(fileNames[i])) != true)
                    continue;

                string fileName = Path.GetFileNameWithoutExtension(fileNames[i]);
                images.Add(fileName, new Image(fileNames[i]));
            }
        }

        private static bool IsImage(string fileExtension)
        {
            if (fileExtension == ".png")
                return true;
            else
                return false;
        }
    }
}
