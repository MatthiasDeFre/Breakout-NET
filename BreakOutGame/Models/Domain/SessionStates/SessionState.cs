using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.SessionStates
{
    public abstract class SessionState
    {
        protected BoBSession Session { get; set; }

        private String standardError = "Deze actie is momenteel niet geldig";

        protected SessionState(BoBSession session)
        {
            Session = session;
        }

        public virtual void Activate() => throw new InvalidOperationException(standardError);
        public virtual void Start() => throw new InvalidOperationException(standardError);
        public virtual void Lock() => throw new InvalidOperationException(standardError);
        public virtual void Unlock() => throw new InvalidOperationException(standardError);
        public virtual void SelectGroup(BoBGroup group) => throw new InvalidOperationException("De sessie is momenteel niet actief");
        public virtual void DeselectGroup(BoBGroup group) => throw new InvalidOperationException("De sessie is momenteel bezig dus kan de groep niet gewijzigd worden");
        public virtual Boolean ValidateAnswer(BoBGroup group, Assignment assignment, String answer) => throw new InvalidOperationException();
        public virtual Boolean ValidateCode(BoBGroup group, Assignment assignment, int code) => throw new InvalidOperationException();
    }
}
