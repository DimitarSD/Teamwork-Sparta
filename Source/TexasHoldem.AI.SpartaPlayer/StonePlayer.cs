
namespace TexasHoldem.AI.SpartaPlayer
{
    using System;
    using System.Collections.Generic;
    using Helpers;
    using Logic;
    using Logic.Cards;
    using TexasHoldem.Logic.Players;

    public class StonePlayer : BasePlayer
    {
        public override string Name { get; } = "StonePlayer_" + Guid.NewGuid();

        public override PlayerAction GetTurn(GetTurnContext context)
        {
            var preFlopCards = CustomHandEvaluator.PreFlop(context, this.FirstCard, this.SecondCard);

            List<Card> currentCards = new List<Card>();
            currentCards.Add(this.FirstCard);
            currentCards.Add(this.SecondCard);

            if (context.RoundType == GameRoundType.PreFlop)
            {
                if (preFlopCards == CardValueType.Unplayable)
                {
                    return CheckOrFoldCustomAction(context);
                }

                if (preFlopCards == CardValueType.Risky || preFlopCards == CardValueType.NotRecommended)
                {
                    if (preFlopCards == CardValueType.NotRecommended && context.MoneyToCall > context.SmallBlind * 7)
                    {
                        return CheckOrFoldCustomAction(context);
                    }

                    if (preFlopCards == CardValueType.Risky && context.MoneyToCall > context.SmallBlind * 15)
                    {
                        return CheckOrFoldCustomAction(context);
                    }

                    return PlayerAction.CheckOrCall();
                }

                if (preFlopCards == CardValueType.Recommended && context.MoneyLeft > 0)
                {
                    return PlayerAction.Raise(context.MoneyLeft);
                }

                return PlayerAction.CheckOrCall();
            }
            else if (context.RoundType == GameRoundType.Flop)
            {
                currentCards.AddRange(this.CommunityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (GotStrongHand(combination))
                {
                    if (GotVeryStrongHand(combination))
                    {
                        if (context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.CurrentPot * 3);
                        }

                        return PlayerAction.CheckOrCall();
                    }
                    else if (context.MoneyLeft > 0)
                    {
                        return PlayerAction.Raise(context.CurrentPot * 2);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else
                {
                    return CheckOrFoldCustomAction(context);
                }

            }
            else if (context.RoundType == GameRoundType.Turn)
            {
                currentCards.Clear();
                currentCards.Add(this.FirstCard);
                currentCards.Add(this.SecondCard);
                currentCards.AddRange(this.CommunityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (GotVeryStrongHand(combination))
                {
                    if (context.MoneyLeft > 0)
                    {
                        return PlayerAction.Raise(context.MoneyLeft);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0)
                    {
                        return PlayerAction.Raise(context.SmallBlind * 8);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else
                {
                    if (context.MoneyToCall < context.SmallBlind * 2.5 || context.MoneyLeft < context.SmallBlind * 2.5)
                    {
                        return PlayerAction.CheckOrCall();
                    }

                    return CheckOrFoldCustomAction(context);
                }

            }
            else // GameRoundType.River (final card)
            {
                currentCards.Clear();
                currentCards.Add(this.FirstCard);
                currentCards.Add(this.SecondCard);
                currentCards.AddRange(this.CommunityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (GotVeryStrongHand(combination))
                {
                    if (context.MoneyLeft > 0)
                    {
                        return PlayerAction.Raise(context.MoneyLeft);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else
                {
                    if (GotStrongHand(combination))
                    {
                        if (preFlopCards == CardValueType.Recommended && context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }

                        return PlayerAction.CheckOrCall();
                    }
                    else
                    {
                        if (context.MoneyToCall < context.SmallBlind * 2 || context.MoneyLeft > context.SmallBlind * 10)
                        {
                            return PlayerAction.CheckOrCall();
                        }

                        return CheckOrFoldCustomAction(context);
                    }
                }
            }
        }

        private static bool GotStrongHand(HandRankType combination)
        {
            return combination == HandRankType.Flush ||
                    combination == HandRankType.FourOfAKind ||
                    combination == HandRankType.FullHouse ||
                    combination == HandRankType.Straight ||
                    combination == HandRankType.StraightFlush ||
                    combination == HandRankType.ThreeOfAKind ||
                    combination == HandRankType.TwoPairs ||
                    combination == HandRankType.Pair;
        }

        private static bool GotVeryStrongHand(HandRankType combination)
        {
            return combination == HandRankType.Flush ||
                    combination == HandRankType.FourOfAKind ||
                    combination == HandRankType.FullHouse ||
                    combination == HandRankType.Straight ||
                    combination == HandRankType.StraightFlush ||
                    combination == HandRankType.ThreeOfAKind;
        }

        private static PlayerAction CheckOrFoldCustomAction(GetTurnContext context)
        {
            if (context.CanCheck)
            {
                return PlayerAction.CheckOrCall();
            }
            else if (!context.CanCheck)
            {
                if (context.MoneyToCall < context.SmallBlind * 5)
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
