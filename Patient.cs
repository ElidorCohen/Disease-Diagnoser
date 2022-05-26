using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public enum PatientType
    {
        Adult,
        Child,
        Baby
    }
    public enum PatientGender
    {
        Male,
        Female
    }
    public class Patient
    {
        public string ID;
        public string firstName;
        public string lastName;
        public PatientGender Gender;
        public int Age;
        public string fever;
        public string bloodPressure;
        public string pulse;
        public PatientType type;
        public BloodValues BloodValues;



        public Patient(string ID, string firstName, string lastName, int Age, PatientGender Gender, string fever, string bloodPressure, string pulse, PatientType type, BloodValues bv)
        {
            this.ID = ID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.Age = Age;
            this.Gender = Gender;
            this.bloodPressure = bloodPressure;
            this.pulse = pulse;
            this.fever = fever;
            this.type = type;
            BloodValues = bv;
        }

        public string getID() { return ID; }
        public string getFirstName() { return firstName; }
        public string getLastName() { return lastName; }
        public PatientGender getGender() { return Gender; }
        public int getAge() { return Age; }
        public void setType(PatientType type) { this.type = type; }
        public string getBloodPress() { return bloodPressure; }
        public string getFever() { return fever; }
        public string getPulse() { return pulse; }
        public string getBloodValue() { return BloodValues.WBC; }

    }
}
