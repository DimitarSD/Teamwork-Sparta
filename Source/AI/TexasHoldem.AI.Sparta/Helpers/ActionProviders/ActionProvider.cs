namespace TexasHoldem.AI.Sparta.Helpers.ActionProviders
{
    using Contracts;
    using Logic.Cards;
    using TexasHoldem.Logic.Players;

    public abstract class ActionProvider
    {
        protected readonly GetTurnContext Context;
        protected Card firstCard;
        protected Card secondCard;
        protected bool isFirst;
        protected IHandEvaluator handEvaluator;

        public ActionProvider(GetTurnContext context, Card first, Card second, bool isFirst)
        {
            this.Context = context;
            this.firstCard = first;
            this.secondCard = second;
            this.isFirst = isFirst;
        }

        internal abstract PlayerAction GetAction();
    }
}
