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
        protected int raise;
        protected int push;

        public ActionProvider(GetTurnContext context, Card first, Card second, bool isFirst)
        {
            this.Context = context;
            this.firstCard = first;
            this.secondCard = second;
            this.isFirst = isFirst;
            this.raise = this.Context.SmallBlind * 8;
            this.push = (this.Context.CurrentPot / 4) * 3;
        }

        protected PlayerAction CheckOrFold()
        {
            if (this.Context.CanCheck)
            {
                return PlayerAction.CheckOrCall();
            }
            else if (!this.isFirst && this.Context.CanCheck)
            {
                return PlayerAction.Raise(this.raise); //TODO: CHECK!
            }
            else
            {
                return PlayerAction.Fold();
            }
        }

        internal abstract PlayerAction GetAction();
    }
}
