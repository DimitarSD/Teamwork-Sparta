namespace TexasHoldem.AI.Spartalayer.Helpers
{
    using Logic.Players;
    using SpartaPlayer.Helpers;
    using TexasHoldem.Logic.Cards;

    public static class CustomHandEvaluator
    {
        private const int MaxCardTypeValue = 14;

        private static readonly int[,] StartingHandRecommendations =
            {
                { 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 1 }, // AA AKs AQs AJs ATs A9s A8s A7s A6s A5s A4s A3s A2s
                { 3, 3, 3, 3, 3, 2, 2, 2, 1, 1, 1, 0, 0 }, // AKo KK KQs KJs KTs K9s K8s K7s K6s K5s K4s K3s K2s
                { 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 0, 0 }, // AQo KQo QQ QJs QTs Q9s Q8s Q7s Q6s Q5s Q4s Q3s Q2s
                { 3, 3, 2, 3, 2, 1, 0, 0, 0, 0, 0, 0, 0 }, // AJo KJo QJo JJ JTs J9s J8s J7s J6s J5s J4s J3s J2s
                { 3, 2, 2, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0 }, // ATo KTo QTo JTo TT T9s T8s T7s T6s T5s T4s T3s T2s
                { 3, 2, 1, 1, 0, 3, 0, 0, 0, 0, 0, 0, 0 }, // A9o K9o Q9o J9o T9o 99 98s 97s 96s 95s 94s 93s 92s
                { 2, 1, 0, 1, 1, 1, 3, 0, 0, 0, 0, 0, 0 }, // A8o K8o Q8o J8o T8o 98o 88 87s 86s 85s 84s 83s 82s
                { 2, 1, 0, 0, 0, 1, 1, 3, 0, 0, 0, 0, 0 }, // A7o K7o Q7o J7o T7o 97o 87o 77 76s 75s 74s 73s 72s
                { 2, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0 }, // A6o K6o Q6o J6o T6o 96o 86o 76o 66 65s 64s 63s 62s
                { 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0 }, // A5o K5o Q5o J5o T5o 95o 85o 75o 65o 55 54s 53s 52s
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0 }, // A4o K4o Q4o J4o T4o 94o 84o 74o 64o 54o 44 43s 42s
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 }, // A3o K3o Q3o J3o T3o 93o 83o 73o 63o 53o 43o 33 32s
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } // A2o K2o Q2o J2o T2o 92o 82o 72o 62o 52o 42o 32o 22
            };

        private static readonly int[,] StartingHandRecommendationsAggressive =
            {
                { 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 1 }, // AA AKs AQs AJs ATs A9s A8s A7s A6s A5s A4s A3s A2s
                { 3, 3, 3, 3, 3, 2, 2, 2, 1, 1, 1, 1, 1 }, // AKo KK KQs KJs KTs K9s K8s K7s K6s K5s K4s K3s K2s
                { 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1 }, // AQo KQo QQ QJs QTs Q9s Q8s Q7s Q6s Q5s Q4s Q3s Q2s
                { 3, 3, 2, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1 }, // AJo KJo QJo JJ JTs J9s J8s J7s J6s J5s J4s J3s J2s
                { 3, 2, 2, 2, 3, 1, 1, 1, 1, 1, 1, 1, 1 }, // ATo KTo QTo JTo TT T9s T8s T7s T6s T5s T4s T3s T2s
                { 3, 2, 1, 1, 0, 3, 1, 1, 1, 1, 1, 1, 1 }, // A9o K9o Q9o J9o T9o 99 98s 97s 96s 95s 94s 93s 92s
                { 2, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1 }, // A8o K8o Q8o J8o T8o 98o 88 87s 86s 85s 84s 83s 82s
                { 2, 1, 1, 1, 0, 1, 1, 3, 1, 1, 1, 1, 1 }, // A7o K7o Q7o J7o T7o 97o 87o 77 76s 75s 74s 73s 72s
                { 2, 1, 1, 0, 0, 0, 0, 0, 2, 1, 1, 1, 1 }, // A6o K6o Q6o J6o T6o 96o 86o 76o 66 65s 64s 63s 62s
                { 2, 1, 1, 0, 0, 0, 0, 0, 0, 2, 1, 1, 1 }, // A5o K5o Q5o J5o T5o 95o 85o 75o 65o 55 54s 53s 52s
                { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 2, 1, 1 }, // A4o K4o Q4o J4o T4o 94o 84o 74o 64o 54o 44 43s 42s
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 }, // A3o K3o Q3o J3o T3o 93o 83o 73o 63o 53o 43o 33 32s
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 } // A2o K2o Q2o J2o T2o 92o 82o 72o 62o 52o 42o 32o 22
            };

        private static readonly double[,] StartingHandRecommendationsPercents =
            {
                { 84.93, 66.21, 65.31, 64.39, 63.48, 61.5, 60.5, 59.38, 58.17, 58.06, 57.13, 56.33, 55.5, },    // AA AKs AQs AJs ATs A9s A8s A7s A6s A5s A4s A3s A2s
                { 64.46, 82.11, 62.4, 61.47, 60.58, 58.63, 56.79, 55.84, 54.8, 53.83, 52.88, 52.07, 51.23, },   // AKo KK KQs KJs KTs K9s K8s K7s K6s K5s K4s K3s K2s
                { 63.5, 60.43, 79.63, 59.07, 58.17, 56.22, 54.41, 52.52, 51.67, 50.71, 49.76, 48.93, 48.1, },   // AQo KQo QQ QJs QTs Q9s Q8s Q7s Q6s Q5s Q4s Q3s Q2s
                { 62.53, 59.44, 56.9, 77.15, 56.15, 54.11, 52.31, 50.45, 48.57, 47.82, 46.86, 46.04, 45.2, },   // AJo KJo QJo JJ JTs J9s J8s J7s J6s J5s J4s J3s J2s
                { 61.56, 58.49, 55.94, 53.82, 74.66, 52.37, 50.5, 48.65, 46.8, 44.93, 44.2, 43.37, 42.54, },    // ATo KTo QTo JTo TT T9s T8s T7s T6s T5s T4s T3s T2s
                { 59.44, 56.4, 53.86, 51.63, 49.81, 71.66, 48.85, 46.99, 45.15, 43.31, 41.4, 40.8, 39.97, },    // A9o K9o Q9o J9o T9o 99 98s 97s 96s 95s 94s 93s 92s
                { 58.37, 54.43, 51.93, 49.71, 47.81, 46.06, 68.71, 45.68, 43.81, 41.99, 40.1, 38.28, 37.67, },  // A8o K8o Q8o J8o T8o 98o 88 87s 86s 85s 84s 83s 82s
                { 57.16, 53.41, 49.9, 47.72, 45.82, 44.07, 42.69, 65.72, 42.82, 40.97, 39.1, 37.3, 35.43, },    // A7o K7o Q7o J7o T7o 97o 87o 77 76s 75s 74s 73s 72s
                { 55.87, 52.29, 48.99, 45.71, 43.84, 42.1, 40.69, 39.65, 62.7, 40.34, 38.48, 36.68, 34.83, },   // A6o K6o Q6o J6o T6o 96o 86o 76o 66 65s 64s 63s 62s
                { 55.74, 51.25, 47.95, 44.9, 41.85, 40.13, 38.74, 37.67, 37.01, 59.64, 38.53, 36.75, 34.92, },  // A5o K5o Q5o J5o T5o 95o 85o 75o 65o 55 54s 53s 52s
                { 54.73, 50.22, 46.92, 43.86, 41.05, 38.08, 36.7, 35.66, 35, 35.07, 56.25, 35.72, 33.91, },     // A4o K4o Q4o J4o T4o 94o 84o 74o 64o 54o 44 43s 42s
                { 53.85, 49.33, 46.02, 42.96, 40.15, 37.42, 34.74, 33.71, 33.06, 33.16, 32.06, 52.83, 33.09, }, // A3o K3o Q3o J3o T3o 93o 83o 73o 63o 53o 43o 33 32s
                { 52.94, 48.42, 45.1, 42.04, 39.23, 36.51, 34.08, 31.71, 31.07, 31.19, 30.11, 29.23, 49.38, },  // A2o K2o Q2o J2o T2o 92o 82o 72o 62o 52o 42o 32o 22
            };

        // http://www.rakebackpros.net/texas-holdem-starting-hands/
        public static CardValueType PreFlop(GetTurnContext context, Card firstCard, Card secondCard)
        {
            var value = firstCard.Suit == secondCard.Suit
                          ? (firstCard.Type > secondCard.Type
                                 ? StartingHandRecommendations[MaxCardTypeValue - (int)firstCard.Type, MaxCardTypeValue - (int)secondCard.Type]
                                 : StartingHandRecommendations[MaxCardTypeValue - (int)secondCard.Type, MaxCardTypeValue - (int)firstCard.Type])
                          : (firstCard.Type > secondCard.Type
                                 ? StartingHandRecommendations[MaxCardTypeValue - (int)secondCard.Type, MaxCardTypeValue - (int)firstCard.Type]
                                 : StartingHandRecommendations[MaxCardTypeValue - (int)firstCard.Type, MaxCardTypeValue - (int)secondCard.Type]);

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

        public static CardValueType PreFlopAggressive(GetTurnContext context, Card firstCard, Card secondCard)
        {
            var value = firstCard.Suit == secondCard.Suit
                          ? (firstCard.Type > secondCard.Type
                                 ? StartingHandRecommendationsAggressive[MaxCardTypeValue - (int)firstCard.Type, MaxCardTypeValue - (int)secondCard.Type]
                                 : StartingHandRecommendationsAggressive[MaxCardTypeValue - (int)secondCard.Type, MaxCardTypeValue - (int)firstCard.Type])
                          : (firstCard.Type > secondCard.Type
                                 ? StartingHandRecommendationsAggressive[MaxCardTypeValue - (int)secondCard.Type, MaxCardTypeValue - (int)firstCard.Type]
                                 : StartingHandRecommendationsAggressive[MaxCardTypeValue - (int)firstCard.Type, MaxCardTypeValue - (int)secondCard.Type]);

            switch (value)
            {
                case 0:
                    return CardValueType.Unplayable;
                case 1:
                    return CardValueType.Risky;
                case 2:
                    return CardValueType.Recommended;
                case 3:
                    return CardValueType.Recommended;
                default:
                    return CardValueType.Unplayable;
            }
        }
    }
}
