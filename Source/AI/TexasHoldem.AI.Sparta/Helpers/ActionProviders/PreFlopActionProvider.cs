namespace TexasHoldem.AI.Sparta.Helpers.ActionProviders
{
    using Logic.Cards;
    using Logic.Players;

    internal class PreFlopActionProvider : ActionProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreFlopActionProvider"/> class.
        /// </summary>
        /// <param name="context">Main game logic context</param>
        /// <param name="first">First player card</param>
        /// <param name="second">Second player card<</param>
        /// <param name="isFirst">Boolean check for SmalBlind/BigBlind position</param>
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
