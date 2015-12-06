namespace TexasHoldem.AI.Sparta.Helpers.ActionProviders
{
    using Contracts;
    using Logic.Cards;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TexasHoldem.Logic.Players;

    public abstract class ActionProvider
    {
        protected readonly GetTurnContext context;
        protected Card firstCard;
        protected Card secondCard;

        public ActionProvider(GetTurnContext context, Card first, Card second)
        {
            this.context = context;
            this.firstCard = first;
            this.secondCard = second;
        }

        internal abstract PlayerAction GetAction();
    }
}
