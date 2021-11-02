using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Yahtzee
{
    public partial class MainPage : ContentPage
    {
        //Constant Variables
        const string ROLL_DICE = "Roll Dice";
        const string NEW_GAME = "";
        const string END_GO = "End of Go";

        //Variables
        int clicks = 0, count = 0; //To End Turn after 3 goes
        int diceRoll1, diceRoll2, diceRoll3, diceRoll4, diceRoll5; //DiceRoll Variables
        int dieSelect1 = 0, dieSelect2 = 0, dieSelect3 = 0, dieSelect4 = 0, dieSelect5 = 0; //If a die selected, it wont change
        int ones, twos, threes, fours, fives, sixes; //Variables for keeping track of scores of upper section
        int upSubtotal, upBonus = 35, upTOTAL, lowerTotal = 0, grandTOTAL = 0; //Totals
        int kind3, kind4, chance, yahBonus = 0, fullHouse, smStr, lgStr, yahtzee; //Variables for keeping track of scores of lower section

        //Create instance of random
        Random random;

        //Initialize
        public MainPage()
        {
            //Initialize
            InitializeComponent();

            //Show Game Board
            GameBoard.IsVisible = true;
            ScoreSheet.IsVisible = false;

            //Hide Buttons
            MakeInvisable(0, 0, 0);
            Btn_RollDice.IsVisible = false;

            //Set count variable to 2
            count = 2;
        }
        
        //----------------------------------------------------------------------------------------------------------------------------------------

        //Start New Game
        private void SetUpNewGame()
        {
            //Make all buttons invisible
            MakeInvisable(0,0,0);

            //Show Gameboard
            GameBoard.IsVisible = true;
            ScoreSheet.IsVisible = false;

            //Make sure counter has reset
            clicks = 0;
            count = 0;

            //Change button Text
            Btn_RollDice.IsVisible = true;
            Btn_RollDice.IsEnabled = true;
            Btn_RollDice.Text = "Roll Dice";

            //Change Info
            DiceArea.Text = "Click Roll Dice";

            //Reset Variables to 0
            LblBtnFullHouseSelected.Text = "";
            LblBtnYAHTZEESelected.Text = "";

            //Set all values to 0
            Btn_Ones.IsEnabled = true;
            Btn_Twos.IsEnabled = true;
            Btn_Threes.IsEnabled = true;
            Btn_Fours.IsEnabled = true;
            Btn_Fives.IsEnabled = true;
            Btn_Sixes.IsEnabled = true;
            Btn_3Kind.IsEnabled = true;
            Btn_4Kind.IsEnabled = true;
            Btn_FullHouse.IsEnabled = true;
            Btn_SmStraight.IsEnabled = true;
            Btn_LgStraight.IsEnabled = true;
            Btn_YAHTZEE.IsEnabled = true;
            Btn_Chance.IsEnabled = true;
            ScoreTracker(0, 0, 0, 0, 0, 0);

            //Set all Button Text to Select
            Btn_Ones.Text = "Select";
            Btn_Twos.Text = "Select";
            Btn_Threes.Text = "Select";
            Btn_Fours.Text = "Select";
            Btn_Fives.Text = "Select";
            Btn_Sixes.Text = "Select";
            Btn_3Kind.Text = "Select";
            Btn_4Kind.Text = "Select";
            Btn_FullHouse.Text = "Select";
            Btn_SmStraight.Text = "Select";
            Btn_LgStraight.Text = "Select";
            Btn_YAHTZEE.Text = "Select";
            Btn_Chance.Text = "Select";
        }
        
        //Start a new game while Already Plaing
        private void NewGame_Clicked(object sender, EventArgs e)
        {
            //Used to make sure player wants to restart
            if (grandTOTAL > 0)
            {
                //Ask Again
                if (count == 0)
                {
                    //Check if Player again
                    NewGame.Text = "You Sure?";
                    count = 1;
                }

                //Ask a Final Time
                else if (count == 1)
                {
                    NewGame.Text = "Restart?";
                    //art Game
                    count = 2;
                }

                //Restart
                else if (count == 2)
                {
                    //Send to Other Method
                    SetUpNewGame();
                }
            }

            //If Game hasnt started start a new one
            else
            {
                SetUpNewGame();
            }
        }
        
        //-----------------------------------------------------------------------------------------------------------------------------------------

        //End of Go- after 3 rolls
        private void EndGo()
        {
            //Change Text
            DiceArea.Text = "Select Score to Continue";

            //Change Text of Button
            Btn_RollDice.Text = "End of Go";

            //Disable Button 
            Btn_RollDice.IsEnabled = false;

            //Reset Counter
            clicks = 0;

            //Make Buttons Visible
            MakeInvisable(1,0,1);
        }

        //Game Over
        private void EndOfGame()
        {
            //Hide Gameboard and Show Score Sheet
            GameBoard.IsVisible = false;
            ScoreSheet.IsVisible = true;

            //Set Labels on Score Sheet
            UpperSection.Text = upTOTAL.ToString();
            LowerSection.Text = lowerTotal.ToString();
            BonusGot.Text = upBonus.ToString();
            Total.Text = grandTOTAL.ToString();

            //Average Score
            if(grandTOTAL >= 200)
            {
                Message.Text = "Congratulations you have scored higher than the average player!!!!!";
            }
            else
            {
                Message.Text = "Well Played! However you didnt score above the average player :(";
            }
        }

        //Button on Score Sheet For new game clicked
        private void GameOver_Clicked(object sender, EventArgs e)
        {
            SetUpNewGame();
        }
        
        //----------------------------------------------------------------------------------------------------------------------------------------
        
        //New Go
        private void NewGo()
        {
            //Game Over when everything selected
            if (Btn_Ones.IsEnabled == false && Btn_Twos.IsEnabled == false &&
               Btn_Threes.IsEnabled == false && Btn_Fours.IsEnabled == false &&
               Btn_Fives.IsEnabled == false && Btn_Sixes.IsEnabled == false &&
               Btn_3Kind.IsEnabled == false && Btn_4Kind.IsEnabled == false &&
               Btn_FullHouse.IsEnabled == false && Btn_SmStraight.IsEnabled == false &&
               Btn_LgStraight.IsEnabled == false && Btn_YAHTZEE.IsEnabled == false &&
               Btn_Chance.IsEnabled == false)
            {
                EndOfGame();
            }

            //Change Text of Button
            Btn_RollDice.Text = "Roll Dice";

            //Enable Button 
            Btn_RollDice.IsEnabled = true;

            //Reset unused Scores
            int die1 = 0, die2 = 0, die3 = 0, die4 = 0, die5 = 0;
            ScoreTracker(die1, die2, die3, die4, die5, 1);

            //Make Buttons invisible again
            MakeInvisable(0,0,1);

            //Deselect all Dice
            dieSelect1 = 0;
            Dice1Select.Text = "Select";
            dieSelect2 = 0;
            Dice2Select.Text = "Select";
            dieSelect3 = 0;
            Dice3Select.Text = "Select";
            dieSelect4 = 0;
            Dice4Select.Text = "Select";
            dieSelect5 = 0;
            Dice5Select.Text = "Select";
        }

        //----------------------------------------------------------------------------------------------------------
        //Make select buttons visable/invisable
        private void MakeInvisable(int decide, int select, int die)
        {
            //Hide Buttons
            if (decide == 0)
            {
                Btn_Ones.IsVisible = false;
                Btn_Twos.IsVisible = false;
                Btn_Threes.IsVisible = false;
                Btn_Fours.IsVisible = false;
                Btn_Fives.IsVisible = false;
                Btn_Sixes.IsVisible = false;
                Btn_3Kind.IsVisible = false;
                Btn_4Kind.IsVisible = false;
                Btn_FullHouse.IsVisible = false;
                Btn_SmStraight.IsVisible = false;
                Btn_LgStraight.IsVisible = false;
                Btn_YAHTZEE.IsVisible = false;
                Btn_Chance.IsVisible = false;
            }

            //Show Buttons if enabled
            else
            {
                //If Conditions met make button visible
                if (Btn_Ones.IsEnabled == true)
                {
                    Btn_Ones.IsVisible = true;
                }

                if (Btn_Twos.IsEnabled == true)
                {
                    Btn_Twos.IsVisible = true;
                }

                if (Btn_Threes.IsEnabled == true)
                {
                    Btn_Threes.IsVisible = true;
                }

                if (Btn_Fours.IsEnabled == true)
                {
                    Btn_Fours.IsVisible = true;
                }

                if (Btn_Fives.IsEnabled == true)
                {
                    Btn_Fives.IsVisible = true;
                }

                if (Btn_Sixes.IsEnabled == true)
                {
                    Btn_Sixes.IsVisible = true;
                }

                if (Btn_3Kind.IsEnabled == true)
                {
                    Btn_3Kind.IsVisible = true;
                }

                if (Btn_4Kind.IsEnabled == true)
                {
                    Btn_4Kind.IsVisible = true;
                }

                if (Btn_FullHouse.IsEnabled == true)
                {
                    Btn_FullHouse.IsVisible = true;
                }

                if (Btn_SmStraight.IsEnabled == true)
                {
                    Btn_SmStraight.IsVisible = true;
                }

                if (Btn_LgStraight.IsEnabled == true)
                {
                    Btn_LgStraight.IsVisible = true;
                }

                if (Btn_YAHTZEE.IsEnabled == true)
                {
                    Btn_YAHTZEE.IsVisible = true;
                }

                if (Btn_Chance.IsEnabled == true)
                {
                    Btn_Chance.IsVisible = true;
                }
            }

            //---------------------------------------------
            //Hide Select Die
            if (select == 0)
            {
                //Make Select Die InVisible
                Dice1Select.IsVisible = false;
                Dice2Select.IsVisible = false;
                Dice3Select.IsVisible = false;
                Dice4Select.IsVisible = false;
                Dice5Select.IsVisible = false;
            }

            else
            {
                //Make Select Die InVisible
                Dice1Select.IsVisible = true;
                Dice2Select.IsVisible = true;
                Dice3Select.IsVisible = true;
                Dice4Select.IsVisible = true;
                Dice5Select.IsVisible = true;
            }

            //---------------------------------------------
            //Hide Die
            if (die == 0)
            {
                //Make Dice inVisible
                DiceRoll1.IsVisible = false;
                DiceRoll2.IsVisible = false;
                DiceRoll3.IsVisible = false;
                DiceRoll4.IsVisible = false;
                DiceRoll5.IsVisible = false;
            }

            else
            {
                //Make Dice inVisible
                DiceRoll1.IsVisible = true;
                DiceRoll2.IsVisible = true;
                DiceRoll3.IsVisible = true;
                DiceRoll4.IsVisible = true;
                DiceRoll5.IsVisible = true;
            }
        }
        
        //----------------------------------------------------------------------------------------------------

        //RollDice
        private void Btn_RollDice_Clicked(object sender, EventArgs e)
        {
            //Update Counter for 3 goes
            clicks++;

            //Read Button Text
            string ButtonText = ((Button)sender).Text;

            //Switch to change objective depending on button text
            switch (ButtonText)
            {
                //If Text is to roll Dice
                case ROLL_DICE:
                    {
                        //Roll Dice
                        RollDice(clicks);
                        break;
                    }

                //If New Game is Text
                case END_GO:
                    {
                        //Start new Go
                        NewGo();
                        break;
                    }

                //If New Game is Text
                case NEW_GAME:
                    {
                        //Make Invisible
                        Btn_RollDice.IsVisible = false;
                        break;
                    }
            }
        }
        
        //Roll Dice 3 Times
        private void RollDice(int i)
        {
            //Display Roll Number
            DiceArea.Text = "Roll #" + i;

            //Show Select Die Buttons
            MakeInvisable(0,1,1);

            //If Random not initialized
            if (random == null)
            {
                random = new Random();
            }

            //Check if Dice are Selected
            //Dice 1
            if (dieSelect1 == 0)
            {
                //Generate a random number between 1 and 6
                diceRoll1 = random.Next(1, 7);
            }

            //Dice 2
            if (dieSelect2 == 0)
            {
                //Generate a random number between 1 and 6
                diceRoll2 = random.Next(1, 7);
            }

            //Dice 3
            if (dieSelect3 == 0)
            {
                //Generate a random number between 1 and 6
                diceRoll3 = random.Next(1, 7);
            }

            //Dice 4
            if (dieSelect4 == 0)
            {
                //Generate a random number between 1 and 6
                diceRoll4 = random.Next(1, 7);
            }

            //Dice 5
            if (dieSelect5 == 0)
            {
                //Generate a random number between 1 and 6
                diceRoll5 = random.Next(1, 7);
            }

            //---------------------------------------------------------------------------------------------------------------------
            
            //Display Dice
            DisplayDice(diceRoll1, 1);
            DisplayDice(diceRoll2, 2);
            DisplayDice(diceRoll3, 3);
            DisplayDice(diceRoll4, 4);
            DisplayDice(diceRoll5, 5);

            //Score
            ScoreTracker(diceRoll1, diceRoll2, diceRoll3, diceRoll4, diceRoll5, 1);

            if (i >= 3)
            {
                EndGo();
            }
        }
        
        //Score Display
        private void ScoreTracker(int die1, int die2, int die3, int die4, int die5, int newGame)
        {
            //Ones
            if (Btn_Ones.IsEnabled == true)
            {
                LblBtnOneSelected.Text = "";
                ones = 0;
                //Ones
                if (die1 == 1)
                {
                    ones += 1;
                }

                if (die2 == 1)
                {
                    ones += 1;
                }

                if (die3 == 1)
                {
                    ones += 1;
                }

                if (die4 == 1)
                {
                    ones += 1;
                }

                if (die5 == 1)
                {
                    ones += 1;
                }
                LblBtnOne.Text = ones.ToString();
            }
            //-------------------------
            //Twos
            if (Btn_Twos.IsEnabled == true)
            {
                LblBtnTwoSelected.Text = "";
                twos = 0;
                if (die1 == 2)
                {
                    twos += 2;
                }

                if (die2 == 2)
                {
                    twos += 2;
                }

                if (die3 == 2)
                {
                    twos += 2;
                }

                if (die4 == 2)
                {
                    twos += 2;
                }

                if (die5 == 2)
                {
                    twos += 2;
                }
                LblBtnTwo.Text = twos.ToString();
            }

            //-------------------------
            //Threes
            if (Btn_Threes.IsEnabled == true)
            {
                LblBtnThreeSelected.Text = "";
                threes = 0;
                if (die1 == 3)
                {
                    threes += 3;
                }

                if (die2 == 3)
                {
                    threes += 3;
                }

                if (die3 == 3)
                {
                    threes += 3;
                }

                if (die4 == 3)
                {
                    threes += 3;
                }

                if (die5 == 3)
                {
                    threes += 3;
                }
                LblBtnThree.Text = threes.ToString();
            }

            //-------------------------
            //Fours
            if (Btn_Fours.IsEnabled == true)
            {
                LblBtnFourSelected.Text = "";
                fours = 0;
                if (die1 == 4)
                {
                    fours += 4;
                }

                if (die2 == 4)
                {
                    fours += 4;
                }

                if (die3 == 4)
                {
                    fours += 4;
                }

                if (die4 == 4)
                {
                    fours += 4;
                }

                if (die5 == 4)
                {
                    fours += 4;
                }
                LblBtnFour.Text = fours.ToString();
            }

            //-------------------------
            //Fives
            if (Btn_Fives.IsEnabled == true)
            {
                LblBtnFiveSelected.Text = "";
                fives = 0;
                if (die1 == 5)
                {
                    fives += 5;
                }

                if (die2 == 5)
                {
                    fives += 5;
                }

                if (die3 == 5)
                {
                    fives += 5;
                }

                if (die4 == 5)
                {
                    fives += 5;
                }

                if (die5 == 5)
                {
                    fives += 5;
                }
                LblBtnFive.Text = fives.ToString();
            }

            //-------------------------
            //Sixes
            if (Btn_Sixes.IsEnabled == true)
            {
                LblBtnSixSelected.Text = "";
                sixes = 0;
                if (die1 == 6)
                {
                    sixes += 6;
                }

                if (die2 == 6)
                {
                    sixes += 6;
                }

                if (die3 == 6)
                {
                    sixes += 6;
                }

                if (die4 == 6)
                {
                    sixes += 6;
                }

                if (die5 == 6)
                {
                    sixes += 6;
                }
                LblBtnSix.Text = sixes.ToString();
            }

            //-------------------------
            //3Kind
            if (Btn_3Kind.IsEnabled == true)
            {
                //Make Sure Selected Area is Blank
                LblBtn3KindSelected.Text = "";

                //Check if you have a 3 of kind
                if ((die1 == die2 && die2 == die3) || (die1 == die2 && die2 == die4) ||
                     (die1 == die2 && die2 == die5) || (die1 == die3 && die3 == die4) ||
                     (die1 == die3 && die3 == die5) || (die1 == die4 && die4 == die5) ||
                     (die2 == die3 && die3 == die4) || (die2 == die3 && die3 == die5) ||
                     (die2 == die4 && die4 == die5) || (die3 == die4 && die4 == die5))
                {
                    //Set Score
                    kind3 = die1 + die2 + die3 + die4 + die5;
                }

                //If rolled again and 3 of kind lost
                else
                {
                    kind3 = 0;
                }

                //Display Score
                LblBtn3Kind.Text = kind3.ToString();
            }

            //----------------------------
            //4Kind
            if (Btn_4Kind.IsEnabled == true)
            {
                //Make Sure Selected Area is Blank
                LblBtn4KindSelected.Text = "";

                if ((die1 == die2 && die2 == die3 && die3 == die4) ||
                    (die1 == die2 && die2 == die3 && die3 == die5) ||
                    (die2 == die3 && die3 == die4 && die4 == die5) ||
                    (die1 == die2 && die2 == die4 && die4 == die5) ||
                    (die2 == die3 && die3 == die4 && die4 == die5) ||
                    (die1 == die3 && die3 == die4 && die4 == die5))
                {
                    //Set Score
                    kind4 = die1 + die2 + die3 + die4 + die5;
                }

                //If rolled again and 4 of kind lost
                else
                {
                    kind4 = 0;
                }

                //Display Score
                LblBtn4Kind.Text = kind4.ToString();
            }

            //----------------------------
            //Full House
            if (Btn_FullHouse.IsEnabled == true && die1 != 0)
            {
                //Make Sure Selected Area is Blank
                LblBtnFullHouseSelected.Text = "";

                //Check if conditions met
                if ((die1 == die2 && die2 == die3 && die4 == die5) || (die1 == die2 && die2 == die4 && die3 == die5) ||
                     (die1 == die2 && die2 == die5 && die3 == die4) || (die1 == die3 && die3 == die4 && die2 == die5) ||
                     (die1 == die3 && die3 == die5 && die2 == die4) || (die1 == die4 && die4 == die5 && die2 == die3) ||
                     (die2 == die3 && die3 == die4 && die1 == die5) || (die2 == die3 && die3 == die5 && die1 == die4) ||
                     (die2 == die4 && die4 == die5 && die1 == die3) || (die3 == die4 && die4 == die5 && die1 == die2))
                {
                    //Set Score
                    fullHouse = 25;
                }

                //If rolled again and fullhouse lost
                else
                {
                    //Set Score
                    fullHouse = 0;
                }

                //Display Score
                LblBtnFullHouse.Text = fullHouse.ToString();
            }

            //----------------------------
            //Sm Straight
            if (Btn_SmStraight.IsEnabled == true)
            {
                //Make Sure Selected Area is Blank
                LblBtnSmStraightSelected.Text = "";
                smStr = 0;

                //1234
                if (die1 == 1 || die2 == 1 || die3 == 1 || die4 == 1 || die5 == 1)
                {
                    if (die1 == 2 || die2 == 2 || die3 == 2 || die4 == 2 || die5 == 2)
                    {
                        if (die1 == 3 || die2 == 3 || die3 == 3 || die4 == 3 || die5 == 3)
                        {
                            if (die1 == 4 || die2 == 4 || die3 == 4 || die4 == 4 || die5 == 4)
                            {
                                //Set Score
                                smStr = 30;
                            }
                        }
                    }
                }

                //2345
                if (die1 == 2 || die2 == 2 || die3 == 2 || die4 == 2 || die5 == 2)
                {
                    if (die1 == 3 || die2 == 3 || die3 == 3 || die4 == 3 || die5 == 3)
                    {
                        if (die1 == 4 || die2 == 4 || die3 == 4 || die4 == 4 || die5 == 4)
                        {
                            if (die1 == 5 || die2 == 5 || die3 == 5 || die4 == 5 || die5 == 5)
                            {
                                //Set Score
                                smStr = 30;
                            }
                        }
                    }
                }

                //3456
                if (die1 == 3 || die2 == 3 || die3 == 3 || die4 == 3 || die5 == 3)
                {
                    if (die1 == 4 || die2 == 4 || die3 == 4 || die4 == 4 || die5 == 4)
                    {
                        if (die1 == 5 || die2 == 5 || die3 == 5 || die4 == 5 || die5 == 5)
                        {
                            if (die1 == 6 || die2 == 6 || die3 == 6 || die4 == 6 || die5 == 6)
                            {
                                //Set Score
                                smStr = 30;
                            }
                        }
                    }
                }

                //If rolled again and Small Straight lost
                else
                {
                    //Set Score
                    smStr = 0;
                }

                //Display Score
                LblBtnSmStraight.Text = smStr.ToString();
            }

            //----------------------------
            //Lg Straight
            if (Btn_LgStraight.IsEnabled == true)
            {
                //Make Sure Selected Area is Blank
                LblBtnLgStraightSelected.Text = "";

                if (ones == 1 && twos == 2 && threes == 3 && fours == 4 && fives == 5)
                {
                    //Set Score
                    lgStr = 40;
                }

                //If rolled again and Large Straight lost
                else
                {
                    //Set Score
                    lgStr = 0;
                }

                //Display Score
                LblBtnLgStraight.Text = lgStr.ToString();
            }

            //----------------------------
            //YAHTZEE
            if (Btn_YAHTZEE.IsEnabled == true && die1 != 0)
            {
                //Make Sure Selected Area is Blank
                LblBtnYAHTZEESelected.Text = "";

                if (die1 == die2 && die2 == die3 && die3 == die4 && die4 == die5)
                {
                    //Set Score
                    yahtzee = 50;
                }

                //If rolled again and Yahtzee lost
                else
                {
                    //Set Score
                    yahtzee = 0;
                }

                //Display Score
                LblBtnYAHTZEE.Text = yahtzee.ToString();
            }

            //----------------------------
            //Chance
            if (Btn_Chance.IsEnabled == true)
            {
                //Make Sure Selected Area is Blank
                LblBtnChanceSelected.Text = "";

                //Calculate Chance
                chance = die1 + die2 + die3 + die4 + die5;

                //Display Possibble Score
                LblBtnChance.Text = chance.ToString();
            }

            //------------------------------------------------------
            //Display Score after selected

            //Ones
            if (Btn_Ones.IsEnabled == false)
            {
                LblBtnOne.Text = "";
                LblBtnOneSelected.Text = ones.ToString();
            }
            //-------------------------
            //Twos
            if (Btn_Twos.IsEnabled == false)
            {
                LblBtnTwo.Text = "";
                LblBtnTwoSelected.Text = twos.ToString();
            }

            //-------------------------
            //Threes
            if (Btn_Threes.IsEnabled == false)
            {
                LblBtnThree.Text = "";
                LblBtnThreeSelected.Text = threes.ToString();
            }

            //-------------------------
            //Fours
            if (Btn_Fours.IsEnabled == false)
            {
                LblBtnFour.Text = "";
                LblBtnFourSelected.Text = fours.ToString();
            }

            //-------------------------
            //Fives
            if (Btn_Fives.IsEnabled == false)
            {
                LblBtnFive.Text = "";
                LblBtnFiveSelected.Text = fives.ToString();
            }

            //-------------------------
            //Sixes
            if (Btn_Sixes.IsEnabled == false)
            {
                LblBtnSix.Text = "";
                LblBtnSixSelected.Text = sixes.ToString();
            }

            //----------------------------
            //3Kind
            if (Btn_3Kind.IsEnabled == false)
            {
                LblBtn3Kind.Text = "";
                LblBtn3KindSelected.Text = kind3.ToString();
            }

            //----------------------------
            //4Kind
            if (Btn_4Kind.IsEnabled == false)
            {
                LblBtn4Kind.Text = "";
                LblBtn4KindSelected.Text = kind4.ToString();
            }

            //----------------------------
            //Full House
            if (Btn_FullHouse.IsEnabled == false)
            {
                LblBtnFullHouse.Text = "";
                LblBtnFullHouseSelected.Text = fullHouse.ToString();
            }

            //----------------------------
            //Small Straight
            if (Btn_SmStraight.IsEnabled == false)
            {
                LblBtnSmStraight.Text = "";
                LblBtnSmStraightSelected.Text = smStr.ToString();
            }

            //----------------------------
            //Large Straight
            if (Btn_LgStraight.IsEnabled == false)
            {
                LblBtnLgStraight.Text = "";
                LblBtnLgStraightSelected.Text = lgStr.ToString();
            }

            //----------------------------
            //Yahtzee
            if (Btn_YAHTZEE.IsEnabled == false)
            {
                LblBtnYAHTZEE.Text = "";
                LblBtnYAHTZEESelected.Text = yahtzee.ToString();

                //Yahtzee Bonus
                if (die1 == die2 && die2 == die3 && die3 == die4 && die4 == die5 && die1 != 0)
                {
                    //Update Bonus
                    yahBonus += 100;

                    //Update Text
                    LblBtnYAHTZEEBONUS.Text = yahBonus.ToString();
                }
            }

            //----------------------------
            //Chance
            if (Btn_Chance.IsEnabled == false)
            {
                LblBtnChance.Text = "";
                LblBtnChanceSelected.Text = chance.ToString();
            }

            //Update Totals

            //BONUS
            if (upSubtotal >= 63)
            {
                //Display 63 extra points
                Bonus.Text = "63";

                //Add Bonus to up total
                upTOTAL = upSubtotal + upBonus;
            }

            else
            {
                upTOTAL = upSubtotal;
            }

            if (newGame == 0)
            {
                //Change to 0
                upSubtotal = 0;
                upTOTAL = 0;
                lowerTotal = 0;
                grandTOTAL = 0;

                //Display
                TotalBeforeBonus.Text = upSubtotal.ToString();
                TotalUpper.Text = upTOTAL.ToString();
                TotalLower.Text = lowerTotal.ToString();
                TotalGRAND.Text = grandTOTAL.ToString();
            }

            else
            {
                //Grand Total
                grandTOTAL = lowerTotal + upTOTAL;

                //Update Strings
                TotalBeforeBonus.Text = upSubtotal.ToString();
                TotalUpper.Text = upTOTAL.ToString();
                TotalLower.Text = lowerTotal.ToString();
                TotalGRAND.Text = grandTOTAL.ToString();
            }

        }

        //Display Dice on Screen
        private void DisplayDice(int roll, int dice)
        {
            //Depending on which dice it is
            switch (dice)
            {
                //Dice 1
                case 1:
                    //Select dice face image depending on random number
                    switch (roll)
                    {
                        case 1:
                            DiceRoll1.Source = "DiceImages/Dice1.png";
                            break;

                        case 2:
                            DiceRoll1.Source = "DiceImages/Dice2.png";
                            break;

                        case 3:
                            DiceRoll1.Source = "DiceImages/Dice3.png";
                            break;

                        case 4:
                            DiceRoll1.Source = "DiceImages/Dice4.png";
                            break;

                        case 5:
                            DiceRoll1.Source = "DiceImages/Dice5.png";
                            break;

                        case 6:
                            DiceRoll1.Source = "DiceImages/Dice6.png";
                            break;
                    }
                    break;

                //Dice 2
                case 2:
                    //Select dice face image depending on random number
                    switch (roll)
                    {
                        case 1:
                            DiceRoll2.Source = "DiceImages/Dice1.png";
                            break;

                        case 2:
                            DiceRoll2.Source = "DiceImages/Dice2.png";
                            break;

                        case 3:
                            DiceRoll2.Source = "DiceImages/Dice3.png";
                            break;

                        case 4:
                            DiceRoll2.Source = "DiceImages/Dice4.png";
                            break;

                        case 5:
                            DiceRoll2.Source = "DiceImages/Dice5.png";
                            break;

                        case 6:
                            DiceRoll2.Source = "DiceImages/Dice6.png";
                            break;
                    }
                    break;

                //Dice 3
                case 3:
                    //Select dice face image depending on random number
                    switch (roll)
                    {
                        case 1:
                            DiceRoll3.Source = "DiceImages/Dice1.png";
                            break;

                        case 2:
                            DiceRoll3.Source = "DiceImages/Dice2.png";
                            break;

                        case 3:
                            DiceRoll3.Source = "DiceImages/Dice3.png";
                            break;

                        case 4:
                            DiceRoll3.Source = "DiceImages/Dice4.png";
                            break;

                        case 5:
                            DiceRoll3.Source = "DiceImages/Dice5.png";
                            break;

                        case 6:
                            DiceRoll3.Source = "DiceImages/Dice6.png";
                            break;
                    }
                    break;

                //Dice 4
                case 4:
                    //Select dice face image depending on random number
                    switch (roll)
                    {
                        case 1:
                            DiceRoll4.Source = "DiceImages/Dice1.png";
                            break;

                        case 2:
                            DiceRoll4.Source = "DiceImages/Dice2.png";
                            break;

                        case 3:
                            DiceRoll4.Source = "DiceImages/Dice3.png";
                            break;

                        case 4:
                            DiceRoll4.Source = "DiceImages/Dice4.png";
                            break;

                        case 5:
                            DiceRoll4.Source = "DiceImages/Dice5.png";
                            break;

                        case 6:
                            DiceRoll4.Source = "DiceImages/Dice6.png";
                            break;
                    }
                    break;

                //Dice 5
                case 5:
                    //Select dice face image depending on random number
                    switch (roll)
                    {
                        case 1:
                            DiceRoll5.Source = "DiceImages/Dice1.png";
                            break;

                        case 2:
                            DiceRoll5.Source = "DiceImages/Dice2.png";
                            break;

                        case 3:
                            DiceRoll5.Source = "DiceImages/Dice3.png";
                            break;

                        case 4:
                            DiceRoll5.Source = "DiceImages/Dice4.png";
                            break;

                        case 5:
                            DiceRoll5.Source = "DiceImages/Dice5.png";
                            break;

                        case 6:
                            DiceRoll5.Source = "DiceImages/Dice6.png";
                            break;
                    }
                    break;
            }
            LblBtnOne.Text = ones.ToString();
            LblBtnTwo.Text = twos.ToString();
            LblBtnThree.Text = threes.ToString();
            LblBtnFour.Text = fours.ToString();
            LblBtnFive.Text = fives.ToString();
            LblBtnSix.Text = sixes.ToString();
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        //Select and Unselect Die

        //Dice 1 when Selected
        private void Dice1Select_Clicked(object sender, EventArgs e)
        {
            //Keep Selected Die
            if (dieSelect1 == 0)
            {
                dieSelect1 = 1;
                Dice1Select.Text = "Selected";
            }

            //Deselect die
            else
            {
                dieSelect1 = 0;
                Dice1Select.Text = "Select";
            }
        }

        //Dice 2 when Selected
        private void Dice2Select_Clicked(object sender, EventArgs e)
        {
            //Keep Selected Die
            if (dieSelect2 == 0)
            {
                dieSelect2 = 1;
                Dice2Select.Text = "Selected";
            }

            //Deselect die
            else
            {
                dieSelect2 = 0;
                Dice2Select.Text = "Select";
            }
        }

        //Dice 3 when Selected
        private void Dice3Select_Clicked(object sender, EventArgs e)
        {
            //Keep Selected Die
            if (dieSelect3 == 0)
            {
                dieSelect3 = 1;
                Dice3Select.Text = "Selected";
            }

            //Deselect die
            else
            {
                dieSelect3 = 0;
                Dice3Select.Text = "Select";
            }
        }

        //Dice 4 when Selected
        private void Dice4Select_Clicked(object sender, EventArgs e)
        {
            //Keep Selected Die
            if (dieSelect4 == 0)
            {
                dieSelect4 = 1;
                Dice4Select.Text = "Selected";
            }

            //Deselect die
            else
            {
                dieSelect4 = 0;
                Dice4Select.Text = "Select";
            }
        }

        //Dice 5 when Selected
        private void Dice5Select_Clicked(object sender, EventArgs e)
        {
            //Keep Selected Die
            if (dieSelect5 == 0)
            {
                dieSelect5 = 1;
                Dice5Select.Text = "Selected";
            }

            //Deselect die
            else
            {
                dieSelect5 = 0;
                Dice5Select.Text = "Select";
            }
        }

        //----------------------------------------------------------------------------------------------------------------
        //Select Buttons for Scoring

        //Upper Section
        //Select Ones
        private void Btn_Ones_Clicked(object sender, EventArgs e)
        {
            //Disable Button
            Btn_Ones.IsEnabled = false;

            //Change Text to Show its Seleted
            Btn_Ones.Text = "Selected";

            //Add Score to Upper Total
            int number;
            int.TryParse(LblBtnOne.Text, out number);
            upSubtotal += number;

            //New Go
            NewGo();
        }

        //Select Twos
        private void Btn_Twos_Clicked(object sender, EventArgs e)
        {
            //Disable Button
            Btn_Twos.IsEnabled = false;

            //Change Text to Show its Seleted
            Btn_Twos.Text = "Selected";

            //Add Score to Upper Total
            int number;
            int.TryParse(LblBtnTwo.Text, out number);
            upSubtotal += number;

            //New Go
            NewGo();
        }

        //Select Threes
        private void Btn_Threes_Clicked(object sender, EventArgs e)
        {
            //Disable Button
            Btn_Threes.IsEnabled = false;

            //Change Text to Show its Seleted
            Btn_Threes.Text = "Selected";

            //Add Score to Upper Total
            int number;
            int.TryParse(LblBtnThree.Text, out number);
            upSubtotal += number;

            //New Go
            NewGo();
        }

        //Select Fours
        private void Btn_Fours_Clicked(object sender, EventArgs e)
        {
            //Disable Button
            Btn_Fours.IsEnabled = false;

            //Change Text to Show its Seleted
            Btn_Fours.Text = "Selected";

            //Add Score to Upper Total
            int number;
            int.TryParse(LblBtnFour.Text, out number);
            upSubtotal += number;

            //New Go
            NewGo();
        }

        //Select Fives
        private void Btn_Fives_Clicked(object sender, EventArgs e)
        {
            //Disable Button
            Btn_Fives.IsEnabled = false;

            //Change Text to Show its Seleted
            Btn_Fives.Text = "Selected";

            //Add Score to Upper Total
            int number;
            int.TryParse(LblBtnFive.Text, out number);
            upSubtotal += number;

            //New Go
            NewGo();
        }

        //Select Sixes
        private void Btn_Sixes_Clicked(object sender, EventArgs e)
        {
            //Disable Button
            Btn_Sixes.IsEnabled = false;

            //Change Text to Show its Seleted
            Btn_Sixes.Text = "Selected";

            //Add Score to Upper Total
            int number;
            int.TryParse(LblBtnSix.Text, out number);
            upSubtotal += number;

            //New Go
            NewGo();
        }

        //--------------------------------------------------------------------------------------------------------------------------------------
        //Lower Section
        //Select 3Kind
        private void Btn_3Kind_Clicked(object sender, EventArgs e)
        {
            //Add to lower total
            lowerTotal += kind3;

            //Disable Button
            Btn_3Kind.IsEnabled = false;

            //Change Text to Show its Seleted
            Btn_3Kind.Text = "Selected";

            //New Go
            NewGo();
        }

        //Select 4Kind
        private void Btn_4Kind_Clicked(object sender, EventArgs e)
        {
            //Add to lower total
            lowerTotal += kind4;

            //Disable Button
            Btn_4Kind.IsEnabled = false;

            //Change Text to Show its Seleted
            Btn_4Kind.Text = "Selected";

            //New Go
            NewGo();
        }

        //Select FullHouse
        private void Btn_FullHouse_Clicked(object sender, EventArgs e)
        {
            //Add to lower total
            lowerTotal += fullHouse;

            //Disable Button
            Btn_FullHouse.IsEnabled = false;

            //Change Text to Show its Seleted
            Btn_FullHouse.Text = "Selected";

            //New Go
            NewGo();
        }

        //Select smStraight
        private void Btn_SmStraight_Clicked(object sender, EventArgs e)
        {
            //Add to lower total
            lowerTotal += smStr;

            //Disable Button
            Btn_SmStraight.IsEnabled = false;

            //Change Text to Show its Seleted
            Btn_SmStraight.Text = "Selected";

            //New Go
            NewGo();
        }

        //Select lgStraight
        private void Btn_LgStraight_Clicked(object sender, EventArgs e)
        {
            //Add to lower total
            lowerTotal += lgStr;

            //Disable Button
            Btn_LgStraight.IsEnabled = false;

            //Change Text to Show its Seleted
            Btn_LgStraight.Text = "Selected";

            //New Go
            NewGo();
        }

        //Select Yahtzee
        private void Btn_YAHTZEE_Clicked(object sender, EventArgs e)
        {
            //Add to lower total
            lowerTotal += yahtzee;

            //Disable Button
            Btn_YAHTZEE.IsEnabled = false;

            //Change Text to Show its Seleted
            Btn_YAHTZEE.Text = "Selected";

            //New Go
            NewGo();
        }

        //Select Chance
        private void Btn_Chance_Clicked(object sender, EventArgs e)
        {
            //Add to total
            lowerTotal += chance;

            //Disable Button
            Btn_Chance.IsEnabled = false;

            //Change Text to Show its Seleted
            Btn_Chance.Text = "Selected";

            //New Go
            NewGo();
        }
    }//End of Main
}//End of NameSpace