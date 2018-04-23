using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain.GroupStates;

namespace BreakOutGame.Models.Domain
{
    public class BoBGroup
    {
        public int Id { get; set; }
        public String GroupName { get; set; }
        public IEnumerable<GroupStudent> Students { get; set; }

        public int PathId { get; set; }
      

        public GroupStatus Status { get; set; }


        private GroupState _groupState;
        public GroupState GroupState
        {
            get
            {
                if (_groupState == null)
                    _groupState = GroupStateFactory.CreateState(this, Status);
                return _groupState;
            }
            set => _groupState = value;
        }

        public BoBGroup()
        {
            Status = GroupStatus.NotSelected;
        }
        public void Select()
        {
            GroupState.Select();
        }

        public void Deselect()
        {
            GroupState.Deselect();
        }

        public void Lock(Boolean force)
        {
            GroupState.Lock(force);
        } 
        public void Block()
        {
            GroupState.Block();
        }

        public void Deblock()
        {
            GroupState.Deblock();
        }
    }
}
