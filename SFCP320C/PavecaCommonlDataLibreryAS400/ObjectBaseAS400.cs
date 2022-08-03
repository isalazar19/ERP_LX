using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PavecaCommonlDataLibreryAS400
{
    [Serializable]
    public abstract class ObjectBaseAS400
    {
        public string GUID { get; set; }


        public ObjectBaseAS400()
        {
            GUID = Guid.NewGuid().ToString();
        }

        public string ConvertToXML()
        {
            return EntityHelperAS400.Serialize(this, this.GetType());
        }
    }
}
