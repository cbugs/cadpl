using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadbury.Inventor.Core.ResponseObj
{
    public class ResponseObject
    {
        public string code { get; set; }
        public string detail { get; set; }
        public string message { get; set; }
        public string[] meta { get; set; }
    }

    public static class Responses
    {
        public static ResponseObject BadAge
        {
            get
            {
                ResponseObject response = new ResponseObject();
                response.code = "BAD_AGE";
                response.detail = "The minimum participation age is 16.";
                response.message = "Sorry but you need to be 16 years and over to participate.";
                return response;
            }
        }

        public static ResponseObject BadWord
        {
            get
            {
                ResponseObject response = new ResponseObject();
                response.code = "BAD_WORD";
                response.detail = "Bad word(s) detected.";
                response.message = "The text that you entered contains forbidden words.";
                return response;
            }
        }

        public static ResponseObject IllegalCharacter
        {
            get
            {
                ResponseObject response = new ResponseObject();
                response.code = "ILLEGAL_CHAR";
                response.detail = "Illegal characters were detected.";
                response.message = "The text that you entered contains illegal characters.";
                return response;
            }
        }

    }
}
