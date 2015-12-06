﻿using System;
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
            var preflopCardsCoefficient = this.handEvaluator.PreFlopCoefficient(this.firstCard, this.secondCard);

            if (this.IsFirst)
            {
                if (this.Context.MoneyLeft > 0)
                {
                    if (this.Context.MoneyLeft > this.Context.SmallBlind * 6)
                    {
                        if (preflopCardsCoefficient >= 55.00)
                        {
                            return PlayerAction.Raise(this.Context.MoneyLeft);
                        }
                        else if (preflopCardsCoefficient < 55.00)
                        {
                            return PlayerAction.Fold();
                        }
                    }
                    else
                    {
                        return PlayerAction.Raise(this.Context.MoneyLeft);
                    }
                }

                return PlayerAction.CheckOrCall();
            }
            else
            {
                if (this.Context.MoneyLeft > 0)
                {
                    if (this.Context.MoneyLeft > this.Context.SmallBlind * 6)
                    {
                        if (preflopCardsCoefficient >= 61.00
                            || (this.firstCard.Type == CardType.Ace || this.secondCard.Type == CardType.Ace)
                            || (this.firstCard.Type == CardType.King || this.secondCard.Type == CardType.King))
                        {
                            return PlayerAction.Raise(this.Context.MoneyLeft);
                        }
                        else
                        {
                            return PlayerAction.Fold();
                        }
                    }
                    else
                    {
                        return PlayerAction.Raise(this.Context.MoneyLeft);
                    }
                }

                return PlayerAction.CheckOrCall();
            }
        }
    }
}
