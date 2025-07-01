using TagLib;
using System.IO;

namespace MusicOrganizer
{
    public class MusicOrganizer
    {
        public static string Adress()
        {
            string adress = new string("");
            Console.WriteLine("Enter Adress Folder: ");
            bool check = true;
            while (check)
            {
                adress = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(adress) || !Directory.Exists(adress))
                {
                    Console.WriteLine("Pleas Enter Adress Folder: ");
                }
                else
                {
                    check = false;
                }
            }
            return adress;
        }

        public static string CleanPath(string input)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                input = input.Replace(c, '_');
            }
            return input;
        }


        static void Main(string[] args)
        {
            Console.ResetColor();
            string adress = Adress();
            string[] files = Directory.GetFiles(adress, "*.mp3", SearchOption.AllDirectories);
            foreach (string file2 in files)
            {
                var file = TagLib.File.Create(file2);

                string artist = string.IsNullOrWhiteSpace(file.Tag.FirstPerformer) ? "Unknown Artist" : CleanPath(file.Tag.FirstPerformer);
                string album = string.IsNullOrWhiteSpace(file.Tag.Album) ? "Unknown Album" : CleanPath(file.Tag.Album);

                string artistFolder = Path.Combine(adress,artist);
                string albumFolder = Path.Combine(artistFolder,album);

                if(!Directory.Exists(albumFolder))
                {
                    Directory.CreateDirectory(albumFolder);
                }

                string newFolder = Path.Combine(albumFolder, Path.GetFileName(file2));

                if (!System.IO.File.Exists(newFolder))
                {
                    System.IO.File.Move(file2, newFolder, true);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("File [{0}] Moved success", Path.GetFileName(file2));
                }
                else
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("File [{0}] Exists", Path.GetFileName(file2));
                }
                Console.ResetColor();
            }
        }
    }
}