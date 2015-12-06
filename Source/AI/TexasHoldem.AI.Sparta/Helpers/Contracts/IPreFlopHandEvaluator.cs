namespace TexasHoldem.AI.Sparta.Helpers.Contracts
{
    using TexasHoldem.Logic.Cards;

    public interface IPreFlopHandEvaluator : IHandEvaluator
    {
        double PreFlopCoefficient(Card firstCard, Card secondCard);
    }
}
