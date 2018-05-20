using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreakOutGame.Models.Domain
{
    public class SessionProgress
    {
        /// <summary>
        /// <param name="_current">Current assingment pointer</param>
        /// </summary>
        private int _current;

        /// <summary>
        /// <param name="_total">Total assignments in a session</param>
        /// </summary>
        private int _total;

        public SessionProgress(int total, int current)
        {
            if (current > total)
            {
                throw new ArgumentException("Voortgang mag niet groter zijn dan het totaal van de vragen");
            }
            this._total = total;
            this._current = current;
        }
        public double getSessionProgress()
        {
            return 100 * ((double)_current / _total);
        }
        public int Current { get { return this._current; } }

        public int Total => this._total;
    }
}
