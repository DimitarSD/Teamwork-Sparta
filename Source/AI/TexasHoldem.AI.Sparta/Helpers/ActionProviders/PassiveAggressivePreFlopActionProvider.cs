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
        internal PassiveAggressivePreFlopActionProvider(GetTurnContext context, Card first, Card second)
            : base(context, first, second)
        {
            this.handEvaluator = new PreFlopHandEvaluator();
        }

        internal override PlayerAction GetAction()
        {
            var preflopCardsCoefficient = this.handEvaluator.PreFlopCoefficient(this.firstCard, this.secondCard);

            if (this.IsFirst)
            {
                if (this.Context.MoneyLeft > 0)
                {
                    if (preflopCardsCoefficient >= 58.00)
                    {
                        if (!this.Context.CanCheck && this.Context.MoneyToCall > this.Context.SmallBlind)
                        {
                            if (preflopCardsCoefficient >= 63.00)
                            {
                                return PlayerAction.Raise(this.Context.MoneyLeft);
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

                return PlayerAction.CheckOrCall();
            }

            return null;
        }
    }
}
