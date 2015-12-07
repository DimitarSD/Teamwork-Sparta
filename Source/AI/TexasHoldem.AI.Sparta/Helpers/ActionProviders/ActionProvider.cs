namespace TexasHoldem.AI.Sparta.Helpers.ActionProviders
{
    using Contracts;
    using Logic.Cards;
    using Logic.Players;

    /// <summary>
    /// Abstract Action provider class.
    /// </summary>
    public abstract class ActionProvider
    {
        protected readonly GetTurnContext Context;
        protected Card firstCard;
        protected Card secondCard;
        protected bool isFirst;
        protected IHandEvaluator handEvaluator;
        protected int raise;
        protected int push;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionProvider"/> class.
        /// </summary>
        /// <param name="context">Main game logic context</param>
        /// <param name="first">First player card</param>
        /// <param name="second">Second player card<</param>
        /// <param name="isFirst">Boolean check for SmalBlind/BigBlind position</param>
        public ActionProvider(GetTurnContext context, Card first, Card second, bool isFirst)
        {
            this.Context = context;
            this.firstCard = first;
            this.secondCard = second;
            this.isFirst = isFirst;
            this.raise = this.Context.SmallBlind * 8;
            this.push = (this.Context.CurrentPot / 4) * 3;
        }

        internal abstract PlayerAction GetAction();

        protected PlayerAction CheckOrFold()
        {
            if (this.Context.CanCheck)
            {
                return PlayerAction.CheckOrCall();
            }
            else if (!this.isFirst && this.Context.CanCheck)
            {
                return PlayerAction.Raise(this.raise);
            }
            else
            {
                return PlayerAction.Fold();
            }
        }
    }
}
