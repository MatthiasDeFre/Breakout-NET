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
        public BoBSession ValidSession { get; }
        public BoBSession InvalidSession1 { get; }
        public BoBSession InvalidSession2 { get; }

        public Assignment Assignment1 { get; }
        public Assignment LazyAssignment { get; }
        public BoBGroup SelectedGroup { get; }
        public DummyApplicationDbContext()
        {
            //Invullen
            ValidSession = new BoBSession();
            InvalidSession1 = new BoBSession();
            InvalidSession2 = new BoBSession();
            ValidSession.SessionStatus = SessionStatus.Activated;
            InvalidSession1.SessionStatus = SessionStatus.Scheduled;
            InvalidSession2.SessionStatus = SessionStatus.Closed;

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
            SelectedGroup.GroupState = new SelectedState(SelectedGroup);
        }
    }
}
