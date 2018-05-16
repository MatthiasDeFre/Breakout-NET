using System;
using System.Collections.Generic;
using System.Text;
using BreakOutGame.Models.Domain;
using BreakOutGame.Models.Domain.GroupOperations;
using BreakOutGame.Models.Domain.GroupStates;
using BreakOutGameTest.Data;
using Xunit;

namespace BreakOutGameTest.Domain
{
    public class AssignmentTest
    {

        private readonly DummyApplicationDbContext _dummyContext;
        public AssignmentTest()
        {
            _dummyContext = new DummyApplicationDbContext();
        }


        [Fact]
        public void CorrectAnswer_ReturnsTrue()
        {
            Assignment assignment = _dummyContext.Assignment1;
            Assert.True(assignment.ValidateAnswer("5", true));
            Assert.Equal(0, assignment.WrongCount);
        }

        

        [Fact]
        public void LazyLoading_AnswerBehaviour()
        {
            Assignment assignment = _dummyContext.LazyAssignment;
            Assert.True(assignment.ValidateAnswer("5", true));
            Assert.Equal(0, assignment.WrongCount);
            Assert.NotNull(assignment.GroupOperation.AnswerBehaviour);
        }

        [Fact]
        public void TripleWrong_BlocksGroup()
        {
            BoBGroup group = _dummyContext.SelectedGroup;
            Assignment assignment = _dummyContext.Assignment1;
            
            for (int i = 1; i <= 3; i++)
            {
                Assert.False(group.ValidateAnswer(assignment,"nope",true, true));
                Assert.Equal(i, assignment.WrongCount);
            }
            Assert.Equal(GroupStatus.Blocked, group.Status);
            Assert.Equal(typeof(BlockedState), group.GroupState.GetType());
            Assert.Throws<InvalidOperationException>(() => group.ValidateAnswer(assignment, "5", true, true));
        }
    }
}
