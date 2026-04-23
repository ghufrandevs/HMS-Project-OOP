namespace HMS_project
{
    internal class Program
    {
        static List<Patient>patients = new List<Patient>();
        static List<Doctor>Doctors = new List<Doctor>();
        public static void showMenu()
        {
            Console.WriteLine("Hello to Managing Health Care Clinic");
            Console.WriteLine("Please choice option : ");
            Console.WriteLine("1.Register New Patient");
            Console.WriteLine("2.Admit Patient ");
            Console.WriteLine("3.Discharge Patient");
            Console.WriteLine("4.Search Patient");
            Console.WriteLine("5.List All Admitted Patients");
            Console.WriteLine("6.Transfer Patient to Another Doctor");
            Console.WriteLine("7.View Most Visited Patients ");
            Console.WriteLine("8.Search Patients by Department ");
            Console.WriteLine("9.Billing Report");
            Console.WriteLine("10.Add Doctor");
            Console.WriteLine("11.Doctor Salary Report");
            Console.WriteLine("12.Exit");
            

        }
        public static void SeedData()
        {
            patients.Add(new Patient("Ali Hassan","Flu", "General", "A+"));
            patients.Add(new Patient("Sara Ahmed","Fracture", "Orthopedics", "O-"));
            patients.Add(new Patient("Omar Khalid","Diabetes", "Cardiology", "B+"));

        }
        public static void RegisterPatient()
        {
            Console.WriteLine("Enter the patient name : ");
            string Name=(Console.ReadLine()?? string.Empty).Trim();
            Console.WriteLine("Enter the diagnosis :");
            string Diagnosis=Console.ReadLine();

        }
        static void Main(string[] args)
        {
            SeedData();
            bool exit = false;
            int option = 0;
            while(!exit)
            {
                showMenu();
                try
                {
                    option = int.Parse(Console.ReadLine());

                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please choose a number from 1 to 12");

                }
                switch (option)
                {
                    case 1:

                        break;

                        case 2:
                        break;
                        case 3:
                        break;
                }


            }


        }
    }
    class Patient
    {
        public string patientName;
        public string patientID;
        public string diagnoses;
        public bool admitted;
        public string assignedDoctors;
        public string departments;
        public int visitCount;
        public double billingAmount;
        public DateTime lastVisitDate;
        public DateTime lastDischargeDate;
        public int daysInHospital;
        public string bloodType;
        public static int patientCount=0;
        public Patient(string patientName, string diagnoses, string departments, string bloodType)
        {
            this.patientName = patientName;
            this.diagnoses = diagnoses;
            this.departments = departments;
            this.bloodType= bloodType;
            //defult value
            admitted= false;
            assignedDoctors = "";
            visitCount = 0;
            billingAmount = 0;
            lastVisitDate = DateTime.MinValue;
            lastDischargeDate = DateTime.MinValue;
            daysInHospital = 0;
            patientID = ("P" + patientCount);
            patientCount++;
            Console.WriteLine("the patient ID is : "+ patientID);

            

        }
    }

    class Doctor
    {
        public string doctorNames;
        public int doctorAvailableSlots;
        public int doctorVisitCount;
    }
}