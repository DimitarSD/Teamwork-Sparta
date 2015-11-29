namespace TexasHoldem.AI.SpartaPlayer
{
    using System;
    using System.Collections.Generic;
    using Logic;
    using Helpers;
    using Logic.Cards;
    using Spartalayer.Helpers;
    using Logic.Players;

    public class TestBot : BasePlayer
    {
        public override string Name { get; } = "TestBot" + Guid.NewGuid();

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
                // TODO
                // add strong logic for FLOP
                // (do we have good card conmbination from our 2 cards and the floppef 3 cards)
                // иif we have already played aggresivly (all-in) we should check/call
                // if NOT god combination - we can check or fold
                // if strong combination we can put more agressiong and raise/all-in

                currentCards.AddRange(this.CommunityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

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
                    return CheckOrFoldCustomAction(context);
                }

            }
            else if (context.RoundType == GameRoundType.Turn)
            {
                // TODO
                // add strong logic for FLOP
                // (do we have good card conmbination from our 2 cards and the floppef 4 cards)
                // иif we have already played aggresivly (all-in) we should check/call
                // if NOT god combination - we can check or fold
                // if strong combination we can put more agressiong and raise/all-in

                currentCards.Clear();
                currentCards.Add(this.FirstCard);
                currentCards.Add(this.SecondCard);
                currentCards.AddRange(this.CommunityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

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
                    return CheckOrFoldCustomAction(context);
                }

            }
            else // GameRoundType.River (final card)
            {
                // TODO
                // add strong logic for FLOP
                // (do we have good card conmbination from our 2 cards and the floppef 5 cards)
                // иif we have already played aggresivly (all-in) we should check/call
                // if NOT god combination - we can check or fold
                // if strong combination we can put more agressiong and raise/all-in

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

                    return PlayerAction.Raise(context.CurrentPot * 2);
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
                                combination == HandRankType.Flush ||
                                combination == HandRankType.Pair;

        }

        private static bool GotVeryStrongHand(HandRankType combination)
        {
            return combination == HandRankType.Flush ||
                                combination == HandRankType.FourOfAKind ||
                                combination == HandRankType.FullHouse ||
                                combination == HandRankType.Straight ||
                                combination == HandRankType.StraightFlush ||
                                combination == HandRankType.ThreeOfAKind ||
                                combination == HandRankType.Flush;
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
