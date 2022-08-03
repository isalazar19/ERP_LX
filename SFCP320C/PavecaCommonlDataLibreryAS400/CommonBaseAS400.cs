using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PavecaCommonlDataLibreryAS400
{

    [Serializable]
    public abstract class CommonBaseAS400 : ObjectBaseAS400
    {
        protected void AddExceptionAS400(CommonList<ExceptionCustomAS400> exceptionList, Exception exception, ExceptionType exceptionType)
        {
            ExceptionCustomAS400 m_exceptionAS400 = new ExceptionCustomAS400(ExceptionLevel.Critical, exceptionType, exception.Message);
            exceptionList.Add(m_exceptionAS400);
        }

        protected void AddExceptionAS400(CommonList<ExceptionCustomAS400> exceptionList, string exceptionMessage, ExceptionLevel exceptionLevel, ExceptionType exceptionType)
        {
            ExceptionCustomAS400 m_exceptionAS400 = new ExceptionCustomAS400(exceptionLevel, exceptionType, exceptionMessage, -1);
            exceptionList.Add(m_exceptionAS400);
        }

        protected void AddExceptionAS400(CommonList<ExceptionCustomAS400> exceptionList, Exception exception, ExceptionLevel exceptionLevel, ExceptionType exceptionType)
        {
            int m_number = 0;
            if (exception.GetType().FullName == "System.Data.Common.DbException")
            {
                m_number = ((System.Data.Common.DbException)exception).ErrorCode;
            }

            ExceptionCustomAS400 m_exception = new ExceptionCustomAS400(exceptionLevel, exceptionType, exception.Message, m_number);
            exceptionList.Add(m_exception);
        }
    }
}
