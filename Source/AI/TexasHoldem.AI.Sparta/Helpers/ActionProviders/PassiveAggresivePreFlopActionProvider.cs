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
    internal class PassiveAggresivePreFlopActionProvider : ActionProvider
    {
        internal PassiveAggresivePreFlopActionProvider(GetTurnContext context, Card first, Card second)
            : base(context, first, second)
        {
            this.handEvaluator = new PreFlopHandEvaluator();
        }

        internal override PlayerAction GetAction()
        {
            throw new NotImplementedException();
        }
    }
}
