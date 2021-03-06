﻿namespace TexasHoldem.AI.Sparta.Helpers.ActionProviders
{
    using System;
    using HandEvaluators;
    using Logic.Cards;
    using Logic.Players;

    /// <summary>
    /// Aggressive preflop action provider
    /// </summary>
    internal class AggressivePreFlopActionProvider : ActionProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggressivePreFlopActionProvider"/> class.
        /// </summary>
        /// <param name="context">Main game logic context</param>
        /// <param name="first">First player card</param>
        /// <param name="second">Second player card<</param>
        /// <param name="isFirst">Boolean check for SmalBlind/BigBlind position</param>
        internal AggressivePreFlopActionProvider(GetTurnContext context, Card first, Card second, bool isFirst)
            : base(context, first, second, isFirst)
        {
            this.handEvaluator = new PreFlopHandEvaluator();
        }

        /// <summary>
        /// Main action method to return logic-based game action
        /// </summary>
        /// <returns>Returns decisions based by the aggression level</returns>
        internal override PlayerAction GetAction()
        {
            var preflopCardsCoefficient = this.handEvaluator.PreFlopCoefficient(this.firstCard, this.secondCard);

            if (this.Context.MoneyLeft > 0)
            {
                if (this.isFirst)
                {
                    if (preflopCardsCoefficient >= 56.00)
                    {
                        if (!this.Context.CanCheck && this.Context.MoneyToCall > this.Context.SmallBlind)
                        {
                            if (preflopCardsCoefficient >= 61.00)
                            {
                                return PlayerAction.Raise(this.Context.MoneyLeft);
                            }
                            else if (preflopCardsCoefficient > 56.00 && preflopCardsCoefficient < 61.00
                                && this.Context.MoneyToCall <= this.Context.SmallBlind * 8)
                            {
                                return PlayerAction.CheckOrCall();
                            }
                            else
                            {
                                return PlayerAction.Fold();
                            }
                        }

                        return PlayerAction.Raise(Math.Max(this.push, this.raise));
                    }
                    else
                    {
                        // preflopCardsCoefficient < 56.00)
                        return PlayerAction.Fold();
                    }
                }
                else
                {
                    // we are BB (second)
                    if (this.Context.CanCheck && this.Context.MyMoneyInTheRound == this.Context.SmallBlind * 2)
                    {
                        // opponent calls one SB only
                        if (preflopCardsCoefficient >= 55.00)
                        {
                            return PlayerAction.Raise(Math.Max(this.push, this.raise));
                        }
                        else
                        {
                            return PlayerAction.CheckOrCall();
                        }
                    }
                    else if (!this.Context.CanCheck && this.Context.MoneyToCall < this.raise)
                    {
                        // opponent raises < 3-Bet
                        if (preflopCardsCoefficient >= 56.00)
                        {
                            return PlayerAction.Raise(Math.Max(this.push, this.raise));
                        }
                        else if (preflopCardsCoefficient >= 53.00 && preflopCardsCoefficient < 56.00)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else
                        {
                            return PlayerAction.Fold();
                        }
                    }
                    else if (!this.Context.CanCheck && this.Context.MoneyToCall >= this.raise)
                    {
                        // opponent raises >= 3-Bet
                        if (preflopCardsCoefficient >= 60.00)
                        {
                            return PlayerAction.Raise(Math.Max(this.push, this.raise));
                        }
                        else if (preflopCardsCoefficient >= 56.00 && preflopCardsCoefficient < 60.00)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else
                        {
                            return PlayerAction.Fold();
                        }
                    }

                    return PlayerAction.CheckOrCall();
                }
            }

            return PlayerAction.CheckOrCall();
        }
    }
}
