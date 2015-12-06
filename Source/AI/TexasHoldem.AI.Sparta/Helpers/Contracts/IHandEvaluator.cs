namespace TexasHoldem.AI.Sparta.Helpers.Contracts
{
    using TexasHoldem.Logic.Cards;

    public interface IHandEvaluator
    {
        double PreFlopCoefficient(Card firstCard, Card secondCard);
    }
}
