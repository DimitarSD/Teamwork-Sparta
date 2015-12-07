namespace TexasHoldem.AI.Sparta.Helpers.ActionProviders.FlopProviders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HandEvaluators;
    using Logic.Cards;
    using TexasHoldem.Logic.Players;

    internal class PassiveAggressiveFlopProvider : ActionProvider
    {
        private IReadOnlyCollection<Card> communityCards;
        private List<Card> allCards;

        internal PassiveAggressiveFlopProvider(GetTurnContext context, Card first, Card second, IReadOnlyCollection<Card> communityCards, bool isFirst)
            : base(context, first, second, isFirst)
        {
            this.handEvaluator = new PreFlopHandEvaluator();
            this.communityCards = communityCards;
            this.allCards = new List<Card> { first, second };
            this.allCards.AddRange(communityCards.ToList());
        }

        internal override PlayerAction GetAction()
        {
            var combination = Logic.Helpers.Helpers.GetHandRank(this.allCards);

            if (this.Context.MoneyLeft > 0)
            {
                if (this.isFirst)
                {
                    if (PostFlopHandEvaluator.GotAnyCombination(combination))
                    {
                        if (PostFlopHandEvaluator.GotStrongHand(combination))
                        {
                            if (this.Context.MyMoneyInTheRound == this.Context.SmallBlind * 2)
                            {
                                // no aggressors
                                return PlayerAction.Raise(Math.Max(this.push, this.raise));
                            }
                            else if (this.Context.MyMoneyInTheRound >= this.Context.SmallBlind * 2
                                && this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 25)
                            {
                                // re-raise and we are re-re-raising
                                return PlayerAction.Raise(Math.Max(this.push, this.raise));
                            }
                            else
                            {
                                if (PostFlopHandEvaluator.GotTheStrongestCombination(combination))
                                {
                                    return PlayerAction.Raise(this.Context.MoneyLeft);
                                }
                                // call
                                return PlayerAction.CheckOrCall();
                            }
                        }
                        else if (combination == Logic.HandRankType.Pair)
                        {
                            var pair = CheckPair.Get(this.allCards);

                            if (pair == -1)
                            {
                                if (PostFlopHandEvaluator.GotAceHighCardPreFlop(firstCard, secondCard) ||
                                    PostFlopHandEvaluator.GotKingighCardPreFlop(firstCard, secondCard))
                                {
                                    if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 7)
                                    {
                                        return PlayerAction.Raise(Math.Max(this.push, this.raise));
                                    }

                                    if (this.Context.MoneyToCall <= this.Context.SmallBlind * 35)
                                    {
                                        return PlayerAction.CheckOrCall();
                                    }
                                    else
                                    {
                                        return this.CheckOrFold();
                                    }
                                }
                                else
                                {
                                    return this.CheckOrFold();
                                }
                            }
                            else if (pair < 8)
                            {
                                //TODO: CHECH WET OR DRY!!!!!!
                                if (PostFlopHandEvaluator.GotAceHighCardPreFlop(firstCard, secondCard) ||
                                    PostFlopHandEvaluator.GotKingighCardPreFlop(firstCard, secondCard))
                                {
                                    if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 10)
                                    {
                                        return PlayerAction.Raise(Math.Max(this.push, this.raise));
                                    }

                                    if (this.Context.MoneyToCall <= this.Context.SmallBlind * 15)
                                    {
                                        return PlayerAction.CheckOrCall();
                                    }
                                    else
                                    {
                                        return this.CheckOrFold();
                                    }
                                }
                                else
                                {
                                    if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 7)
                                    {
                                        return PlayerAction.Raise(Math.Max(this.push, this.raise));
                                    }

                                    if (this.Context.MoneyToCall <= this.Context.SmallBlind * 15)
                                    {
                                        return PlayerAction.CheckOrCall();
                                    }
                                    else
                                    {
                                        return this.CheckOrFold();
                                    }
                                }
                            }
                            else
                            {
                                //Pair >= 9
                                //TODO: CHECH WET OR DRY!!!!!!
                                if (PostFlopHandEvaluator.GotAceHighCardPreFlop(firstCard, secondCard) ||
                                    PostFlopHandEvaluator.GotKingighCardPreFlop(firstCard, secondCard))
                                {
                                    if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 15)
                                    {
                                        return PlayerAction.Raise(Math.Max(this.push, this.raise));
                                    }

                                    if (this.Context.MoneyToCall <= this.Context.SmallBlind * 25)
                                    {
                                        return PlayerAction.CheckOrCall();
                                    }
                                    else
                                    {
                                        return this.CheckOrFold();
                                    }
                                }
                                else
                                {
                                    if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 10)
                                    {
                                        return PlayerAction.Raise(Math.Max(this.push, this.raise));
                                    }

                                    if (this.Context.MoneyToCall <= this.Context.SmallBlind * 15)
                                    {
                                        return PlayerAction.CheckOrCall();
                                    }
                                    else
                                    {
                                        return this.CheckOrFold();
                                    }
                                }
                            }

                        }
                        else // two-pairs
                        {
                            if (this.Context.MyMoneyInTheRound == this.Context.SmallBlind * 2)
                            {
                                // no aggressors
                                return PlayerAction.Raise(Math.Max(this.push, this.raise));
                            }
                            else
                            {
                                // call
                                if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 15)
                                {
                                    return PlayerAction.Raise(Math.Max(this.push, this.raise));
                                }
                                else
                                {
                                    return PlayerAction.CheckOrCall();
                                }
                            }
                        }
                    }
                    else
                    {
                        // no combination at all - todo look for outsa and wet/dry flop
                        if (this.firstCard.Suit == this.secondCard.Suit)
                        {
                            var count = 2;
                            foreach (var item in this.communityCards)
                            {
                                if (item.Suit == this.firstCard.Suit)
                                {
                                    count++;
                                }
                            }

                            if (count == 4)
                            {
                                if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 11)
                                {
                                    return PlayerAction.Raise(this.raise);
                                }
                                else if (this.Context.MoneyToCall < this.Context.SmallBlind * 21)
                                {
                                    return PlayerAction.CheckOrCall();
                                }

                                return this.CheckOrFold();
                            }
                        }
                        else if (PostFlopHandEvaluator.GotAceHighCardPreFlop(this.firstCard, this.secondCard) ||
                            PostFlopHandEvaluator.GotKingighCardPreFlop(this.firstCard, this.secondCard))
                        {
                            if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 11)
                            {
                                return PlayerAction.Raise(this.raise);
                            }
                            else if (this.Context.MoneyToCall < this.Context.SmallBlind * 21)
                            {
                                return PlayerAction.CheckOrCall();
                            }

                            return this.CheckOrFold();
                        }

                        return this.CheckOrFold();
                    }
                }
                else // Second (BB)
                {
                    if (PostFlopHandEvaluator.GotAnyCombination(combination))
                    {
                        if (PostFlopHandEvaluator.GotStrongHand(combination))
                        {
                            if (this.Context.MyMoneyInTheRound == this.Context.SmallBlind * 2)
                            {
                                // no aggressors
                                return PlayerAction.Raise(Math.Max(this.push, this.raise));
                            }
                            else if (this.Context.MyMoneyInTheRound >= this.Context.SmallBlind * 2
                                && this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 25)
                            {
                                // re-raise and we are re-re-raising
                                return PlayerAction.Raise(Math.Max(this.push, this.raise));
                            }
                            else
                            {
                                if (PostFlopHandEvaluator.GotTheStrongestCombination(combination))
                                {
                                    return PlayerAction.Raise(this.Context.MoneyLeft);
                                }
                                // call
                                return PlayerAction.CheckOrCall();
                            }
                        }
                        else if (combination == Logic.HandRankType.Pair)
                        {
                            var pair = CheckPair.Get(this.allCards);

                            if (pair == -1)
                            {
                                if (PostFlopHandEvaluator.GotAceHighCardPreFlop(firstCard, secondCard) ||
                                    PostFlopHandEvaluator.GotKingighCardPreFlop(firstCard, secondCard))
                                {
                                    if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 7)
                                    {
                                        return PlayerAction.Raise(Math.Max(this.push, this.raise));
                                    }

                                    if (this.Context.MoneyToCall <= this.Context.SmallBlind * 15)
                                    {
                                        return PlayerAction.CheckOrCall();
                                    }
                                    else
                                    {
                                        return this.CheckOrFold();
                                    }
                                }
                                else
                                {
                                    return this.CheckOrFold();
                                }
                            }
                            else if (pair < 8)
                            {
                                //TODO: CHECH WET OR DRY!!!!!!
                                if (PostFlopHandEvaluator.GotAceHighCardPreFlop(firstCard, secondCard) ||
                                    PostFlopHandEvaluator.GotKingighCardPreFlop(firstCard, secondCard))
                                {
                                    if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 10)
                                    {
                                        return PlayerAction.Raise(Math.Max(this.push, this.raise));
                                    }

                                    if (this.Context.MoneyToCall <= this.Context.SmallBlind * 15)
                                    {
                                        return PlayerAction.CheckOrCall();
                                    }
                                    else
                                    {
                                        return this.CheckOrFold();
                                    }
                                }
                                else
                                {
                                    if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 7)
                                    {
                                        return PlayerAction.Raise(Math.Max(this.push, this.raise));
                                    }

                                    if (this.Context.MoneyToCall <= this.Context.SmallBlind * 15)
                                    {
                                        return PlayerAction.CheckOrCall();
                                    }
                                    else
                                    {
                                        return this.CheckOrFold();
                                    }
                                }
                            }
                            else
                            {
                                //Pair >= 9
                                //TODO: CHECH WET OR DRY!!!!!!
                                if (PostFlopHandEvaluator.GotAceHighCardPreFlop(firstCard, secondCard) ||
                                    PostFlopHandEvaluator.GotKingighCardPreFlop(firstCard, secondCard))
                                {
                                    if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 15)
                                    {
                                        return PlayerAction.Raise(Math.Max(this.push, this.raise));
                                    }

                                    if (this.Context.MoneyToCall <= this.Context.SmallBlind * 25)
                                    {
                                        return PlayerAction.CheckOrCall();
                                    }
                                    else
                                    {
                                        return this.CheckOrFold();
                                    }
                                }
                                else
                                {
                                    if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 10)
                                    {
                                        return PlayerAction.Raise(Math.Max(this.push, this.raise));
                                    }

                                    if (this.Context.MoneyToCall <= this.Context.SmallBlind * 15)
                                    {
                                        return PlayerAction.CheckOrCall();
                                    }
                                    else
                                    {
                                        return this.CheckOrFold();
                                    }
                                }
                            }

                        }
                        else // two-pairs
                        {
                            if (this.Context.MyMoneyInTheRound == this.Context.SmallBlind * 2)
                            {
                                // no aggressors
                                return PlayerAction.Raise(Math.Max(this.push, this.raise));
                            }
                            else
                            {
                                // call
                                if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 15)
                                {
                                    return PlayerAction.Raise(Math.Max(this.push, this.raise));
                                }
                                else
                                {
                                    return PlayerAction.CheckOrCall();
                                }
                            }
                        }
                    }
                    else
                    {
                        // no combination at all - todo look for outsa and wet/dry flop
                        if (this.firstCard.Suit == this.secondCard.Suit)
                        {
                            var count = 2;
                            foreach (var item in this.communityCards)
                            {
                                if (item.Suit == this.firstCard.Suit)
                                {
                                    count++;
                                }
                            }

                            if (count == 4)
                            {
                                if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 11)
                                {
                                    return PlayerAction.Raise(this.raise);
                                }
                                else if (this.Context.MoneyToCall < this.Context.SmallBlind * 21)
                                {
                                    return PlayerAction.CheckOrCall();
                                }

                                return this.CheckOrFold();
                            }
                        }
                        else if (PostFlopHandEvaluator.GotAceHighCardPreFlop(this.firstCard, this.secondCard) ||
                            PostFlopHandEvaluator.GotKingighCardPreFlop(this.firstCard, this.secondCard))
                        {
                            if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 11)
                            {
                                return PlayerAction.Raise(this.raise);
                            }
                            else if (this.Context.MoneyToCall < this.Context.SmallBlind * 21)
                            {
                                return PlayerAction.CheckOrCall();
                            }

                            return this.CheckOrFold();
                        }

                        if (this.Context.CanCheck)
                        {
                            return PlayerAction.Raise(this.raise);
                        }

                        return this.CheckOrFold();
                    }
                }
            }

            return PlayerAction.CheckOrCall();
        }
    }
}
