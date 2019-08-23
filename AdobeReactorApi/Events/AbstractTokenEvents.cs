using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AdobeLoginBase;

namespace AdobeReactorApi.Events
{
    public abstract class AbstractTokenEvents
    {
        public event Func<Task, Task<AccessToken>> NewTokenEvent;
        public event Func<Task, Task<AccessToken>> NullTokenEvent;
        public event Func<Task, Task<AccessToken>> ExpiredTokenEvent;

        protected virtual Task<AccessToken> OnNewTokenEvent(Task arg)
        {
            return NewTokenEvent?.Invoke(arg);
        }
        protected virtual Task<AccessToken> OnNullTokenEvent(Task arg)
        {
            return NullTokenEvent?.Invoke(arg);
        }
        protected virtual Task<AccessToken> OnExpiredTokenEvent(Task arg)
        {
            return ExpiredTokenEvent?.Invoke(arg);
        }
    }
}
