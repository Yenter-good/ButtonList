using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ButtonList.Helper
{
    internal static class IconHelper
    {
        public static string ResourcePath { get; set; }
        private static List<IconEntry> _iconEntries = new List<IconEntry>();

        private static List<IconEntry> GetAllIcons()
        {
            if (_iconEntries.Any())
                return _iconEntries;

            using (FileStream fs = new FileStream(ResourcePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Read))
            {
                foreach (var entry in archive.Entries.Where(p => p.ExternalAttributes == 32))
                {
                    if (entry != null)
                    {
                        using (Stream stream = entry.Open())
                        {
                            _iconEntries.Add(new IconEntry()
                            {
                                Name = entry.FullName,
                                Icon = Image.FromStream(entry.Open()) as Bitmap
                            });
                        }
                    }
                }
            }

            return _iconEntries;
        }

        public static List<IconEntry> GetIcons()
        {
            return GetAllIcons().Where(p => p.Name.Contains("Icons")).ToList();
        }

        public static IconEntry GetIcon(string name)
        {
            return GetAllIcons().FirstOrDefault(p => p.Name == name);
        }

        public static IconEntry GetNormalButton()
        {
            return GetAllIcons().FirstOrDefault(p => p.Name.Contains("normal"));
        }

        public static IconEntry GetPressButton()
        {
            return GetAllIcons().FirstOrDefault(p => p.Name.Contains("press"));
        }
    }

    internal class IconEntry
    {
        public string Name { get; set; }
        public Bitmap Icon { get; set; }
    }
}
