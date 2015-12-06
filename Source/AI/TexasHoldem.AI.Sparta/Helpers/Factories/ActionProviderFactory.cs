namespace TexasHoldem.AI.Sparta.Helpers.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Contracts;
    using Logic.Players;
    using ActionProviders;
    using Logic;
    using Logic.Cards;

    internal class ActionProviderFactory : IActionProviderFactory
    {
        //public GetTurnContext Context { get; set; }

        public ActionProvider GetActionProvider(GetTurnContext context, Card first, Card second, IReadOnlyCollection<Card> community)
        {
            if (context == null)
            {
                throw new NullReferenceException("Context must be set.");
            }

            if (context.RoundType == GameRoundType.PreFlop)
            {
                if (context.MoneyLeft < 400)
                {
                    if (context.MoneyLeft / context.SmallBlind <= 15)
                    {
                        return new SuperAggressivePreFlopActionProvider(context, first, second);
                    }
                    else if (context.MoneyLeft / context.SmallBlind > 15 && context.MoneyLeft / context.SmallBlind <= 50)
                    {
                        return new SuperAggressivePreFlopActionProvider(context, first, second);
                    }
                    else
                    {
                        // ontext.MoneyLeft / context.SmallBlind > 50
                        return new SuperAggressivePreFlopActionProvider(context, first, second);
                    }

                }

                return new AggressivePreFlopActionProvider(context, first, second);
            }
            else if (context.RoundType == GameRoundType.Flop)
            {
                return new AggressivePreFlopActionProvider(context, first, second);
            }
            else if (context.RoundType == GameRoundType.Turn)
            {
                return new AggressivePreFlopActionProvider(context, first, second);
            }
            else
            {
                // RIVER (final state)
                return new AggressivePreFlopActionProvider(context, first, second);
            }
        }
    }
}
