using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BreakOutGame.Models.Domain.SessionStates;
using Newtonsoft.Json;

namespace BreakOutGame.Models.Domain
{
    public class BoBSession
    {
        
        public int Id { get; set; }
        public String name { get; set; }
        public IEnumerable<BoBGroup> Groups { get; set; }
        public int BoxId { get; set; }
        [JsonIgnore]
        public IEnumerable<SessionAction> Actions { get; set; }

        public Boolean AreActionsEnabled { get; set; }
        public Boolean IsFreeJoinEnabled { get; set; }
        public Boolean IsFeedbackEnabled { get; set; }
        [JsonIgnore]
        public StudentClass StudentClass { get; set; }
        public Boolean IsDistant { get; set; }
        public SessionStatus SessionStatus { get; set; }

        private SessionState _sessionState;

        [JsonIgnore]
        public SessionState SessionState {
            get
            {
                if (_sessionState == null)
                    _sessionState = SessionStateFactory.CreateState(this, SessionStatus);
                return _sessionState;
            }
            set => _sessionState = value;
        }

        public void Activate()
        {
            SessionState.Activate();   
        }
        public void Start()
        {
            SessionState.Start();
        }
        public void Lock()
        {
            SessionState.Lock();
        }
        public void Unlock()
        {
            SessionState.Unlock();
        }

        public void SelectGroup(BoBGroup group)
        {
            SessionState.SelectGroup(group);
        }

        public void DeselectGroup(BoBGroup group)
        {
            SessionState.DeselectGroup(group);
        }

        public Boolean ValidateAnswer(BoBGroup group, Assignment assignment, String answer)
        {
            return SessionState.ValidateAnswer(group, assignment, answer);
        }

        public Boolean ValidateCode(BoBGroup group, Assignment assignment, int code)
        {
            return SessionState.ValidateCode(group, assignment, code);
        }
    }
}
