namespace TexasHoldem.AI.Sparta.Helpers.Contracts
{
    using Logic.Cards;
    using Logic.Players;
    using System.Collections.Generic;
    using TexasHoldem.AI.Sparta.Helpers.ActionProviders;

    public interface IActionProviderFactory
    {
        ActionProvider GetActionProvider(GetTurnContext context, Card first, Card second, IReadOnlyCollection<Card> community);
    }
}
