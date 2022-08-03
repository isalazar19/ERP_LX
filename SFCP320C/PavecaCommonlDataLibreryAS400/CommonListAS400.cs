using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PavecaCommonlDataLibreryAS400
{
    [Serializable]
    public class CommonList<T> : List<T>, ICollection<T>
    {
        public event EventHandler<ChangeStateEventArgsAS400> RaiseChangeStateEvent;

        protected void HandleChangeStateEvent(object sender, ChangeStateEventArgsAS400 e)
        {
            OnRaiseChangeStateEvent(e);
        }

        protected void OnRaiseChangeStateEvent(ChangeStateEventArgsAS400 e)
        {
            EventHandler<ChangeStateEventArgsAS400> handler = RaiseChangeStateEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public string ConvertToXML()
        {
            return EntityHelperAS400.Serialize(this, this.GetType());
        }
    }
}
