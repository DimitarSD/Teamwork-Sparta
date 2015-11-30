namespace TexasHoldem.AI.SpartaPlayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Helpers;
    using Logic;
    using Logic.Cards;
    using Spartalayer.Helpers;
    using TexasHoldem.Logic.Players;

    public class SpartaPlayer : BasePlayer
    {
        public override string Name { get; } = "SpartaPlayer_" + Guid.NewGuid();

        public override PlayerAction GetTurn(GetTurnContext context)
        {
            if (context.MoneyLeft <= 400)
            {
                var preFlopCards = CustomHandEvaluator.PreFlopAggressive(context, this.FirstCard, this.SecondCard);
                return this.BigStackMethod(context, preFlopCards);
            }
            else if (context.MoneyLeft > 400 && context.MoneyLeft < 1600)
            {
                var preFlopCards = CustomHandEvaluator.PreFlopAggressive(context, this.FirstCard, this.SecondCard);
                return this.NormalStackMethod(context, preFlopCards);
            }
            else // context.MoneyLeft > 1600 we are CHIPLEADERS - FIGHT With AGRESSION!
            {
                var preFlopCards = CustomHandEvaluator.PreFlopAggressive(context, this.FirstCard, this.SecondCard);
                return this.BigStackMethod(context, preFlopCards);
            }
        }

        private PlayerAction NormalStackMethod(GetTurnContext context, CardValueType preFlopCards)
        {
            List<Card> currentCards = new List<Card>();
            currentCards.Add(this.FirstCard);
            currentCards.Add(this.SecondCard);

            if (context.RoundType == GameRoundType.PreFlop)
            {
                if (context.MoneyLeft / context.SmallBlind > 50)
                {
                    if (!context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return AgressivePlayerActionPreflop(context, preFlopCards, 8, 12);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return this.PassivePlayerActionPreFlop(context, preFlopCards, 12);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return this.AgressivePlayerActionPreflop(context, preFlopCards, 10, 12);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (context.MoneyLeft / context.SmallBlind > 15 && (context.MoneyLeft / context.SmallBlind <= 50))
                {
                    if (!context.CanCheck && context.MoneyToCall <= context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return this.AgressivePlayerActionPreflop(context, preFlopCards, 6, 9);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return this.PassivePlayerActionPreFlop(context, preFlopCards, 12);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return this.AgressivePlayerActionPreflop(context, preFlopCards, 8, 14);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (context.MoneyLeft / context.SmallBlind <= 15)
                {
                    if (!context.CanCheck && context.MoneyToCall <= context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return this.AgressivePlayerActionPreflop(context, preFlopCards, 8, 14);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return this.PassivePlayerActionPreFlop(context, preFlopCards, 14);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return this.AgressivePlayerActionPreflop(context, preFlopCards, 8, 14);
                    }

                    return PlayerAction.CheckOrCall();
                }

                return CheckOrFoldCustomAction(context);

            }
            else if (context.RoundType == GameRoundType.Flop)
            {
                // TODO
                // add strong logic for FLOP
                // (do we have good card conmbination from our 2 cards and the floppef 3 cards)
                // иif we have already played aggresivly (all-in) we should check/call
                // if NOT god combination - we can check or fold
                // if strong combination we can put more agressiong and raise/all-in

                currentCards.AddRange(this.CommunityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (this.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && this.GotTheStrongestHand(combination))
                    {
                        return this.AgressivePlayerAction(context, preFlopCards, combination, 7, 9);
                    }
                    else if (context.MoneyLeft > 0 && this.GotVeryStrongHand(combination))
                    {
                        return this.AgressivePlayerAction(context, preFlopCards, combination, 9, 11);
                    }
                    else
                    {
                        return this.AgressivePlayerAction(context, preFlopCards, combination, 5, 9);
                    }
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0 && context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return this.PassivePlayerAction(context, combination, 5, 9);
                    }

                    return CheckOrFoldCustomAction(context);
                }

            }
            else if (context.RoundType == GameRoundType.Turn)
            {
                // TODO
                // add strong logic for FLOP
                // (do we have good card conmbination from our 2 cards and the floppef 4 cards)
                // иif we have already played aggresivly (all-in) we should check/call
                // if NOT god combination - we can check or fold
                // if strong combination we can put more agressiong and raise/all-in

                currentCards.Clear();
                currentCards.Add(this.FirstCard);
                currentCards.Add(this.SecondCard);
                currentCards.AddRange(this.CommunityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (this.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && this.GotTheStrongestHand(combination))
                    {
                        return this.AgressivePlayerAction(context, preFlopCards, combination, 7, 9);
                    }
                    else if (context.MoneyLeft > 0 && this.GotVeryStrongHand(combination))
                    {
                        return this.AgressivePlayerAction(context, preFlopCards, combination, 9, 11);
                    }
                    else
                    {
                        return this.AgressivePlayerAction(context, preFlopCards, combination, 5, 9);
                    }
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0 && context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return this.PassivePlayerAction(context, combination, 5, 9);
                    }

                    return CheckOrFoldCustomAction(context);
                }
            }
            else // GameRoundType.River (final card)
            {
                // TODO
                // add strong logic for FLOP
                // (do we have good card conmbination from our 2 cards and the floppef 5 cards)
                // иif we have already played aggresivly (all-in) we should check/call
                // if NOT god combination - we can check or fold
                // if strong combination we can put more agressiong and raise/all-in

                currentCards.Clear();
                currentCards.Add(this.FirstCard);
                currentCards.Add(this.SecondCard);
                currentCards.AddRange(this.CommunityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (this.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && this.GotTheStrongestHand(combination))
                    {
                        return this.AgressivePlayerAction(context, preFlopCards, combination, 7, 9);
                    }
                    else if (context.MoneyLeft > 0 && this.GotVeryStrongHand(combination))
                    {
                        return this.AgressivePlayerAction(context, preFlopCards, combination, 9, 11);
                    }
                    else
                    {
                        return this.AgressivePlayerAction(context, preFlopCards, combination, 5, 9);
                    }
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0 && context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return this.PassivePlayerAction(context, combination, 5, 9);
                    }

                    return CheckOrFoldCustomAction(context);
                }
            }
        }

        private PlayerAction SmallStackrMethod(GetTurnContext context, CardValueType preFlopCards)
        {
            List<Card> currentCards = new List<Card>();
            currentCards.Add(this.FirstCard);
            currentCards.Add(this.SecondCard);

            if (context.RoundType == GameRoundType.PreFlop)
            {
                if (context.MoneyLeft / context.SmallBlind > 50)
                {
                    if (!context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return AgressivePlayerActionPreflop(context, preFlopCards, 6, 8);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return PassivePlayerActionPreFlop(context, preFlopCards, 10);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return AgressivePlayerActionPreflop(context, preFlopCards, 7, 9);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (context.MoneyLeft / context.SmallBlind > 15 && (context.MoneyLeft / context.SmallBlind <= 50))
                {
                    if (!context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return AgressivePlayerActionPreflop(context, preFlopCards, 7, 10);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return PassivePlayerActionPreFlop(context, preFlopCards, 8);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return AgressivePlayerActionPreflop(context, preFlopCards, 7, 12);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (context.MoneyLeft / context.SmallBlind <= 15)
                {
                    if (!context.CanCheck && context.MyMoneyInTheRound == context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return AgressivePlayerActionPreflop(context, preFlopCards, 8, 12);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return PassivePlayerActionPreFlop(context, preFlopCards, 10);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return AgressivePlayerActionPreflop(context, preFlopCards, 9, 14);
                    }

                    return PlayerAction.CheckOrCall();
                }

                return CheckOrFoldCustomAction(context);

            }
            else if (context.RoundType == GameRoundType.Flop)
            {
                // TODO
                // add strong logic for FLOP
                // (do we have good card conmbination from our 2 cards and the floppef 3 cards)
                // иif we have already played aggresivly (all-in) we should check/call
                // if NOT god combination - we can check or fold
                // if strong combination we can put more agressiong and raise/all-in

                currentCards.AddRange(this.CommunityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (this.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && this.GotTheStrongestHand(combination))
                    {
                        return AgressivePlayerAction(context, preFlopCards, combination, 8, 10);
                    }
                    else if (context.MoneyLeft > 0 && this.GotVeryStrongHand(combination))
                    {
                        return AgressivePlayerAction(context, preFlopCards, combination, 10, 12);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0 && context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return PlayerAction.CheckOrCall();
                    }

                    return CheckOrFoldCustomAction(context);
                }
            }
            else if (context.RoundType == GameRoundType.Turn)
            {
                // TODO
                // add strong logic for FLOP
                // (do we have good card conmbination from our 2 cards and the floppef 4 cards)
                // иif we have already played aggresivly (all-in) we should check/call
                // if NOT god combination - we can check or fold
                // if strong combination we can put more agressiong and raise/all-in

                currentCards.Clear();
                currentCards.Add(this.FirstCard);
                currentCards.Add(this.SecondCard);
                currentCards.AddRange(this.CommunityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (this.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && this.GotTheStrongestHand(combination))
                    {
                        return AgressivePlayerAction(context, preFlopCards, combination, 8, 10);
                    }
                    else if (context.MoneyLeft > 0 && this.GotVeryStrongHand(combination))
                    {
                        return AgressivePlayerAction(context, preFlopCards, combination, 10, 12);
                    }
                    else
                    {
                        return AgressivePlayerAction(context, preFlopCards, combination, 6, 8);
                    }
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0 && context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return PlayerAction.CheckOrCall();
                    }

                    return CheckOrFoldCustomAction(context);
                }
            }
            else // GameRoundType.River (final card)
            {
                // TODO
                // add strong logic for FLOP
                // (do we have good card conmbination from our 2 cards and the floppef 5 cards)
                // иif we have already played aggresivly (all-in) we should check/call
                // if NOT god combination - we can check or fold
                // if strong combination we can put more agressiong and raise/all-in

                currentCards.Clear();
                currentCards.Add(this.FirstCard);
                currentCards.Add(this.SecondCard);
                currentCards.AddRange(this.CommunityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (this.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && this.GotTheStrongestHand(combination))
                    {
                        return AgressivePlayerAction(context, preFlopCards, combination, 8, 10);
                    }
                    else if (context.MoneyLeft > 0 && this.GotVeryStrongHand(combination))
                    {
                        return AgressivePlayerAction(context, preFlopCards, combination, 10, 12);
                    }
                    else
                    {
                        return AgressivePlayerAction(context, preFlopCards, combination, 6, 8);
                    }
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0 && context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return PlayerAction.CheckOrCall();
                    }

                    return CheckOrFoldCustomAction(context);
                }
            }
        }

        private PlayerAction BigStackMethod(GetTurnContext context, CardValueType preFlopCards)
        {
            List<Card> currentCards = new List<Card>();
            currentCards.Add(this.FirstCard);
            currentCards.Add(this.SecondCard);

            if (context.RoundType == GameRoundType.PreFlop)
            {
                if (context.MoneyLeft / context.SmallBlind > 50)
                {
                    if (!context.CanCheck && context.MyMoneyInTheRound == context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return AgressivePlayerActionPreflop(context, preFlopCards, 7, 9);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return PassivePlayerActionPreFlop(context, preFlopCards, 12);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return AgressivePlayerActionPreflop(context, preFlopCards, 8, 11);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (context.MoneyLeft / context.SmallBlind > 15 && (context.MoneyLeft / context.SmallBlind <= 50))
                {
                    if (!context.CanCheck && context.MyMoneyInTheRound == context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return AgressivePlayerActionPreflop(context, preFlopCards, 8, 11);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return PassivePlayerActionPreFlop(context, preFlopCards, 9);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return AgressivePlayerActionPreflop(context, preFlopCards, 8, 14);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (context.MoneyLeft / context.SmallBlind <= 15)
                {
                    if (!context.CanCheck && context.MyMoneyInTheRound == context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return AgressivePlayerActionPreflop(context, preFlopCards, 9, 14);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return PassivePlayerActionPreFlop(context, preFlopCards, 12);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return AgressivePlayerActionPreflop(context, preFlopCards, 10, 14);
                    }

                    return PlayerAction.CheckOrCall();
                }

                return CheckOrFoldCustomAction(context);

            }
            else if (context.RoundType == GameRoundType.Flop)
            {
                // TODO
                // add strong logic for FLOP
                // (do we have good card conmbination from our 2 cards and the floppef 3 cards)
                // иif we have already played aggresivly (all-in) we should check/call
                // if NOT god combination - we can check or fold
                // if strong combination we can put more agressiong and raise/all-in

                currentCards.AddRange(this.CommunityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (this.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && this.GotTheStrongestHand(combination))
                    {
                        return this.AgressivePlayerAction(context, preFlopCards, combination, 2, 12);
                    }
                    else if (context.MoneyLeft > 0 && this.GotVeryStrongHand(combination))
                    {
                        return this.AgressivePlayerAction(context, preFlopCards, combination, 2, 14);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0)
                    {
                        return this.PassivePlayerAction(context, combination, 6, 8);
                    }

                    return CheckOrFoldCustomAction(context);
                }

                return PlayerAction.CheckOrCall();

            }
            else if (context.RoundType == GameRoundType.Turn)
            {
                // TODO
                // add strong logic for FLOP
                // (do we have good card conmbination from our 2 cards and the floppef 4 cards)
                // иif we have already played aggresivly (all-in) we should check/call
                // if NOT god combination - we can check or fold
                // if strong combination we can put more agressiong and raise/all-in

                currentCards.Clear();
                currentCards.Add(this.FirstCard);
                currentCards.Add(this.SecondCard);
                currentCards.AddRange(this.CommunityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (this.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && this.GotTheStrongestHand(combination))
                    {
                        return this.AgressivePlayerAction(context, preFlopCards, combination, 2, 12);
                    }
                    else if (context.MoneyLeft > 0 && this.GotVeryStrongHand(combination))
                    {
                        return this.AgressivePlayerAction(context, preFlopCards, combination, 2, 15);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0)
                    {
                        return this.PassivePlayerAction(context, combination, 6, 8);
                    }

                    return CheckOrFoldCustomAction(context);
                }
            }
            else // GameRoundType.River (final card)
            {
                // TODO
                // add strong logic for FLOP
                // (do we have good card conmbination from our 2 cards and the floppef 5 cards)
                // иif we have already played aggresivly (all-in) we should check/call
                // if NOT god combination - we can check or fold
                // if strong combination we can put more agressiong and raise/all-in

                currentCards.Clear();
                currentCards.Add(this.FirstCard);
                currentCards.Add(this.SecondCard);
                currentCards.AddRange(this.CommunityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (this.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && this.GotTheStrongestHand(combination))
                    {
                        return this.AgressivePlayerAction(context, preFlopCards, combination, 2, 18);
                    }
                    else if (context.MoneyLeft > 0 && this.GotVeryStrongHand(combination))
                    {
                        return this.AgressivePlayerAction(context, preFlopCards, combination, 2, 15);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0)
                    {
                        return this.PassivePlayerAction(context, combination, 6, 8);
                    }

                    return CheckOrFoldCustomAction(context);
                }
            }
        }

        private PlayerAction AgressivePlayerActionPreflop(GetTurnContext context, CardValueType preFlopCards, int raiseAmount, int pushAmount)
        {
            if (!context.CanCheck && context.MoneyToCall == context.SmallBlind && preFlopCards != CardValueType.Unplayable)
            {
                if (preFlopCards == CardValueType.NotRecommended)
                {
                    if (context.MoneyLeft > 0)
                    {
                        if (context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount);
                        }
                        else if (context.CanCheck && context.MyMoneyInTheRound > context.SmallBlind)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (preFlopCards == CardValueType.Risky)
                {
                    if (context.MoneyLeft > 0)
                    {
                        if (context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount);
                        }
                        else if (context.CanCheck && context.MyMoneyInTheRound > context.SmallBlind)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else
                        {
                            return PlayerAction.CheckOrCall();
                        }
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (preFlopCards == CardValueType.Recommended)
                {
                    if (context.MoneyLeft > 0)
                    {
                        if (context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.SmallBlind * (raiseAmount + 2));
                        }
                        else if (context.CanCheck && context.MyMoneyInTheRound > context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.CurrentPot);
                        }
                        else
                        {
                            return PlayerAction.CheckOrCall();
                        }
                    }

                    return PlayerAction.CheckOrCall();
                }
            }
            else
            {
                if (preFlopCards == CardValueType.Unplayable)
                {
                    return CheckOrFoldCustomAction(context);
                }

                if (preFlopCards == CardValueType.NotRecommended)
                {
                    if (context.MoneyLeft > 0)
                    {
                        if (context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount);
                        }
                        else if (context.CanCheck && context.MyMoneyInTheRound > context.SmallBlind)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else if (!context.CanCheck && context.MoneyToCall < context.SmallBlind * 5)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (preFlopCards == CardValueType.Risky)
                {
                    if (context.MoneyLeft > 0)
                    {
                        if (context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.SmallBlind * raiseAmount);
                        }
                        else if(context.CanCheck && context.MyMoneyInTheRound > context.SmallBlind)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else if (!context.CanCheck && context.MoneyToCall < context.SmallBlind * 6)
                        {
                            return PlayerAction.CheckOrCall();
                        }
                        else
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                    }

                    return PlayerAction.CheckOrCall();
                }

                if (preFlopCards == CardValueType.Recommended)
                {
                    if (context.MoneyLeft > 0)
                    {
                        if (context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.SmallBlind * (raiseAmount + 2));
                        }
                        else if (context.CanCheck && context.MyMoneyInTheRound > context.SmallBlind)
                        {
                            return PlayerAction.Raise(context.CurrentPot);
                        }
                        else if (!context.CanCheck && context.MoneyToCall < context.SmallBlind * 6)
                        {
                            return PlayerAction.Raise(context.CurrentPot);
                        }
                        else
                        {
                            return PlayerAction.CheckOrCall();
                        }
                    }

                    return PlayerAction.CheckOrCall();
                }
            }

            return CheckOrFoldCustomAction(context);
        }

        private PlayerAction PassivePlayerActionPreFlop(GetTurnContext context, CardValueType preFlopCards, int pushAmount)
        {
            if ((!context.CanCheck && context.MoneyToCall == context.SmallBlind) && preFlopCards != CardValueType.Unplayable)
            {
                if (preFlopCards == CardValueType.Risky || preFlopCards == CardValueType.NotRecommended)
                {
                    if (context.MoneyLeft > 0 && context.MoneyToCall <= context.SmallBlind * 6)
                    {
                        return PlayerAction.CheckOrCall();
                    }

                    return CheckOrFoldCustomAction(context);
                }

                if (preFlopCards == CardValueType.Recommended)
                {
                    if (context.MoneyLeft > 0 && context.MyMoneyInTheRound <= context.SmallBlind * 20)
                    {
                        return PlayerAction.Raise(context.SmallBlind * pushAmount);
                    }
                    else if (context.MoneyLeft > 0 && context.MyMoneyInTheRound > context.SmallBlind * 20)
                    {
                        return PlayerAction.Raise(context.MoneyLeft);
                    }

                    return PlayerAction.CheckOrCall();
                }
            }
            else
            {
                if (preFlopCards == CardValueType.Unplayable)
                {
                    return CheckOrFoldCustomAction(context);
                }

                if (preFlopCards == CardValueType.Risky || preFlopCards == CardValueType.NotRecommended)
                {
                    if (context.MoneyLeft > 0 && context.MoneyToCall <= context.SmallBlind * 6)
                    {
                        return PlayerAction.CheckOrCall();
                    }

                    return CheckOrFoldCustomAction(context);
                }

                if (preFlopCards == CardValueType.Recommended)
                {
                    if (context.MoneyLeft > 0 && context.MyMoneyInTheRound <= context.SmallBlind * 20)
                    {
                        return PlayerAction.Raise(context.SmallBlind * pushAmount);
                    }
                    else if (context.MoneyLeft > 0 && context.MyMoneyInTheRound > context.SmallBlind * 20)
                    {
                        return PlayerAction.Raise(context.MoneyLeft);
                    }

                    return PlayerAction.CheckOrCall();
                }
            }

            return CheckOrFoldCustomAction(context);
        }

        private PlayerAction AgressivePlayerAction(GetTurnContext context,CardValueType preFlopCards, HandRankType combination, int potMultiplier, int raiseSbMultipliyer)
        {
            if (preFlopCards == CardValueType.Recommended)
            {
                if (this.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && this.GotTheStrongestHand(combination))
                    {
                        if (!context.CanCheck
                            && (context.MoneyToCall > context.CurrentPot / 2 || context.MoneyToCall > context.SmallBlind * 14)
                            && context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                        else if (context.MoneyLeft >= context.SmallBlind * raiseSbMultipliyer)
                        {
                            return PlayerAction.Raise(context.CurrentPot * potMultiplier);
                        }
                        else
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                    }
                    else if (context.MoneyLeft > 0 && this.GotVeryStrongHand(combination))
                    {
                        if (!context.CanCheck
                            && (context.MoneyToCall > context.CurrentPot / 2 * 3 || context.MoneyToCall > context.SmallBlind * 20)
                            && context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                        else if (context.MoneyLeft >= context.SmallBlind * raiseSbMultipliyer)
                        {
                            return PlayerAction.Raise(context.CurrentPot * potMultiplier);
                        }
                        else
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                    }
                    else
                    {
                        if (context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.SmallBlind * 6);
                        }
                        else
                        {
                            return PlayerAction.CheckOrCall();
                        }
                    }
                }
                else
                {
                    if (context.CanCheck && context.MoneyLeft > 0)
                    {
                        return PlayerAction.Raise((context.SmallBlind * raiseSbMultipliyer) - context.SmallBlind);
                    }
                    else if (context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return PlayerAction.CheckOrCall();
                    }

                    return CheckOrFoldCustomAction(context);
                }
            }
            else if (preFlopCards == CardValueType.NotRecommended || preFlopCards == CardValueType.Risky)
            {
                if (this.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && this.GotTheStrongestHand(combination))
                    {
                        if (!context.CanCheck
                            && (context.MoneyToCall > context.SmallBlind * 10)
                            && context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                        else if (context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.CurrentPot * potMultiplier);
                        }
                        else
                        {
                            return PlayerAction.CheckOrCall();
                        }
                    }
                    else if (context.MoneyLeft > 0 && this.GotVeryStrongHand(combination))
                    {
                        if (!context.CanCheck
                            && (context.MoneyToCall > context.CurrentPot / 2 * 3 || context.MoneyToCall > context.SmallBlind * 20)
                            && context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                        else if (context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.CurrentPot * potMultiplier);
                        }
                        else
                        {
                            return PlayerAction.CheckOrCall();
                        }
                    }
                    else
                    {
                        if (context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.SmallBlind * 6);
                        }
                        else
                        {
                            return CheckOrFoldCustomAction(context);
                        }
                    }
                }
                else
                {
                    if (context.CanCheck && context.MoneyLeft > 0)
                    {
                        return PlayerAction.CheckOrCall();
                    }
                    else if (context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return PlayerAction.CheckOrCall();
                    }

                    return PlayerAction.CheckOrCall();
                }
            }
            else
            {
                if (this.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && this.GotTheStrongestHand(combination))
                    {
                        if (!context.CanCheck
                            && (context.MoneyToCall > context.CurrentPot / 2 || context.MoneyToCall > context.SmallBlind * 14)
                            && context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                        else if (context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.CurrentPot * potMultiplier);
                        }
                        else
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                    }
                    else if (context.MoneyLeft > 0 && this.GotVeryStrongHand(combination))
                    {
                        if (!context.CanCheck
                            && (context.MoneyToCall > context.CurrentPot / 2 * 3 || context.MoneyToCall > context.SmallBlind * 20)
                            && context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                        else if (context.MoneyLeft >= context.SmallBlind * raiseSbMultipliyer)
                        {
                            return PlayerAction.Raise(context.CurrentPot * potMultiplier);
                        }
                        else
                        {
                            return PlayerAction.Raise(context.MoneyLeft);
                        }
                    }
                    else
                    {
                        if (context.MoneyLeft > 0)
                        {
                            return PlayerAction.Raise(context.SmallBlind * 6);
                        }
                        else
                        {
                            return PlayerAction.CheckOrCall();
                        }
                    }
                }
                else
                {
                    if (context.MoneyToCall <= context.SmallBlind * 2 && context.MoneyLeft > 0)
                    {
                        return PlayerAction.CheckOrCall();
                    }
                    return CheckOrFoldCustomAction(context);
                }
            }
        }

        private PlayerAction PassivePlayerAction(GetTurnContext context, HandRankType combination,int raiseAmount, int pushAmount)
        {
            if (this.GotStrongHand(combination))
            {
                if (context.MoneyLeft > 0 && this.GotTheStrongestHand(combination))
                {
                    if (!context.CanCheck
                        && (context.MoneyToCall > context.CurrentPot / 2 || context.MoneyToCall > context.SmallBlind * 14)
                        && context.MoneyLeft > 0)
                    {
                        return PlayerAction.Raise(context.MoneyLeft);
                    }
                    else if (context.MoneyLeft >= context.SmallBlind * pushAmount)
                    {
                        return PlayerAction.Raise(context.SmallBlind * pushAmount);
                    }
                    else
                    {
                        return PlayerAction.Raise(context.MoneyLeft);
                    }
                }
                else if (context.MoneyLeft > 0 && this.GotVeryStrongHand(combination))
                {
                    if (!context.CanCheck
                        && (context.MoneyToCall > context.CurrentPot / 2 * 3 || context.MoneyToCall > context.SmallBlind * 20)
                        && context.MoneyLeft > 0)
                    {
                        return PlayerAction.Raise(context.MoneyLeft);
                    }
                    else if (context.MoneyLeft >= context.SmallBlind * raiseAmount)
                    {
                        return PlayerAction.Raise(context.SmallBlind * raiseAmount);
                    }
                    else
                    {
                        return PlayerAction.Raise(context.MoneyLeft);
                    }
                }
                else
                {
                    if (context.MoneyLeft > 0)
                    {
                        return PlayerAction.Raise(context.SmallBlind * 6);
                    }
                    else
                    {
                        return PlayerAction.CheckOrCall();
                    }
                }
            }
            else
            {
                if (context.CanCheck && context.MoneyLeft > 0)
                {
                    return PlayerAction.Raise((context.SmallBlind * raiseAmount) - context.SmallBlind);
                }
                else if (context.MoneyToCall <= context.SmallBlind * 2)
                {
                    return PlayerAction.CheckOrCall();
                }

                return CheckOrFoldCustomAction(context);
            }
        }

        private static PlayerAction CheckOrFoldCustomAction(GetTurnContext context)
        {
            if (context.CanCheck)
            {
                return PlayerAction.CheckOrCall();
            }
            else if (!context.CanCheck)
            {
                if (context.CurrentPot < context.SmallBlind * 5)
                {
                    return PlayerAction.CheckOrCall();
                }
                return PlayerAction.Fold();
            }
            else
            {
                return PlayerAction.Fold();
            }
        }

        public bool GotAceHighCardPreFlop(HandRankType combination, GetTurnContext context)
        {
            var preFlopCards = CustomHandEvaluator.PreFlop(context, this.FirstCard, this.SecondCard);

            return combination == HandRankType.HighCard &&
                (this.FirstCard.Type == CardType.Ace || this.SecondCard.Type == CardType.Ace);
        }

        public bool GotSuitedCardsCardPreFlop(HandRankType combination, GetTurnContext context)
        {
            var preFlopCards = CustomHandEvaluator.PreFlop(context, this.FirstCard, this.SecondCard);

            return this.FirstCard.Suit == this.SecondCard.Suit;
        }

        private bool GotStrongHand(HandRankType combination)
        {
            return combination == HandRankType.Pair ||
                    combination == HandRankType.TwoPairs ||
                    combination == HandRankType.ThreeOfAKind ||
                    combination == HandRankType.Straight ||
                    combination == HandRankType.Flush ||
                    combination == HandRankType.FullHouse ||
                    combination == HandRankType.FourOfAKind ||
                    combination == HandRankType.StraightFlush;
        }

        private bool GotVeryStrongHand(HandRankType combination)
        {
            return combination == HandRankType.ThreeOfAKind ||
                    combination == HandRankType.Straight ||
                    combination == HandRankType.Flush ||
                    combination == HandRankType.FullHouse ||
                    combination == HandRankType.FourOfAKind ||
                    combination == HandRankType.StraightFlush;
        }

        private bool GotTheStrongestHand(HandRankType combination)
        {
            return combination == HandRankType.Flush ||
                    combination == HandRankType.FullHouse ||
                    combination == HandRankType.FourOfAKind ||
                    combination == HandRankType.StraightFlush;
        }

        private int PercentagePerToMakeCombination()
        {
            return -1;
            // TODO method to calculate the chance to make good combination

            //  combination == HandRankType.Pair ||
            //  combination == HandRankType.TwoPairs ||
            //  combination == HandRankType.ThreeOfAKind ||
            //  combination == HandRankType.Straight ||
            //  combination == HandRankType.Flush ||
            //  combination == HandRankType.FullHouse ||
            //  combination == HandRankType.FourOfAKind ||
            //  combination == HandRankType.StraightFlush;
        }

    }
}
