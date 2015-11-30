namespace TexasHoldem.AI.SpartaPlayer.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Spartalayer.Helpers;
    using TexasHoldem.Logic;
    using TexasHoldem.Logic.Cards;
    using TexasHoldem.Logic.Players;

    public static class CustomStackActions
    {
        public static PlayerAction NormalStackMethod(GetTurnContext context, CardValueType preFlopCards, Card firstCard, Card secondCard, IReadOnlyCollection<Card> communityCards)
        {
            List<Card> currentCards = new List<Card>();
            currentCards.Add(firstCard);
            currentCards.Add(secondCard);

            if (context.RoundType == GameRoundType.PreFlop)
            {
                if (context.MoneyLeft / context.SmallBlind > 50)
                {
                    if (!context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 8, 12);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return CustomPlayerActions.PassivePlayerActionPreFlop(context, preFlopCards, 12);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 10, 12);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (context.MoneyLeft / context.SmallBlind > 15 && (context.MoneyLeft / context.SmallBlind <= 50))
                {
                    if (!context.CanCheck && context.MoneyToCall <= context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 6, 9);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return CustomPlayerActions.PassivePlayerActionPreFlop(context, preFlopCards, 12);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 8, 14);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (context.MoneyLeft / context.SmallBlind <= 15)
                {
                    if (!context.CanCheck && context.MoneyToCall <= context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 8, 14);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return CustomPlayerActions.PassivePlayerActionPreFlop(context, preFlopCards, 14);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 8, 14);
                    }

                    return PlayerAction.CheckOrCall();
                }

                return CustomPlayerActions.CheckOrFoldCustomAction(context);

            }
            else if (context.RoundType == GameRoundType.Flop)
            {
                // TODO
                // add strong logic for FLOP
                // (do we have good card conmbination from our 2 cards and the floppef 3 cards)
                // иif we have already played aggresivly (all-in) we should check/call
                // if NOT god combination - we can check or fold
                // if strong combination we can put more agressiong and raise/all-in

                currentCards.AddRange(communityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (CustomHandStreightChecks.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotTheStrongestHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 7, 9);
                    }
                    else if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotVeryStrongHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 9, 11);
                    }
                    else
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 5, 9);
                    }
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0 && context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return CustomPlayerActions.PassivePlayerAction(context, combination, 5, 9);
                    }

                    return CustomPlayerActions.CheckOrFoldCustomAction(context);
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
                currentCards.Add(firstCard);
                currentCards.Add(secondCard);
                currentCards.AddRange(communityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (CustomHandStreightChecks.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotTheStrongestHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 7, 9);
                    }
                    else if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotVeryStrongHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 9, 11);
                    }
                    else
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 5, 9);
                    }
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0 && context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return CustomPlayerActions.PassivePlayerAction(context, combination, 5, 9);
                    }

                    return CustomPlayerActions.CheckOrFoldCustomAction(context);
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
                currentCards.Add(firstCard);
                currentCards.Add(secondCard);
                currentCards.AddRange(communityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (CustomHandStreightChecks.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotTheStrongestHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 7, 9);
                    }
                    else if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotVeryStrongHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 9, 11);
                    }
                    else
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 5, 9);
                    }
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0 && context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return CustomPlayerActions.PassivePlayerAction(context, combination, 5, 9);
                    }

                    return CustomPlayerActions.CheckOrFoldCustomAction(context);
                }
            }
        }

        public static PlayerAction SmallStackrMethod(GetTurnContext context, CardValueType preFlopCards, Card firstCard, Card secondCard, IReadOnlyCollection<Card> communityCards)
        {
            List<Card> currentCards = new List<Card>();
            currentCards.Add(firstCard);
            currentCards.Add(secondCard);

            if (context.RoundType == GameRoundType.PreFlop)
            {
                if (context.MoneyLeft / context.SmallBlind > 50)
                {
                    if (!context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 6, 8);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return CustomPlayerActions.PassivePlayerActionPreFlop(context, preFlopCards, 10);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 7, 9);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (context.MoneyLeft / context.SmallBlind > 15 && (context.MoneyLeft / context.SmallBlind <= 50))
                {
                    if (!context.CanCheck && context.MyMoneyInTheRound <= context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 7, 10);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return CustomPlayerActions.PassivePlayerActionPreFlop(context, preFlopCards, 8);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 7, 12);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (context.MoneyLeft / context.SmallBlind <= 15)
                {
                    if (!context.CanCheck && context.MyMoneyInTheRound == context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 8, 12);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return CustomPlayerActions.PassivePlayerActionPreFlop(context, preFlopCards, 10);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 9, 14);
                    }

                    return PlayerAction.CheckOrCall();
                }

                return CustomPlayerActions.CheckOrFoldCustomAction(context);

            }
            else if (context.RoundType == GameRoundType.Flop)
            {
                // TODO
                // add strong logic for FLOP
                // (do we have good card conmbination from our 2 cards and the floppef 3 cards)
                // иif we have already played aggresivly (all-in) we should check/call
                // if NOT god combination - we can check or fold
                // if strong combination we can put more agressiong and raise/all-in

                currentCards.AddRange(communityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (CustomHandStreightChecks.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotTheStrongestHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 8, 10);
                    }
                    else if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotVeryStrongHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 10, 12);
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

                    return CustomPlayerActions.CheckOrFoldCustomAction(context);
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
                currentCards.Add(firstCard);
                currentCards.Add(secondCard);
                currentCards.AddRange(communityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (CustomHandStreightChecks.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotTheStrongestHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 8, 10);
                    }
                    else if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotVeryStrongHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 10, 12);
                    }
                    else
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 6, 8);
                    }
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0 && context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return PlayerAction.CheckOrCall();
                    }

                    return CustomPlayerActions.CheckOrFoldCustomAction(context);
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
                currentCards.Add(firstCard);
                currentCards.Add(secondCard);
                currentCards.AddRange(communityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (CustomHandStreightChecks.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotTheStrongestHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 8, 10);
                    }
                    else if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotVeryStrongHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 10, 12);
                    }
                    else
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 6, 8);
                    }
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0 && context.MoneyToCall <= context.SmallBlind * 2)
                    {
                        return PlayerAction.CheckOrCall();
                    }

                    return CustomPlayerActions.CheckOrFoldCustomAction(context);
                }
            }
        }

        public static PlayerAction BigStackMethod(GetTurnContext context, CardValueType preFlopCards, Card firstCard, Card secondCard, IReadOnlyCollection<Card> communityCards)
        {
            List<Card> currentCards = new List<Card>();
            currentCards.Add(firstCard);
            currentCards.Add(secondCard);

            if (context.RoundType == GameRoundType.PreFlop)
            {
                if (context.MoneyLeft / context.SmallBlind > 50)
                {
                    if (!context.CanCheck && context.MyMoneyInTheRound == context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 7, 9);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return CustomPlayerActions.PassivePlayerActionPreFlop(context, preFlopCards, 12);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 8, 11);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (context.MoneyLeft / context.SmallBlind > 15 && (context.MoneyLeft / context.SmallBlind <= 50))
                {
                    if (!context.CanCheck && context.MyMoneyInTheRound == context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 8, 11);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return CustomPlayerActions.PassivePlayerActionPreFlop(context, preFlopCards, 9);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 8, 14);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else if (context.MoneyLeft / context.SmallBlind <= 15)
                {
                    if (!context.CanCheck && context.MyMoneyInTheRound == context.SmallBlind)
                    {
                        // we are first and we can paid SmallBlind , can Raise and can Fold
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 9, 14);
                    }
                    else if (!context.CanCheck && context.MoneyToCall > context.SmallBlind)
                    {
                        // oppponent is first and has raised with moneyToCall - opponent has raised pre-flop
                        // we can Re-Raise (very strong hand only) - we can call (Verystrong or string) - we Fold
                        return CustomPlayerActions.PassivePlayerActionPreFlop(context, preFlopCards, 12);
                    }
                    else if (context.CanCheck)
                    {
                        // opponet is first and he has paid SmallBlind
                        // we can check or raise here
                        return CustomPlayerActions.AgressivePlayerActionPreflop(context, preFlopCards, 10, 14);
                    }

                    return PlayerAction.CheckOrCall();
                }

                return CustomPlayerActions.CheckOrFoldCustomAction(context);

            }
            else if (context.RoundType == GameRoundType.Flop)
            {
                // TODO
                // add strong logic for FLOP
                // (do we have good card conmbination from our 2 cards and the floppef 3 cards)
                // иif we have already played aggresivly (all-in) we should check/call
                // if NOT god combination - we can check or fold
                // if strong combination we can put more agressiong and raise/all-in

                currentCards.AddRange(communityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (CustomHandStreightChecks.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotTheStrongestHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 2, 12);
                    }
                    else if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotVeryStrongHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 2, 14);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0)
                    {
                        return CustomPlayerActions.PassivePlayerAction(context, combination, 6, 8);
                    }

                    return CustomPlayerActions.CheckOrFoldCustomAction(context);
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
                currentCards.Add(firstCard);
                currentCards.Add(secondCard);
                currentCards.AddRange(communityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (CustomHandStreightChecks.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotTheStrongestHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 2, 12);
                    }
                    else if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotVeryStrongHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 2, 15);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0)
                    {
                        return CustomPlayerActions.PassivePlayerAction(context, combination, 6, 8);
                    }

                    return CustomPlayerActions.CheckOrFoldCustomAction(context);
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
                currentCards.Add(firstCard);
                currentCards.Add(secondCard);
                currentCards.AddRange(communityCards);

                var combination = Logic.Helpers.Helpers.GetHandRank(currentCards);

                if (CustomHandStreightChecks.GotStrongHand(combination))
                {
                    if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotTheStrongestHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 2, 18);
                    }
                    else if (context.MoneyLeft > 0 && CustomHandStreightChecks.GotVeryStrongHand(combination))
                    {
                        return CustomPlayerActions.AgressivePlayerAction(context, preFlopCards, combination, 2, 15);
                    }

                    return PlayerAction.CheckOrCall();
                }
                else
                {
                    // TODO add here a method to see if we have chance to make good hand and add logic
                    if (context.MoneyLeft > 0)
                    {
                        return CustomPlayerActions.PassivePlayerAction(context, combination, 6, 8);
                    }

                    return CustomPlayerActions.CheckOrFoldCustomAction(context);
                }
            }
        }

    }
}