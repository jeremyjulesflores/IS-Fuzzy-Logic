using DotFuzzy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IS_Fuzzy_Logic
{
    public partial class Form1 : Form
    {
        FuzzyEngine fe;
        MembershipFunctionCollection roomTemp, targetTemp, acOutput;
        LinguisticVariable roomTempLevel, targetTempLevel, acOutputLevel;
        FuzzyRuleCollection myrules;



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setMembers();
            setRules();
        }
        public void setMembers()
        {

            roomTemp = new MembershipFunctionCollection();
            roomTemp.Add(new MembershipFunction("VERY_COLD", -10.0, 0.0, 0.0, 10.0));
            roomTemp.Add(new MembershipFunction("COLD", 0.0, 10.0, 10.0, 20.0));
            roomTemp.Add(new MembershipFunction("WARM", 10.0, 20.0, 20.0, 30.0));
            roomTemp.Add(new MembershipFunction("HOT", 20.0, 30.0, 30.0, 40.0));
            roomTemp.Add(new MembershipFunction("VERY_HOT", 30.0, 40.0, 40.0, 50.0));
            roomTempLevel = new LinguisticVariable("ROOM", roomTemp);


            targetTemp = new MembershipFunctionCollection();
            targetTemp.Add(new MembershipFunction("VERY_COLD", -10.0, 0.0, 0.0, 10.0));
            targetTemp.Add(new MembershipFunction("COLD", 0.0, 10.0, 10.0, 20.0));
            targetTemp.Add(new MembershipFunction("WARM", 10.0, 20.0, 20.0, 30.0));
            targetTemp.Add(new MembershipFunction("HOT", 20.0, 30.0, 30.0, 40.0));
            targetTemp.Add(new MembershipFunction("VERY_HOT", 30.0, 40.0, 40.0, 50.0));
            targetTempLevel = new LinguisticVariable("TARGET", targetTemp);

            acOutput = new MembershipFunctionCollection();
            acOutput.Add(new MembershipFunction("COOL", -10.0, -5.0, -5.0, 0.0));
            acOutput.Add(new MembershipFunction("NO_CHANGE", -5.0, 0.0, 0.0, 5.0));
            acOutput.Add(new MembershipFunction("HEAT", 0.0, 5.0, 5.0, 10.0));
            acOutputLevel = new LinguisticVariable("ACOUTPUT", acOutput);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fuziffyvalues();
            defuzzy();
        }

        public void setRules()
        {
            myrules = new FuzzyRuleCollection();
            myrules.Add(new FuzzyRule("IF (ROOM IS COLD) AND (TARGET IS WARM) THEN ACOUTPUT IS HEAT"));
            myrules.Add(new FuzzyRule("IF (ROOM IS VERY_COLD) AND (TARGET IS WARM) THEN ACOUTPUT IS HEAT"));


            myrules.Add(new FuzzyRule("IF (ROOM IS HOT) AND (TARGET IS WARM) THEN ACOUTPUT IS COOL"));
            myrules.Add(new FuzzyRule("IF (ROOM IS VERY_HOT) AND (TARGET IS WARM) THEN ACOUTPUT IS COOL"));


            myrules.Add(new FuzzyRule("IF (ROOM IS WARM) AND (TARGET IS WARM) THEN ACOUTPUT IS NO_CHANGE"));
            myrules.Add(new FuzzyRule("IF (ROOM IS COLD) AND (TARGET IS COLD) THEN ACOUTPUT IS NO_CHANGE"));
            myrules.Add(new FuzzyRule("IF (ROOM IS VERY_COLD) AND (TARGET IS VERY_COLD) THEN ACOUTPUT IS NO_CHANGE"));
            myrules.Add(new FuzzyRule("IF (ROOM IS VERY_HOT) AND (TARGET IS VERY_HOT) THEN ACOUTPUT IS NO_CHANGE"));
            myrules.Add(new FuzzyRule("IF (ROOM IS HOT) AND (TARGET IS HOT) THEN ACOUTPUT IS NO_CHANGE"));

        }

        public void setFuzzyEngine()
        {
            fe = new FuzzyEngine();
            fe.LinguisticVariableCollection.Add(roomTempLevel);
            fe.LinguisticVariableCollection.Add(targetTempLevel);
            fe.LinguisticVariableCollection.Add(acOutputLevel);
            fe.FuzzyRuleCollection = myrules;
        }

        public void fuziffyvalues()
        {
            roomTempLevel.InputValue = 30.0;
            roomTempLevel.Fuzzify("HOT");
            targetTempLevel.InputValue = (Convert.ToDouble(textBox1.Text));
            targetTempLevel.Fuzzify("VERY_HOT");

        }
        public void defuzzy()
        {
            setFuzzyEngine();
            fe.Consequent = "ACOUTPUT";
            label11.Text = "" + fe.Defuzzify();
            label6.Text = textBox1.Text;

            fe.Consequent = "ROOM";
            label7.Text = "" + fe.Defuzzify();

        }

    }
}
