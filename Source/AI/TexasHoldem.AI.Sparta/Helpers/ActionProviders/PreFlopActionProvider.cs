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
            if (base.IsFirst)
            {
                if (base.Context.MoneyLeft > 0)
                {
                    return PlayerAction.Raise(10);
                }

                return PlayerAction.Fold();
            }
            else
            {
                if (base.Context.MoneyLeft > 0)
                {
                    return PlayerAction.Raise(20);
                }

                return PlayerAction.Fold();
            }            
        }
    }
}
