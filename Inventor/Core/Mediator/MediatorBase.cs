using Cadbury.Inventor.Core.DTO;
using Cadbury.Inventor.Core.Mediator.Models;
using System;
using System.Net;

namespace Cadbury.Inventor.Core.Mediator
{
    public class MediatorBase<T>
    {
        public T MediatorModel { get; set; }
        public ResultDTO Result { get; set; }

        public MediatorBase(IMediatorModel mediatorModel)
        {
            try
            {
                MediatorModel = (T)Convert.ChangeType(mediatorModel, typeof(T));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Result = new ResultDTO()
            {
                HttpStatusCode = HttpStatusCode.BadRequest
            };
        }
    }
}
