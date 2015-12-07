namespace TexasHoldem.AI.Sparta.Helpers.FlopHelpers
{
    using System.Collections.Generic;
    using System.Linq;

    using Logic.Cards;

    public class WetDryFlopHelper
    {
        public bool CheckForWet(List<Card> cards)
        {

            var sortedCardsByType = cards.OrderBy(c => c.Type).ToList();

            var firstCard = sortedCardsByType[0];

            int sizeOfIncreasingSequence = 1;
            int numberOfInIncreasingSequenceCardsWithSameSuit = 1;

            bool areFromSameSuit = false;
            bool areIncreasingSequence = false;
            bool areIncreasingSequenceAndFromSameSuit = false;

            for (int i = 0; i < sortedCardsByType.Count - 1; i++)
            {
                if (firstCard.Type < sortedCardsByType[i + 1].Type)
                {
                    sizeOfIncreasingSequence++;
                }

                if (firstCard.Suit == sortedCardsByType[i + 1].Suit)
                {
                    numberOfInIncreasingSequenceCardsWithSameSuit++;
                }

                firstCard = sortedCardsByType[i + 1];
            }

            var sortedCardsBySuit = cards.OrderBy(c => c.Suit).ToList();
            var currentCard = sortedCardsBySuit[0];
            int numberOfCardsWithSameSuit = 1;

            for (int i = 0; i < sortedCardsBySuit.Count - 1; i++)
            {
                if (currentCard.Suit == sortedCardsBySuit[i + 1].Suit)
                {
                    numberOfCardsWithSameSuit++;
                }

                currentCard = sortedCardsBySuit[i + 1];
            }
+++++++++++++++++++++++++++++++++++++
            if (sizeOfIncreasingSequence == sortedCardsByType.Count - 1 ||
                sizeOfIncreasingSequence == sortedCardsByType.Count)
            {
                areIncreasingSequence = true;
            }

            // Increasing sequence and from same suit
            if (numberOfInIncreasingSequenceCardsWithSameSuit == sortedCardsByType.Count - 1 ||
                numberOfInIncreasingSequenceCardsWithSameSuit == sortedCardsByType.Count)
            {
                areIncreasingSequenceAndFromSameSuit = true;
            }

            // From same suit
            if (numberOfCardsWithSameSuit == sortedCardsBySuit.Count - 1 ||
                numberOfCardsWithSameSuit == sortedCardsBySuit.Count)
            {
                areFromSameSuit = true;
            }

            bool result = areFromSameSuit || areIncreasingSequence || areIncreasingSequenceAndFromSameSuit;

            return result;
        }
    }
}