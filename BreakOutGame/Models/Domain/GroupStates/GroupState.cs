using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain.GroupStates
{
    public abstract class GroupState
    {
        public BoBGroup Group { get; set; }

        protected GroupState(BoBGroup group)
        {
            Group = group;
        }

        public virtual void Select() => throw new InvalidOperationException();
        public virtual void Deselect() => throw new InvalidOperationException();
        public virtual void Lock(Boolean force) => throw new InvalidOperationException();
        public virtual void Block() => throw new InvalidOperationException();
        public virtual void Deblock() => throw new InvalidOperationException();
        public virtual Boolean ValidateAnswer(Assignment assignment, String answer, Boolean actionsEnabled, Boolean blockingEnabled) => throw new InvalidOperationException();
        public virtual Boolean ValidateCode(Assignment assignment, int code) => throw new InvalidOperationException();


    }
}
