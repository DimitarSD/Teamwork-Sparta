namespace TexasHoldem.Tests.GameSimulations.GameSimulators
{
    using TexasHoldem.AI.DummyPlayer;
    using AI.SpartaPlayer;
    using TexasHoldem.Logic.Players;
    using AI.SmartPlayer;

    /// <summary>
    /// For performance profiling
    /// </summary>
    public class SpartaVsSmartSimulation : BaseGameSimulator
    {
        private readonly IPlayer firstPlayer = new SpartaPlayer();
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
