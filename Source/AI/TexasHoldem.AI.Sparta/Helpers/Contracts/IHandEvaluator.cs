namespace TexasHoldem.AI.Sparta.Helpers.Contracts
{
    using Logic.Cards;

    /// <summary>
    /// Interface providing method for resolving preflop pocket coefficient streinght
    /// </summary>
    public interface IHandEvaluator
    {
        double PreFlopCoefficient(Card firstCard, Card secondCard);
    }
}
