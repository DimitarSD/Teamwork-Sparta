namespace TexasHoldem.AI.Sparta.Helpers.ActionProviders
{
    using Contracts;
    using Logic.Cards;
    using TexasHoldem.Logic.Players;

    public abstract class ActionProvider
    {
        protected readonly GetTurnContext Context;
        protected readonly bool IsFirst;
        protected Card firstCard;
        protected Card secondCard;
        protected IHandEvaluator handEvaluator;

        public ActionProvider(GetTurnContext context, Card first, Card second)
        {
            this.Context = context;
            this.firstCard = first;
            this.secondCard = second;
            this.IsFirst = this.CheckIfFirst();
        }

        private bool CheckIfFirst()
        {
            return this.Context.MyMoneyInTheRound == this.Context.SmallBlind &&
                    !this.Context.CanCheck;
        }

        internal abstract PlayerAction GetAction();
    }
}
