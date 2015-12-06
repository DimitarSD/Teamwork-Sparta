using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Players;

namespace TexasHoldem.AI.Sparta.Helpers.ActionProviders
{
    internal class AggresivePreFlopActionProvider : ActionProvider
    {
        internal AggresivePreFlopActionProvider(GetTurnContext context, Card first, Card second)
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
