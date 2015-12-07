namespace TexasHoldem.AI.Sparta.Helpers.Contracts
{
    using Logic.Cards;

    /// <summary>
    /// Interface providing method for resolving the preflop pocket cards coefficient streinght
    /// </summary>
    public interface IPreFlopHandEvaluator : IHandEvaluator
    {
        double PreFlopCoefficient(Card firstCard, Card secondCard);
    }
}
