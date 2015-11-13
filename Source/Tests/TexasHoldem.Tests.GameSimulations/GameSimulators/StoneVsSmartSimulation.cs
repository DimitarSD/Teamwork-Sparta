using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.AI.SmartPlayer;
using TexasHoldem.AI.SpartaPlayer;
using TexasHoldem.Logic.Players;

namespace TexasHoldem.Tests.GameSimulations.GameSimulators
{
    public class StoneVsSmartSimulation : BaseGameSimulator
    {
        private readonly IPlayer firstPlayer = new StonePlayer();
        private readonly IPlayer secondPlayer = new SmartPlayer();

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
