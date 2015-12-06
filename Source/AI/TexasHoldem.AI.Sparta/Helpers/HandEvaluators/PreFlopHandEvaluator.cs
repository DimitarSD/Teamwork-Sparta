using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Cards;
using TexasHoldem.Logic.Players;

namespace TexasHoldem.AI.Sparta.Helpers.HandEvaluators
{
    internal class PreFlopHandEvaluator
    {
        private const int MaxCardTypeValue = 14;

        private static readonly double[,] StartingHandTablePercents =
        {
                { 84.93, 66.21, 65.31, 64.39, 63.48, 61.50, 60.50, 59.38, 58.17, 58.06, 57.13, 56.33, 55.50 },  // AA AKs AQs AJs ATs A9s A8s A7s A6s A5s A4s A3s A2s
                { 64.46, 82.11, 62.40, 61.47, 60.58, 58.63, 56.79, 55.84, 54.80, 53.83, 52.88, 52.07, 51.23 },  // AKo KK KQs KJs KTs K9s K8s K7s K6s K5s K4s K3s K2s
                { 63.50, 60.43, 79.63, 59.07, 58.17, 56.22, 54.41, 52.52, 51.67, 50.71, 49.76, 48.93, 48.10 },  // AQo KQo QQ QJs QTs Q9s Q8s Q7s Q6s Q5s Q4s Q3s Q2s
                { 62.53, 59.44, 56.90, 77.15, 56.15, 54.11, 52.31, 50.45, 48.57, 47.82, 46.86, 46.04, 45.20 },  // AJo KJo QJo JJ JTs J9s J8s J7s J6s J5s J4s J3s J2s
                { 61.56, 58.49, 55.94, 53.82, 74.66, 52.37, 50.50, 48.65, 46.80, 44.93, 44.20, 43.37, 42.54 },  // ATo KTo QTo JTo TT T9s T8s T7s T6s T5s T4s T3s T2s
                { 59.44, 56.40, 53.86, 51.63, 49.81, 71.66, 48.85, 46.99, 45.15, 43.31, 41.40, 40.80, 39.97 },  // A9o K9o Q9o J9o T9o 99 98s 97s 96s 95s 94s 93s 92s
                { 58.37, 54.43, 51.93, 49.71, 47.81, 46.06, 68.71, 45.68, 43.81, 41.99, 40.10, 38.28, 37.67 },  // A8o K8o Q8o J8o T8o 98o 88 87s 86s 85s 84s 83s 82s
                { 57.16, 53.41, 49.90, 47.72, 45.82, 44.07, 42.69, 65.72, 42.82, 40.97, 39.10, 37.30, 35.43 },  // A7o K7o Q7o J7o T7o 97o 87o 77 76s 75s 74s 73s 72s
                { 55.87, 52.29, 48.99, 45.71, 43.84, 42.10, 40.69, 39.65, 62.70, 40.34, 38.48, 36.68, 34.83 },  // A6o K6o Q6o J6o T6o 96o 86o 76o 66 65s 64s 63s 62s
                { 55.74, 51.25, 47.95, 44.90, 41.85, 40.13, 38.74, 37.67, 37.01, 59.64, 38.53, 36.75, 34.92 },  // A5o K5o Q5o J5o T5o 95o 85o 75o 65o 55 54s 53s 52s
                { 54.73, 50.22, 46.92, 43.86, 41.05, 38.08, 36.70, 35.66, 35.00, 35.07, 56.25, 35.72, 33.91 },  // A4o K4o Q4o J4o T4o 94o 84o 74o 64o 54o 44 43s 42s
                { 53.85, 49.33, 46.02, 42.96, 40.15, 37.42, 34.74, 33.71, 33.06, 33.16, 32.06, 52.83, 33.09 },  // A3o K3o Q3o J3o T3o 93o 83o 73o 63o 53o 43o 33 32s
                { 52.94, 48.42, 45.10, 42.04, 39.23, 36.51, 34.08, 31.71, 31.07, 31.19, 30.11, 29.23, 49.38 },  // A2o K2o Q2o J2o T2o 92o 82o 72o 62o 52o 42o 32o 22
        };

        public static double PreFlopCoefficient(Card firstCard, Card secondCard)
        {
            var value = firstCard.Suit == secondCard.Suit
                          ? (firstCard.Type > secondCard.Type
                                 ? StartingHandTablePercents[MaxCardTypeValue - (int)firstCard.Type, MaxCardTypeValue - (int)secondCard.Type]
                                 : StartingHandTablePercents[MaxCardTypeValue - (int)secondCard.Type, MaxCardTypeValue - (int)firstCard.Type])
                          : (firstCard.Type > secondCard.Type
                                 ? StartingHandTablePercents[MaxCardTypeValue - (int)secondCard.Type, MaxCardTypeValue - (int)firstCard.Type]
                                 : StartingHandTablePercents[MaxCardTypeValue - (int)firstCard.Type, MaxCardTypeValue - (int)secondCard.Type]);

            return value;
        }
    }
}
