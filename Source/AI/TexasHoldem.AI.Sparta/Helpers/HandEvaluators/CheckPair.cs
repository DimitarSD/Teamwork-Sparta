namespace TexasHoldem.AI.Sparta.Helpers.HandEvaluators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TexasHoldem.Logic.Cards;

    internal static class CheckPair
    {
        internal static int Get(List<Card> allCards)
        {
            int pair = 0;

            for (int i = 0; i < allCards.Count; i++)
            {
                for (int j = 0; j < allCards.Count; j++)
                {
                    if (i != j)
                    {
                        if ((int)allCards[i].Type == (int)allCards[j].Type)
                        {
                            pair = (int)allCards[i].Type;
                        }
                    }
                }
            }

            if ((int)allCards[0].Type != pair && (int)allCards[1].Type != pair)
            {
                return -1;
            }

            return pair;
        }
    }
}
