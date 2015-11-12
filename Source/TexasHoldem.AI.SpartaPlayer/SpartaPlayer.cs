namespace TexasHoldem.AI.SpartaPlayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Logic;
    using TexasHoldem.Logic.Players;
    using Helpers;

    public class SpartaPlayer : BasePlayer
    {
        public override string Name { get; } = "SpartaPlayer_" + Guid.NewGuid();

        public override PlayerAction GetTurn(GetTurnContext context)
        {
            var preFlopCards = HandEvaluator.PreFlop(context, this.FirstCard, this.SecondCard);

            if (context.RoundType == GameRoundType.PreFlop)
            {
                if (preFlopCards == CardValueType.Unplayable)
                {
                    return CheckOrFoldCustomAction(context);
                }

                if (preFlopCards == CardValueType.Risky || preFlopCards == CardValueType.NotRecommended)
                {
                    if (preFlopCards == CardValueType.NotRecommended && context.MoneyToCall > context.SmallBlind * 11)
                    {
                        return CheckOrFoldCustomAction(context);
                    }

                    if (preFlopCards == CardValueType.Risky && context.MoneyToCall > context.SmallBlind * 21)
                    {
                        return CheckOrFoldCustomAction(context);
                    }

                    return PlayerAction.Raise(context.SmallBlind * 3);
                }

                if (preFlopCards == CardValueType.Recommended)
                {
                    return PlayerAction.Raise(context.SmallBlind * 6);
                }

                return PlayerAction.CheckOrCall();
            }
            else if (context.RoundType == GameRoundType.Flop)
            {
                if (preFlopCards == CardValueType.Recommended && context.MoneyLeft > 0)
                {
                    return PlayerAction.Raise(context.MoneyLeft);
                }

                return PlayerAction.CheckOrCall();
            }
            else if (context.RoundType == GameRoundType.Turn)
            {
                if (preFlopCards == CardValueType.Recommended && context.MoneyLeft > 0)
                {
                    return PlayerAction.Raise(context.MoneyLeft);
                }

                return PlayerAction.CheckOrCall();
            }
            else // GameRoundType.River (final card)
            {
                if (preFlopCards == CardValueType.Recommended && context.MoneyLeft > 0)
                {
                    return PlayerAction.Raise(context.MoneyLeft);
                }

                return PlayerAction.CheckOrCall();
            }
        }

        private static PlayerAction CheckOrFoldCustomAction(GetTurnContext context)
        {
            if (context.CanCheck)
            {
                return PlayerAction.CheckOrCall();
            }
            else if (!context.CanCheck)
            {
                if (context.CurrentPot < context.SmallBlind * 5)
                {
                    return PlayerAction.CheckOrCall();
                }
                return PlayerAction.Fold();
            }
            else
            {
                return PlayerAction.Fold();
            }
        }
    }
}
