namespace TexasHoldem.AI.SpartaPlayer.Helpers
{
    using Logic.Players;
    using TexasHoldem.Logic.Cards;

    public static class CustomHandEvaluator
    {
        private const int MaxCardTypeValue = 14;

        private static readonly int[,] StartingHandSuited =
            {
                { 3, 3, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1 }, // A
                { 2, 3, 3, 3, 3, 2, 1, 1, 1, 1, 1, 1, 1 }, // K
                { 2, 2, 3, 3, 3, 2, 2, 0, 0, 0, 0, 0, 0 }, // Q
                { 2, 2, 1, 3, 3, 3, 2, 1, 0, 0, 0, 0, 0 }, // J
                { 2, 2, 1, 1, 3, 3, 2, 1, 0, 0, 0, 0, 0 }, // 10
                { 0, 0, 0, 0, 0, 3, 2, 1, 1, 0, 0, 0, 0 }, // 9
                { 0, 0, 0, 0, 0, 0, 3, 1, 1, 0, 0, 0, 0 }, // 8
                { 0, 0, 0, 0, 0, 0, 0, 3, 1, 1, 0, 0, 0 }, // 7
                { 0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 0, 0, 0 }, // 6
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 0, 0 }, // 5
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 }, // 4
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 }, // 3
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 } // 2
            };

        private static readonly int[,] StartingHandUnsuited =
            {
                { 3, 3, 3, 3, 3, 1, 1, 1, 0, 0, 0, 0, 0 }, // A
                { 2, 3, 3, 3, 2, 1, 0, 0, 0, 0, 0, 0, 0 }, // K
                { 2, 2, 3, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0 }, // Q
                { 0, 1, 1, 3, 2, 1, 1, 0, 0, 0, 0, 0, 0 }, // J
                { 0, 0, 0, 1, 3, 1, 1, 0, 0, 0, 0, 0, 0 }, // 10
                { 0, 0, 0, 0, 0, 3, 1, 1, 0, 0, 0, 0, 0 }, // 9
                { 0, 0, 0, 0, 0, 0, 3, 1, 0, 0, 0, 0, 0 }, // 8
                { 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0 }, // 7
                { 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0 }, // 6
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0 }, // 5
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 }, // 4
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 }, // 3
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 } // 2
            };

        private static readonly int[,] EndingHandSuited =
    {
                { 3, 3, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1 }, // A
                { 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2 }, // K
                { 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2 }, // Q
                { 2, 2, 2, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2 }, // J
                { 2, 2, 2, 1, 3, 3, 2, 1, 1, 1, 0, 0, 0 }, // 10
                { 2, 2, 2, 1, 1, 3, 2, 1, 1, 0, 0, 0, 0 }, // 9
                { 2, 2, 2, 1, 1, 1, 3, 1, 1, 0, 0, 0, 0 }, // 8
                { 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 0, 0, 0 }, // 7
                { 1, 1, 1, 1, 1, 1, 1, 0, 2, 1, 0, 0, 0 }, // 6
                { 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 1, 0, 0 }, // 5
                { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0 }, // 4
                { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0 }, // 3
                { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 } // 2
            };

        private static readonly int[,] EndingHandUnsuited =
            {
                { 3, 3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 1 }, // A
                { 3, 3, 3, 3, 2, 1, 0, 1, 1, 1, 1, 1, 1 }, // K
                { 3, 3, 3, 2, 2, 1, 0, 1, 1, 1, 1, 1, 1 }, // Q
                { 3, 3, 2, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1 }, // J
                { 1, 1, 1, 2, 3, 2, 1, 0, 0, 0, 0, 0, 0 }, // 10
                { 1, 1, 1, 1, 2, 3, 2, 1, 0, 0, 0, 0, 0 }, // 9
                { 1, 1, 1, 1, 1, 2, 3, 1, 0, 0, 0, 0, 0 }, // 8
                { 1, 1, 1, 1, 1, 1, 0, 3, 0, 0, 0, 0, 0 }, // 7
                { 1, 1, 1, 1, 0, 0, 0, 0, 2, 0, 0, 0, 0 }, // 6
                { 1, 1, 1, 1, 0, 0, 0, 0, 0, 2, 0, 0, 0 }, // 5
                { 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0 }, // 4
                { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 }, // 3
                { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 } // 2
            };
        public static CardValueType PreFlop(GetTurnContext context, Card firstCard, Card secondCard)
        {

            if (context.MoneyLeft >= context.SmallBlind * 23)
            {
                var value = firstCard.Suit == secondCard.Suit
                    ? (firstCard.Type > secondCard.Type
                           ? StartingHandSuited[MaxCardTypeValue - (int)firstCard.Type, MaxCardTypeValue - (int)secondCard.Type]
                           : StartingHandSuited[MaxCardTypeValue - (int)secondCard.Type, MaxCardTypeValue - (int)firstCard.Type])
                    : (firstCard.Type > secondCard.Type
                           ? StartingHandUnsuited[MaxCardTypeValue - (int)firstCard.Type, MaxCardTypeValue - (int)secondCard.Type]
                           : StartingHandUnsuited[MaxCardTypeValue - (int)secondCard.Type, MaxCardTypeValue - (int)firstCard.Type]);

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
            else // if (context.MoneyLeft < context.SmallBlind * 23)
            {
                var value = firstCard.Suit == secondCard.Suit
                   ? (firstCard.Type > secondCard.Type
                          ? EndingHandSuited[MaxCardTypeValue - (int)firstCard.Type, MaxCardTypeValue - (int)secondCard.Type]
                          : EndingHandSuited[MaxCardTypeValue - (int)secondCard.Type, MaxCardTypeValue - (int)firstCard.Type])
                   : (firstCard.Type > secondCard.Type
                          ? EndingHandSuited[MaxCardTypeValue - (int)firstCard.Type, MaxCardTypeValue - (int)secondCard.Type]
                          : EndingHandSuited[MaxCardTypeValue - (int)secondCard.Type, MaxCardTypeValue - (int)firstCard.Type]);

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
}
