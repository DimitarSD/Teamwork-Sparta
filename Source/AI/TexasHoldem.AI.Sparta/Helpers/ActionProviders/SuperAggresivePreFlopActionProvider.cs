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
    internal class SuperAggresivePreFlopActionProvider : ActionProvider
    {
        internal SuperAggresivePreFlopActionProvider(GetTurnContext context, Card first, Card second)
            : base(context, first, second)
        {
            this.handEvaluator = new PreFlopHandEvaluator();
        }

        internal override PlayerAction GetAction()
        {
            if (this.IsFirst)
            {
                var preflopCardsCoefficient = this.handEvaluator.PreFlopCoefficient(this.firstCard, this.secondCard);

                if (this.Context.MoneyLeft > 0)
                {
                    return PlayerAction.Raise(10);
                }

                return PlayerAction.CheckOrCall();
            }
            else
            {
                if (this.Context.MoneyLeft > 0)
                {
                    return PlayerAction.Raise(20);
                }

                return PlayerAction.CheckOrCall();
            }
        }
    }
}
