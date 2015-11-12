namespace TexasHoldem.AI.SpartaPlayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TexasHoldem.Logic.Players;

    public class SpartaPlayer : BasePlayer
    {
        public override string Name { get; } = "SpartaPlayer_" + Guid.NewGuid();

        public override PlayerAction GetTurn(GetTurnContext context)
        {
            if (context.MoneyLeft > 0 && context.IsAllIn)
            {
                return PlayerAction.Raise(context.MoneyLeft);
            }
            else
            {
                return PlayerAction.CheckOrCall();
            }
        }
    }
}
