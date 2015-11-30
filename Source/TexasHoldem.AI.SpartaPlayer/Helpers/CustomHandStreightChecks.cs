using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.AI.Spartalayer.Helpers;
using TexasHoldem.Logic;
using TexasHoldem.Logic.Cards;
using TexasHoldem.Logic.Players;

namespace TexasHoldem.AI.SpartaPlayer.Helpers
{
    public static class CustomHandStreightChecks
    {
        public static bool GotAceHighCardPreFlop(HandRankType combination, GetTurnContext context, Card firstCard, Card secondCard)
        {
            return combination == HandRankType.HighCard &&
                (secondCard.Type == CardType.Ace ^ secondCard.Type == CardType.Ace);
        }

        public static bool GotSuitedCardsCardPreFlop(HandRankType combination, GetTurnContext context, Card firstCard, Card secondCard)
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
