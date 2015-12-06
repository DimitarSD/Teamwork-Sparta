namespace TexasHoldem.AI.Sparta.Helpers.ActionProviders
{
    using Logic.Cards;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TexasHoldem.Logic.Players;

    internal class PreFlopActionProvider : ActionProvider
    {
        internal PreFlopActionProvider(GetTurnContext context, Card first, Card second)
            : base(context, first, second)
        {
        }

        internal override PlayerAction GetAction()
        {
            if (base.context.MoneyLeft > 0)
            {
                return PlayerAction.Raise(base.context.MoneyLeft);
            }

            return PlayerAction.Fold();
        }
    }
}
