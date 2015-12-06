﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Cards;
using TexasHoldem.Logic.Players;

namespace TexasHoldem.AI.Sparta.Helpers.ActionProviders
{
    internal class SuperAggresivePreFlopActionProvider : ActionProvider
    {
        internal SuperAggresivePreFlopActionProvider(GetTurnContext context, Card first, Card second)
            : base(context, first, second)
        {
        }

        internal override PlayerAction GetAction()
        {
            if (this.IsFirst)
            {
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
