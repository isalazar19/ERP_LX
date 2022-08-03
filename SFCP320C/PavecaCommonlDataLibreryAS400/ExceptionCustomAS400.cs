using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PavecaCommonlDataLibreryAS400
{
    #region Enums
    [Serializable]
    public enum ExceptionLevel
    {
        Notification,
        Critical
    }

    [Serializable]
    public enum ExceptionType
    {
        DomainIntegrity,//Notification level. Errors in data format and/or domain integrity
        Business,//Notification level. Errors created in the BLL layer (eg authenticated user can not delete record)
        Technical,//Critical level. Mainly database errors
        Undetermined //Critical level. Defaul Type 
    }
    #endregion

    #region ExceptionCustom
    [Serializable]
    public class ExceptionCustomAS400 : Exception
    {
        public ExceptionLevel Level { get; set; }
        public ExceptionType Type { get; set; }
        public int Number { get; set; }
        new public string Message { get; set; }
        public Exception InnerException { get; set; }

        public ExceptionCustomAS400(ExceptionLevel exceptionLevel, ExceptionType exceptionType, string message)
        {
            Message = message;
            Level = exceptionLevel;
            Type = exceptionType;
        }

        public ExceptionCustomAS400(ExceptionLevel exceptionLevel, ExceptionType exceptionType, string message, int number)
        {
            Message = message;
            Level = exceptionLevel;
            Type = exceptionType;
            Number = number;
        }

        public ExceptionCustomAS400(ExceptionLevel exceptionLevel, ExceptionType exceptionType, string message, int number, Exception innerException)
        {
            Message = message;
            Level = exceptionLevel;
            Type = exceptionType;
            Number = number;
            InnerException = innerException;
        }
    }
    #endregion

    #region ExceptionCustomList
    public class ExceptionCustomList : Exception
    {
        public string ObjectWihtExceptionName { get; private set; }
        public ObjectBaseAS400 ObjectWihtException { get; set; }

        public CommonList<ExceptionCustomAS400> Items { get; private set; }

        public ExceptionCustomList(string objectWihtExceptionName)
        {
            ObjectWihtExceptionName = objectWihtExceptionName;
        }

        public ExceptionCustomList(string objectWihtExceptionName, CommonList<ExceptionCustomAS400> items)
        {
            ObjectWihtExceptionName = objectWihtExceptionName;
            Items = items;
        }

        public void Clear()
        {
            Items.Clear();
        }

        public void Add(ExceptionCustomAS400 value)
        {
            Items.Add(value);
        }
    }
    #endregion
}
