using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatApp.Dto.Error;

namespace WebChatApp.Dto.Result
{
    public class BusinessLayerResult<T>
    {
        public T Result { get; set; }
        public ResultStatus Status
        {
            get
            {
                if(ErrorMessages==null||ErrorMessages.Count==0)
                {
                    return ResultStatus.Success;
                }
                return ResultStatus.Error;
            }
        }

        public List<ErrorMessageObj> ErrorMessages
        {get; private set;          
        }
        public BusinessLayerResult()
        {
           
                this.ErrorMessages = new List<ErrorMessageObj>();
            
        }
       
        public void AddErrorMessages(ErrorMessageCode code,string message)
        {
            ErrorMessages.Add(new ErrorMessageObj { Code=code,Message=message});
        }


    }
}
