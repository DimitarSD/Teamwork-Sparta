namespace TexasHoldem.AI.Sparta.Helpers.Factories
{
    using System;
    using System.Collections.Generic;
    using ActionProviders;
    using ActionProviders.FlopProviders;
    using Contracts;
    using Logic;
    using Logic.Cards;
    using Logic.Players;

    internal class ActionProviderFactory : IActionProviderFactory
    {
        private bool isFirst;
        private GetTurnContext context;

        public ActionProvider GetActionProvider(GetTurnContext currentContext, Card first, Card second, IReadOnlyCollection<Card> community)
        {
            this.context = currentContext;

            if (this.context == null)
            {
                throw new NullReferenceException("Context must be set.");
            }

            if (this.context.RoundType == GameRoundType.PreFlop)
            {
                if (this.context.MyMoneyInTheRound == this.context.SmallBlind
                    && this.context.MoneyToCall == this.context.SmallBlind
                    && !this.context.CanCheck)
                {
                    this.isFirst = true;
                }
                else if (this.context.MyMoneyInTheRound == this.context.SmallBlind * 2 &&
                        this.context.PreviousRoundActions.Count == 3)
                {
                    this.isFirst = false;
                }

                if (this.context.MoneyLeft < 200)
                {
                    if (this.context.MoneyLeft / this.context.SmallBlind <= 15)
                    {
                        return new SuperAggressivePreFlopActionProvider(this.context, first, second, this.isFirst);
                    }
                    else if (this.context.MoneyLeft / this.context.SmallBlind > 15 && this.context.MoneyLeft / this.context.SmallBlind <= 50)
                    {
                        return new AggressivePreFlopActionProvider(this.context, first, second, this.isFirst);
                    }
                    else
                    {
                        // ontext.MoneyLeft / context.SmallBlind > 50
                        return new PassiveAggressivePreFlopActionProvider(this.context, first, second, this.isFirst);
                    }
                }

                return new AggressivePreFlopActionProvider(this.context, first, second, this.isFirst);
            }
            else if (this.context.RoundType == GameRoundType.Flop)
            {
                return new PassiveAggressiveFlopProvider(this.context, first, second, community, this.isFirst);
            }
            else if (this.context.RoundType == GameRoundType.Turn)
            {
                return new PassiveAggressiveFlopProvider(this.context, first, second, community, this.isFirst);
            }
            else
            {
                // RIVER (final state)
                return new PassiveAggressiveFlopProvider(this.context, first, second, community, this.isFirst);
            }
        }
    }
}
