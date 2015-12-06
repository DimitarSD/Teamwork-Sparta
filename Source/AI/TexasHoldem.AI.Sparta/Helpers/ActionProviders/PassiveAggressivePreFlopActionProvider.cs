using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.AI.Sparta.Helpers.HandEvaluators;
using TexasHoldem.Logic.Cards;
using TexasHoldem.Logic.Players;

namespace TexasHoldem.AI.Sparta.Helpers.ActionProviders
{
    internal class PassiveAggressivePreFlopActionProvider : ActionProvider
    {
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
                                return PlayerAction.Raise(this.Context.CurrentMaxBet + this.Context.SmallBlind * 2);
                            }
                            else if (preflopCardsCoefficient >= 58.00 && preflopCardsCoefficient < 63.00)
                            {
                                return PlayerAction.CheckOrCall();
                            }
                            else
                            {
                                return PlayerAction.Fold();
                            }
                        }

                        return PlayerAction.Raise(this.Context.SmallBlind * 6);
                    }
                    else
                    {
                        return PlayerAction.Fold();
                    }
                }
                else
                {
                    // todo : we are second
                    if (this.Context.CanCheck && this.Context.MyMoneyInTheRound == this.Context.SmallBlind * 2)
                    {
                        if (preflopCardsCoefficient >= 55.00)
                        {
                            return PlayerAction.Raise(this.Context.SmallBlind * 6);
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
                            return PlayerAction.Raise(this.Context.SmallBlind * 6);
                        }
                        else if (preflopCardsCoefficient >= 58.00 && preflopCardsCoefficient < 61.00)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else
                        {
                            // preflopCardsCoefficient < 58.00
                            return PlayerAction.Fold();
                        }
                    }
                    else
                    {
                        // he has raised with 3BB (or 6 SB)
                        if (preflopCardsCoefficient >= 65.00)
                        {
                            return PlayerAction.Raise(this.Context.SmallBlind * 6);
                        }
                        else if (preflopCardsCoefficient >= 62.00 && preflopCardsCoefficient < 65.00)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else
                        {
                            return PlayerAction.Fold();
                        }
                    }
                }
            }
            return PlayerAction.CheckOrCall();
        }
    }
}
