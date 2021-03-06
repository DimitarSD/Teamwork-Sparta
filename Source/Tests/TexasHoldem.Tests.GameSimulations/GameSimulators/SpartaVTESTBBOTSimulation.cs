﻿namespace TexasHoldem.Tests.GameSimulations.GameSimulators
{
    using TexasHoldem.AI.SpartaPlayer;
    using TexasHoldem.Logic.Players;
    using TexasHoldem.AI.Sparta;

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
