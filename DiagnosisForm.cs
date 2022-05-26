using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class DiagnosisForm : Form
    {
        public Patient Patient;
        public DiagnosisInfo info;

        public DiagnosisForm(Patient patient)
        {
            Patient = patient;
            InitializeComponent();
            StartQuestionaire();
        }

        public void StartQuestionaire()
        {
            PatientDiagnoser diagnoser = new PatientDiagnoser(Patient);
            info = diagnoser.GetDiagnose();


            foreach (Treatment t in info.TotalTreatments)
            {
                richTextBox1.Text += "•  " + t.Name;
                richTextBox1.Text += "\n";

                richTextBox2.Text += "•  " + t.Details;
                richTextBox2.Text += "\n";
            }
            foreach (string extrainfo in info.TotalExtraInfo)
            {
                richTextBox3.Text += "•  " + extrainfo;
                richTextBox3.Text += "\n";
            }
        }
    }

    public class PatientDiagnoser
    {
        public List<Question> AskedQuestions = new List<Question>();
        public Patient Patient;

        public PatientDiagnoser(Patient patient)
        {
            Patient = patient;
        }



        public DiagnosisInfo GetDiagnose()
        {
            WBCDiagnose WBCD = new WBCDiagnose(Patient.type, Patient.BloodValues.WBC, this);
            NeutDiagnose NEUTD = new NeutDiagnose(Patient.BloodValues.Neut, Patient.BloodValues.WBC);
            LymphDiagnose LD = new LymphDiagnose(Patient.BloodValues.Lymph, Patient.BloodValues.WBC);
            RBCDiagnose RBCD = new RBCDiagnose(Patient.BloodValues.RBC, this);
            HCTDiagnose HCTD = new HCTDiagnose(Patient.BloodValues.HCT, Patient.Gender, this);
            UreaDiagnose UREAD = new UreaDiagnose(Patient.BloodValues.Urea, Patient.Gender, this);
            HbDiagnose HBD = new HbDiagnose(Patient.BloodValues.Hb, Patient.type);
            CrtnDiagnose CRTND = new CrtnDiagnose(Patient.BloodValues.Crtn, Patient.Age, this);
            HDLDiagnose HDLD = new HDLDiagnose(Patient.BloodValues.HDL, Patient.Gender, this);
            IronDiagnose IROND = new IronDiagnose(Patient.BloodValues.Iron, Patient.Gender, this);
            APDiagnose APD = new APDiagnose(Patient.BloodValues.AP, Patient.Gender, this);

            Report[] reports = new Report[] { WBCD.Diagnose(), NEUTD.Diagnose(), LD.Diagnose(), RBCD.Diagnose(), HCTD.Diagnose(), UREAD.Diagnose(), HBD.Diagnose(), CRTND.Diagnose(), HDLD.Diagnose(), IROND.Diagnose(), APD.Diagnose() };

            DiagnosisInfo diagnosisInfo = new DiagnosisInfo();

            foreach (Report report in reports)
            {
                if (report == null)
                {
                    continue;
                }

                if (report.Treatments != null)
                {
                    foreach (Treatment treatment in report.Treatments)
                    {
                        if(diagnosisInfo.TotalTreatments.Find(x => x.GetType() == treatment.GetType()) == null)
                        {
                            diagnosisInfo.TotalTreatments.Add(treatment);
                        }
                    }
                }

                if (report.extraInformation != null)
                    diagnosisInfo.TotalExtraInfo.Add(report.extraInformation);

            }

            return diagnosisInfo;

        }
        public DialogResult AskQuestion(Question question)
        {
            Question ques = AskedQuestions.Find(q => q.QuestionType == question.QuestionType);
            if (ques != null)
            {
                return ques.Result;
            }
            else
            {
                question.Result = MessageBox.Show(question.Text, question.Title, MessageBoxButtons.YesNo);
                AskedQuestions.Add(question);
                return question.Result;
            }

        }
    }

    public class DiagnosisInfo
    {
        public List<Treatment> TotalTreatments = new List<Treatment>();
        public List<string> TotalExtraInfo = new List<string>();

        
    }

    
    
    
    public abstract class Diagnose_Base
    {
        public abstract float BloodValue { get; set; }
        public abstract Report Diagnose();
    }
    /*----------------------------------------------------------------------------------------------*/
    public class WBCDiagnose : Diagnose_Base, IQuestionire
    {
        public override float BloodValue { get; set; }
        public PatientType PatientType { get; set; }
        public Question Question { get; set; }
        public PatientDiagnoser PatientDiagnoser { get; set; }

        public WBCDiagnose(PatientType type, string bv, PatientDiagnoser diagnoser)
        {
            BloodValue = int.Parse(bv);
            PatientType = type;
            PatientDiagnoser = diagnoser;
            Question = new Question(QuestionType.HasFever, "Do you know if you have a fever illness/disease?");
        }

        public override Report Diagnose()
        {
            
            switch (PatientType)
            {
                case PatientType.Adult:

                    return adultCaseWBC();

                case PatientType.Child:
                    return childCaseWBC();

                case PatientType.Baby:
                    return babyCaseWBC();
            }
            return null;
        }

        private Report babyCaseWBC()
        {
            if (BloodValue >= 18500)
            {
                Treatment[] treatments = new Treatment[] { new CancerTreatment(), new BloodDiseaseTreatment() };
                return new Report(treatments);
            }
            else if (BloodValue > 17500)
            {
                DialogResult dialogResult = PatientDiagnoser.AskQuestion(Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Treatment[] treatments = new Treatment[] { new InfectionTreatment() };
                    return new Report(treatments);
                }
            }
            else if (BloodValue < 6000)
            {
                Treatment[] treatments = new Treatment[] { new ViralTreatment(), new CancerTreatment() };
                return new Report(treatments);
            }
            return null;
        }
        private Report childCaseWBC()
        {
            if (BloodValue >= 16500)
            {
                Treatment[] treatments = new Treatment[] { new CancerTreatment(), new BloodDiseaseTreatment() };
                return new Report(treatments);
            }
            else if (BloodValue > 15500)
            {
                DialogResult dialogResult = PatientDiagnoser.AskQuestion(Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Treatment[] treatments = new Treatment[] { new InfectionTreatment() };
                    return new Report(treatments);
                }
            }
            else if (BloodValue < 5500)
            {
                Treatment[] treatments = new Treatment[] { new ViralTreatment(), new CancerTreatment() };
                return new Report(treatments);
            }
            return null;
        }
        private Report adultCaseWBC()
        {
            if (BloodValue >= 12500)
            {
                Treatment[] treatments = new Treatment[] { new CancerTreatment(), new BloodDiseaseTreatment() };
                return new Report(treatments);
            }
            else if (BloodValue > 11000)
            {
                DialogResult dialogResult = PatientDiagnoser.AskQuestion(Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Treatment[] treatments = new Treatment[] { new InfectionTreatment() };
                    return new Report(treatments);
                }
            }
            else if (BloodValue < 4500)
            {
                Treatment[] treatments = new Treatment[] { new ViralTreatment(), new CancerTreatment() };
                return new Report(treatments);
            }
            return null;
        }
    }
    /*----------------------------------------------------------------------------------------------*/
    public class NeutDiagnose : Diagnose_Base
    {
        public override float BloodValue { get; set; }
        public int WBCBloodValue;

        public NeutDiagnose(string bv, string wbcValue)
        {
            BloodValue = int.Parse(bv);
            WBCBloodValue = int.Parse(wbcValue);
        }

        public override Report Diagnose()
        {
            return NeutTerms();
        }

        private Report NeutTerms()
        {

            if (BloodValue > 54)
            {
                Treatment[] treatments = new Treatment[] { new InfectionTreatment() };
                return new Report(treatments);
            }
            else if (BloodValue < 28)
            {
                Treatment[] treatments = new Treatment[] { new BloodCellCreationTreatment(), new InfectionTreatment(), new CancerTreatment() };
                return new Report(treatments);
            }
            return null;
        }
    } 
    /*----------------------------------------------------------------------------------------------*/
    public class LymphDiagnose : Diagnose_Base
    {
        public override float BloodValue { get; set; }
        public int WBCBloodValue;

        public LymphDiagnose(string bv, string wbcValue)
        {
            BloodValue = int.Parse(bv);
            WBCBloodValue = int.Parse(wbcValue);
        }

        public override Report Diagnose()
        {
            return LymphTerms();
        }

        private Report LymphTerms()
        {
            if (BloodValue > 52)
            {
                Treatment[] treatments = new Treatment[] { new InfectionTreatment(), new CancerTreatment() };
                return new Report(treatments);
            }
            else if (BloodValue < 36)
            {
                Treatment[] treatments = new Treatment[] { new BloodCellCreationTreatment() };
                return new Report(treatments);
            }
            return null;
        }
    } 
    /*----------------------------------------------------------------------------------------------*/
    public class RBCDiagnose : Diagnose_Base, IQuestionire
    {
        public override float BloodValue { get; set; }
        public Question Question { get; set; }
        public PatientDiagnoser PatientDiagnoser { get; set; }

        public RBCDiagnose(string bv, PatientDiagnoser diagnoser)
        {
            BloodValue = float.Parse(bv);
            PatientDiagnoser = diagnoser;
            Question = new Question(QuestionType.SmokingLungs, "Are you a smoker or have any lungs illness?");

        }

        public override Report Diagnose()
        {
            return RBCTerms();
        }

        private Report RBCTerms()
        {
            if (BloodValue >= 7)
            {
                DialogResult dialogResult = PatientDiagnoser.AskQuestion(Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Treatment[] treatments = new Treatment[] { new SmokeTreatment(), new LungsTreatment(), new BloodCellCreationTreatment() };
                    return new Report(treatments);
                }

            }
            else if (BloodValue > 6)
            {
                Treatment[] treatments = new Treatment[] { new BloodCellCreationTreatment(), new SmokeTreatment() };
                return new Report(treatments);
            }
            else if (BloodValue < 4.5)
            {
                Treatment[] treatments = new Treatment[] { new AnemiaTreatment(), new BleedingTreatment() };
                return new Report(treatments);
            }
            return null;
        }
    }
    /*----------------------------------------------------------------------------------------------*/
    public class HCTDiagnose : Diagnose_Base, IQuestionire
    {
        public override float BloodValue { get; set; }
        public Question Question { get; set; }
        public PatientDiagnoser PatientDiagnoser { get; set; }
        public PatientGender Gender { get; set; }

        public HCTDiagnose(string bv, PatientGender gender, PatientDiagnoser diagnoser)
        {
            BloodValue = int.Parse(bv);
            PatientDiagnoser = diagnoser;
            Question = new Question(QuestionType.IsSmoking, "Are you a smoker?");
            Gender = gender;
        }

        public override Report Diagnose()
        {
            return HCTTerms();
        }

        private Report HCTTerms()
        {
            switch (Gender)
            {
                case PatientGender.Male:
                    return MaleTerm();
                case PatientGender.Female:
                    return FemaleTerm();
            }
            return null;
        }
        private Report MaleTerm()
        {
            if (BloodValue > 54)
            {
                DialogResult dialogResult = PatientDiagnoser.AskQuestion(Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Treatment[] treatments = new Treatment[] { new SmokeTreatment() };
                    return new Report(treatments);
                }
            }
            else if (BloodValue < 37)
            {
                Treatment[] treatments = new Treatment[] { new AnemiaTreatment(), new BleedingTreatment() };
                return new Report(treatments);
            }
            return null;
        }
        private Report FemaleTerm()
        {
            if (BloodValue > 47)
            {
                DialogResult dialogResult = PatientDiagnoser.AskQuestion(Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Treatment[] treatments = new Treatment[] { new SmokeTreatment() };
                    return new Report(treatments);
                }
            }
            else if (BloodValue < 33)
            {
                Treatment[] treatments = new Treatment[] { new AnemiaTreatment(), new BleedingTreatment() };
                return new Report(treatments);
            }
            return null;
        }
    }
    /*----------------------------------------------------------------------------------------------*/
    public partial class UreaDiagnose : Diagnose_Base, IQuestionire
    {
        public override float BloodValue { get; set; }
        public PatientGender Gender { get; set; }
        public Question Question { get; set; }
        public PatientDiagnoser PatientDiagnoser { get; set; }

        public UreaDiagnose(string bv, PatientGender gender, PatientDiagnoser diagnoser)
        {
            BloodValue = int.Parse(bv);
            PatientDiagnoser = diagnoser;
            Question = new Question(QuestionType.IsEasternen, "Are you an Easterner?");
            Gender = gender;
        }

        public override Report Diagnose()
        {
            return UreaTerms();
        }

        private Report UreaTerms()
        {
            DialogResult dialogResult = PatientDiagnoser.AskQuestion(Question);
            if (dialogResult == DialogResult.Yes)
            {
                BloodValue += (BloodValue * 0.1f);
            }

            if (BloodValue < 17)
            {
                Treatment[] treatments = new Treatment[] { new MalnutritionTreatment(), new DietTreatment(), new LiverTreatment() };

                if (Gender == PatientGender.Female)
                {
                    DialogResult secondDialogResult1 = PatientDiagnoser.AskQuestion(Question);
                    if (secondDialogResult1 == DialogResult.Yes)
                    {
                        return new Report(treatments, "Values are likely to be lower when the patient is pregnant.\nI recommend you take pregnancy test.");
                    }

                }
                else
                {
                    return new Report(treatments);
                }
            }
            if (BloodValue > 43)
            {
                Treatment[] treatments1 = new Treatment[] { new DehydrationTreatment(), new KidneyTreatment(), new DietTreatment() };
                return new Report(treatments1);
            }

            return null;
        }

    }
    /*----------------------------------------------------------------------------------------------*/
    public partial class HbDiagnose : Diagnose_Base
    {
        public override float BloodValue { get; set; }
        public PatientType PatientType { get; set; }


        public HbDiagnose(string bv, PatientType type)
        {
            BloodValue = float.Parse(bv);
            PatientType = type;
        }

        public override Report Diagnose()
        {
            return HbTerms();
        }

        private Report HbTerms()
        {
            Treatment[] treatments = new Treatment[] { new AnemiaTreatment(), new HematologicalTreatment(), new IronDefTreatment(), new BleedingTreatment() };
            switch (PatientType)
            {
                case PatientType.Adult:
                    if (BloodValue < 12) { return new Report(treatments); }
                    break;
                default:
                    if (BloodValue < 11.5) { return new Report(treatments); }
                    break;
            }
            return null;
        }
    }
    /*----------------------------------------------------------------------------------------------*/
    public partial class CrtnDiagnose : Diagnose_Base, IQuestionire
    {
        public override float BloodValue { get; set; }
        public Question Question { get; set; }
        public PatientDiagnoser PatientDiagnoser { get; set; }
        public int Age;


        public CrtnDiagnose(string bv, int age, PatientDiagnoser diagnoser)
        {
            BloodValue = float.Parse(bv);
            PatientDiagnoser = diagnoser;
            Question = new Question(QuestionType.DiarrheaVomits, "Do you have Diarrhea / Vomits?");
            Age = age;
        }

        public override Report Diagnose()
        {
            Treatment[] treatments1 = new Treatment[] { new KidneyTreatment(), new MuscleTreatment(), new MeatTreatment() };
            Treatment[] treatments2 = new Treatment[] { new MalnutritionTreatment() };

            if (isHighValue(Age))
            {
                DialogResult dialogResult = PatientDiagnoser.AskQuestion(Question);
                if (dialogResult == DialogResult.Yes)
                {
                    return new Report(treatments1);
                }
            }
            if (isLowValue(Age))
            {
                return new Report(treatments2);
            }
            return null;
        }
        private bool isHighValue(int Age)
        {
            if (Age >= 0 && Age <= 2)
            {
                if (BloodValue > 0.5f)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (Age >= 3 && Age <= 17)
            {
                if (BloodValue > 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (Age >= 18 && Age <= 59)
            {
                if (BloodValue > 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (Age > 59)
            {
                if (BloodValue > 1.2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        private bool isLowValue(int Age)
        {
            if (Age >= 0 && Age <= 2)
            {
                if (BloodValue < 0.2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (Age >= 3 && Age <= 17)
            {
                if (BloodValue < 0.5)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (Age >= 18)
            {
                if (BloodValue < 0.6)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

    }
    /*----------------------------------------------------------------------------------------------*/
    public partial class HDLDiagnose : Diagnose_Base
    {
        public override float BloodValue { get; set; }
        public Question Question { get; set; }
        public PatientDiagnoser PatientDiagnoser { get; set; }
        public PatientGender Gender;


        public HDLDiagnose(string bv, PatientGender gender, PatientDiagnoser diagnoser)
        {
            BloodValue = int.Parse(bv);
            PatientDiagnoser = diagnoser;
            Question = new Question(QuestionType.isEthyopian, "Are you Ethyopian?");
            Gender = gender;
        }

        public override Report Diagnose()
        {
            Treatment[] treatments = new Treatment[] { new HyperlipidemiaTreatment(), new AdultsDiabetesTreatment(), new HeartTreatment() };

            DialogResult dialogResult = PatientDiagnoser.AskQuestion(Question);
            if (dialogResult == DialogResult.Yes)
            {
                BloodValue += (BloodValue * 0.2f);
            }

            return HDLTerms(treatments);

        }

        private Report HDLTerms(Treatment[] treatments)
        {
            if (Gender == PatientGender.Male)
            {
                if (BloodValue < 29)
                {
                    return new Report(treatments);
                }
                if (BloodValue > 62)
                {
                    return new Report("You might consider physical practice to keep increasing the positive HDL level.");
                }
            }
            if (Gender == PatientGender.Female)
            {
                if (BloodValue < 34)
                {
                    return new Report(treatments);
                }
                if (BloodValue > 82)
                {
                    return new Report("You might consider physical practice to keep increasing the positive HDL level.");
                }
            }
            return null;
        }

    }
    /*----------------------------------------------------------------------------------------------*/
    public partial class APDiagnose : Diagnose_Base, IQuestionire
    {
        public override float BloodValue { get; set; }
        public Question Question { get; set; }
        public PatientDiagnoser PatientDiagnoser { get; set; }
        public PatientGender Gender;


        public APDiagnose(string bv, PatientGender gender, PatientDiagnoser diagnoser)
        {
            PatientDiagnoser = diagnoser;
            Question = new Question(QuestionType.IsEasternen, "Are you an Easterner?");
            BloodValue = int.Parse(bv);
            Gender = gender;
        }

        public override Report Diagnose()
        {

            Treatment[] highTreatments = new Treatment[] { new LiverTreatment(), new BiliaryTreatment(), new DifMedicineTreatment(), new ThyroidTreatment() };
            Treatment[] lowTreatment = new Treatment[] { new LackOfVitsTreatment(), new MalnutritionTreatment() };

            DialogResult dialogResult = PatientDiagnoser.AskQuestion(Question);
            if (dialogResult == DialogResult.Yes)
            {
                return easternerTerms(highTreatments, lowTreatment);
            }
            else
            {
                return restOfPopulation(highTreatments, lowTreatment);

            }
        }

        private Report restOfPopulation(Treatment[] highTreatments, Treatment[] lowTreatment)
        {
            if (BloodValue > 90)
            {
                if (Gender == PatientGender.Female)
                {
                    return new Report(highTreatments, "Please check if you're pregnant, it could be the cause of the high values.");
                }
                else
                {
                    return new Report(highTreatments);
                }
            }
            if (BloodValue < 30)
            {
                return new Report(lowTreatment, "You might lack Vitamines like B12, C, B6 and Folic Acid, it's recommended you take them.");

            }
            return null;
        }
        private Report easternerTerms(Treatment[] highTreatments, Treatment[] lowTreatment)
        {
            if (BloodValue > 120)
            {
                if (Gender == PatientGender.Female)
                {
                    return new Report(highTreatments, "Please check if you're pregnant, it could be the cause of the high values.");
                }
                else
                {
                    return new Report(highTreatments);
                }
            }
            if (BloodValue < 60)
            {
                return new Report(lowTreatment, "You might lack Vitamines like B12, C, B6 and Folic Acid, it's recommended you take them.");

            }
            return null;
        }


    }
    /*----------------------------------------------------------------------------------------------*/
    public partial class IronDiagnose : Diagnose_Base, IQuestionire
    {
        public override float BloodValue { get; set; }
        public Question Question { get; set; }
        public PatientDiagnoser PatientDiagnoser { get; set; }
        public PatientGender Gender;


        public IronDiagnose(string bv, PatientGender gender, PatientDiagnoser diagnoser)
        {
            BloodValue = int.Parse(bv);
            PatientDiagnoser = diagnoser;
            Question = new Question(QuestionType.isPregnant, "Are you an pregnant?");
            Gender = gender;
        }

        public override Report Diagnose()
        {
            if (Gender == PatientGender.Female)
            {
                BloodValue -= (BloodValue * 0.2f);
                highForBoth();
                if (isLowValue())
                {
                    DialogResult dialogResult = PatientDiagnoser.AskQuestion(Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Treatment[] PregTreatments = new Treatment[] { new MalnutritionTreatment(), new IronDefTreatment(), new BleedingTreatment() };
                        return new Report(PregTreatments);
                    }
                    else
                    {
                        return lowForboth();
                    }
                }
                return null;
            }
            else
            {
                highForBoth();
                return lowForboth();
            }
        }

        private Report lowForboth()
        {
            if (!isLowValue())
            {
                Treatment[] treatments = new Treatment[] { new BleedingTreatment() };
                return new Report(treatments);
            }
            return null;
        }
        private Report highForBoth()
        {
            if (isHighValue())
            {
                Treatment[] hightreatments = new Treatment[] { new IronPoisTreatment() };
                return new Report(hightreatments);
            }
            return null;
        }
        private bool isHighValue()
        {
            if (BloodValue > 160)
            {
                return true;
            }
            return false;
        }
        private bool isLowValue()
        {
            if (BloodValue < 160)
            {
                return true;
            }
            return false;
        }
    }
}


