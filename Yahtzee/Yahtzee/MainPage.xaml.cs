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
        const string NEW_GAME = "New Game";
        const string END_GO = "End of Go";

        //Variables
        int clicks = 0; //To End Turn after 3 goes
        int diceRoll1, diceRoll2, diceRoll3, diceRoll4, diceRoll5; //DiceRoll Variables
        int dieSelect1 = 0, dieSelect2 = 0, dieSelect3 = 0, dieSelect4 = 0, dieSelect5 = 0; //If 1 then die selected and wont change
        int ones, twos, threes, fours, fives, sixes; //Variables for keeping track of scores of upper section
        int upSubtotal, upBonus, upTOTAL, lowerTotal=0, grandTOTAL; //Totals
        int kind3, kind4, fullHouse, smStr, lgStr, yahtzee, chance; //Variables for keeping track of scores of lower section
        int threeKind=0;//Checking if Conditions met

        //Create instance of random
        Random random;

        //Initialize
        public MainPage()
        {
            InitializeComponent();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------

        //Start New Game

        private void SetUpNewGame()
        {
            //Make all buttons invisible
            MakeInvisable(0);

            //Make sure counter has reset
            clicks = 0;

            //Change button Text
            Btn_RollDice.Text = "Roll Dice";

            //Change Info
            DiceArea.Text = "Click Roll Dice";
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------

        //End of Go
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
            MakeInvisable(1);
        }

        //New Go
        private void NewGo()
        {
            //Change Text of Button
            Btn_RollDice.Text = "Roll Dice";

            //Enable Button 
            Btn_RollDice.IsEnabled = true;

            //Reset unused Scores
            int die1 = 0, die2 = 0, die3 = 0, die4 = 0, die5 = 0;
            ScoreTracker(die1, die2, die3, die4, die5);

            //Make Buttons invisible again
            MakeInvisable(0);

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
        private void MakeInvisable(int decide)
        {
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

            else
            {
                Btn_Ones.IsVisible = true;
                Btn_Twos.IsVisible = true;
                Btn_Threes.IsVisible = true;
                Btn_Fours.IsVisible = true;
                Btn_Fives.IsVisible = true;
                Btn_Sixes.IsVisible = true;
                Btn_3Kind.IsVisible = true;
                Btn_4Kind.IsVisible = true;
                Btn_FullHouse.IsVisible = true;
                Btn_SmStraight.IsVisible = true;
                Btn_LgStraight.IsVisible = true;
                Btn_YAHTZEE.IsVisible = true;
                Btn_Chance.IsVisible = true;
            }
        }


        //----------------------------------------------------------------------------------------------------
        
        //RollDice
        private void Btn_RollDice_Clicked(object sender, EventArgs e)
        {
            //Update Counter
            clicks++;

            //Read Button Text
            string ButtonText = ((Button)sender).Text;

            //switch to change objective depending on button text
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
                    //Start new Game
                    SetUpNewGame();
                    break;
                }
            }
        }

        //Roll Dice 3 Times
        private void RollDice(int i)
        {
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
            ScoreTracker(diceRoll1, diceRoll2, diceRoll3, diceRoll4, diceRoll5);

            if (i >= 3)
            {
                EndGo();
            }
         }

        //Score Display
        private void ScoreTracker (int die1, int die2, int die3, int die4, int die5)
        {
            //Ones
            if (Btn_Ones.IsEnabled == true)
            {
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

                //If Conditions not Met
                if(ones == 0)
                {
                    Btn_Ones.IsVisible = false;
                }
            }
            //-------------------------
            //Twos
            if (Btn_Twos.IsEnabled == true)
            {
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

                //Conditions not Met
                if (twos == 0)
                {
                    Btn_Twos.IsVisible = false;
                }
            }

            //-------------------------
            //Threes
            if (Btn_Threes.IsEnabled == true)
            {
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

                //Conditions not Met
                if (threes == 0)
                {
                    Btn_Threes.IsVisible = false;
                }
            }

            //-------------------------
            //Fours
            if (Btn_Fours.IsEnabled == true)
            {
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

                //If Conditions not Met
                if (fours == 0)
                {
                    Btn_Fours.IsVisible = false;
                }
            }

            //-------------------------
            //Fives
            if (Btn_Fives.IsEnabled == true)
            {
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

                //If Conditions not Met
                if (fives == 0)
                {
                    Btn_Fives.IsVisible = false;
                }
            }

            //-------------------------
            //Sixes
            if (Btn_Sixes.IsEnabled == true)
            {
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

                //If Conditions not Met
                if (sixes == 0)
                {
                    Btn_Sixes.IsVisible = false;
                }
            }

            //-------------------------
            //3Kind
            if (Btn_3Kind.IsEnabled == true)
            {
               if (die1  == die2 && die2 == die3 || die1 == die2 && die2 == die4 ||
                    die1 == die2 && die2 == die5 || die1 == die3 && die3 == die4 ||
                    die1 == die3 && die3 == die5 || die1 == die4 && die4 == die5)
               {
                    //Set Score
                    kind3 = die1 * 3;

                    //Conditions Met
                    threeKind = 1;
               }

               if(die2 == die3 && die3 == die4 || die2 == die3 && die3 == die5 ||
                  die2 == die4 && die4 == die5)
                {
                    //Set Score
                    kind3 = die2 * 3;

                    //Conditions Met
                    threeKind = 1;
                }

               if(die3 == die4 && die4 == die5)
                {
                    //Set Score
                    kind3 = die3 * 3;

                    //Conditions Met
                    threeKind = 1;
                }

                lowerTotal += kind3;
                Lbl3Kind.Text = kind3.ToString();
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

            //Update Totals
            
            //BONUS
            if (upSubtotal >= 63)
            {
                //Display 63 extra points
                Bonus.Text = "63";

                //Add Bonus to up total
                upTOTAL = upSubtotal + 63;
            }

            else
            {
                upTOTAL = upSubtotal;
            }

            //Grand Total
            grandTOTAL = lowerTotal + upTOTAL;

            //Update Strings
            TotalBeforeBonus.Text = upSubtotal.ToString();
            TotalUpper.Text = upTOTAL.ToString();
            TotalLower.Text = lowerTotal.ToString();

            TotalGRAND.Text = grandTOTAL.ToString();
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

        //-------------------------------------------------------------------------------------------------------------------------------
        //Display what you can Select
        private void Dice(int roll, int die)
        {
            switch (die)
            {
                case 1:
                    switch (roll)
                    {
                        case 1:
                            ones += 1;
                            break;

                        case 2:
                            twos += 2;
                            break;

                        case 3:
                            threes += 3;
                            break;

                        case 4:
                            fours += 4;
                            break;

                        case 5:
                            fives += 5;
                            break;

                        case 6:
                            sixes += 6;
                            break;
                    }
                    break;

                case 2:
                    switch (roll)
                    {
                        case 1:
                            ones += 1;
                            break;

                        case 2:
                            twos += 2;
                            break;

                        case 3:
                            threes += 3;
                            break;

                        case 4:
                            fours += 4;
                            break;

                        case 5:
                            fives += 5;
                            break;

                        case 6:
                            sixes += 6;
                            break;
                    }
                    break;
            }
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
            if (threeKind == 1)
            {
                //Disable Button
                Btn_3Kind.IsEnabled = false;

                //Change Text to Show its Seleted
                Btn_3Kind.Text = "Selected";

                //New Go
                NewGo();
            }

            else
            {
                DiceArea.Text = "Conditions not Met!!";
                Btn_3Kind.IsVisible = false;
            }
        }

        //Select 4Kind
        private void Btn_4Kind_Clicked(object sender, EventArgs e)
        {

        }

        //Select FullHouse
        private void Btn_FullHouse_Clicked(object sender, EventArgs e)
        {

        }

        //Select smStraight
        private void Btn_SmStraight_Clicked(object sender, EventArgs e)
        {

        }

        //Select lgStraight
        private void Btn_LgStraight_Clicked(object sender, EventArgs e)
        {

        }

        //Select Yahtzee
        private void Btn_YAHTZEE_Clicked(object sender, EventArgs e)
        {

        }

        //Select Chance
        private void Btn_Chance_Clicked(object sender, EventArgs e)
        {

        }

    }//End of Main
}//End of NameSpace
