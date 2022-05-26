namespace WindowsFormsApp1
{


    

    public abstract class Treatment
    {
        public abstract string Details { get; set; }
        public abstract string Name { get; set; }
    }


    
    public class InfectionTreatment : Treatment
    {
        public override string Details { get; set; }
        public override string Name { get; set; }
        public InfectionTreatment()
        {
            Details = "Dedicated Antibiotic";
            Name = "Infection";
        }
    }
    public class ViralTreatment : Treatment
    {
        public override string Details { get; set; }
        public override string Name { get; set; }
        public ViralTreatment()
        {
            Details = "Rest at home";
            Name = "Viral Disease";
        }
    }
    public class SmokeTreatment : Treatment
    {
        public override string Details { get; set; }
        public override string Name { get; set; }
        public SmokeTreatment()
        {
            Details = "Stop smoking, it's bad for you.";
            Name = "Smoking";
        }
    }
    public class AnemiaTreatment : Treatment
    {
        public override string Details { get; set; }
        public override string Name { get; set; }
        public AnemiaTreatment()
        {
            Name = "Anemia";
            Details = "x2 pills of 10mg of B12 every day for 30 days.";
        }
    }
    public class LackOfVitsTreatment : Treatment
    {
        public override string Details { get; set; }
        public override string Name { get; set; }
        public LackOfVitsTreatment()
        {
            Name = "Lack of Vitamines";
            Details = "Referral to blood test to detect the missing vitamines.";
        }
    }
    public class MalnutritionTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public MalnutritionTreatment()
        {
            Name = "Malnutrition";
            Details = "Set an appointment with a nutritionist.";
        }
    }
    public class LiverTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public LiverTreatment()
        {
            Name = "Liver Disease";
            Details = "Referral to a specific diagnosis for the purpose of setting a treatment";
        }
    }
    public class BiliaryTreatment : Treatment //darkei amara
    {
        public override string Details { get; set; }
        public override string Name { get; set; }
        public BiliaryTreatment()
        {
            Name = "Biliary Disease";
            Details = "Referral to surgical treatment";
        }
    }
    public class ThyroidTreatment : Treatment //balutat a tris
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public ThyroidTreatment()
        {
            Name = "Overactive Thyroid Gland";
            Details = " Propylthiouracil for lowering the thyroid's activity.";
        }
    }
    public class DifMedicineTreatment : Treatment
    {
        public override string Name { get; set;}
        public override string Details { get; set; }
        public DifMedicineTreatment()
        {
            Name = "Different Medicines Usage";
            Details = "Referral to a family's doctor to check the match between different medicines.";
        }
    }
    public class MeatTreatment : Treatment
    {
        public override string Name { get; set;}
        public override string Details { get; set; }
        public MeatTreatment()
        {
            Name = "Increased consumption of meat";
            Details = "Set an appointment with a nutritionist.";
        }
    }
    public class CancerTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public CancerTreatment()
        {
            Name = "Possible Cancer";
            Details = "Entrectinib.";
        }
    }
    public class AdultsDiabetesTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public AdultsDiabetesTreatment()
        {
            Name = "Adults Diabetes";
            Details = "Insulin level has been set.";
        }
    }
    public class LungsTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public LungsTreatment()
        {
            Name = "Lungs Disease";
            Details = "Referral to X-Ray. Stop smoking, it's bad for you.";
        }
    }
    public class MuscleTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public MuscleTreatment()
        {
            Name = "Muscle Disease";
            Details = "2 pills of 5mg of turmeric C3 of Altman every day for 30 days.";
        }
    }
    public class IronDefTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public IronDefTreatment()
        {
            Name = "Iron Deficiency";
            Details = "2 pills of 10mg of B12 every day for 30 days.";
        }
    }
    public class KidneyTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public KidneyTreatment()
        {
            Name = "Kidney Disease";
            Details = "Balance blood sugar level.";
        }
    }
    public class BloodDiseaseTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public BloodDiseaseTreatment()
        {
            Name = "Blood Disease";
            Details = "A combination of cyclophosphamide and corticosteroids.";
        }
    }
    public class HeartTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public HeartTreatment()
        {
            Name = "Heart Disease";
            Details = "Set an appointment with a nutritionist.";
        }
    }
    public class DehydrationTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public DehydrationTreatment()
        {
            Name = "Dehydration";
            Details = "Complete rest while lying down, returning fluids by drinking.";
        }
    }
    public class IronPoisTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public IronPoisTreatment()
        {
            Name = "Iron Poisoning";
            Details = "Evacuation to the hospital.";
        }
    }
    public class HematologicalTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public HematologicalTreatment()
        {
            Name = "Hematological disorder";
            Details = "Injection of a hormone to encourage red blood cell production.";
        }
    }
    public class BloodCellCreationTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public BloodCellCreationTreatment()
        {
            Name = "Disorder of blood cell formation / creation";
            Details = "One pill of 10mg B12 every day for 30 days. One pill of 5mg of folic acid every day for 30 days.";
        }
    }
    public class HyperlipidemiaTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public HyperlipidemiaTreatment()
        {
            Name = "Hyperlipidemia";
            Details = "Set an appointment with a nutritionist. One pill of 5mg of Simobil every day for 7 days.";
        }
    }
    public class BleedingTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public BleedingTreatment()
        {
            Name = "Bleed";
            Details = "Immediate evacuation to the nearest hospital.";
        }
    }
    public class DietTreatment : Treatment
    {
        public override string Name { get; set; }
        public override string Details { get; set; }
        public DietTreatment()
        {
            Name = "Diet";
            Details = "Set an appointment with a nutritionist.";
        }
    }
}
