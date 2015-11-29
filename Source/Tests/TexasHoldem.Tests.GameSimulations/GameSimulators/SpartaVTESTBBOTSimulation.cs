using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.AI.SpartaPlayer;
using TexasHoldem.Logic.Players;

namespace TexasHoldem.Tests.GameSimulations.GameSimulators
{
    public class SpartaVTESTBBOTSimulation : BaseGameSimulator
    {
        private readonly IPlayer firstPlayer = new SpartaPlayer();
        private readonly IPlayer secondPlayer = new TestBot();

        protected override IPlayer GetFirstPlayer()
        {
            return this.firstPlayer;
        }

        protected override IPlayer GetSecondPlayer()
        {
            return this.secondPlayer;
        }
    }
}
