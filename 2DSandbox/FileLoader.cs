using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topgram;

namespace _2DSandbox
{
    public class FileLoader
    {
        public static List<RotatedPiece> LoadPieces(string path)
        {
            var str = File.ReadAllText(path);
            var pieces = JsonConvert.DeserializeObject<List<RotatedPiece>>(str);
            return pieces;
        }

        public static History LoadHistory(string path)
        {
            var str = File.ReadAllText(path);
            var history = JsonConvert.DeserializeObject<History>(str);
            return history;
        }
    }
}
