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
        internal PreFlopActionProvider(GetTurnContext context, Card first, Card second, bool isFirst)
            : base(context, first, second, isFirst)
        {
        }

        internal override PlayerAction GetAction()
        {
            if (this.isFirst)
            {
                if (this.Context.MoneyLeft > 0)
                {
                    return PlayerAction.Raise(10);
                }

                return PlayerAction.Fold();
            }
            else
            {
                if (this.Context.MoneyLeft > 0)
                {
                    return PlayerAction.Raise(20);
                }

                return PlayerAction.Fold();
            }
        }
    }
}
