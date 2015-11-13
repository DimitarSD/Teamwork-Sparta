using TexasHoldem.AI.DummyPlayer;
using TexasHoldem.AI.SpartaPlayer;
using TexasHoldem.Logic.Players;

namespace TexasHoldem.Tests.GameSimulations.GameSimulators
{
    public class StoneVsDummySimulation : BaseGameSimulator
    {
        private readonly IPlayer firstPlayer = new StonePlayer();
        private readonly IPlayer secondPlayer = new DummyPlayer();

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
