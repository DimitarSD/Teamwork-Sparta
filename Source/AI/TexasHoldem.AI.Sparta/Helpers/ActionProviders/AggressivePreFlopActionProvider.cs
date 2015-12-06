namespace TexasHoldem.AI.Sparta.Helpers.ActionProviders
{
    using TexasHoldem.AI.Sparta.Helpers.HandEvaluators;
    using TexasHoldem.Logic.Cards;
    using TexasHoldem.Logic.Players;

    internal class AggressivePreFlopActionProvider : ActionProvider
    {
        internal AggressivePreFlopActionProvider(GetTurnContext context, Card first, Card second)
            : base(context, first, second)
        {
            this.handEvaluator = new PreFlopHandEvaluator();
        }

        internal override PlayerAction GetAction()
        {
            var preflopCardsCoefficient = this.handEvaluator.PreFlopCoefficient(this.firstCard, this.secondCard);

            if (this.Context.MoneyLeft > 0)
            {
                if (this.IsFirst)
                {

                    if (preflopCardsCoefficient >= 61.00)
                    {
                        if (!this.Context.CanCheck && this.Context.MoneyToCall > this.Context.SmallBlind)
                        {
                            if (preflopCardsCoefficient >= 63.00)
                            {
                                return PlayerAction.Raise(this.Context.MoneyLeft);
                            }
                            else if (preflopCardsCoefficient > 61.00 && preflopCardsCoefficient < 63.00
                                && this.Context.MoneyToCall <= this.Context.SmallBlind * 8)
                            {
                                return PlayerAction.CheckOrCall();
                            }
                            else
                            {
                                return PlayerAction.Fold();
                            }
                        }

                        return PlayerAction.Raise(this.Context.SmallBlind * 6);
                    }
                    else
                    {
                        // preflopCardsCoefficient < 61.00)
                        return PlayerAction.Fold();
                    }


                }
                else
                {
                    // we are BB (second)

                    if (this.Context.CanCheck && this.Context.MyMoneyInTheRound == this.Context.SmallBlind)
                    {
                        // opponent calls one SB only
                        if (preflopCardsCoefficient >= 55.00)
                        {
                            return PlayerAction.Raise(this.Context.SmallBlind * 6);
                        }
                        else
                        {
                            return PlayerAction.CheckOrCall();
                        }
                    }
                    else if (this.Context.CanCheck && this.Context.MyMoneyInTheRound > this.Context.SmallBlind)
                    {
                        // we can check but our money are larger then SB (previous raises are found)
                        return PlayerAction.CheckOrCall();
                    }
                    else if (!this.Context.CanCheck && this.Context.MoneyToCall < this.Context.SmallBlind * 6)
                    {
                        // opponent raises < 3-Bet
                        if (preflopCardsCoefficient >= 61.00)
                        {
                            return PlayerAction.Raise(this.Context.SmallBlind * 6);
                        }
                        else if (preflopCardsCoefficient >= 55.00 && preflopCardsCoefficient < 61.00)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else
                        {
                            return PlayerAction.Fold();
                        }
                    }
                    else if (!this.Context.CanCheck && this.Context.MoneyToCall >= this.Context.SmallBlind * 6)
                    {
                        // opponent raises >= 3-Bet
                        if (preflopCardsCoefficient >= 65.00)
                        {
                            return PlayerAction.Raise(this.Context.SmallBlind * 6);
                        }
                        else
                        {
                            return PlayerAction.Fold();
                        }
                    }

                    return PlayerAction.CheckOrCall();

                }
            }

            return PlayerAction.CheckOrCall();
        }
    }
}
