using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace RollTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        //Calling rolls
        private int RollTranslate(string dice)
        { //translate the numeric string to int to count the dice
            int diceTranslate = int.Parse(dice);

            return diceTranslate;
        }

        private List<int> rollFour(int dice) 
        {// Roll 4 dice
            const int four = 4;
            const int incdec = 1;
            uint dieType = Convert.ToUInt32(dice);
            List<int> results = new List<int>();
            int count = four;
            int zero = 0;

            do
            {
                RNGCryptoServiceProvider caster = new RNGCryptoServiceProvider();
                var byteList = new byte[4];
                caster.GetBytes(byteList);

                //convert 4 bytes to an integer
                var randInt = BitConverter.ToUInt32(byteList, 0);

                //var byteList2 = new byte[8];
                //caster.GetBytes(byteList2);

                //convert 8 bytes to a double 56:10
                //var randdbl = BitConverter.ToDouble(byteList2, 0);
                uint dieCast = (Convert.ToUInt32(randInt % dieType + incdec));

                //mostly for damage rolls, but will include alpha
                //sum += Convert.ToInt32(dieCast);
                results.Add(Convert.ToInt32(dieCast));
                count -= incdec;
            } while (count > zero);
            
            return results;
        }

        private List<int> rollMany(int dice , int total)
        {// Dice Type set to 4 for potions when called
            const int four = 4;
            const int incdec = 1;
            uint dieType = Convert.ToUInt32(dice);
            List<int> results = new List<int>();
            int count = total;
            int zero = 0;

            do
            {
                RNGCryptoServiceProvider caster = new RNGCryptoServiceProvider();
                var byteList = new byte[4];
                caster.GetBytes(byteList);

                //convert 4 bytes to an integer
                var randInt = BitConverter.ToUInt32(byteList, 0);

                //var byteList2 = new byte[8];
                //caster.GetBytes(byteList2);

                //convert 8 bytes to a double 56:10
                //var randdbl = BitConverter.ToDouble(byteList2, 0);
                uint dieCast = (Convert.ToUInt32(randInt % dieType + incdec));

                //mostly for damage rolls, but will include alpha
                //sum += Convert.ToInt32(dieCast);
                results.Add(Convert.ToInt32(dieCast));
                count -= incdec;
            } while (count > zero);

            return results;
        }


        private string RollD(int dieType,int dice, Boolean? advantage =false, Boolean? disadvantage = false)
        {   //Could be done as a method passing the modulo
            //instead of several different roll methods,
            //just have it take two arguments, the data and the dietype
            //instead of just the data
            //establishes diceCount for determining the roll
            int diceCount = dice;
            //result string is established
            string result = "";
            //leave method if dice entered is 0
            if (diceCount.Equals(0))
            {
                return result;
            }
            //increment decrement value
            int incdec = 1;
            //Var for two for doubling
            int two = 2;
            //sum for totals, only really useful for damage and finding average
            int sum = 0;
            //highest and lowest, only really used during advantage and disadvantage
            uint highest = 0;
            uint lowest = 100;
            //Calls dieType to declare dice type for output
            result += "D"+Convert.ToString(dieType)+": ";
            //same as above, placeholders for the comparison
            //After conversion
            int high = 0;
            int low = 1000;
            List<uint> rolls = new List<uint>();
            Boolean adv = Convert.ToBoolean(advantage);

            Boolean dadv = Convert.ToBoolean(disadvantage);
            Boolean quickRoll = false;
            //For building the result string and the 
            //Effects string (like advantage rolls)
            StringBuilder builder = new StringBuilder();
           
       

            //if advantage or disadvantage
            //Double diceCount
            //Testing adv, setting to t
            //adv = true;
            //textBox1.AppendText("adv status: " + adv.ToString());
            if ((adv | dadv).Equals(true))
            {
                diceCount = diceCount * two;
            }
            //quickRoll = false;
            //enables quick roll for single rolls
            //Returns one result
            //May set output text as just the roll
            //Testing quickroll status
            //textBox1.AppendText("QuickRoll status: " + quickRoll.ToString());
            //textBox1.AppendText("Dice count status: " + diceCount.ToString());
            if (diceCount.Equals(1)) 
            {
                quickRoll = true;
                //textBox1.AppendText("QuickRoll status: "+quickRoll);
            }
            do
            {
                
                RNGCryptoServiceProvider caster = new RNGCryptoServiceProvider();
                var byteList = new byte[4];
                caster.GetBytes(byteList);

                //convert 4 bytes to an integer
                var randInt = BitConverter.ToUInt32(byteList, 0);

                //var byteList2 = new byte[8];
                //caster.GetBytes(byteList2);

                //convert 8 bytes to a double 56:10
                //var randdbl = BitConverter.ToDouble(byteList2, 0);
                uint dieCast = (Convert.ToUInt32(randInt % dieType +incdec));

                //mostly for damage rolls, but will include alpha
                sum += Convert.ToInt32(dieCast);

               

                if (dieCast > highest)
                {
                    highest = dieCast;
                }
                if (dieCast < lowest)
                {
                    lowest = dieCast;
                }//Testing the conditions
                //textBox1.AppendText(" Advantage status: " + adv.ToString());
                if ((adv || dadv).Equals(true))
                {
                    //textBox1.AppendText(" Die Cast result: " + dieCast.ToString());
                    rolls.Add(dieCast);
                    //Test current values
                    //textBox1.AppendText(Convert.ToString(dieCast));
                    //textBox1.AppendText(rolls.ToString());
                    //textBox1.AppendText("Rolls count: " + Convert.ToString(rolls.Count()) + "roll one: " + rolls[0].ToString());
                    if (rolls.Count().Equals(2))
                    {
                        high = Convert.ToInt32(rolls.Max());
                        low = Convert.ToInt32(rolls.Min());
                        if ((adv).Equals(true))
                        {
                            //textBox1.AppendText("Advantage roll");
                            //textBox1.AppendText(" |" + Convert.ToString(high) + " with advantage! (" + Convert.ToString(high) + " ," + Convert.ToString(low) + ") |");
                            //textBox1.AppendText(result.ToString());
                            result += " result: |" + Convert.ToString(high) + " with advantage! (" + Convert.ToString(high) + " ," + Convert.ToString(low) + ") |";
                            //test string
                            //textBox1.AppendText(result.ToString());
                        }
                        if ((dadv).Equals(true))
                        {
                            //textBox1.AppendText(" Disdvantage roll");
                            //textBox1.AppendText(" |" + Convert.ToString(high) + " with advantage! (" + Convert.ToString(high) + " ," + Convert.ToString(low) + ") |");
                            //textBox1.AppendText(result.ToString());
                            result += " result: |" + Convert.ToString(low) + " with disadvantage! (" + Convert.ToString(low) + " ," + Convert.ToString(high) + ") |";
                            //test string
                            //textBox1.AppendText(result.ToString());
                        }
                        rolls.Clear();
                    }

                }
                else if ((quickRoll).Equals(true))
                {
                    result += " " + Convert.ToString(dieCast) + "  ";
                } else if ((adv || dadv).Equals(false)) 
                {
                    result += " " + Convert.ToString(dieCast) + "  ";
                    rolls.Add(dieCast);

                    if (rolls.Count().Equals(dice))
                    {

                        //result+= Convert.ToString(rolls.ToString());
                        rolls.Clear();
                    }


                }
                // decrement dice
                diceCount -= incdec;


            } while (diceCount > 0);

            if (quickRoll.Equals(false))
            {

                if ((adv || dadv).Equals(false))
                {
                    result += "| Total: " + Convert.ToString(sum)+"  ";
                }
            }

            

            return result;
        }

        private List<int> dropLowest(List<int> statList) 
        {
            int lowest = statList.Min();
            statList.Sort();
            statList.Reverse();
            //textBox1.AppendText(statList.ToString());
            return statList;
        }

        private string roll4D6() 
        {   const int dSix = 6;
            int sum = 0;
            List<int> statList = new List<int>();
            
            statList = dropLowest(rollFour(dSix));
            sum = statList.Sum()-statList.Min();
            string result = sum.ToString()+":[" + statList[0].ToString() + ", " + statList[1].ToString() + ", " + statList[2].ToString() + " | -" + statList[3].ToString()+"]";
            //textBox1.AppendText(result);
            return result;
        }
        private List<string> prepareTotals(List<int> totals) 
        {   //increment decrement value
            const int incDec = 0;
            List<string> totalString = new List<string>();
            int counter = 0;
            
            foreach(int item in totals) 
            {
                if (item < 10)
                {
                    totalString.Add("  " + Convert.ToString(item));
                }
                else 
                {
                    totalString.Add(Convert.ToString(item));
                }
                counter += incDec;
                
            }
            return totalString;
        }
        private string roll70() 
        {
            const int seventy = 70;
            const int zero = 0;
            Boolean sixStats = false;
            List<int> totals = new List<int>();
            List<string> totalString = new List<string>();
            string result = "";
           // int statCounter = sixStats;
            int statSum = 0;
            string firstStat = "";
            string secondStat = "";
            string thirdStat = "";
            string fourthStat = "";
            string fifthStat = "";
            string sixthStat = "";

            do
            {
                result = "";
                statSum = zero;
                firstStat = roll4D6();
                //Using match for formatting
                Match statsMatching = Regex.Match(firstStat.Substring(0, 2), @"^[^:]*");
                //string matched = statsMatching.
                //possibly dangerous, using regex without exception handling
                //messy solution to missing colon situation, add
                //a space to account for missing element
                //the spaces should be a variable of two spaces versus floating spaces
                if (Convert.ToInt32(statsMatching.ToString())<10)
                    {
                        firstStat= " "+ firstStat;
                }
                totals.Add(Convert.ToInt32(statsMatching.ToString()));
                statSum += Convert.ToInt32(statsMatching.ToString());
                //textBox1.AppendText("First total: "+ Regex.Match(firstStat.Substring(0, 2), @"^[^:]*"));
                result += "[ " + (Regex.Match(firstStat.Substring(0, 2), @"^[^:]*")) + ", ";
                //textBox1.AppendText(Regex.Match(firstStat.Substring(0, 2), @"^[^:]*"));
                //textBox1.AppendText(Environment.NewLine);
                secondStat = roll4D6();
                statsMatching = Regex.Match(secondStat.Substring(0, 2), @"^[^:]*");
                if (Convert.ToInt32(statsMatching.ToString()) < 10)
                {
                    secondStat = " " + secondStat;
                }
                totals.Add(Convert.ToInt32(statsMatching.ToString()));
                statSum += Convert.ToInt32(statsMatching.ToString());
                //textBox1.AppendText(Regex.Match(secondStat.Substring(0, 2), @"^[^:]*"));
                //textBox1.AppendText(Environment.NewLine);
                result += (Regex.Match(secondStat.Substring(0, 2), @"^[^:]*") + ", ");
                thirdStat = roll4D6();
                statsMatching = Regex.Match(thirdStat.Substring(0, 2), @"^[^:]*");
                if (Convert.ToInt32(statsMatching.ToString()) < 10)
                {
                    thirdStat = " " + thirdStat;
                }
                totals.Add(Convert.ToInt32(statsMatching.ToString()));
                statSum += Convert.ToInt32(statsMatching.ToString());                 
                //textBox1.AppendText(Regex.Match(thirdStat.Substring(0, 2), @"^[^:]*"));
                //textBox1.AppendText(Environment.NewLine);
                result += (Regex.Match(thirdStat.Substring(0, 2), @"^[^:]*") + ", ");
                fourthStat = roll4D6();
                statsMatching = Regex.Match(fourthStat.Substring(0, 2), @"^[^:]*");
                if (Convert.ToInt32(statsMatching.ToString()) < 10)
                {
                    fourthStat = " " + fourthStat;
                }
                totals.Add(Convert.ToInt32(statsMatching.ToString()));
                statSum += Convert.ToInt32(statsMatching.ToString());
                //textBox1.AppendText(Regex.Match(fourthStat.Substring(0, 2), @"^[^:]*"));
                //textBox1.AppendText(Environment.NewLine);
                result += (Regex.Match(fourthStat.Substring(0, 2), @"^[^:]*") + ", ");
                fifthStat = roll4D6();
                statsMatching = Regex.Match(fifthStat.Substring(0, 2), @"^[^:]*");
                if (Convert.ToInt32(statsMatching.ToString()) < 10)
                {
                    fifthStat = " " + fifthStat;
                }
                totals.Add(Convert.ToInt32(statsMatching.ToString()));
                statSum += Convert.ToInt32(statsMatching.ToString());
                //textBox1.AppendText(Regex.Match(fifthStat.Substring(0, 2), @"^[^:]*"));
                //textBox1.AppendText(Environment.NewLine);
                result += (Regex.Match(fifthStat.Substring(0, 2), @"^[^:]*") + ", ");
                sixthStat = roll4D6();
                //textBox1.AppendText(Regex.Match(sixthStat.Substring(0, 2), @"^[^:]*"));
                //textBox1.AppendText(Environment.NewLine);
                statsMatching = Regex.Match(sixthStat.Substring(0, 2), @"^[^:]*");
                if (Convert.ToInt32(statsMatching.ToString()) < 10)
                {
                    sixthStat = " " + sixthStat;
                }
                totals.Add(Convert.ToInt32(statsMatching.ToString()));
                statSum += Convert.ToInt32(statsMatching.ToString());
                result += (Regex.Match(sixthStat.Substring(0, 2), @"^[^:]*") + ", ");
                result = "Total: " + Convert.ToString(statSum) + ": " + result;
                //textBox1.AppendText(Environment.NewLine);
                //textBox1.AppendText("Stat sum: "+Convert.ToString(statSum));
                //test
                //statSum = 72;
                if (statSum >= seventy)
                {
                    sixStats = true;
                    totalString =prepareTotals(totals);
                }
                else 
                {
                    totals.Clear();
                }
            } while (sixStats.Equals(false));
            //substring should be a named variable versus floating 2's
            //need to fix formatting on character generator, colons disappear on totals less than 10
            //Fixed the formatting by addin the missing element as an empty space if total is below 10
            textBox1.AppendText(Environment.NewLine);
            //textBox1.AppendText(totalString[0] + firstStat.Substring(2, firstStat.Length - 2));
            textBox1.AppendText(Regex.Match(totalString[0], @"^[^:]*")+firstStat.Substring(2, firstStat.Length - 2));
            textBox1.AppendText(Environment.NewLine);
            //textBox1.AppendText(totalString[1] + secondStat.Substring(2, secondStat.Length - 2));
            textBox1.AppendText(Regex.Match(totalString[1], @"^[^:]*")+ secondStat.Substring(2, secondStat.Length - 2));
            textBox1.AppendText(Environment.NewLine);
            //textBox1.AppendText(totalString[2] + thirdStat.Substring(2, thirdStat.Length - 2));
            textBox1.AppendText(Regex.Match(totalString[2], @"^[^:]*")+thirdStat.Substring(2, thirdStat.Length - 2));
            textBox1.AppendText(Environment.NewLine);
            //textBox1.AppendText(totalString[3] + fourthStat.Substring(2, fourthStat.Length - 2));
            textBox1.AppendText(Regex.Match(totalString[3], @"^[^:]*")+fourthStat.Substring(2, fourthStat.Length - 2));
            textBox1.AppendText(Environment.NewLine);
            //textBox1.AppendText(totalString[4] + fifthStat.Substring(2, fifthStat.Length - 2));
            textBox1.AppendText(Regex.Match(totalString[4], @"^[^:]*")+fifthStat.Substring(2, fifthStat.Length - 2));
            textBox1.AppendText(Environment.NewLine);
            //textBox1.AppendText(totalString[5] + sixthStat.Substring(2, sixthStat.Length - 2));
            textBox1.AppendText(Regex.Match(totalString[5], @"^[^:]*")+sixthStat.Substring(2, sixthStat.Length - 2));
            textBox1.AppendText(Environment.NewLine);



            // return result;
            //create a variable for this and beautify the code
            return "Total: "+statSum.ToString()+"[ "+ totalString[0].Trim()+", "+totalString[1].Trim() + ", "+ totalString[2].Trim() + ", "+totalString[3].Trim() + ", "+totalString[4].Trim() + ", "+totalString[5].Trim() + "]";
        }


        private void Roll_Click(object sender, EventArgs e)
        {
            int d20 = Decimal.ToInt32(numericUpDown1.Value);
            int d12 = Decimal.ToInt32(numericUpDown2.Value);
            int d10 = Decimal.ToInt32(numericUpDown3.Value);
            int d8 = Decimal.ToInt32(numericUpDown4.Value);
            int d6 = Decimal.ToInt32(numericUpDown5.Value);
            int d4 = Decimal.ToInt32(numericUpDown6.Value);
            //int d20Number = numericUpDown1.value;
            //string getDice = string.Format("D20: {0}\n D12: {1}", numericUpDown1.Value, numericUpDown2.Value, numericUpDown3.Value, numericUpDown4.Value, numericUpDown5.Value, numericUpDown6.Value);
            //Send Results to TextBox1
            //string getDiceResult = "";
            if (numericUpDown1.Value > 0)
            {
                this.textBox1.AppendText(RollD(20, d20, checkBox1.Checked, checkBox2.Checked)) ;
                this.textBox1.AppendText(Environment.NewLine);
            }
            if (numericUpDown2.Value > 0)
            {
                this.textBox1.AppendText(RollD(12, d12, checkBox6.Checked, checkBox5.Checked));
                this.textBox1.AppendText(Environment.NewLine);
            }
            if (numericUpDown3.Value > 0)
            {
                this.textBox1.AppendText(RollD(10, d10, checkBox8.Checked, checkBox7.Checked));
                this.textBox1.AppendText(Environment.NewLine);
            }
            if (numericUpDown4.Value > 0)
            {
                this.textBox1.AppendText(RollD(8, d8, checkBox4.Checked, checkBox3.Checked));
                this.textBox1.AppendText(Environment.NewLine);
            }
            if (numericUpDown5.Value > 0)
            {
                this.textBox1.AppendText(RollD(6, d6, checkBox10.Checked, checkBox9.Checked));
                this.textBox1.AppendText(Environment.NewLine);
            }
            if (numericUpDown6.Value > 0)
            {
                this.textBox1.AppendText(RollD(4, d4, checkBox12.Checked, checkBox11.Checked));
                this.textBox1.AppendText(Environment.NewLine);
            }
            //Call the roll D20 method from the numeric value
            //this.textBox1.AppendText(rollD20(d20));
            //textBox AppendText requires Environment.NewLine for 
            //Newlines
            this.textBox1.AppendText(Environment.NewLine);
        }
        

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //d20 up/down
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            //d12
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            //d10
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            //d8
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            //d6
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            //d4
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //d20 advantage
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
        
        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {   //d20 quick roll
            this.textBox1.AppendText(RollD(20, 1));
            this.textBox1.AppendText(Environment.NewLine);
        }

        private void d100_Click(object sender, EventArgs e)
        {   //d100 quick roll
            //RollD(100, 1);
            this.textBox1.AppendText(RollD(100, 1));
            this.textBox1.AppendText(Environment.NewLine);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void potionDomain_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private string potionClick() 
        {
            string results = "";
            const string quit = "Potions";
            const int zero = 0;
            const int dieType = 4;
            const int regular = 2;
            const int greater = 4;
            const int superior = 8;
            const int supreme = 10;
            const int supremeDice = supreme*2;
            const string reg = "Regular",
                        great = "Greater",
                        super = "Superior",
                        supremeDie = "Supreme",
                        divider = " |";
            int counter = 0;
            int calculate = 0;
            
            string potionRoll = potionDomain.Text.ToString();
            string potionType = "";

            //List<int> rolls = rollMany(4,4);

            if (potionRoll.Equals(quit))
            {
                results = "Please select a listed option.";
                return results;
            }

            const string oneStat = "4d6Drop1";
            const string character = "6-4d6Drop1-70min";
            string accumulator = "";
            List<int> rolls = new List<int>();
            string totalString = " Total: ";
            if (potionRoll.Equals(reg))
            {
                potionType = reg;
                rolls = rollMany(dieType, regular);
                counter = regular;
                totalString = " + " + Convert.ToString(regular) + totalString;

            } else if(potionRoll.Equals(great))
                {
                potionType = great;
                rolls = rollMany(dieType, greater);
                counter = greater;
                totalString = " + " + Convert.ToString(greater) + totalString;

            } else if(potionRoll.Equals(super))
                {
                potionType = super;
                rolls = rollMany(dieType, superior);
                counter = superior;
                totalString = " + " + Convert.ToString(superior) + totalString;

            } else if(potionRoll.Equals(supremeDie))
                {
                potionType = supremeDie;
                rolls = rollMany(dieType, supreme);
                counter = supremeDice;
                totalString = " + " + Convert.ToString(supremeDice) + totalString;
            }
            else {
                results += "You have found a bug! Character stat roll call, roll operation not found.";
                return results;

            }
            results = "" + potionType + ": ";
            
            foreach(int roll in rolls)
                {
                calculate += roll;
                results += Convert.ToString(roll)+", ";
                
                }
            //results.Substring(results.Length - 2).Replace(',', ']');
            calculate += counter;
            //results += 
            results += totalString + Convert.ToString(calculate) + divider;
            
            return results;
        }



        //Likely deprecated method, remaining for reference
        private string potionClick_formatting()
        {
            const string reg = "Potion",
            great = "Greater",
            super = "Superior",
            supreme = "Supreme";
            int calculate = 0;
            string potionRoll = potionDomain.Text.ToString();
            const string quit = "Potions";
            const int zero = 0;
            string result = "";

            if (potionRoll.Equals(quit))
            {
                result = "Please select a listed option.";
                return result;
            }

            const string oneStat = "4d6Drop1";
            const string character = "6-4d6Drop1-70min";
            string accumulator = "";


            if (potionRoll.Equals(reg))
            {   //Roll 2D4
                //working
                //with format issues
                calculate = zero;
                accumulator = "";
                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                result += accumulator + " ";





                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", "") );
                calculate += 2;
                result += accumulator + " + 2|";

                textBox1.AppendText(Environment.NewLine);
                return result + " Total regained: " + Convert.ToString(calculate);
                textBox1.AppendText(Environment.NewLine);
            }
            else if (potionRoll.Equals(great))
            {   //Does not appear to be working
                //test output:
                //D4:  2   D4:  1   + 4| Total regained: 247
                //textBox1.AppendText("space for charactr rolling");
                calculate = zero;
                result += RollD(4, 1);
                calculate = zero;
                accumulator = "";
                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", "") );
                result += accumulator + " ";



                accumulator = RollD(4, 1) + " ";
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                result += accumulator + " ";


                accumulator = RollD(4, 1) + " ";
                calculate = Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", "") );
                result += accumulator + " ";

                accumulator += RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", "") );
                calculate += 4;

                result = accumulator + " + 4|";


                textBox1.AppendText(Environment.NewLine);
                return result + " Total regained: " + Convert.ToString(calculate);
                textBox1.AppendText(Environment.NewLine);
            }
            else if (potionRoll.Equals(super))
            {   //appears to be working
                //test output:
                //2D4: 2   D4: 3   D4: 2   D4: 1   D4: 2   D4: 1   D4: 3   D4: 4 + 8 | Total regained: 26
                //the leading decimal is not counted, the 2 before the D4
                calculate = zero;
                accumulator = "";
                accumulator = RollD(4, 1);
                textBox1.AppendText(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                result += accumulator + " ";



                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                result += accumulator + " ";


                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                result += accumulator + " ";

                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));


                result += accumulator + " ";

                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                result += accumulator + " ";

                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                result += accumulator + " ";

                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                result += accumulator + " ";

                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", "") );
                calculate += 8;

                result += accumulator + " + 8|";
                textBox1.AppendText(Environment.NewLine);
                return result + " Total regained: " + Convert.ToString(calculate);
                textBox1.AppendText(Environment.NewLine);
            } else if (potionRoll.Equals(supreme))
            {   //appears to be working
                //test output:
                //D4:  2   D4:  3   D4:  3   D4:  1   D4:  1   D4:  3   D4:  1   D4:  2   D4:  1   D4:  2   +20| Total regained: 39
                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                result += accumulator + " ";

                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                result += accumulator + " "; accumulator = RollD(4, 1);

                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                result += accumulator + " ";

                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                result += accumulator + " "; accumulator = RollD(4, 1);

                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                result += accumulator + " ";

                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                result += accumulator + " "; accumulator = RollD(4, 1);

                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                result += accumulator + " ";

                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                
                result += accumulator + " "; accumulator = RollD(4, 1);

                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));

                result += accumulator + " ";

                accumulator = RollD(4, 1);
                calculate += Convert.ToInt32(Regex.Replace(accumulator.Substring(2), "[^0-9]", ""));
                calculate += 20;
                result += accumulator + " +20|";

                textBox1.AppendText(Environment.NewLine);
                return result + " Total regained: " + Convert.ToString(calculate);
                textBox1.AppendText(Environment.NewLine);
            }
            else
            {
                result += "You have found a bug! Character stat roll call, roll operation not found.";
                return result;
            }

        }
        private void potionRoll_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(potionClick());
        }

        private void domainCharacterRoll_SelectedItemChanged(object sender, EventArgs e)
        {

        }
        private string statClick()
        {
            string statRoll = domainCharacterRoll.Text.ToString();
            const string quit = "5e Character Roll";
            string result = "";

            if (statRoll.Equals(quit))
            {
                result = "Please select a listed option.";
                return result;
            }

            const string oneStat = "4d6Drop1";
            const string character = "6-4d6Drop1-70min";



            if (statRoll.Equals(oneStat))
            {   //Roll 4D6, drop the lowest
                return result += roll4D6();

            }
            else if (statRoll.Equals(character))
            {
                //textBox1.AppendText("space for charactr rolling");
                result = roll70();
                return result;
            }
            else
            {
                result += "You have found a bug! Character stat roll call, roll operation not found.";
                return result;
            }

        }
        private void characterRoll_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(statClick());
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create a new instance of the Form3 class
            Form3 Credits = new Form3();

            // Show the settings form
            Credits.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create a new instance of the Form3 class
            Network Network = new Network();

            // Show the settings form
            Network.Show();
        }
    }
}
