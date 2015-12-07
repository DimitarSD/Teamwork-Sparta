namespace TexasHoldem.AI.Sparta.Helpers.ActionProviders
{
    using System;
    using HandEvaluators;
    using Logic.Cards;
    using Logic.Players;

    internal class PassiveAggressivePreFlopActionProvider : ActionProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PassiveAggressivePreFlopActionProvider"/> class.
        /// </summary>
        /// <param name="context">Main game logic context</param>
        /// <param name="first">First player card</param>
        /// <param name="second">Second player card<</param>
        /// <param name="isFirst">Boolean check for SmalBlind/BigBlind position</param>
        internal PassiveAggressivePreFlopActionProvider(GetTurnContext context, Card first, Card second, bool isFirst)
            : base(context, first, second, isFirst)
        {
            this.handEvaluator = new PreFlopHandEvaluator();
        }

        internal override PlayerAction GetAction()
        {
            var preflopCardsCoefficient = this.handEvaluator.PreFlopCoefficient(this.firstCard, this.secondCard);

            if (this.Context.MoneyLeft > 0)
            {
                if (this.isFirst)
                {
                    if (preflopCardsCoefficient >= 58.00)
                    {
                        // has he re-raised
                        if (!this.Context.CanCheck && this.Context.MoneyToCall > this.Context.SmallBlind)
                        {
                            if (preflopCardsCoefficient >= 63.00)
                            {
                                return PlayerAction.Raise(Math.Max(this.push, this.raise));
                            }
                            else if (preflopCardsCoefficient >= 58.00 && preflopCardsCoefficient < 63.00)
                            {
                                return PlayerAction.CheckOrCall();
                            }
                            else
                            {
                                return this.CheckOrFold();
                            }
                        }

                        return PlayerAction.Raise(this.raise);
                    }
                    else
                    {
                        return this.CheckOrFold();
                    }
                }
                else
                {
                    // todo : we are second
                    if (this.Context.CanCheck && this.Context.MyMoneyInTheRound == this.Context.SmallBlind * 2)
                    {
                        if (preflopCardsCoefficient >= 55.00)
                        {
                            return PlayerAction.Raise(Math.Max(this.push, this.raise));
                        }
                        else
                        {
                            return PlayerAction.CheckOrCall();
                        }
                    }
                    else if (!this.Context.CanCheck && this.Context.MoneyToCall <= this.Context.SmallBlind * 5)
                    {
                        if (preflopCardsCoefficient >= 61.00)
                        {
                            return PlayerAction.Raise(Math.Max(this.push, this.raise));
                        }
                        else if (preflopCardsCoefficient >= 58.00 && preflopCardsCoefficient < 61.00)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else
                        {
                            // preflopCardsCoefficient < 58.00
                            return this.CheckOrFold();
                        }
                    }
                    else
                    {
                        // he has raised with 3BB (or 6 SB)
                        if (preflopCardsCoefficient >= 65.00)
                        {
                            return PlayerAction.Raise(Math.Max(this.push, this.raise));
                        }
                        else if (preflopCardsCoefficient >= 62.00 && preflopCardsCoefficient < 65.00)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else
                        {
                            return this.CheckOrFold();
                        }
                    }
                }
            }

            return PlayerAction.CheckOrCall();
        }
    }
}
