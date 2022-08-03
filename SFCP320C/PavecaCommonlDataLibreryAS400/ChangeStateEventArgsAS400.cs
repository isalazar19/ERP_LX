using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PavecaCommonlDataLibreryAS400
{
    [Serializable]
    public class ChangeStateEventArgsAS400 : EventArgs
    {
        public string Property;

        public ChangeStateEventArgsAS400(string property)
        {
            Property = property;
        }
    }
}
