﻿namespace TexasHoldem.AI.SpartaPlayer.Helpers
{
    using TexasHoldem.Logic;
    using TexasHoldem.Logic.Cards;
    using TexasHoldem.Logic.Players;

    public static class CustomHandStreightChecks
    {
        public static bool GotAceHighCardPreFlop(GetTurnContext context, Card firstCard, Card secondCard)
        {
            return secondCard.Type == CardType.Ace ^ secondCard.Type == CardType.Ace;
        }

        public static bool GotKingighCardPreFlop(GetTurnContext context, Card firstCard, Card secondCard)
        {
            return secondCard.Type == CardType.King ^ secondCard.Type == CardType.King;
        }

        public static bool GotSuitedCardsCardPreFlop(GetTurnContext context, Card firstCard, Card secondCard)
        {
            return firstCard.Suit == secondCard.Suit;
        }

        public static bool GotStrongHand(HandRankType combination)
        {
            return combination == HandRankType.Pair ||
                    combination == HandRankType.TwoPairs ||
                    combination == HandRankType.ThreeOfAKind ||
                    combination == HandRankType.Straight ||
                    combination == HandRankType.Flush ||
                    combination == HandRankType.FullHouse ||
                    combination == HandRankType.FourOfAKind ||
                    combination == HandRankType.StraightFlush;
        }

        public static bool GotVeryStrongHand(HandRankType combination)
        {
            return combination == HandRankType.ThreeOfAKind ||
                    combination == HandRankType.Straight ||
                    combination == HandRankType.Flush ||
                    combination == HandRankType.FullHouse ||
                    combination == HandRankType.FourOfAKind ||
                    combination == HandRankType.StraightFlush;
        }

        public static bool GotTheStrongestHand(HandRankType combination)
        {
            return combination == HandRankType.Flush ||
                    combination == HandRankType.FullHouse ||
                    combination == HandRankType.FourOfAKind ||
                    combination == HandRankType.StraightFlush;
        }

        public static int PercentagePerToMakeCombination()
        {
            return -1;

            // TODO method to calculate the chance to make good combination
            //  combination == HandRankType.Pair ||
            //  combination == HandRankType.TwoPairs ||
            //  combination == HandRankType.ThreeOfAKind ||
            //  combination == HandRankType.Straight ||
            //  combination == HandRankType.Flush ||
            //  combination == HandRankType.FullHouse ||
            //  combination == HandRankType.FourOfAKind ||
            //  combination == HandRankType.StraightFlush;
        }
    }
}
