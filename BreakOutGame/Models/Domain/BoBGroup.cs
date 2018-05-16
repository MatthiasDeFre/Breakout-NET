using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain.GroupStates;
using Newtonsoft.Json;

namespace BreakOutGame.Models.Domain
{
    public class BoBGroup
    {
        public int Id { get; set; }
        public String GroupName { get; set; }
        [JsonIgnore]
        public IList<GroupStudent> Students { get; set; }


        public GroupStatus Status { get; set; }


        private GroupState _groupState;
        [JsonIgnore]
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

        public SessionPath Path { get; set; }

        public Assignment NextAssignment
        {
            get { return Path.Assignments.OrderBy(a => a.ReferenceNr).FirstOrDefault(a => a.Status != AssignmentStatus.Completed); }
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

        public bool ValidateAnswer(Assignment assignment, String answer, Boolean areActionsEnabled, Boolean blockingEnabled)
        {
            return GroupState.ValidateAnswer(assignment, answer, areActionsEnabled, blockingEnabled);
        }

        public bool ValidateCode(Assignment assignment, int code)
        {
            return GroupState.ValidateCode(assignment, code);

        }

        public void AddStudent(Student student)
        {
            Students.Add(new GroupStudent(){BoBGroupId = Id,Group = this, Student = student, StudentId = student.Id});
        }
    }
}
