namespace TexasHoldem.AI.Sparta.Helpers.Contracts
{
    using System.Collections.Generic;

    using ActionProviders;
    using Logic.Cards;
    using Logic.Players;

    /// <summary>
    /// ActionProviderFactory interface to provide method for returning logic based method
    /// </summary>
    public interface IActionProviderFactory
    {
        ActionProvider GetActionProvider(GetTurnContext context, Card first, Card second, IReadOnlyCollection<Card> community);
    }
}
