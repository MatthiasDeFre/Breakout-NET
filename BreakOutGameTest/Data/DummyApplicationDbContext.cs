using BreakOutGame.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakOutGameTest.Data
{
    class DummyApplicationDbContext
    {
        public BoBSession ValidSession { get; }
        public BoBSession InvalidSession1 { get; }
        public BoBSession InvalidSession2 { get; }
        public DummyApplicationDbContext()
        {
            //Invullen
            ValidSession = new BoBSession();
            InvalidSession1 = new BoBSession();
            InvalidSession2 = new BoBSession();
            ValidSession.SessionStatus = SessionStatus.ACTIVATED;
            InvalidSession1.SessionStatus = SessionStatus.SCHEDULED;
            InvalidSession2.SessionStatus = SessionStatus.CLOSED;

        }
    }
}
