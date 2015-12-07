namespace TexasHoldem.AI.Sparta
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TexasHoldem.Logic.Players;
    using TexasHoldem.AI.Sparta.Helpers.Factories;
    using TexasHoldem.AI.Sparta.Helpers.Contracts;

    public class SpartaPlayer : BasePlayer
    {
        private readonly IActionProviderFactory actionProviderFactory;
        private bool isFirts;

        public SpartaPlayer(IActionProviderFactory actionProviderFactory)
        {
            this.actionProviderFactory = actionProviderFactory;
        }

        public SpartaPlayer()
            : this(new ActionProviderFactory())
        {
        }

        public override string Name { get; } = "Sparta" + Guid.NewGuid();

        public override PlayerAction GetTurn(GetTurnContext context)
        {
            var actionProvider = this.actionProviderFactory.GetActionProvider(context, this.FirstCard, this.SecondCard, this.CommunityCards);

            return actionProvider.GetAction();
        }
    }
}
