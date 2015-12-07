﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic;
using TexasHoldem.Logic.Cards;
using TexasHoldem.Logic.Players;

namespace TexasHoldem.AI.Sparta.Helpers.HandEvaluators
{
    internal static class PostFlopHandEvaluator
    {
        public static bool GotAceHighCardPreFlop(Card firstCard, Card secondCard)
        {
            return secondCard.Type == CardType.Ace ^ secondCard.Type == CardType.Ace;
        }

        public static bool GotKingighCardPreFlop(Card firstCard, Card secondCard)
        {
            return secondCard.Type == CardType.King ^ secondCard.Type == CardType.King;
        }

        public static bool GotSuitedCardsCardPreFlop(Card firstCard, Card secondCard)
        {
            return firstCard.Suit == secondCard.Suit;
        }

        public static bool GotAnyCombination(HandRankType combination)
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

        public static bool GotStrongHand(HandRankType combination)
        {
            return combination == HandRankType.ThreeOfAKind ||
                    combination == HandRankType.Straight ||
                    combination == HandRankType.Flush ||
                    combination == HandRankType.FullHouse ||
                    combination == HandRankType.FourOfAKind ||
                    combination == HandRankType.StraightFlush;
        }

        public static bool GotTheStrongestCombination(HandRankType combination)
        {
            return combination == HandRankType.Flush ||
                    combination == HandRankType.FullHouse ||
                    combination == HandRankType.FourOfAKind ||
                    combination == HandRankType.StraightFlush;
        }

    }
}