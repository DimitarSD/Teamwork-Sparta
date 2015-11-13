using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Cards;
using TexasHoldem.Logic.Players;

namespace TexasHoldem.AI.SpartaPlayer.Helpers
{
    public class StoneHandEvaluator
    {
        private const int MaxCardTypeValue = 14;

        private static readonly int[,] StartingHandSuited =
            {
                { 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 1 }, // A
                { 3, 3, 3, 3, 3, 2, 2, 2, 1, 1, 1, 1, 1 }, // K
                { 3, 3, 3, 2, 2, 2, 1, 1, 1, 1, 1, 0, 0 }, // Q
                { 3, 3, 2, 3, 1, 1, 1, 1, 1, 0, 0, 0, 0 }, // J
                { 3, 2, 1, 1, 3, 1, 1, 0, 0, 0, 0, 0, 0 }, // 10
                { 3, 2, 1, 1, 1, 3, 0, 0, 0, 0, 0, 0, 0 }, // 9
                { 2, 1, 1, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0 }, // 8
                { 2, 1, 1, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 }, // 7
                { 2, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0 }, // 6
                { 2, 1, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0 }, // 5
                { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0 }, // 4
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0 }, // 3
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 } // 2
            };

        private static readonly int[,] StartingHandUnSuited =
    {
                { 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 1, 1, 0 }, // A
                { 3, 3, 3, 3, 3, 2, 1, 1, 1, 0, 0, 0, 0 }, // K
                { 3, 3, 3, 2, 1, 1, 1, 0, 0, 0, 0, 0, 0 }, // Q
                { 3, 3, 2, 3, 1, 1, 0, 0, 0, 0, 0, 0, 0 }, // J
                { 3, 1, 1, 0, 3, 1, 0, 0, 0, 0, 0, 0, 0 }, // 10
                { 3, 1, 1, 0, 1, 3, 0, 0, 0, 0, 0, 0, 0 }, // 9
                { 2, 1, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0 }, // 8
                { 1, 1, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 }, // 7
                { 1, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0 }, // 6
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0 }, // 5
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 }, // 4
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 }, // 3
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 } // 2
            };

        public static CardValueType PreFlop(GetTurnContext context, Card firstCard, Card secondCard)
        {
            var value = firstCard.Suit == secondCard.Suit
                ? (firstCard.Type > secondCard.Type
                        ? StartingHandSuited[MaxCardTypeValue - (int)firstCard.Type, MaxCardTypeValue - (int)secondCard.Type]
                        : StartingHandSuited[MaxCardTypeValue - (int)secondCard.Type, MaxCardTypeValue - (int)firstCard.Type])
                : (firstCard.Type > secondCard.Type
                        ? StartingHandUnSuited[MaxCardTypeValue - (int)firstCard.Type, MaxCardTypeValue - (int)secondCard.Type]
                        : StartingHandUnSuited[MaxCardTypeValue - (int)secondCard.Type, MaxCardTypeValue - (int)firstCard.Type]);

            switch (value)
            {
                case 0:
                    return CardValueType.Unplayable;
                case 1:
                    return CardValueType.NotRecommended;
                case 2:
                    return CardValueType.Risky;
                case 3:
                    return CardValueType.Recommended;
                default:
                    return CardValueType.Unplayable;
            }
        }
    }
}
