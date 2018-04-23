using System;
using System.Collections.Generic;
using System.Text;
using BreakOutGame.Models.Domain;
using BreakOutGame.Models.Domain.GroupStates;
using Xunit;

namespace BreakOutGameTest.Domain
{
    public class GroupTest
    {
        private BoBGroup _group;

        public GroupTest()
        {
            _group = new BoBGroup();
        }
        [Fact]
        public void SelectGroup()
        {
            _group.Select();
            Assert.Equal(GroupStatus.Selected, _group.Status);
            Assert.Equal(typeof(SelectedState), _group.GroupState.GetType());
        }

        [Fact]
        public void DeselectGroup()
        {
            _group.GroupState = new SelectedState(_group);
            _group.Deselect();
            Assert.Equal(GroupStatus.NotSelected, _group.Status);
            Assert.Equal(typeof(NotSelectedState), _group.GroupState.GetType());
        }

        [Fact]
        public void LockGroup_NoForce_Selected()
        {
            _group.GroupState = new SelectedState(_group);
            _group.Lock(false);
            Assert.Equal(GroupStatus.Locked, _group.Status);
            Assert.Equal(typeof(LockedState), _group.GroupState.GetType());
        }

        [Fact]
        public void BlockGroup()
        {
            _group.GroupState = new LockedState(_group);
            _group.Block();
            Assert.Equal(GroupStatus.Blocked, _group.Status);
            Assert.Equal(typeof(BlockedState), _group.GroupState.GetType());
        }

        [Fact]
        public void LockGroup_Force_NotSelected()
        {
            _group.GroupState =  new NotSelectedState(_group);
            _group.Lock(true);
            Assert.Equal(GroupStatus.Locked, _group.Status);
            Assert.Equal(typeof(LockedState), _group.GroupState.GetType());
        }

        
    }
}
