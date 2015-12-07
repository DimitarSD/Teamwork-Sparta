namespace TexasHoldem.AI.Sparta.Helpers.ActionProviders
{
    using HandEvaluators;
    using Logic.Cards;
    using Logic.Players;

    internal class SuperAggressivePreFlopActionProvider : ActionProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuperAggressivePreFlopActionProvider"/> class.
        /// </summary>
        /// <param name="context">Main game logic context</param>
        /// <param name="first">First player card</param>
        /// <param name="second">Second player card<</param>
        /// <param name="isFirst">Boolean check for SmalBlind/BigBlind position</param>
        internal SuperAggressivePreFlopActionProvider(GetTurnContext context, Card first, Card second, bool isFirst)
            : base(context, first, second, isFirst)
        {
            this.handEvaluator = new PreFlopHandEvaluator();
        }

        internal override PlayerAction GetAction()
        {
            var preflopCardsCoefficient = this.handEvaluator.PreFlopCoefficient(this.firstCard, this.secondCard);

            if (this.isFirst)
            {
                if (this.Context.MoneyLeft > 0)
                {
                    if (this.Context.MoneyLeft > this.raise)
                    {
                        if (preflopCardsCoefficient >= 52.00)
                        {
                            return PlayerAction.Raise(this.Context.MoneyLeft);
                        }
                        else if (preflopCardsCoefficient < 52.00)
                        {
                            return this.CheckOrFold();
                        }
                    }
                    else
                    {
                        // less then 6SB left - Going all in to return in the game
                        return PlayerAction.Raise(this.Context.MoneyLeft);
                    }
                }

                return PlayerAction.CheckOrCall();
            }
            else
            {
                if (this.Context.MoneyLeft > 0)
                {
                    if (this.Context.MoneyLeft > this.raise)
                    {
                        if (preflopCardsCoefficient >= 58.00
                            || (this.firstCard.Type == CardType.Ace || this.secondCard.Type == CardType.Ace)
                            || (this.firstCard.Type == CardType.King || this.secondCard.Type == CardType.King))
                        {
                            // we have stronger then 61 coeff. or Ace, King kickers
                            return PlayerAction.Raise(this.Context.MoneyLeft);
                        }
                        else
                        {
                            return this.CheckOrFold();
                        }
                    }
                    else
                    {
                        // less then 6SB left - Going all in to return in the game
                        return PlayerAction.Raise(this.Context.MoneyLeft);
                    }
                }

                return PlayerAction.CheckOrCall();
            }
        }
    }
}
