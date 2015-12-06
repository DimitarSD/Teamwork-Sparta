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
                                return PlayerAction.Raise(Math.Max(this.Context.CurrentPot / 2, this.Context.SmallBlind * 6));
                            }
                            else if (this.Context.MyMoneyInTheRound >= this.Context.SmallBlind * 2
                                && this.Context.MyMoneyInTheRound >= this.Context.SmallBlind * 25)
                            {
                                // re-raise and we are re-re-raising
                                return PlayerAction.Raise(Math.Max(this.Context.CurrentPot / 2, this.Context.SmallBlind * 6));
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
                        else // pair or two-pairs
                        {
                            if (this.Context.MyMoneyInTheRound == this.Context.SmallBlind * 2)
                            {
                                // no aggressors
                                return PlayerAction.Raise(Math.Max(this.Context.CurrentPot / 2, this.Context.SmallBlind * 6));
                            }
                            else 
                            {
                                // call
                                if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 15)
                                {
                                    return PlayerAction.Raise(Math.Max(this.Context.CurrentPot / 2, this.Context.SmallBlind * 6));
                                }

                                if (this.Context.MoneyToCall <= this.Context.SmallBlind * 21)
                                {
                                    return PlayerAction.CheckOrCall();
                                }
                                else
                                {
                                    if (combination == Logic.HandRankType.TwoPairs)
                                    {
                                        return PlayerAction.CheckOrCall();
                                    }

                                    return PlayerAction.Fold();
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
                            foreach (var item in this.allCards)
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
                                    return PlayerAction.Raise(this.Context.SmallBlind * 6);
                                }
                                else if (this.Context.MoneyToCall < this.Context.SmallBlind * 21)
                                {
                                    return PlayerAction.CheckOrCall();
                                }

                                return PlayerAction.Fold();
                            }
                        }
                        else if (PostFlopHandEvaluator.GotAceHighCardPreFlop(this.firstCard, this.secondCard) ||
                            PostFlopHandEvaluator.GotKingighCardPreFlop(this.firstCard, this.secondCard))
                        {
                            if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 11)
                            {
                                return PlayerAction.Raise(this.Context.SmallBlind * 6);
                            }
                            else if (this.Context.MoneyToCall < this.Context.SmallBlind * 21)
                            {
                                return PlayerAction.CheckOrCall();
                            }

                            return PlayerAction.Fold();
                        }


                        return PlayerAction.Fold();
                    }
                }
                else
                {
                    if (PostFlopHandEvaluator.GotAnyCombination(combination))
                    {
                        if (PostFlopHandEvaluator.GotStrongHand(combination))
                        {
                            if (this.Context.MyMoneyInTheRound == this.Context.SmallBlind * 2)
                            {
                                // no aggressors
                                return PlayerAction.Raise(Math.Max(this.Context.CurrentPot / 2, this.Context.SmallBlind * 6));
                            }
                            else if (this.Context.MyMoneyInTheRound >= this.Context.SmallBlind * 2
                                && this.Context.MyMoneyInTheRound >= this.Context.SmallBlind * 25)
                            {
                                // re-raise and we are re-re-raising
                                return PlayerAction.Raise(Math.Max(this.Context.CurrentPot / 2, this.Context.SmallBlind * 6));
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
                        else // pair or two-pairs
                        {
                            if (this.Context.MyMoneyInTheRound == this.Context.SmallBlind * 2)
                            {
                                // no aggressors
                                return PlayerAction.Raise(Math.Max(this.Context.CurrentPot / 2, this.Context.SmallBlind * 6));
                            }
                            else
                            {
                                // call
                                if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 15)
                                {
                                    return PlayerAction.Raise(Math.Max(this.Context.CurrentPot / 2, this.Context.SmallBlind * 6));
                                }

                                if (this.Context.MoneyToCall <= this.Context.SmallBlind * 21)
                                {
                                    return PlayerAction.CheckOrCall();
                                }
                                else
                                {
                                    return PlayerAction.Fold();
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
                            foreach (var item in this.allCards)
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
                                    return PlayerAction.Raise(this.Context.SmallBlind * 6);
                                }
                                else if (this.Context.MoneyToCall < this.Context.SmallBlind * 21)
                                {
                                    return PlayerAction.CheckOrCall();
                                }

                                return PlayerAction.Fold();
                            }
                        }
                        else if (PostFlopHandEvaluator.GotAceHighCardPreFlop(this.firstCard, this.secondCard) ||
                            PostFlopHandEvaluator.GotKingighCardPreFlop(this.firstCard, this.secondCard))
                        {
                            if (this.Context.MyMoneyInTheRound <= this.Context.SmallBlind * 11)
                            {
                                return PlayerAction.Raise(this.Context.SmallBlind * 6);
                            }
                            else if (this.Context.MoneyToCall < this.Context.SmallBlind * 21)
                            {
                                return PlayerAction.CheckOrCall();
                            }

                            return PlayerAction.Fold();
                        }


                        return PlayerAction.Fold();
                    }
                }
            }

            return PlayerAction.CheckOrCall();
        }
    }
}
