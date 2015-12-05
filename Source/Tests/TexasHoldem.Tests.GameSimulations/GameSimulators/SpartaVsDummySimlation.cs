namespace TexasHoldem.Tests.GameSimulations.GameSimulators
{
    using TexasHoldem.AI.DummyPlayer;
    using AI.SpartaPlayer;
    using TexasHoldem.Logic.Players;

    /// <summary>
    /// For performance profiling
    /// </summary>
    public class SpartaVsDummySimlation : BaseGameSimulator
    {
        private readonly IPlayer firstPlayer = new SpartaPlayerBeta();
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
