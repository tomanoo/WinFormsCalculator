using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsCalculator
{

    /******************************************************************
                                THINGS TO DO
                                
    -> limit the amount of numbers in a value
    -> improve deleting last index/sign
    -> name methods properly - not these bad looking "buttonXX_click()"
    -> improve "-" at the beginning of second value
    -> later on, add keyboard usage

    ******************************************************************/

    public partial class Form1 : Form
    {
        string firstValue = null,       //first value
            secondValue = null,         //second value
            sign = null;                //arithmetic operation sign
        bool comma = false,             //is there already a comma (dot/point) in a value
            isSign = false,             //has the "sign" been chosen
            isFirstValueSet = false,    //as it states
            minus = false,              //is there already a minus in front of first value
            isResult = false;           //has the "=" sign been pressed
        double result;                  //as it states
        int currentLabel = 0;           //label to control which value/sign to delete


        public Form1()
        {
            InitializeComponent();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        //inserting numbers to the values
        private void button2_Click(object sender, EventArgs e)
        {
            var value = sender as Button;
            if (!isFirstValueSet)
            {
                if (value.Text == "." && !comma)
                {
                    if (firstValue == null || firstValue == "-")
                    {
                        firstValue += "0" + value.Text;
                    }
                    else
                    {
                        firstValue += value.Text;
                    }
                    comma = true;
                }

                else if (value.Text == "0" && (firstValue == null || firstValue == "-"))
                {
                    firstValue += value.Text + ".";
                    comma = true;
                }
                
                else if (comma && value.Text != ".")
                {
                    firstValue += value.Text;
                }

                else if (!comma && value.Text != ".")
                {
                    if (value.Text == ".")
                        comma = true;
                    firstValue += value.Text;
                }

                if (isSign)
                {
                    isFirstValueSet = true;
                    comma = false;
                }

                textBox1.Text = firstValue;
            }
            else
            {
                currentLabel = 2;
                if (value.Text == "." && !comma)
                {
                    if (secondValue == null || secondValue == "-")
                    {
                        secondValue += "0" + value.Text;
                    }
                    else
                    {
                        secondValue += value.Text;
                    }
                    comma = true;
                }

                else if (value.Text == "0" && (secondValue == null || secondValue == "-"))
                {
                    secondValue += value.Text + ".";
                    comma = true;
                }

                else if (comma && value.Text != ".")
                {
                    secondValue += value.Text;
                }

                else if (!comma && value.Text != ".")
                {
                    if (value.Text == ".")
                        comma = true;
                    secondValue += value.Text;
                }

                textBox1.Text = firstValue + " " + sign + " " + secondValue;
            }
        }

        //getting arithmetic operation sign
        private void button12_Click(object sender, EventArgs e)
        {
            var value = sender as Button;
            if (!isResult)
            {
                if (!isFirstValueSet && !isSign)
                {
                    if (firstValue != "-" && firstValue != "0." && firstValue != "-0.")
                    {
                        sign = value.Text;
                        ++currentLabel;
                        if (sign == "-" && firstValue == null && !minus)
                        {
                            firstValue += sign;
                            minus = true;
                            textBox1.Text += sign;
                            sign = null;
                        }
                        else if (firstValue != null && firstValue[firstValue.Length - 1] != '.')
                        {
                            isSign = true;
                            isFirstValueSet = true;
                            textBox1.Text += " " + sign + " ";
                        }
                    }
                }
                else if (isSign && secondValue == null && value.Text == "-")
                {
                    secondValue += value.Text;
                    textBox1.Text += value.Text;
                }
            }
            else
            {
                if (!isSign)
                {
                    sign = value.Text;
                    ++currentLabel;
                    isSign = true;
                    textBox1.Text += " " + sign + " ";
                }
            }
        }

        //counting
        private void button16_Click(object sender, EventArgs e)
        {
            switch (sign)
            {
                case "+":
                    result = Convert.ToDouble(firstValue) + Convert.ToDouble(secondValue);
                    break;
                case "-":
                    result = Convert.ToDouble(firstValue) - Convert.ToDouble(secondValue);
                    break;
                case "*":
                    result = Convert.ToDouble(firstValue) * Convert.ToDouble(secondValue);
                    break;
                case "/":
                    if (secondValue == "0.")
                        MessageBox.Show("Nie dziel przez zero, cholero!");
                    else
                        result = Convert.ToDouble(firstValue) / Convert.ToDouble(secondValue);
                    break;
            }
            textBox1.Text = Convert.ToString(result);
            firstValue = Convert.ToString(result);
            sign = null;
            isSign = false;
            secondValue = null;
            currentLabel = 1;
            isResult = true;
        }

        //deleting last index/sign
        private void button18_Click(object sender, EventArgs e)
        {
            
            switch (currentLabel)
            {
                case 0:
                    if (firstValue == "0." || firstValue == "-0.")// || firstValue.Length == 1)
                    {
                        firstValue = null;
                        minus = false;
                        comma = false;
                        textBox1.Text = null;
                        break;
                    }
                    else
                    {
                        try
                        {
                            if (firstValue[firstValue.Length - 1] == '.')
                                comma = false;
                            firstValue = firstValue.Remove(firstValue.Length - 1);
                        }
                        catch
                        {
                            MessageBox.Show("Nie ma czego usuwać!");
                        }
                        textBox1.Text = firstValue;
                        break;
                    }
                case 1:
                    sign = null;
                    isSign = false;
                    isFirstValueSet = false;
                    textBox1.Text = firstValue;
                    currentLabel = 0;
                    break;
                case 2:
                    if (secondValue == "0." || secondValue == "-0.")// || secondValue.Length == 1)
                    {
                        secondValue = null;
                        //minus = false;
                        comma = false;
                        textBox1.Text = firstValue + " " + sign;
                        currentLabel = 1;
                        break;
                    }
                    else
                    {
                        if (secondValue != null)
                        {
                            if (secondValue[secondValue.Length - 1] == '.')
                                comma = false;
                            secondValue = secondValue.Remove(secondValue.Length - 1);
                            textBox1.Text = firstValue + " " + sign + " " + secondValue;
                            break;
                        }
                        if (secondValue == null)
                            currentLabel = 1;
                        break;
                    }
            }
        }

        //clear all
        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = firstValue = secondValue = sign = null;
            result = currentLabel = 0;
            comma = isFirstValueSet = isSign = isResult = minus = false;
        }

        //clear current label
        private void button19_Click(object sender, EventArgs e)
        {
            switch (currentLabel)
            {
                case 0:
                    textBox1.Text = firstValue = null;
                    comma = isFirstValueSet = isSign = isResult = minus = false;
                    break;
                case 1:
                    sign = null;
                    --currentLabel;
                    isSign = minus = false;
                    textBox1.Text = firstValue;
                    break;
                case 2:
                    secondValue = null;
                    comma = minus = false;
                    textBox1.Text = firstValue + " " + sign + " ";
                    break;

            }
        }
    }
}
