using BreakOutGame.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using BreakOutGame.Models.Domain.GroupOperations;
using BreakOutGame.Models.Domain.GroupStates;

namespace BreakOutGameTest.Data
{
    class DummyApplicationDbContext
    {
        private BoBSession[] _sessions;

        public BoBSession ValidSession { get { return _sessions[0]; } }
        public BoBSession ActiveSession { get { return _sessions[1]; } }

        public BoBSession ClosedSession { get { return _sessions[2]; } }
        public BoBSession StartedSession { get { return _sessions[3]; } }

        public Assignment Assignment1 { get; }
        public Assignment LazyAssignment { get; }
        public BoBGroup SelectedGroup { get; }
        public DummyApplicationDbContext()
        {
            //Invullen
            this._sessions = new BoBSession[4];
            _sessions[0] = new BoBSession();
            //invalid
            _sessions[1] = new BoBSession();
            _sessions[2] = new BoBSession();
            _sessions[3] = new BoBSession();

            _sessions[0].SessionStatus = SessionStatus.Scheduled;
            _sessions[1].SessionStatus = SessionStatus.Activated;
            _sessions[2].SessionStatus = SessionStatus.Closed;
            _sessions[3].SessionStatus = SessionStatus.Started;

            Assignment1 = new Assignment
            {
                Exercise = new Exercise()
                {
                   Answer = "10"
                },
                GroupOperation = new GroupOperation()
                {
                    AnswerBehaviour = new MinBehaviour(),
                    ValueString = "5"
                }
            };

            LazyAssignment = new Assignment
            {
                Exercise = new Exercise()
                {
                    Answer = "10"
                },
                GroupOperation = new GroupOperation()
                {
                    GroupOperationCategory = GroupOperationCategory.Min,
                    ValueString = "5"
                }
            };
            SelectedGroup = new BoBGroup
            {
                Path = new SessionPath
                {
                    Assignments = new List<Assignment>() { Assignment1}
                },
             

            };
            SelectedGroup.GroupState = new LockedState(SelectedGroup);
        }
    }
}
