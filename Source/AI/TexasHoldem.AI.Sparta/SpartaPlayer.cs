/// <summary>
/// Sparta player entry point
/// Receives action provided by the object factory
/// </summary>
namespace TexasHoldem.AI.Sparta
{
    using System;

    using Helpers.Contracts;
    using Helpers.Factories;
    using Logic.Players;

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
