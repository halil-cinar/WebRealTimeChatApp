using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebChatApp.Dto.Error
{
    public enum ErrorMessageCode
    {
        Unknown = 0,
        ChatAddChatValidationError = 1,
        ChatAddChatExceptionError = 2,
        ChatUpdateChatValidationError = 3,
        ChatUpdateChatExceptionError = 4,
        ChatDeleteChatExceptionError = 5,
        ChatFilterChatListError = 6,
        ChatFilterChatListExceptionError = 7,
        ChatGetChatNotFoundExceptionError = 8,
        ChatGetChatExceptionError = 9,
        ChatFilterChatExceptionError = 10,
        MessageAddMessageValidationError = 11,
        MessageAddMessageExceptionError = 12,
        MessageUpdateMessageValidationError = 13,
        MessageUpdateMessageExceptionError = 14,
        MessageDeleteMessageExceptionError = 15,
        MessageFilterMessageListError = 16,
        MessageFilterMessageListExceptionError = 17,
        MessageGetMessageNotFoundExceptionError = 18,
        MessageGetMessageExceptionError = 19,
    }
}
