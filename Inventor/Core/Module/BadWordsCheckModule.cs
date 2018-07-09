using Cadbury.Inventor.Core.DTO;
using Cadbury.Inventor.Core.WordTree;
using System.Net;

namespace Cadbury.Inventor.Core.Module
{
    public class BadWordsCheckModule : ModuleBase, IModule
    {
        public string Text { get; set; }
        public WordTree.WordTree WordTree { get; set; }

        public ResultDTO Process()
        {
            Detector detector = new Detector(WordTree);
            string barName = Text.ToLower();
            barName = Charset.Clean(barName);
            if (!detector.IsValid(barName))
            {
                Result.Code = CodeKeys.BAD_WORDS_DETECTED;
                return Result;
            }

            Result.HttpStatusCode = HttpStatusCode.OK;

            return Result;
        }
    }
}
