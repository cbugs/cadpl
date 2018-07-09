using Cadbury.Inventor.Core.DTO;
using System.Net;
using System.Text.RegularExpressions;

namespace Cadbury.Inventor.Core.Module
{
    public class InvalidCharacterCheckModule : ModuleBase, IModule
    {
        public string Text { get; set; }

        public ResultDTO Process()
        {
            // Allowed characters are :  -_/áÁéÉíÍóÓúÚabcdefghijklmnopqrstuvwxyz0123456789?!@#$%^&*()[];,.+=’':{}\\\"íóá
            // Chars to escape : [ ] \ "
            // Dash "-" to put in the end of the string

            Regex regex = new Regex(
                @"^[áÁéÉíÍóÓúÚabcdefghijklmnopqrstuvwxyz0123456789\s_/?!@#$%^&*()\[\];,.+=’':{}\\""-]*$", 
                RegexOptions.IgnoreCase
            );
            if (!regex.IsMatch(Text.ToLower()))
            {
                Result.Code = CodeKeys.INVALID_BARNAME;
                return Result;
            }

            Result.HttpStatusCode = HttpStatusCode.OK;

            return Result;
        }
    }
}
