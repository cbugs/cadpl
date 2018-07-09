using Cadbury.Inventor.Core.DTO;
using Cadbury.Inventor.Core.Mediator.Models;
using Cadbury.Inventor.Core.Module;
using Cadbury.Inventor.Core.Utils;
using System;
using System.Net;

namespace Cadbury.Inventor.Core.Mediator
{
    public class TextCheckMediator : MediatorBase<TextCheckMediatorModel>, IMediator
    {
        public TextCheckMediator(IMediatorModel mediatorModel)
            : base(mediatorModel)
        {
        }

        public ResultDTO Process()
        {
            // Missing word
            if (String.IsNullOrEmpty(MediatorModel.Text))
            {
                Result.Code = CodeKeys.MISSING_BARNAME;
                return Result;
            }

            // Bad words check
            var badWordsCheckModule = new BadWordsCheckModule()
            {
                Text = MediatorModel.Text,
                WordTree = MediatorModel.WordTree
            };
            ResultDTO badWordsCheckResult = badWordsCheckModule.Process();
            if (badWordsCheckResult.HttpStatusCode != HttpStatusCode.OK)
            {
                return badWordsCheckResult;
            }

            // Illegal characters detection
            var invalidCharacterCheckModule = new InvalidCharacterCheckModule()
            {
                Text = MediatorModel.Text
            };
            ResultDTO invalidCharacterCheckResult = invalidCharacterCheckModule.Process();
            if (invalidCharacterCheckResult.HttpStatusCode != HttpStatusCode.OK)
            {
                return invalidCharacterCheckResult;
            }

            // Text length
            if (MediatorModel.Text.Length > 22)
            {
                Result.Code = CodeKeys.EXCEEDED_BARNAME;
                return Result;
            }

            // URL detection
            if (StringUtils.ContainsURL(MediatorModel.Text))
            {
                Result.Code = CodeKeys.URLS_DETECTED;
                return Result;
            }

            Result.HttpStatusCode = HttpStatusCode.NoContent;

            return Result;
        }
    }
}
